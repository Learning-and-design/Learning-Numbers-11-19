using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_1_Manager : MonoBehaviour
{
    #region Getter
    private static Level_1_Manager _instance;
    public static Level_1_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_1_Manager>();
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

    [Header("Int")]
    public int CoinsToAddPerLvl = 10;
    public int CoinsAtInitialLvl;
    [Header("GameaObjects")]
    public GameObject Tutorial_Panel;
    public GameObject Game_Panel;
    public GameObject ExitPage_UI_FromBack;
    public GameObject ExitPage_UI_FromHome;
    public GameObject TutorialOrGamePage_AfterTut;
    public GameObject ExitPageUI_EndGame;
    public GameObject ExitPageUI_SkipTut;
    public GameObject ExitPageUI_SkipFrmLvl2;
    public GameObject EnterPageUI_AfterExitAtLvl0;
    public GameObject SkipTut_UI_SameGame;
    public GameObject SkipAfterPlayAgain_Popup;//new vid p
    public GameObject _popupObj_temp;
    [Header("Texts")]
    public Text CoinsShow_Text;
    [Header("Progress Bar")]
    public Image[] Levels_BarFill;
    public Image SoundBtn_Img;
    public Image HintBtn_Img;
    public Sprite[] HintButns_Sprt;
    public Image BackBtn_Img;
    public Sprite[] BackButns_Sprt;
    public Image SkipHintBtn_Img;
    public Image Skip_Btn_Tut_SecTime;
    public Image SkipFrmLvl2Btn_Img;
    public Image SkipBtn_AftPlayAgainClicked_Img;//new vid p
    [Header("Floats")]
    private float IncToValue_f, IncFromValue_f;
    private float Lvl1_BarVal_temp;
    [Header("Bools")]
    public static bool Is_FirstHintShown;
    public static bool Is_SecMoreHintShown;
    public static bool Is_PlayTutorialAgain_Lvl1;
    public static bool Is_PlayAgain_Game_SameLvl;
    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;
    public AudioSource PopUpsVO_AS;
    [Header("AudioClips")]
    public AudioClip QuestionVoiceAfterTut_AC;
    public AudioClip PopupAtHomeClickedVoice_AC;
    public AudioClip PopupAtEndGameVoice_AC;
    public AudioClip PopupAtBackClickedVoice_AC;
    public AudioClip PopupAtSkipFrmLvl2ClikedVoice_AC;
    public AudioClip PopupAtStartGameAftLvl1ExitVoice_AC;
    public AudioClip PopupAtSkipTutorilVoice_AC;
    public AudioClip PopupAtSkipTutSameLvlVoice_AC;
    public AudioClip PopupAtSkipGameSameLvlVoice_GLA17_AC;

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
        SkipFrmLvl2Btn_Img.enabled = false;
        SkipBtn_AftPlayAgainClicked_Img.enabled = false;

        if (StaticVariables.Is_FromLvl0_CameFrmLvl1_GameSkipToLvl1)//25-04
        {
            Manage_TutOrGame(false, true);
            /*StartGame_AfterLvl1Exit_PopUp_Show();
            Check_GameState = GameStates.PopUpShow;*/
        }
        else if (StaticVariables.Is_FromLvl2_Back) //else ///
        {
            Manage_TutOrGame(false, true);
            SkipFrmLvl2Btn_Img.enabled = true; //ikadae
            StaticVariables.Is_FromLvl2_Back = false;
        }
        else if (Is_PlayAgain_Game_SameLvl)
        {            
            //Is_PlayAgain_Game_SameLvl = false;
            Manage_TutOrGame(false, true);
            SkipBtn_AftPlayAgainClicked_Img.enabled = true;
            //Level_1_Game.QuesCount_i = 0; //123465789
        }
        else
        {
            Manage_TutOrGame(true, false);
            EnterPageUI_AfterExitAtLvl0.SetActive(false);
        }

        ExitPage_UI_FromBack.SetActive(false);
        ExitPage_UI_FromHome.SetActive(false);
        TutorialOrGamePage_AfterTut.SetActive(false);
        ExitPageUI_EndGame.SetActive(false);
        ExitPageUI_SkipFrmLvl2.SetActive(false);
        ExitPageUI_SkipTut.SetActive(false);
        ///////////////////////////////   
        Lvl1_BarVal_temp = PlayerPrefs.GetFloat(StaticVariables.Level1_BarVal);
        Levels_BarFill[0].fillAmount = Lvl1_BarVal_temp;        
        Levels_BarFill[1].fillAmount = PlayerPrefs.GetFloat(StaticVariables.Level2_BarVal);
        Levels_BarFill[2].fillAmount = PlayerPrefs.GetFloat(StaticVariables.Level3_BarVal);
        Levels_BarFill[3].fillAmount = PlayerPrefs.GetFloat(StaticVariables.Level4_BarVal);

        if (PlayerPrefs.GetFloat(StaticVariables.Level1_BarVal) >= 1)
        {
            SkipBtn_AftPlayAgainClicked_Img.enabled = true;
        }
        ///////////////////////////////

        SkipHintBtn_Img.enabled = false;

        CoinsAtInitialLvl = PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);
        CoinsShow_Text.text = " " + CoinsAtInitialLvl.ToString();

        ///////////////////////////////

        if (Is_PlayTutorialAgain_Lvl1)
        {
            Skip_Btn_Tut_SecTime.enabled = true;
            Is_PlayTutorialAgain_Lvl1 = false;
        }
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

            SoundBtn_Img.enabled = true;
            HintBtn_Img.sprite = HintButns_Sprt[1];
            HintBtn_Img.raycastTarget = true;

            BackBtn_Img.sprite = BackButns_Sprt[1];
            BackBtn_Img.raycastTarget = true;

            SkipHintBtn_Img.enabled = false;
        }
        else if (Check_GameState == GameStates.InTutorial)
        {
            Tutorial_Panel.SetActive(true);
            Game_Panel.SetActive(false);

            SoundBtn_Img.enabled = false;
            HintBtn_Img.sprite = HintButns_Sprt[0];
            HintBtn_Img.raycastTarget = false;

            BackBtn_Img.sprite = BackButns_Sprt[0];
            BackBtn_Img.raycastTarget = false;

            SoundBtn_Img.enabled = false;
        }
    }
    #region Progressbar
    //[Tooltip("Progressbar Method")]
    //private float ProgressValBarInc_f = 0.11f; //(1/9)
    public void Update_ProgresBarValuee(float ProgressValBarInc_f)// Updating the progress bar
    {
        if (PlayerPrefs.GetFloat(StaticVariables.Level1_BarVal) >= 1f)
            return;

        IncToValue_f = IncToValue_f + ProgressValBarInc_f;

        if (IncToValue_f > Lvl1_BarVal_temp)
        {
            iTween.ValueTo(Levels_BarFill[0].gameObject, iTween.Hash("from", IncFromValue_f, "to", IncToValue_f, "onupdatetarget", gameObject, "onupdate", "UpdateFromValue", "time", 0.5f, "delay", 0.3f, "easetype", iTween.EaseType.linear));
            PlayerPrefs.SetFloat(StaticVariables.Level1_BarVal, IncToValue_f);
            PlayerPrefs.Save();
            //Debug.Log("PlayerPrefs.GetInt(StaticVariables.Level1_BarVal)::::::::::" + PlayerPrefs.GetFloat(StaticVariables.Level1_BarVal));
        }
    }
    void UpdateFromValue(float newval)
    {
        Levels_BarFill[0].fillAmount = IncFromValue_f = newval;
    }
    #endregion Progressbar

    #region InGameBtns
    public void Hint_Clicked()
    {
        Level_1_Game.Instance.ReStoreDefaultPosnsAftHint();
        SkipHintBtn_Img.enabled = true;
        Manage_TutOrGame(true, false);//tut-false||game-true//
    }
    public void SkipEndHind_Clicked()
    {
        Check_GameState = GameStates.PopUpShow;

        VoiceOver_AS.Pause();

        ExitPageUI_SkipTut.SetActive(true);

        _popupObj_temp = ExitPageUI_SkipTut.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        PopUpsVO_AS.clip = PopupAtSkipTutorilVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.3f);

        Time.timeScale = 0;
    }
    public void Exit_Clicked_SkipTut()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;
           
        ExitPageUI_SkipTut.SetActive(false);

        VoiceOver_AS.Pause();

        PopUpsVO_AS.Pause();

        if (Is_FirstHintShown == true)
        {
            Is_SecMoreHintShown = true;
        }
        Is_FirstHintShown = true;
        //Debug.Log("Before::::Is_HintShown::::" + Is_SecMoreHintShown);

        SkipHintBtn_Img.enabled = false;
        Manage_TutOrGame(false, true);//tut-false||game-true//
    }
    public void Continue_Clicked_SkipTut()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;
        
        ExitPageUI_SkipTut.SetActive(false);

        PopUpsVO_AS.Pause();

        VoiceOver_AS.UnPause();
    }
    #endregion InGameBtns

    #region Nav_Btns    
    //////////////////Home PopUp //////////////////////////   
    public void Home_Clicked()
    {
        Check_GameState = GameStates.PopUpShow;

        VoiceOver_AS.Pause();

        ExitPage_UI_FromHome.SetActive(true);

        _popupObj_temp = ExitPage_UI_FromHome.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp,false);
              
        PopUpsVO_AS.clip = PopupAtHomeClickedVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.5f);

        Time.timeScale = 0;        
    }
    public void PickNewGame_Clicked_Home()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;

        VoiceOver_AS.UnPause();

        Time.timeScale = 1;

        ExitPage_UI_FromBack.SetActive(false);
        ExitPage_UI_FromHome.SetActive(false);

        Level_1_Game.QuesCount_i = 0;
        Application.LoadLevel("0_MenuScene");
    }
    public void Continue_Clicked_Home()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;

        VoiceOver_AS.UnPause();

        ExitPage_UI_FromBack.SetActive(false);
        ExitPage_UI_FromHome.SetActive(false);

        PopUpsVO_AS.Stop();
    }
    /////////////////Back popup///////////////////////////   
    public void Back_Clicked()
    {
        Check_GameState = GameStates.PopUpShow;

        VoiceOver_AS.Pause();

        ExitPage_UI_FromBack.SetActive(true);

        _popupObj_temp = ExitPage_UI_FromBack.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        PopUpsVO_AS.clip = PopupAtBackClickedVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.5f);
    }
    public void PreviousLevel_Clicked_Back()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;

        VoiceOver_AS.UnPause();

        ExitPage_UI_FromBack.SetActive(false);
       
        //Level_1_Game.QuesCount_i = 0;
        StaticVariables.Is_FromLvl1_Back = true; //25-04
        Application.LoadLevel("Level_0");
    }
    public void ContinueGame_Clicked_Back()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;

        VoiceOver_AS.UnPause();

        ExitPage_UI_FromBack.SetActive(false);
       
        PopUpsVO_AS.Stop();
        
        if (Level_1_Game.Instance)
        {
            Level_1_Game.Instance.RepeatQuestion_VO(0f);
        }
    }
    /////////////////After Tutorial Page UI///////////////////////////    
    public void AfterTut_PopUp_Show()
    {
        Check_GameState = GameStates.PopUpShow;

        TutorialOrGamePage_AfterTut.SetActive(true);

        _popupObj_temp = TutorialOrGamePage_AfterTut.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        PopUpsVO_AS.clip = QuestionVoiceAfterTut_AC;
        PopUpsVO_AS.Play();
    }
    public void PlayAgain_Clicked_AftTut()
    {
        Check_GameState = GameStates.InGame;
        Is_PlayTutorialAgain_Lvl1 = true;
        TutorialOrGamePage_AfterTut.SetActive(false);
        SkipHintBtn_Img.enabled = true;
        Manage_TutOrGame(true, false);
        Application.LoadLevel(Application.loadedLevel);
    }
    public void PlayGame_Clicked_AftTut()
    {
        Check_GameState = GameStates.InGame;
        PopUpsVO_AS.Stop();
        TutorialOrGamePage_AfterTut.SetActive(false);
        Manage_TutOrGame(false, true);
    }
    //////////////////After EndGame//////////////////////////
    public void EndGame_PopUp_Show()
    {
        Check_GameState = GameStates.PopUpShow;

        ExitPageUI_EndGame.SetActive(true);

        _popupObj_temp = ExitPageUI_EndGame.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        PopUpsVO_AS.clip = PopupAtEndGameVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.3f);
    }
    public void PlayAgain_Clicked_EndGame()
    {
        Check_GameState = GameStates.InGame;

        Is_PlayAgain_Game_SameLvl = true;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void PlayNextLevel_Clicked__EndGame()
    {
        Check_GameState = GameStates.InGame;

        Level_1_Game.QuesCount_i = 0;
        Application.LoadLevel("Level_2");
    }
    ////////////////////////////////////////////    
    public void ReplayLevel_Clicked__AfterLvl1Exit()
    {
        PopUpsVO_AS.Stop();
        Skip_Btn_Tut_SecTime.enabled = true;
        StaticVariables.Is_FromLvl1_CameFrmLvl2_GameSkipToLvl2 = false;
        EnterPageUI_AfterExitAtLvl0.SetActive(false);
        Level_1_Game.QuesCount_i = 0;
        Manage_TutOrGame(true, false);

        VoiceOver_AS.UnPause();
    }
    ////////////////////////SKipTut clicked ingames Lvl0///////////////
    public void SkipTutSameLvl_PopUp_Show()//27-04
    {
        Check_GameState = GameStates.PopUpShow;

        VoiceOver_AS.Pause();

        SkipTut_UI_SameGame.SetActive(true);

        _popupObj_temp = SkipTut_UI_SameGame.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);

        PopUpsVO_AS.clip = PopupAtSkipTutSameLvlVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.3f);
        Time.timeScale = 0;
    }
    public void Exit_Clicked_SkipLvl1Tut()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;

        SkipTut_UI_SameGame.SetActive(false);
        PopUpsVO_AS.Stop();

        if (Is_FirstHintShown == true)
        {
            Is_SecMoreHintShown = true;
        }
        Manage_TutOrGame(false, true);
    }
    public void Continue_Clicked_SkipLvl1Tut()
    {
        Time.timeScale = 1;

        Check_GameState = GameStates.InGame;

        VoiceOver_AS.UnPause();

        SkipTut_UI_SameGame.SetActive(false);
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
    }
    public void PlayNextGame_Clicked_SkipSameLvl()
    {
        Check_GameState = GameStates.InGame;
        Application.LoadLevel("Level_2");
    }
    public void Continue_Clicked_SkipSameLvl()
    {
        Check_GameState = GameStates.InGame;

        VoiceOver_AS.UnPause();

        SkipAfterPlayAgain_Popup.SetActive(false);

        PopUpsVO_AS.Stop();

        if (Level_1_Game.Instance)
        {
            Level_1_Game.Instance.RepeatQuestion_VO(0.3f);
        }

    }
    /////////////////////SKip From Lvl2//////////////////////////
    public void SkipFromLvl2Game_PopUp_Show()//25-04
    {
        Check_GameState = GameStates.PopUpShow;

        ExitPageUI_SkipFrmLvl2.SetActive(true);

        _popupObj_temp = ExitPageUI_SkipFrmLvl2.gameObject;

        OffOnBtnsCastPopupTillVO_Completes(_popupObj_temp, false);
        
        PopUpsVO_AS.clip = PopupAtSkipFrmLvl2ClikedVoice_AC;
        PopUpsVO_AS.PlayDelayed(0.3f);
    }
    public void Exit_Clicked_SkipLvl2()
    {
        Check_GameState = GameStates.InGame;

        StaticVariables.Is_FromLvl1_CameFrmLvl2_GameSkipToLvl2 = true;
        Application.LoadLevel("Level_2");
    }
    public void Continue_Clicked_SkipLvl2()
    {
        Check_GameState = GameStates.InGame;

        ExitPageUI_SkipFrmLvl2.SetActive(false);

        PopUpsVO_AS.Stop();

        if (Level_1_Game.Instance)
        {
            Level_1_Game.Instance.RepeatQuestion_VO(0.3f);
        }
    }
    #endregion Nav_Btns

    ////////////////////////////Making the btns raycast is false untill the VO is completes ////////////////////////////////////////////////
    void OffOnBtnsCastPopupTillVO_Completes(GameObject _popupObj,bool is_OnOffReycast) //Making the btns raycast is false untill the vo is completes
    {
        //Debug.Log("ExitPageUI_EndGame==========");
        _popupObj.transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = is_OnOffReycast;
        _popupObj.transform.GetChild(1).gameObject.GetComponent<Image>().raycastTarget = is_OnOffReycast;

    }     
    //////////////////////////////////////////////////////////////////
}


