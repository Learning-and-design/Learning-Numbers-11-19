using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_2_Game : MonoBehaviour
{
    #region Getter
    private static Level_2_Game _instance;
    public static Level_2_Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_2_Game>();
            }
            return _instance;
        }
    }
    #endregion
        
    [Header("QuestionsLists")]
    public List<int> QuestionsNumbers_L;
    [Header("Ints")]
    public static int QuesCount_i = 0;
    public int QuesNumb_i;
    public int CoinsScored_i = 0;
    [Header("GameObjects")]
    public GameObject[] UnitsBoxsImgs_Arr_ToShow;
    public GameObject[] NumbersAnsImg_Arr;
    public GameObject[] FakeNumbersAnsImg_Arr;
    public GameObject BoardBoxs_MoveObj;
    public GameObject TargetObj;   
    public GameObject DragBoxSet;
    public GameObject PapersEffect;
    public GameObject ScoreCardMove;
    [Header("WordsNUmbersImages")]
    public Image NumberWord_Img;
    public Sprite[] NumbersWords_Sprte;
    [Header("====HintImages====")]    
    public GameObject NumbersHintImgsPar;
    [Header("Bools")]       
    private bool Is_NumbBoxsCardPlaced;    
    private bool Is_TenBoxsCardPlaced_Hint;    
    private bool Is_LastbutSecQues_FirstAttempt;
    private bool Is_LastbutOneQues_FirstAttempt;
    private bool Is_LastQues_FirstAttempt;    
    [Header("Images")]      
    public Image SoundBtn_Img;
    [Header("Texts")]
    public Text CoinsShowTemp_Text;
    [Header("Vector")]
    public Vector3[] NumbAnsImgPos_v; //6
    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;
    [Header("AudioClips")]   
    public AudioClip[] NumbersVoices_AC;
    public AudioClip TryAgain_AC;
    public AudioClip QuestionVoice_AC;
    public AudioClip IncorrectAnsSond_AC;
    public AudioClip CorrectAnsSond_AC;
    public AudioClip RewardCollctSond_AC;
    public AudioClip CompleteSond_AC;

    void OnEnable()
    {
        if (Level_2_Manager.Is_SecMoreHintShown)
        {
            StaticVariables.Is_Tutorial = false;

            //Debug.Log("Before::::Is_HintShown::::" + Level_2_Manager.Is_SecMoreHintShown);
            QuesCount_i = QuesCount_i - 1;
            //CoinsShow_Text.text = " " + CoinsScored_i.ToString();
            Level_2_Manager.Is_SecMoreHintShown = false;
        }

        //if(StaticVariables.Is_FromLvl2)//25-04
        if (StaticVariables.Is_FromLvl1_CameFrmLvl2_GameSkipToLvl2)//25-04
        {
           QuesCount_i = QuesCount_i - 1;
            StaticVariables.Is_FromLvl1_CameFrmLvl2_GameSkipToLvl2 = false;
        }

        if (Level_2_Manager.Is_PlayAgain_Game_SameLvl)//25-04
        {
            QuesCount_i =  0;            
            Level_2_Manager.Is_PlayAgain_Game_SameLvl = false;           
        }

        Is_LastbutSecQues_FirstAttempt = false;
        Is_LastbutOneQues_FirstAttempt = false;
        Is_LastQues_FirstAttempt = false;

        Default_State();              
        //AtStart_TimeGapMethod();
        GenerateNumbsWords();
        SoundBtn_Img.enabled = true;
    }


    // Start is called before the first frame update
    void Start()
    {      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Level_2_Manager.Instance.Check_GameState == Level_2_Manager.GameStates.InGame)
        {
            AtStart_TimeGapMethod();            
        }
    }

    void Default_State()
    {
        StaticVariables.Is_Tutorial = false;

        TargetObj.SetActive(true);
              
        Level_2_Manager.Instance.CoinsShow_Text.text = " " + PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);

        PapersEffect.SetActive(false);

        ScoreCardMove.transform.localScale = new Vector3(0,0,0);
       
        CoinsTempPrev_s = PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);
    }
    void GenerateNumbsWords()
    {
        if (Level_2_Manager.Instance.Check_GameState != Level_2_Manager.GameStates.InGame)
            return;

        NumbersHintImgsPar.gameObject.SetActive(false);

        Is_NumbBoxsCardPlaced = false;
        Is_TenBoxsCardPlaced_Hint = false;

        QuesCount_i = QuesCount_i + 1;
        //Debug.Log("QuesCount_i::::" + QuesCount_i + "::::::QuesNumb_i " + QuesNumb_i);

        WrongAnsCount_i = 0;

        /*if (QuesCount_i > 9)
            return;*/

        if (!Is_LastQues_FirstAttempt || !Is_LastbutOneQues_FirstAttempt || !Is_LastbutSecQues_FirstAttempt)
        {
            if (QuesCount_i == 10)
            {
                QuesCount_i = 7;
                //Debug.Log("QuesCount_i::::" + QuesCount_i + "::::::QuesNumb_i " + QuesNumb_i);
                Level_2_Manager.Instance.Update_ProgresBarValuee(-0.22f);   //Dec val for repeat ans again             
            }
        }

        //Debug.Log("::::::::QuesCount_i::::: " + QuesCount_i);
        QuesNumb_i = QuestionsNumbers_L[QuesCount_i - 1];
        //Debug.Log("::::::::QuesNumb_i::::: " + QuesNumb_i);

        NumberWord_Img.sprite = NumbersWords_Sprte[QuesNumb_i - 11];
        NumberWord_Img.enabled = false;

        for(int u=0; u < UnitsBoxsImgs_Arr_ToShow.Length; u++)
        {
            UnitsBoxsImgs_Arr_ToShow[u].SetActive(false);

            if(u < QuesNumb_i)
            {
                UnitsBoxsImgs_Arr_ToShow[u].SetActive(true);
            }
        }

        RepeatQuestion_VO(01f);

        StartCoroutine(ReArrangingPosofAllBoxes());

        for (int i = 0; i < NumbersAnsImg_Arr.Length; i++)
        {
            NumbersAnsImg_Arr[i].SetActive(true);
            NumbersAnsImg_Arr[i].GetComponent<DragNumber_Level2>().enabled = false;
            //NumbersAnsImg_Arr[i].gameObject.transform.localPosition = new Vector3(0f,-322f,0f);
        }

        SetPostionsOfNumbrsObjs();

        
        for (int i = 0; i < NumbersAnsImg_Arr.Length; i++)
        {           
            NumbersAnsImg_Arr[i].GetComponent<DragNumber_Level2>().enabled = true;

            FakeNumbersAnsImg_Arr[i].gameObject.SetActive(false);
        }

        if (DragNumber_Level2.Instance)
        {
            DragNumber_Level2.Instance.Invoke("Default_Pos",2f);
            //Debug.Log("send to DragNumber_Level2");
        }

        CardBordIn_Anim();        
    }
    void SetPostionsOfNumbrsObjs()
    {
        switch (QuesNumb_i)
        {
            case 11:
                NumbersAnsImg_Arr[0].transform.localPosition = NumbAnsImgPos_v[5];//Number 11 - Position
                NumbersAnsImg_Arr[2].transform.localPosition = NumbAnsImgPos_v[1];
                NumbersAnsImg_Arr[4].transform.localPosition = NumbAnsImgPos_v[2];
                NumbersAnsImg_Arr[6].transform.localPosition = NumbAnsImgPos_v[3];
                NumbersAnsImg_Arr[8].transform.localPosition = NumbAnsImgPos_v[4];
                NumbersAnsImg_Arr[5].transform.localPosition = NumbAnsImgPos_v[0];

                NumbersAnsImg_Arr[1].gameObject.SetActive(false);
                NumbersAnsImg_Arr[3].gameObject.SetActive(false);
                NumbersAnsImg_Arr[7].gameObject.SetActive(false);

                break;

            case 12:
                NumbersAnsImg_Arr[1].transform.localPosition = NumbAnsImgPos_v[2];//Number 12 - Position
                NumbersAnsImg_Arr[3].transform.localPosition = NumbAnsImgPos_v[5];
                NumbersAnsImg_Arr[2].transform.localPosition = NumbAnsImgPos_v[1];
                NumbersAnsImg_Arr[6].transform.localPosition = NumbAnsImgPos_v[4];
                NumbersAnsImg_Arr[7].transform.localPosition = NumbAnsImgPos_v[3];
                NumbersAnsImg_Arr[5].transform.localPosition = NumbAnsImgPos_v[0];

                NumbersAnsImg_Arr[4].gameObject.SetActive(false);
                NumbersAnsImg_Arr[0].gameObject.SetActive(false);
                NumbersAnsImg_Arr[8].gameObject.SetActive(false);

                break;

            case 13:
                NumbersAnsImg_Arr[2].transform.localPosition = NumbAnsImgPos_v[5];//Number 13 - Position
                NumbersAnsImg_Arr[4].transform.localPosition = NumbAnsImgPos_v[0];
                NumbersAnsImg_Arr[0].transform.localPosition = NumbAnsImgPos_v[3];
                NumbersAnsImg_Arr[6].transform.localPosition = NumbAnsImgPos_v[4];
                NumbersAnsImg_Arr[7].transform.localPosition = NumbAnsImgPos_v[1];
                NumbersAnsImg_Arr[8].transform.localPosition = NumbAnsImgPos_v[2];

                NumbersAnsImg_Arr[3].gameObject.SetActive(false);
                NumbersAnsImg_Arr[5].gameObject.SetActive(false);
                NumbersAnsImg_Arr[1].gameObject.SetActive(false);

                break;

            case 14:
                NumbersAnsImg_Arr[3].transform.localPosition = NumbAnsImgPos_v[3];//Number 14 - Position
                NumbersAnsImg_Arr[5].transform.localPosition = NumbAnsImgPos_v[4];
                NumbersAnsImg_Arr[7].transform.localPosition = NumbAnsImgPos_v[2];
                NumbersAnsImg_Arr[1].transform.localPosition = NumbAnsImgPos_v[0];
                NumbersAnsImg_Arr[0].transform.localPosition = NumbAnsImgPos_v[1];
                NumbersAnsImg_Arr[8].transform.localPosition = NumbAnsImgPos_v[5];

                NumbersAnsImg_Arr[4].gameObject.SetActive(false);
                NumbersAnsImg_Arr[6].gameObject.SetActive(false);
                NumbersAnsImg_Arr[2].gameObject.SetActive(false);

                break;

            case 15:
                NumbersAnsImg_Arr[4].transform.localPosition = NumbAnsImgPos_v[5];//Number 15 - Position
                NumbersAnsImg_Arr[8].transform.localPosition = NumbAnsImgPos_v[2];
                NumbersAnsImg_Arr[1].transform.localPosition = NumbAnsImgPos_v[3];
                NumbersAnsImg_Arr[7].transform.localPosition = NumbAnsImgPos_v[4];
                NumbersAnsImg_Arr[5].transform.localPosition = NumbAnsImgPos_v[0];
                NumbersAnsImg_Arr[6].transform.localPosition = NumbAnsImgPos_v[1];

                NumbersAnsImg_Arr[3].gameObject.SetActive(false);
                NumbersAnsImg_Arr[0].gameObject.SetActive(false);
                NumbersAnsImg_Arr[2].gameObject.SetActive(false);

                break;

            case 16:
                NumbersAnsImg_Arr[5].transform.localPosition = NumbAnsImgPos_v[1];//Number 16 - Position
                NumbersAnsImg_Arr[6].transform.localPosition = NumbAnsImgPos_v[4];
                NumbersAnsImg_Arr[7].transform.localPosition = NumbAnsImgPos_v[3];
                NumbersAnsImg_Arr[4].transform.localPosition = NumbAnsImgPos_v[5];
                NumbersAnsImg_Arr[3].transform.localPosition = NumbAnsImgPos_v[2];
                NumbersAnsImg_Arr[8].transform.localPosition = NumbAnsImgPos_v[0];

                NumbersAnsImg_Arr[1].gameObject.SetActive(false);
                NumbersAnsImg_Arr[0].gameObject.SetActive(false);
                NumbersAnsImg_Arr[2].gameObject.SetActive(false);

                break;

            case 17:
                NumbersAnsImg_Arr[6].transform.localPosition = NumbAnsImgPos_v[0];//Number 17 - Position
                NumbersAnsImg_Arr[8].transform.localPosition = NumbAnsImgPos_v[1];
                NumbersAnsImg_Arr[5].transform.localPosition = NumbAnsImgPos_v[5];
                NumbersAnsImg_Arr[4].transform.localPosition = NumbAnsImgPos_v[2];
                NumbersAnsImg_Arr[3].transform.localPosition = NumbAnsImgPos_v[3];
                NumbersAnsImg_Arr[0].transform.localPosition = NumbAnsImgPos_v[4];

                NumbersAnsImg_Arr[7].gameObject.SetActive(false);
                NumbersAnsImg_Arr[2].gameObject.SetActive(false);
                NumbersAnsImg_Arr[1].gameObject.SetActive(false);

                break;

            case 18:
                NumbersAnsImg_Arr[7].transform.localPosition = NumbAnsImgPos_v[4];//Number 18 - Position
                NumbersAnsImg_Arr[2].transform.localPosition = NumbAnsImgPos_v[2];
                NumbersAnsImg_Arr[0].transform.localPosition = NumbAnsImgPos_v[1];
                NumbersAnsImg_Arr[5].transform.localPosition = NumbAnsImgPos_v[3];
                NumbersAnsImg_Arr[3].transform.localPosition = NumbAnsImgPos_v[0];
                NumbersAnsImg_Arr[6].transform.localPosition = NumbAnsImgPos_v[5];

                NumbersAnsImg_Arr[1].gameObject.SetActive(false);
                NumbersAnsImg_Arr[8].gameObject.SetActive(false);
                NumbersAnsImg_Arr[4].gameObject.SetActive(false);

                break;

            case 19:
                NumbersAnsImg_Arr[8].transform.localPosition = NumbAnsImgPos_v[2];//Number 19 - Position
                NumbersAnsImg_Arr[1].transform.localPosition = NumbAnsImgPos_v[4];
                NumbersAnsImg_Arr[7].transform.localPosition = NumbAnsImgPos_v[0];
                NumbersAnsImg_Arr[4].transform.localPosition = NumbAnsImgPos_v[5];
                NumbersAnsImg_Arr[3].transform.localPosition = NumbAnsImgPos_v[3];
                NumbersAnsImg_Arr[0].transform.localPosition = NumbAnsImgPos_v[1];

                NumbersAnsImg_Arr[6].gameObject.SetActive(false);
                NumbersAnsImg_Arr[5].gameObject.SetActive(false);
                NumbersAnsImg_Arr[2].gameObject.SetActive(false);

                break;

            default:
                NumbersAnsImg_Arr[8].transform.localPosition = NumbAnsImgPos_v[2];//Number 19 - Position
                NumbersAnsImg_Arr[1].transform.localPosition = NumbAnsImgPos_v[4];
                NumbersAnsImg_Arr[7].transform.localPosition = NumbAnsImgPos_v[0];
                NumbersAnsImg_Arr[4].transform.localPosition = NumbAnsImgPos_v[5];
                NumbersAnsImg_Arr[3].transform.localPosition = NumbAnsImgPos_v[3];
                NumbersAnsImg_Arr[0].transform.localPosition = NumbAnsImgPos_v[1];

                NumbersAnsImg_Arr[6].gameObject.SetActive(false);
                NumbersAnsImg_Arr[5].gameObject.SetActive(false);
                NumbersAnsImg_Arr[2].gameObject.SetActive(false);
                break;
        }
    }
    //////////////////////////////////////////
    #region CardBordAnim_InOut
    void CardBordIn_Anim()
    {
        if (Level_2_Manager.Instance.Check_GameState == Level_2_Manager.GameStates.InGame)
        { 
            BoardBoxs_MoveObj.transform.localPosition = new Vector3(800f, 36f, 0f);
            iTween.MoveTo(BoardBoxs_MoveObj.gameObject.gameObject, iTween.Hash("x", 0f, "y", 36f, "time", 1.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        }
    }
    void CardBordOut_Anim()
    {
        if (Level_2_Manager.Instance.Check_GameState == Level_2_Manager.GameStates.InGame)
        {
            iTween.MoveTo(BoardBoxs_MoveObj.gameObject.gameObject, iTween.Hash("x", -800f, "y", 36f, "time", 1.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        }
    }
    #endregion CardBordAnim_InOut    
    //////////////////////////////////////////
    
    private int My_AnsVal_i;
    public void Check_AnsEvalution(int AnsVal_i)
    {
        My_AnsVal_i = AnsVal_i;

        if (My_AnsVal_i == QuesNumb_i)
        {
            iTween.ScaleTo(NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "delay", 0.2f, "easetype", iTween.EaseType.linear));
            iTween.ScaleTo(NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.4f, "easetype", iTween.EaseType.linear));


            VoiceOver_AS.clip = CorrectAnsSond_AC;
            VoiceOver_AS.Play();
            //RepeatNumber_VO();
            
            StartCoroutine(After_CorrectAnsEvalution());
            //Debug.Log("YEAH!! Its Equal");
        }
        else
        {
            VoiceOver_AS.clip = TryAgain_AC;
            VoiceOver_AS.PlayDelayed(0.2f);

            StartCoroutine(After_WrongAnsEvalution());
            iTween.MoveTo(NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject, iTween.Hash("x", NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject.transform.position.x - 8f, "time", 0.2f, "delay", 0.1f,"easetype",iTween.EaseType.linear));
            iTween.MoveTo(NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject, iTween.Hash("x", NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject.transform.position.x + 8f, "time", 0.2f, "delay", 0.35f,"easetype", iTween.EaseType.linear));
            iTween.MoveTo(NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject, iTween.Hash("x", NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject.transform.position.x + 8f, "time", 0.2f, "delay", 0.35f,"easetype", iTween.EaseType.linear));
            iTween.MoveTo(NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject, iTween.Hash("x", NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject.transform.position.x - 8f, "time", 0.2f, "delay", 0.5f, "easetype", iTween.EaseType.linear));
            
            Invoke("SetPostionsOfNumbrsObjs", 0.8f);

            //Debug.Log("NO!! not Equal");
        }
    }

    IEnumerator ReArrangingPosofAllBoxes()
    {
        yield return new WaitForSeconds(0f);


        /*for (int i = 0; i < NumbersAnsImg_Arr.Length; i++)
        {            
            NumbersAnsImg_Arr[i].transform.parent = DragBoxSet.transform;
            //Debug.Log("************************************************");
        }  */
    }


    private int AnsEval_i;
    private int WrongAnsCount_i;

    IEnumerator After_CorrectAnsEvalution()
    {
        yield return new WaitForSeconds(1f);

        if (Level_2_Manager.Instance.Check_GameState == Level_2_Manager.GameStates.InGame)
        {
            RepeatNumber_VO();

            AnsEval_i = 1;

            if (WrongAnsCount_i <= 0) //if correct in first attempt
            {
                Level_2_Manager.Instance.Update_ProgresBarValuee(0.11f);

                if ((QuesNumb_i == QuestionsNumbers_L[6]))
                {
                    Is_LastbutSecQues_FirstAttempt = true;
                }
                else if ((QuesNumb_i == QuestionsNumbers_L[7]))
                {
                    Is_LastbutOneQues_FirstAttempt = true;
                }
                else if ((QuesNumb_i == QuestionsNumbers_L[8]))
                {
                    Is_LastQues_FirstAttempt = true;
                }
            }

            NumberWord_Img.enabled = true;
            FakeNumbersAnsImg_Arr[QuesNumb_i - 11].gameObject.SetActive(true);
            NumbersAnsImg_Arr[My_AnsVal_i - 11].gameObject.SetActive(false);

            yield return new WaitForSeconds(1.8f); //6-3
            CardBordOut_Anim();

            //yield return new WaitForSeconds(1.3f); //6-3
            //BlackBg_Img.CrossFadeAlpha(0.7f, 0.2f, true); //6-3

            yield return new WaitForSeconds(2f);

            if (QuesCount_i <= 9)
            {
                //Debug.Log("QuesCount_i ::::::::::::::::::::::::" + QuesCount_i);
                Invoke("GenerateNumbsWords", 1f);

                Is_NumbBoxsCardPlaced = true;
            }

            /*CoinsScored_i = CoinsScored_i + Level_2_Manager.Instance.CoinsToAddPerQues;
            PlayerPrefs.SetInt(StaticVariables.Level2_Score,  CoinsScored_i);
            PlayerPrefs.SetInt(StaticVariables.Coins_TotalScore, PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore) + PlayerPrefs.GetInt(StaticVariables.Level2_Score));*/

            //Debug.Log("Is_LastQues_FirstAttempt :::::" + Is_LastQues_FirstAttempt + "::::::Is_LastbutOneQues_FirstAttempt :::::::::::" + Is_LastbutOneQues_FirstAttempt + "::::::Is_LastbutSecQues_FirstAttempt :::::::::::" + Is_LastbutSecQues_FirstAttempt);
            //Debug.Log("QuesCount_i ::::::::::::::::::::::::" + QuesCount_i);

            if (Is_LastQues_FirstAttempt && Is_LastbutOneQues_FirstAttempt && Is_LastbutSecQues_FirstAttempt)
            {
                Level_2_Manager.Instance.Check_GameState = Level_2_Manager.GameStates.GameEnd;
                Level_2_Manager.Instance.Levels_BarFill[1].fillAmount = 1f;
                PlayerPrefs.SetFloat(StaticVariables.Level2_BarVal, 1f);
                PlayerPrefs.Save();
                yield return new WaitForSeconds(0.5f);
                PapersEffect.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(ShowScoreCardEffect()); //6-3
                yield return new WaitForSeconds(5f);
                Level_2_Manager.Instance.EndGame_PopUp_Show();//6-3            
            }
        }
    }

    IEnumerator After_WrongAnsEvalution()
    {
        if (Level_2_Manager.Instance.Check_GameState == Level_2_Manager.GameStates.InGame)
        {
            VoiceOver_AS.clip = IncorrectAnsSond_AC;
            VoiceOver_AS.Play();

            yield return new WaitForSeconds(0f);
            WrongAnsCount_i = WrongAnsCount_i + 1;

            if (WrongAnsCount_i == 3)
            {
                //WrongAnsCount_i = 0;
                //Debug.Log("hint shown");
                Is_NumbBoxsCardPlaced = false;

                //Invoke("ShowHighliteOutlne_OnSingleHintBox",1f); 20-4
            }
            yield return new WaitForSeconds(0.5f);
            RepeatQuestion_VO(0);
        }
    }

    void ShowHighliteOutlne_OnSingleHintBox()
    {
        if (!Is_NumbBoxsCardPlaced)
        {
            //Debug.Log("QuesNumb_i======== " + QuesNumb_i);
            NumbersHintImgsPar.SetActive(true);
            Invoke("ShowHighliteOutlne_OffSingleHintBox", 0.35f);
        }
    }
    void ShowHighliteOutlne_OffSingleHintBox()
    {
        if (!Is_NumbBoxsCardPlaced)
        {
            //Debug.Log("QuesNumb_i======== " + QuesNumb_i);
            NumbersHintImgsPar.SetActive(false);
            Invoke("ShowHighliteOutlne_OnSingleHintBox", 0.35f);
        }
    }
    ///////////////////////////////////////////////
    IEnumerator ShowScoreCardEffect()
    {           
        yield return new WaitForSeconds(0f);
        VoiceOver_AS.clip = RewardCollctSond_AC;
        VoiceOver_AS.Play();
        iTween.ScaleTo(ScoreCardMove.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 1f, "delay",0f, "islocal", true, "easetype", iTween.EaseType.spring));
        yield return new WaitForSeconds(2f);
        iTween.ScaleTo(ScoreCardMove.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "time", 0.75f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(ScoreCardMove.gameObject, iTween.Hash("x", 450f, "y", 250f, "time",0.75f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        yield return new WaitForSeconds(0.75f);
        CoinsChange_UpdateValue();
    }
    /////////////////////// Update Coins //////////////////////////////   
    private int CoinsTotalInGame_i;
    private int CoinsTempPrev_i;
    private int CoinsTempPrev_s;
    public void CoinsChange_UpdateValue()
    {
        CoinsScored_i = Level_2_Manager.Instance.CoinsToAddPerLvl;       
        PlayerPrefs.SetInt(StaticVariables.Coins_TotalScore, PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore) + CoinsScored_i);
        PlayerPrefs.Save();
        //CoinsTempPrev_s = 0;//in Default_State()
        CoinsTempPrev_i = Level_2_Manager.Instance.CoinsAtInitialLvl;
        CoinsTotalInGame_i = PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);
        CoinsShowTemp_Text.text = CoinsScored_i.ToString();
        ////Debug.Log("CoinsTempPrev_i+++++++++++++++++++::" + CoinsTempPrev_i);
        iTween.ValueTo(Level_2_Manager.Instance.CoinsShow_Text.gameObject, iTween.Hash("from", CoinsTempPrev_i, "to", CoinsTotalInGame_i, "time", 0.5f, "delay", 0.35f, "onupdatetarget", gameObject, "onupdate", "UpdateCoinsTextValue", "easetype", iTween.EaseType.linear));
    }
    void UpdateCoinsTextValue(int CoinsTotalInGame_i)
    {
        Level_2_Manager.Instance.CoinsShow_Text.text = "" + CoinsTotalInGame_i.ToString();
    }
    ///////////////////////////////////////////////    
    [Header("Repeat_VO")]
    private float repeatimerforquesvo;
    public int Count_MethodExecute_i_Display;
    public int CountDownTimer_15;

    void AtStart_TimeGapMethod()
    {
        repeatimerforquesvo += Time.deltaTime;

        Count_MethodExecute_i_Display = (int)(repeatimerforquesvo);
        ////Debug.Log("Check whether clicked after 10sec::repeat sound====="+ tiemrer);

        if (Count_MethodExecute_i_Display > CountDownTimer_15)
        {
            repeatimerforquesvo = 0f;
            Count_MethodExecute_i_Display = 0;

            RepeatQuestion_VO(0f);
            //Debug.Log("Check whether clicked after 10sec::repeat sound=====" + Count_MethodExecute_i_Display);
        }

        if (Input.GetMouseButtonUp(0))
        {
            repeatimerforquesvo = 0;
            Count_MethodExecute_i_Display = 0;
        }       
    }
    //////////////////////////////////////
    public void RepeatNumber_VO() //6-3
    {       
        VoiceOver_AS.clip = NumbersVoices_AC[QuesNumb_i - 11];// to play the ques number       
        VoiceOver_AS.PlayDelayed(0.3f);

        SoundBtn_Img.raycastTarget = false;
        StartCoroutine(EnabllingTHeBtn_Sond());
    }
    ///////////////////////////////////////////
    public void RepeatQuestion_VO(float delayy)
    {
        if (Level_2_Manager.Instance.Check_GameState == Level_2_Manager.GameStates.InGame)
        { 
            for (int i = 0; i < NumbersAnsImg_Arr.Length; i++)
            {
                NumbersAnsImg_Arr[i].GetComponent<Image>().raycastTarget = false;
            }

            VoiceOver_AS.clip = QuestionVoice_AC;
            VoiceOver_AS.PlayDelayed(delayy);

            SoundBtn_Img.raycastTarget = false;
           
            StartCoroutine(EnabllingTHeBtn_Sond()); //Disabled the number voice after Pramila asked//
        }
    }
   
    IEnumerator EnabllingTHeBtn_Sond()
    {
        yield return new WaitForSeconds(VoiceOver_AS.clip.length);
        SoundBtn_Img.raycastTarget = true;

        for(int i = 0; i < NumbersAnsImg_Arr.Length; i++)
        {
            NumbersAnsImg_Arr[i].GetComponent<Image>().raycastTarget = true;
        }
    }
    ////////////////////////////////////////////////////    
}
