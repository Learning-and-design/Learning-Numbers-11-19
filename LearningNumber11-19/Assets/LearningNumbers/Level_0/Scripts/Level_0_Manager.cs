using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_0_Manager : MonoBehaviour
{
    #region Getter
    private static Level_0_Manager _instance;
    public static Level_0_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_0_Manager>();
            }
            return _instance;
        }
    }
    #endregion

    public enum GameStates
    {
        None,
        InGame,
        InTutorial,
        GameEnd,
        PopUpShow
    };
    public GameStates Check_GameState;

    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;
    public AudioSource PopUpsVO_AS;
    [Header("AudioClips")]   
    public AudioClip QuestionVoiceAfterTut_AC;
    public AudioClip PopupAtHomeClickedVoice_AC;
    public AudioClip PopupAtEndGameVoice_AC;
    public AudioClip PopupAtBackClickedVoice_AC;    
    public AudioClip PopupAtSkipFrmLvl1ClikedVoice_AC;
    public AudioClip PopupAtSkipTutSameLvlVoice_AC;
    public AudioClip PopupAtSkipGameSameLvlVoice_GLA17_AC;
    [Header("Ints")]
    public int QuesCount_i;
    private int answeredNumb;
    [Header("Progress Bar")]
    public Image[] Levels_BarFill;
    [Header("Images")]   
    public Image Skip_Btn_Tut_SecTime;
    public Image SkipFrmLvl1Btn_Img;
    public Image SkipBtn_AftPlayAgainClicked_Img;//new vid p
    [Header("Bools")]
    public bool Is_SingleCardPlaced;
    public bool Is_TenBoxsCardPlaced;
    public bool Is_UnitBoxsCardsPlaced;
    public bool Is_SecModeStartd;    
    public static bool Is_FirstTimeHomeBtnPressed;
    public static bool Is_PlayTutorialAgain;
    public static bool Is_PlayAgain_Game_SameLvl;
    [Header("GameObjects")]
    public GameObject Tutorial_Panel;
    public GameObject Game_Panel;
    public GameObject PaperEffect_Show;
    public GameObject ExitPage_UI_FromHome;
    public GameObject TutorialOrGamePage_UI;
    public GameObject NextGamePC_UI_EndGame;
    public GameObject SkipTut_UI_SameGame;
    public GameObject ExitPageUI_SkipFrmLvl1;
    public GameObject SkipAfterPlayAgain_Popup;//new vid p
    public GameObject _popupObj_temp;
    [Header("Texts")]
    public Text CoinsShow_Text;

    void Awake()
    {
        Initial_Set();
    }
    void Initial_Set()
    {
        Time.timeScale = 1;

        Tutorial_Panel.SetActive(false);
        Game_Panel.SetActive(false);

        Manage_TutOrGame(false, false);

        Skip_Btn_Tut_SecTime.enabled = false;
        SkipFrmLvl1Btn_Img.enabled = false;
        SkipBtn_AftPlayAgainClicked_Img.enabled = false;

        if (StaticVariables.Is_FromLvl0_CameFrmLvl1_GameSkipToLvl1)//25-04
        {
            Manage_TutOrGame(false, true);
            /*StartGame_AfterLvl1Exit_PopUp_Show();
            Check_GameState = GameStates.PopUpShow;*/
        }
        else if (StaticVariables.Is_FromLvl1_Back) //else ///
        {
            Manage_TutOrGame(false, true);
            SkipFrmLvl1Btn_Img.enabled = true; //ikadae
            StaticVariables.Is_FromLvl2_Back = false;
        }
        else if (Is_PlayAgain_Game_SameLvl)
        {
            Time.timeScale = 1;
            //Is_PlayAgain_Game_SameLvl = false;
            Manage_TutOrGame(false, true);
            SkipBtn_AftPlayAgainClicked_Img.enabled = true;
            //Level_1_Game.QuesCount_i = 0; //123465789
        }
        else
        {
             Manage_TutOrGame(true, false);
        }
        ////////////
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        Is_SingleCardPlaced = false;
        Is_TenBoxsCardPlaced = false;
        Is_UnitBoxsCardsPlaced = false;
        Is_SecModeStartd = false;

        Skip_Btn_Tut_SecTime.enabled = false;
        SkipFrmLvl1Btn_Img.enabled = false;

        if (StaticVariables.Is_FromLvl1_Back)
        {
            SkipFrmLvl1Btn_Img.enabled = true;
        }
        if (Is_PlayTutorialAgain)
        {
            Skip_Btn_Tut_SecTime.enabled = true;
            Is_PlayTutorialAgain = false;
        }

        CoinsShow_Text.text = " " + PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore).ToString();

        ExitPage_UI_FromHome.gameObject.SetActive(false);
        TutorialOrGamePage_UI.gameObject.SetActive(false);
        PaperEffect_Show.gameObject.SetActive(false);
        NextGamePC_UI_EndGame.gameObject.SetActive(false);

        Levels_BarFill[0].fillAmount = PlayerPrefs.GetFloat(StaticVariables.Level1_BarVal);
        Levels_BarFill[1].fillAmount = PlayerPrefs.GetFloat(StaticVariables.Level2_BarVal);
        Levels_BarFill[2].fillAmount = PlayerPrefs.GetFloat(StaticVariables.Level3_BarVal);
        Levels_BarFill[3].fillAmount = PlayerPrefs.GetFloat(StaticVariables.Level4_BarVal);
    }
    private void Update()
    {
        if (Check_GameState == GameStates.PopUpShow)
        {
            if (!PopUpsVO_AS.isPlaying)
            {                
                OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, true);
            }
        }
    }
    public void Manage_TutOrGame(bool Is_TutorealShow, bool Is_GameShow)
    {
        if (Is_TutorealShow)
        {
            Check_GameState = GameStates.InTutorial;
            ShowUIAtStates();
        }
        else if (Is_GameShow)
        {
            Check_GameState = GameStates.InGame;
            ShowUIAtStates();
        }
    }
    void ShowUIAtStates()
    {
        if (Check_GameState == GameStates.InGame)
        {
            Tutorial_Panel.SetActive(false);
            Game_Panel.SetActive(true);                            
        }
        else if (Check_GameState == GameStates.InTutorial)
        {
            Tutorial_Panel.SetActive(true);
            Game_Panel.SetActive(false);
           
        }
    }
    //////////////////////////////////////
    public void AfterTutorialPage_Active()
    {
        Check_GameState = GameStates.PopUpShow;

        //Debug.Log("AfterTutorialPage_Active");
        VoiceOver_AS.Pause();

        PopUpsVO_AS.clip = QuestionVoiceAfterTut_AC;
        PopUpsVO_AS.Play();

        TutorialOrGamePage_UI.gameObject.SetActive(true);

        _popupObj_temp = TutorialOrGamePage_UI.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);
    }
    public void PlayAgainClicked_AftTutUI()
    {
        Time.timeScale = 1;
        TutorialOrGamePage_UI.gameObject.SetActive(false);
        //Is_Tutorial = true;
        Is_PlayTutorialAgain = true;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void ContinueGameClicked_AftTutUI()
    {
        Time.timeScale = 1;
        PopUpsVO_AS.Stop();
        TutorialOrGamePage_UI.gameObject.SetActive(false);
        Manage_TutOrGame(false, true);
        //Is_Tutorial = false;
        Start();
        
    }
    //////////////////////////////////////
    public void Home_Clicked()
    {
        Check_GameState = GameStates.PopUpShow;

        VoiceOver_AS.Pause();

        PopUpsVO_AS.clip = PopupAtHomeClickedVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.5f);

        ExitPage_UI_FromHome.SetActive(true);

        _popupObj_temp = ExitPage_UI_FromHome.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        StartCoroutine(MakeHighliteIconsinPopup(true)); //for home popop icons scale anim

        if(Level_0_Tutorial.Instance)
        {
            Level_0_Tutorial.Instance.DotAnim_Obj.enabled = false;
        }

        Time.timeScale = 0;

        //Invoke("MakePauseAftDelaay", 8f);
        /*if (Is_FirstTimeHomeBtnPressed)
        {
            Time.timeScale = 0;
        }
        else
        {
            Invoke("MakePauseAftDelaay", 6f);
        }*/
    }
    void MakePauseAftDelaay()
    {
        //yield return new WaitForSeconds(5f);
        Time.timeScale = 0;
        //Debug.Log("Time.timeScale+++++" + Time.timeScale);
    }
    public void PickNewGame_Clicked_Home()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;

        Is_FirstTimeHomeBtnPressed = true;

        VoiceOver_AS.UnPause();
        Application.LoadLevel("0_MenuScene");
    }
    public void Continue_Clicked_Home()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;

        Is_FirstTimeHomeBtnPressed = true;
        
        VoiceOver_AS.UnPause();
        ExitPage_UI_FromHome.SetActive(false);
        PopUpsVO_AS.Stop();

        if (Level_0_Tutorial.Instance)
        {
            Level_0_Tutorial.Instance.DotAnim_Obj.enabled = true;
        }
    }
    //////////////////////////////////////
    public IEnumerator EndGameUI_PopUpShow()
    {
        Check_GameState = GameStates.PopUpShow;

        PlayerPrefs.SetInt(StaticVariables.Is_CompletedLvl0, 1);
        PlayerPrefs.Save();

        yield return new WaitForSeconds(3);
        PaperEffect_Show.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        NextGamePC_UI_EndGame.gameObject.SetActive(true);

        _popupObj_temp = NextGamePC_UI_EndGame.gameObject;
        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        PopUpsVO_AS.clip = PopupAtEndGameVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.3f);

        yield return new WaitForSeconds(2f);
        StartCoroutine(MakeHighliteIconsinPopup(false));                
    }
    public void PlayAgain_Clicked_EndGame()
    {
        Time.timeScale = 1;
        Is_PlayAgain_Game_SameLvl = true;        
        NextGamePC_UI_EndGame.gameObject.SetActive(false);        
        Application.LoadLevel(Application.loadedLevel);
    }
    public void Play_NextGame_Clicked_EndGame()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Level_1");
    }
    ////////////////////////SKipTut clicked ingames Lvl0///////////////
    public void SkipTutSameLvl_PopUp_Show()//27-04
    {
        Check_GameState = GameStates.PopUpShow;

        VoiceOver_AS.Pause();
        SkipTut_UI_SameGame.SetActive(true);

        _popupObj_temp = SkipTut_UI_SameGame.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        StartCoroutine(MakeHighliteIconsinPopup(false)); //for home popop icons scale anim

        PopUpsVO_AS.clip = PopupAtSkipTutSameLvlVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.3f);

        Time.timeScale = 0;
    }
    public void Exit_Clicked_SkipLvl0Tut()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;
       
        SkipTut_UI_SameGame.SetActive(false);
        //Is_Tutorial = false;
        Manage_TutOrGame(false, true);
        PopUpsVO_AS.Stop();
        Start();
    }
    public void Continue_Clicked_SkipLvl0Tut()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;
        VoiceOver_AS.UnPause();
        SkipTut_UI_SameGame.SetActive(false);
        PopUpsVO_AS.Stop();
    }
    ////////////////////////SKip From Lvl1///////////////
    public void SkipFromLvl1Game_PopUp_Show()//25-04
    {
        Check_GameState = GameStates.PopUpShow;

        VoiceOver_AS.Pause();
        ExitPageUI_SkipFrmLvl1.SetActive(true);

        _popupObj_temp = ExitPageUI_SkipFrmLvl1.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        PopUpsVO_AS.clip = PopupAtSkipFrmLvl1ClikedVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.3f);

        StartCoroutine(MakeHighliteIconsinPopup(false)); //for home popop icons scale anim

        Time.timeScale = 0;
    }
    public void Exit_Clicked_SkipLvl0()
    {
        Check_GameState = GameStates.InGame;
        Time.timeScale = 1;
        StaticVariables.Is_FromLvl0_CameFrmLvl1_GameSkipToLvl1 = true;
        Application.LoadLevel("Level_1");
    }
    public void Continue_Clicked_SkipLvl0()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;
        Time.timeScale = 1;
        VoiceOver_AS.UnPause();
        ExitPageUI_SkipFrmLvl1.SetActive(false);

        PopUpsVO_AS.Stop();
    }
    /// /////////////////////Play Again SKip Clicked in same level//////////////////////////
    public void SkipBtn_AftrPlayAgainSameGame_PopUp_Show()//25-04
    {
        Check_GameState = GameStates.PopUpShow;

        VoiceOver_AS.Pause();

        SkipAfterPlayAgain_Popup.SetActive(true);

        _popupObj_temp = SkipAfterPlayAgain_Popup.gameObject;
        
        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        PopUpsVO_AS.clip = PopupAtSkipGameSameLvlVoice_GLA17_AC;
        PopUpsVO_AS.PlayDelayed(0.3f);

        StartCoroutine(MakeHighliteIconsinPopup(true)); //for home popop icons scale anim

        Time.timeScale = 0;
    }
    public void PlayNextGame_Clicked_SkipSameLvl()
    {
        Time.timeScale = 1;
        Check_GameState = GameStates.InGame;
        Application.LoadLevel("Level_1");
    }
    public void Continue_Clicked_SkipSameLvl()
    {
        Time.timeScale = 1;
        Check_GameState = GameStates.InGame;

        VoiceOver_AS.UnPause();

        SkipAfterPlayAgain_Popup.SetActive(false);
        //Debug.Log("its in the condition::"+Time.deltaTime);

        PopUpsVO_AS.Stop();

        if (Level_0_Game.Instance)
        {
            Level_0_Game.Instance.RepeatQuestion_VO(0.3f);
        }
    }
    ////////////////////////////Making the btns raycast is false untill the VO is completes ////////////////////////////////////////////////
    void OffOnBtnsCastPopupTillVO_Completes(GameObject _popupObj, bool is_OnOffReycast) //Making the btns raycast is false untill the vo is completes
    {
        _popupObj.transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = is_OnOffReycast;
        _popupObj.transform.GetChild(1).gameObject.GetComponent<Image>().raycastTarget = is_OnOffReycast;
    }
    IEnumerator MakeHighliteIconsinPopup(bool _homeclick)
    {
        if (_homeclick)
        {
            yield return new WaitForSeconds(1f);
            iTween.ScaleTo(_popupObj_temp.transform.GetChild(1).gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(_popupObj_temp.transform.GetChild(1).gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            yield return new WaitForSeconds(3f);
            iTween.ScaleTo(_popupObj_temp.transform.GetChild(0).gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(_popupObj_temp.transform.GetChild(0).gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            ////Debug.Log("Is_HomeClick"+ _homeclick);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            iTween.ScaleTo(_popupObj_temp.transform.GetChild(0).gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(_popupObj_temp.transform.GetChild(0).gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            yield return new WaitForSeconds(3f);
            iTween.ScaleTo(_popupObj_temp.transform.GetChild(1).gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(_popupObj_temp.transform.GetChild(1).gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            ////Debug.Log("Is_HomeClick" + _homeclick);
        }
        /*yield return new WaitForSeconds(1f);
        Time.timeScale = 0;*/
    }
    //////////////////////////////////////////////////////////////////
}