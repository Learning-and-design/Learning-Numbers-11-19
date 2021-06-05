using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_1_Game : MonoBehaviour
{
    #region Getter
    private static Level_1_Game _instance;
    public static Level_1_Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_1_Game>();
            }
            return _instance;
        }
    }
    #endregion
           
    [Header("Ints")]
    public static int QuesCount_i = 0;
    public int QuesNumb_i;
    [Header("QuestionsLists")]
    public List<int> QuestionsNumbers_L;
    public static int CoinsScored_i;   
    [Header("GameObjects")]
    public GameObject[] UnitsBoxs_Arr;
    public GameObject[] TensBoxs_Arr;
    public GameObject[] TargetObjs;
    public GameObject TrainObj;
    public GameObject ScoreCardMove;
    public GameObject DragBoxSet;
    public GameObject PapersEffect;
    [Header("Vectors")]
    public Vector3[] UnitsBoxs_Arr_Pos;
    public Vector3[] TensBoxs_Arr_Pos;
    [Header("NumbersImgsSprte")]
    public Image NumbersImg;
    public Sprite[] NumbersSet_Sprt;
    [Header("WordsNUmbersImages")]
    public Image NumberWord_Img;
    public Sprite[] NumbersWords_Sprte;
    [Header("====HintImages====")]
    public GameObject[] UnitsHintBoxsPar;
    public GameObject[] TensHintBoxsPar;
    [Header("Bools")]    
    //public static bool Is_HintShown;
    private bool Is_TenBoxsCardPlaced;
    private bool Is_UnitBoxsCardsPlaced;
    private bool Is_TenBoxsCardPlaced_Hint;
    private bool Is_UnitBoxsCardsPlaced_Hint;
    private bool Is_FifteenQuesFirstAttempt;
    private bool Is_SeventeenQuesFirstAttempt;
    private bool Is_ThirteenQuesFirstAttempt;
    [Header("Images")]
    public Image DoneBtn;       
    public Image SoundBtn_Img;    
    [Header("Texts")]
    public Text CoinsScoreEndShow_Text;
    public static float Lvl1_BarVal_f;
    [Header("Scripts")]
    public SingleBoxsPlaces_Level1 SingleBoxsScrpts_Lvl1;
    public TensBoxsPlaces_Level1 TensBoxsScrpts_Lvl1;
    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;
    [Header("AudioClips")]
    public AudioClip[] NumbersVoices_AC;
    public AudioClip TryAgain_AC;
    public AudioClip[] QuestionVoicesPerCount_AC;
    public AudioClip IncorrectAnsSond_AC;
    public AudioClip CorrectAnsSond_AC;
    public AudioClip RewardCollctSond_AC;
    public AudioClip CompleteSond_AC;

    void OnEnable()
    {
        if (Level_1_Manager.Is_SecMoreHintShown)
        {           
            //Debug.Log("Before::::Is_HintShown::::" + Level_1_Manager.Is_SecMoreHintShown);
                        
            QuesCount_i = QuesCount_i - 1;
            //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);
            
            //CoinsShow_Text.text = " " + CoinsScored_i.ToString();
            Level_1_Manager.Is_SecMoreHintShown = false;
        }
        if (StaticVariables.Is_FromLvl0_CameFrmLvl1_GameSkipToLvl1)
        {
            QuesCount_i = QuesCount_i - 1;
            //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);
            StaticVariables.Is_FromLvl0_CameFrmLvl1_GameSkipToLvl1 = false;
        }
        if (StaticVariables.Is_FromLvl2_Back)
        {       
            if(QuesCount_i>0)
            QuesCount_i = QuesCount_i - 1;
            //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);           
        }
        if (Level_1_Manager.Is_PlayAgain_Game_SameLvl)
        {            
            QuesCount_i = 0;
            //Debug.LogError("Is_PlayAgain_Game_SameLvl========");
            //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);            
            Level_1_Manager.Is_PlayAgain_Game_SameLvl = false;
        }

        Default_State();           
        //AtStart_TimeGapMethod();
        GenerateNumbsWords();
        SoundBtn_Img.enabled = true;

        Is_FifteenQuesFirstAttempt = false;
        Is_SeventeenQuesFirstAttempt = false;
        Is_ThirteenQuesFirstAttempt = false;
        //ReStoreDefaultPosnsAftHint();//From Skip Tut 
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }    
    // Update is called once per frame
    void Update()
    {       
        if (Level_1_Manager.Instance.Check_GameState == Level_1_Manager.GameStates.InGame)
        {
            AtStart_TimeGapMethod();   
        }
    }
    void Default_State()
    {
        TrainObj.transform.localPosition = new Vector3(1050f, -80f, 0f);

        PapersEffect.SetActive(false);

        for (int i = 0; i < UnitsHintBoxsPar.Length; i++)
        {
            UnitsHintBoxsPar[i].gameObject.SetActive(false);
            TensHintBoxsPar[i].gameObject.SetActive(false);
        }

        
        ScoreCardMove.transform.localScale = new Vector3(0, 0, 0);

        for (int i = 0; i < TargetObjs.Length; i++)
        {
            TargetObjs[i].SetActive(true);
        }

        //TryAgain_Bg.SetActive(false);

        UnitsBoxs_Arr_Pos = new Vector3[9];
        TensBoxs_Arr_Pos = new Vector3[9];

        for (int i = 0; i < UnitsBoxs_Arr.Length; i++) //Get Positions
        {
            UnitsBoxs_Arr_Pos[i] = UnitsBoxs_Arr[i].transform.localPosition;
            ////Debug.Log(":::::: default positions in start :::::: "+ UnitsBoxs_Arr_Pos[i]);
        }
        for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
        {
            TensBoxs_Arr_Pos[i] = TensBoxs_Arr[i].transform.localPosition;
            ////Debug.Log(":::::: default positions in start :::::: " + TensBoxs_Arr_Pos[i]);
        }

        CoinsTempPrev_s = PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);
    }
    public void ReStoreDefaultPosnsAftHint()
    {
        /*UnitsBoxs_Arr_Pos[0] = new Vector3(-220f, 10f, 0f);
        UnitsBoxs_Arr_Pos[1] = new Vector3(-170f, 10f, 0f);
        UnitsBoxs_Arr_Pos[2] = new Vector3(-120f, 10f, 0f);
        UnitsBoxs_Arr_Pos[3] = new Vector3(-60f, 10f, 0f);
        UnitsBoxs_Arr_Pos[4] = new Vector3(-12f, 10f, 0f);
        UnitsBoxs_Arr_Pos[5] = new Vector3(40f, 10f, 0f);
        UnitsBoxs_Arr_Pos[6] = new Vector3(90f, 10f, 0f);
        UnitsBoxs_Arr_Pos[7] = new Vector3(145f, 10f, 0f);
        UnitsBoxs_Arr_Pos[8] = new Vector3(195f, 10f, 0f);
        /////////////
        TensBoxs_Arr_Pos[0] = new Vector3(-169.6f, - 43.1f, 0f);
        TensBoxs_Arr_Pos[1] = new Vector3(-169.6f, -77.1f, 0f);
        TensBoxs_Arr_Pos[2] = new Vector3(-169.6f, -111.1f, 0f);
        TensBoxs_Arr_Pos[3] = new Vector3(-10.9f, -77.1f, 0f);
        TensBoxs_Arr_Pos[4] = new Vector3(-10.9f, -43.1f, 0f);
        TensBoxs_Arr_Pos[5] = new Vector3(-10.9f, -111.1f, 0f);
        TensBoxs_Arr_Pos[6] = new Vector3(143.0695f, -43.1f, 0f);
        TensBoxs_Arr_Pos[7] = new Vector3(143.0695f, -77.1f, 0f);
        TensBoxs_Arr_Pos[8] = new Vector3(143.0695f, -111.1f, 0f);*/
        //////////////////////////////////////////////////////////      


        for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
        {
            UnitsBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            UnitsBoxs_Arr[i].transform.localPosition = UnitsBoxs_Arr_Pos[i];

            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }
        for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
        {
            TensBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            TensBoxs_Arr[i].transform.localPosition = TensBoxs_Arr_Pos[i];
            //   //Debug.Log("reaarranged the folowing pos+++++++++++++++++++++++++++++++++");
            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }        
    }
    void GenerateNumbsWords()
    {
        if (Level_1_Manager.Instance.Check_GameState != Level_1_Manager.GameStates.InGame)//25-04
            return;

        TrainIn_Anim();
        // VoiceOver_AS.mute = false;

        for (int i = 0; i < UnitsHintBoxsPar.Length; i++)
        {
            UnitsHintBoxsPar[i].gameObject.SetActive(false);
            TensHintBoxsPar[i].gameObject.SetActive(false);
        }

        DoneBtn_State(false,false);

        Is_TenBoxsCardPlaced = false;
        Is_UnitBoxsCardsPlaced = false;
        Is_TenBoxsCardPlaced_Hint = false;
        Is_UnitBoxsCardsPlaced_Hint = false;

        //Debug.Log("Before::::QuesCount_i::::" + QuesCount_i + "::::::QuesNumb_i " + QuesNumb_i);
       
        QuesCount_i = QuesCount_i + 1;
        //Debug.Log("Before::::QuesCount_i::::" + QuesCount_i + "::::::QuesNumb_i " + QuesNumb_i);
        
        WrongAnsCount_i = 0;

        StartCoroutine(ReArrangingPosofAllBoxes());

        /*if (QuesCount_i > 9)
            return;*/

        if (!Is_FifteenQuesFirstAttempt || !Is_SeventeenQuesFirstAttempt || !Is_ThirteenQuesFirstAttempt)
        {
            //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

            if (QuesCount_i == 10)
            {
                QuesCount_i = 7;
                Level_1_Manager.Instance.Update_ProgresBarValuee(-0.33f);   //Dec val for repeat ans again   
            }
            //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);
        }
        //Debug.Log("::::::::QuesCount_i::::: " + QuesCount_i);
        QuesNumb_i = QuestionsNumbers_L[QuesCount_i - 1];
        //Debug.Log("::::::::QuesNumb_i::::: " + QuesNumb_i);
                

        switch (QuesNumb_i)
        {
            case 11:
                NumbersImg.sprite = NumbersSet_Sprt[0];
                NumberWord_Img.sprite = NumbersWords_Sprte[0];
                break;
            case 12:
                NumbersImg.sprite = NumbersSet_Sprt[1];
                NumberWord_Img.sprite = NumbersWords_Sprte[1];
                break;
            case 13:
                NumbersImg.sprite = NumbersSet_Sprt[2];
                NumberWord_Img.sprite = NumbersWords_Sprte[2];
                break;
            case 14:
                NumbersImg.sprite = NumbersSet_Sprt[3];
                NumberWord_Img.sprite = NumbersWords_Sprte[3];
                break;
            case 15:
                NumbersImg.sprite = NumbersSet_Sprt[4];
                NumberWord_Img.sprite = NumbersWords_Sprte[4];
                break;
            case 16:
                NumbersImg.sprite = NumbersSet_Sprt[5];
                NumberWord_Img.sprite = NumbersWords_Sprte[5];
                break;
            case 17:
                NumbersImg.sprite = NumbersSet_Sprt[6];
                NumberWord_Img.sprite = NumbersWords_Sprte[6];
                break;
            case 18:
                NumbersImg.sprite = NumbersSet_Sprt[7];
                NumberWord_Img.sprite = NumbersWords_Sprte[7];
                break;
            case 19:
                NumbersImg.sprite = NumbersSet_Sprt[8];
                NumberWord_Img.sprite = NumbersWords_Sprte[8];
                break;
            default:
                NumbersImg.sprite = NumbersSet_Sprt[0];
                NumberWord_Img.sprite = NumbersWords_Sprte[0];
                break;
        }

        NumberWord_Img.enabled = false;

        RepeatQuestion_VO(0f);

        iTween.ScaleTo(NumbersImg.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(NumbersImg.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

    }
    ///////////////////////////
    private int UnitBoxCount_i;
    public void CheckUnitsBoxQuantity()
    {
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

        UnitBoxCount_i = TargetObjs[0].transform.childCount;

        if (UnitBoxCount_i >= 1)
        {
            Is_UnitBoxsCardsPlaced = true;
        }
        if (UnitBoxCount_i == 0)
        {
            Is_UnitBoxsCardsPlaced = false;
        }
        //Debug.Log("Is_UnitBoxsCardsPlaced===============+===++++++++++++" + Is_UnitBoxsCardsPlaced);
        ////////////////////////////////////////
        if (UnitBoxCount_i >= (QuesNumb_i - 10))
        {
            if (WrongAnsCount_i >= 3)
            {
                Is_UnitBoxsCardsPlaced_Hint = true;
            }
            // //Debug.Log("UnitBoxCount_i====:: " + UnitBoxCount_i);
        }

        SingleBoxsScrpts_Lvl1.SetPosnOfUnitsBoxs();
        //Debug.Log("============================================================");
               
        DoneBtn_State(Is_UnitBoxsCardsPlaced, Is_TenBoxsCardPlaced);
        // //Debug.Log("UnitBoxCount_i:: " + UnitBoxCount_i);

        if (Is_TenBoxsCardPlaced_Hint && Is_UnitBoxsCardsPlaced_Hint)
        {
            for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
            {
                TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
            }
            for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
            {
                UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
            }
        }
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);
    }
    //////////////////////////
    private int TenBoxCount_i;
    public void CheckTensBoxQuantity()
    {
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

        TenBoxCount_i = TargetObjs[1].transform.childCount;

        if (TenBoxCount_i >= 1)
        {
            Is_TenBoxsCardPlaced = true;

            if (WrongAnsCount_i >= 3)
            {
                Is_TenBoxsCardPlaced_Hint = true;
            }

        }
        if (TenBoxCount_i == 0)
        {
            Is_TenBoxsCardPlaced = false;
        }

        TensBoxsScrpts_Lvl1.SetPosnOfTensBoxs();
        //Debug.Log("Is_TenBoxsCardPlaced============================================================"+ Is_TenBoxsCardPlaced);
              
        DoneBtn_State(Is_UnitBoxsCardsPlaced, Is_TenBoxsCardPlaced);
        // //Debug.Log("TenBoxCount_i:: " + TenBoxCount_i);

        if (Is_TenBoxsCardPlaced_Hint && Is_UnitBoxsCardsPlaced_Hint)
        {
            for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
            {
                //TensBoxs_Arr[6].GetComponent<Image>().raycastTarget = false;
                //TensBoxs_Arr[6].GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, true);
                //TensBoxs_Arr[6].GetComponent<Image>().color = new Color(0f, 194f, 4f, 0f);
            }
            for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
            {
                //UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
            }
        }
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);
    }
    //////////////////////////
    #region TrainAnim_InOut
    void TrainIn_Anim()
    {
        TrainObj.transform.localPosition = new Vector3(1050f, -80f, 0f);
        iTween.MoveTo(TrainObj.gameObject, iTween.Hash("x", 700f, "time", 2f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
    }
    void TrainOut_Anim()
    {
        iTween.MoveTo(TrainObj.gameObject, iTween.Hash("x", -1050f, "time", 2f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
    }
    #endregion TrainAnim_InOut    
    //////////////////////////
    void DoneBtn_State(bool is_unitplaced, bool is_tensplaced)
    {
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

        /*DoneBtn.gameObject.SetActive(is_show);
        DoneBtn.raycastTarget = is_show;*/

        if (is_unitplaced || is_tensplaced)
        {
            DoneBtn.gameObject.SetActive(true);
            DoneBtn.raycastTarget = true;
        }
        else if (!is_unitplaced || !is_tensplaced)
        {
            DoneBtn.gameObject.SetActive(false);
            DoneBtn.raycastTarget = false;
        }        
    }

    public void DoneBtn_Clicked()
    {
        DoneBtn_State(false,false);

        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

        if ((TenBoxCount_i == 1) && (UnitBoxCount_i == (QuesNumb_i - 10))) //for getting the value of last digit       
        {
            NumberWord_Img.enabled = true;
            // NumberWord_Img.sprite = NumbersWords_Sprte[QuesCount_i - 1];
            // VoiceOver_AS.clip = NumbersVoices_AC[QuesCount_i - 1];
            /*VoiceOver_AS.clip = NumbersVoices_AC[(QuesNumb_i - 11)];
            VoiceOver_AS.Play();*/

            StartCoroutine(After_CorrectAnsEvalution());
            //   //Debug.Log(":::::: correct answer :::::: ");
            TrainOut_Anim();
        }
        else
        {
            StartCoroutine(After_WrongAnsEvalution());
            //   //Debug.Log(":::::: wrong answer :::::: ");
        }       
       
    }
    ///////////////////////////////////////////////
    IEnumerator ShowScoreCardEffect()
    {
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);
        yield return new WaitForSeconds(0f);
        VoiceOver_AS.clip = RewardCollctSond_AC;
        VoiceOver_AS.Play();
        iTween.ScaleTo(ScoreCardMove.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 1f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.spring));
        yield return new WaitForSeconds(2f);
        iTween.ScaleTo(ScoreCardMove.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "time", 0.75f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(ScoreCardMove.gameObject, iTween.Hash("x", 450f, "y", 250f, "time", 0.75f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        yield return new WaitForSeconds(0.5f);
        CoinsChange_UpdateValue();
    }
    /////////////////////// Update Coins //////////////////////////////   
    private int CoinsTotalInGame_i;
    private int CoinsTempPrev_i;
    private int CoinsTempPrev_s;
    public void CoinsChange_UpdateValue()
    {
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

        CoinsScored_i = Level_1_Manager.Instance.CoinsToAddPerLvl;        
        PlayerPrefs.SetInt(StaticVariables.Coins_TotalScore, PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore) + CoinsScored_i);
        PlayerPrefs.Save();
        //CoinsTempPrev_s =  0;//in Default_State()
        CoinsTempPrev_i = Level_1_Manager.Instance.CoinsAtInitialLvl;
        CoinsTotalInGame_i = PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);
        CoinsScoreEndShow_Text.text = CoinsScored_i.ToString();
        ////Debug.Log("CoinsTempPrev_i+++++++++++++++++++::" + CoinsTempPrev_i);
        iTween.ValueTo(Level_1_Manager.Instance.CoinsShow_Text.gameObject, iTween.Hash("from", CoinsTempPrev_i, "to", CoinsTotalInGame_i, "time", 0.5f, "delay", 0.35f, "onupdatetarget", gameObject, "onupdate", "UpdateCoinsTextValue", "easetype", iTween.EaseType.linear));
    }
    void UpdateCoinsTextValue(int CoinsTotalInGame_i)
    {
        Level_1_Manager.Instance.CoinsShow_Text.text = "" + CoinsTotalInGame_i.ToString();
    }    
    ///////////////////////////////////////////////    
    [Header("Repeat_VO")]
    private float repeatimerforquesvo;
    public int Count_MethodExecute_i_Display = 0;
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
        ////Debug.Log("QuesNumb_i++++++++++++++++++++++++" + QuesNumb_i);
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

        for (int i = 0; i < TensBoxs_Arr.Length; i++)
        {
            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
        }

        if (Level_1_Manager.Instance.Check_GameState == Level_1_Manager.GameStates.InGame)
        {
            ////Debug.Log("QuesNumb_i++++++++++++++++++++++++" + QuesNumb_i);
            VoiceOver_AS.clip = QuestionVoicesPerCount_AC[QuesNumb_i - 11];// to play the ques number       
            VoiceOver_AS.PlayDelayed(delayy);

            //Debug.Log("QuesNumb_i++++++++++++++++++++++++" + QuesNumb_i);

            SoundBtn_Img.raycastTarget = false;
            StartCoroutine(EnabllingTHeBtn_Sond());
        }
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);
    }    
    IEnumerator EnabllingTHeBtn_Sond()
    {
        yield return new WaitForSeconds(0.5f);
        SoundBtn_Img.raycastTarget = true;

        for (int i = 0; i < TensBoxs_Arr.Length; i++)
        {
            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }        
    }   
    ////////////////////////////////////////////////////
    private int AnsEval_i;
    private int WrongAnsCount_i;
    IEnumerator After_CorrectAnsEvalution()
    {
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

        VoiceOver_AS.clip = CorrectAnsSond_AC;
        VoiceOver_AS.Play();

        yield return new WaitForSeconds(VoiceOver_AS.clip.length);

        VoiceOver_AS.clip = NumbersVoices_AC[QuesNumb_i - 11];
        VoiceOver_AS.Play();

        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "z", 1.1f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
    

        AnsEval_i = 1;

        // TargetObjs[0].GetComponent<GridLayoutGroup>().enabled = false;
        // TargetObjs[0].GetComponent<Image>().enabled = false;

        Level_1_Manager.Instance.Update_ProgresBarValuee(0.11f);

        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

        if (WrongAnsCount_i <= 0) //if correct in first attempt
        {           

            if ((QuesNumb_i == 15))
            {
                Is_FifteenQuesFirstAttempt = true;
            }
            else if ((QuesNumb_i == 17))
            {
                Is_SeventeenQuesFirstAttempt = true;
            }
            else if ((QuesNumb_i == 13))
            {
                Is_ThirteenQuesFirstAttempt = true;
            }
        }

        yield return new WaitForSeconds(1f);

        if (QuesCount_i <= 9)
        {
            //Debug.Log("QuesCount_i ::::::::::::::::::::::::" + QuesCount_i);
            Invoke("GenerateNumbsWords", 1f);
            //StartCoroutine(ReArrangingPosofAllBoxes());
        }

        if (Is_FifteenQuesFirstAttempt && Is_SeventeenQuesFirstAttempt && Is_ThirteenQuesFirstAttempt)
        {
            Level_1_Manager.Instance.Check_GameState = Level_1_Manager.GameStates.GameEnd;
            Level_1_Manager.Instance.Levels_BarFill[0].fillAmount = 1f;
            PlayerPrefs.SetFloat(StaticVariables.Level1_BarVal, 1f);
            PlayerPrefs.Save();
            yield return new WaitForSeconds(0.5f);
            PapersEffect.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(ShowScoreCardEffect()); //6-3
            yield return new WaitForSeconds(5f);
            Level_1_Manager.Instance.EndGame_PopUp_Show();//6-3               
        }

        //Debug.Log("Is_FifteenQuesFirstAttempt :::::::::::" + Is_FifteenQuesFirstAttempt + "" + "Is_SeventeenQuesFirstAttempt :::::::::::" + Is_SeventeenQuesFirstAttempt + "Is_ThirteenQuesFirstAttempt :::::::::::" + Is_ThirteenQuesFirstAttempt);

        //off for total score at end
        /*CoinsScored_i = CoinsScored_i + Level_1_Manager.Instance.CoinsToAddPerQues;
        PlayerPrefs.SetInt(StaticVariables.Level1_Score, PlayerPrefs.GetInt(StaticVariables.Level1_Score) + CoinsScored_i);
        PlayerPrefs.SetInt(StaticVariables.Coins_TotalScore, PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore) + PlayerPrefs.GetInt(StaticVariables.Level1_Score));*/

        // DragUnits_Level1.Instance.InitPos_Set();
        // DragTens_Level2.Instance.InitPos_Set();
    }
    IEnumerator After_WrongAnsEvalution()
    {
        VoiceOver_AS.clip = IncorrectAnsSond_AC;
        VoiceOver_AS.Play();

        WrongAnsCount_i = WrongAnsCount_i + 1;

        AnsEval_i = 0;
       
        yield return new WaitForSeconds(0.5f);
        VoiceOver_AS.clip = TryAgain_AC;
        VoiceOver_AS.Play();
        //Debug.Log("Voice call:::::::::TRY AGAIN::::::::::");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ReArrangingPosOfOnlyWrongAnsBoxes());

        Is_UnitBoxsCardsPlaced = false;
        Is_TenBoxsCardPlaced = false;
        Is_UnitBoxsCardsPlaced_Hint = false;
        Is_TenBoxsCardPlaced_Hint = false;

        if (WrongAnsCount_i >= 3)
        {
            //WrongAnsCount_i = 0;
            //    //Debug.Log("hint shown");
            _HintHighlight();
        }
        yield return new WaitForSeconds(0.5f);
        RepeatQuestion_VO(0);
    
    }
    IEnumerator ReArrangingPosOfOnlyWrongAnsBoxes()
    {
        yield return new WaitForSeconds(0f);

        for (int i = 0; i < TensBoxs_Arr.Length; i++)
        {
            TensBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            TensBoxs_Arr[i].transform.localPosition = TensBoxs_Arr_Pos[i];

            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }
        for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
        {
            UnitsBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            UnitsBoxs_Arr[i].transform.localPosition = UnitsBoxs_Arr_Pos[i];

            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }
        /*if ((TenBoxCount_i > 0))
        {
            for (int i = 0; i < TensBoxs_Arr.Length; i++)
            {
                TensBoxs_Arr[i].transform.parent = DragBoxSet.transform;
                TensBoxs_Arr[i].transform.localPosition = TensBoxs_Arr_Pos[i];

                TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
            }
        }
        if ((UnitBoxCount_i > QuesNumb_i - 10) || (UnitBoxCount_i < QuesNumb_i - 10))
        {
            for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
            {
                UnitsBoxs_Arr[i].transform.parent = DragBoxSet.transform;
                UnitsBoxs_Arr[i].transform.localPosition = UnitsBoxs_Arr_Pos[i];

                UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
            }
        }  */     

    }
    void _HintHighlight()
    {
        //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);

        myquesno = QuesNumb_i - 10;
        ShowHighliteOutlne_OnSingleHintBox();
        ShowHighliteOutlne_OnTensCad();
    }
    private int myquesno;
    void ShowHighliteOutlne_OnSingleHintBox()
    {
        if (Is_UnitBoxsCardsPlaced_Hint)
            return;

        for (int i = 0; i <= 8; i++)
        {
            if (i < myquesno)
            {
                UnitsHintBoxsPar[i].gameObject.SetActive(true);
            }
        }

        Invoke("ShowHighliteOutlne_OffSingleHintBox", 0.35f);
    }
    void ShowHighliteOutlne_OffSingleHintBox()
    {
        if (Is_UnitBoxsCardsPlaced_Hint)
            return;

        for (int i = 0; i <= 8; i++)
        {

            if (i < myquesno)
            {
                UnitsHintBoxsPar[i].gameObject.SetActive(false);
            }
        }

        Invoke("ShowHighliteOutlne_OnSingleHintBox", 0.35f);
    }
    ///////
    void ShowHighliteOutlne_OnTensCad()
    {
        if (Is_TenBoxsCardPlaced_Hint)
            return;

        TensHintBoxsPar[0].gameObject.SetActive(true);

        Invoke("ShowHighliteOutlne_OffTensCad", 0.35f);
    }
    void ShowHighliteOutlne_OffTensCad()
    {
        if (Is_TenBoxsCardPlaced_Hint)
            return;

        TensHintBoxsPar[0].gameObject.SetActive(false);

        Invoke("ShowHighliteOutlne_OnTensCad", 0.35f);
    }
    ///////////////////////////////////////////////

    IEnumerator ReArrangingPosofAllBoxes()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
        {
            UnitsBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            UnitsBoxs_Arr[i].transform.localPosition = UnitsBoxs_Arr_Pos[i];

            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }
        for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
        {
            TensBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            TensBoxs_Arr[i].transform.localPosition = TensBoxs_Arr_Pos[i];
            //   //Debug.Log("reaarranged the folowing pos+++++++++++++++++++++++++++++++++");
            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }

    }    
    ///////////////////////////////////////////////////////////////////
   
}



