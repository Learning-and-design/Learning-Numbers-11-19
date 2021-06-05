using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_3_Game : MonoBehaviour
{  
    #region Getter
    private static Level_3_Game _instance;
    public static Level_3_Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_3_Game>();
            }
            return _instance;
        }
    }
    #endregion

    [Header(":::: QuestionsLists ::::")]
    public List<int> QuestionsNumbers_L;
    [Header("***Ints***")]
    public static int QuesCount_i = 0;
    public int QuesNumb_i;
    public int CoinsScored_i;   
    [Header(":::: GameObjects ::::")]
    public GameObject[] UnitsBoxs_Arr;
    public GameObject[] TensBoxs_Arr;
    public GameObject[] NumbersImg_Arr;
    public GameObject[] TargetObjs;
    public GameObject DragBoxSet;
    public GameObject PapersEffect;
    public GameObject ScoreCardMove;
    [Header("====HintImages====")]
    public GameObject[] UnitsHintBoxsPar;
    public GameObject[] TensHintBoxsPar;
    public GameObject NumbersHintImgsPar;
    [Header(":::: WordsNumbersImages ::::")]
    public Image NumberWord_Img;
    public Sprite[] NumbersWords_Sprte;   
    [Header(":::: Bools ::::")]    
    public bool Is_UnitBoxsCardsPlaced;
    public bool Is_TenBoxsCardPlaced;
    public bool Is_NumbBoxsCardPlaced;
    public bool Is_TenBoxsCardPlaced_Hint;
    public bool Is_UnitBoxsCardsPlaced_Hint;
    public bool Is_NumbBoxsCardsPlaced_Hint;
    private bool Is_LastbutSecQues_FirstAttempt;
    private bool Is_LastbutOneQues_FirstAttempt;
    private bool Is_LastQues_FirstAttempt;
    [Header("Texts")]
    public Text CoinsScoreEndShow_Text;
    [Header(":::: Vectors ::::")]
    public Vector3[] UnitsBoxs_Arr_Pos;
    public Vector3[] TensBoxs_Arr_Pos;
    public Vector3[] NumbersBoxs_Arr_Pos;
    [Header(":::: Images ::::")]    
    public Image DoneBtn;       
    public Image SoundBtn_Img;
    [Header("Scripts")]
    public SingleBoxsPlaces_Level3 SingleBoxsScrpts_Lvl3;
    public TensBoxsPlaces_Level3 TensBoxsScrpts_Lvl3;
    [Header(":::: AudioSource ::::")]
    public AudioSource VoiceOver_AS;
    [Header(":::: AudioClips ::::")]
    public AudioClip[] NumbersVoices_AC;
    public AudioClip TryAgain_AC;
    public AudioClip QuestionVoice_AC;
    public AudioClip IncorrectAnsSond_AC;
    public AudioClip CorrectAnsSond_AC;
    public AudioClip RewardCollctSond_AC;
    public AudioClip CompleteSond_AC;

    void OnEnable()
    {
        if (Level_3_Manager.Is_SecMoreHintShown)
        {
            StaticVariables.Is_Tutorial = false;

            //Debug.Log("Before::::Is_HintShown::::" + Level_3_Manager.Is_SecMoreHintShown);
            QuesCount_i = QuesCount_i - 1;
            //CoinsShow_Text.text = " " + CoinsScored_i.ToString();
            Level_3_Manager.Is_SecMoreHintShown = false;
        }
        if (StaticVariables.Is_FromLvl2_CameFrmLvl3_GameSkipToLvl3)
        {
            QuesCount_i = QuesCount_i - 1;
            //Debug.Log("QuesCount_i::::QuesCount_i::::" + QuesCount_i);
            StaticVariables.Is_FromLvl2_CameFrmLvl3_GameSkipToLvl3 = false;
        }
        if (StaticVariables.Is_FromLvl4_Back)//25-04
        {
            if (QuesCount_i > 0)
                QuesCount_i = QuesCount_i - 1;           
        }
        if (Level_3_Manager.Is_PlayAgain_Game_SameLvl)//25-04
        {
            QuesCount_i = 0;            
            Level_3_Manager.Is_PlayAgain_Game_SameLvl = false;
        }

        Is_LastbutSecQues_FirstAttempt = false;
        Is_LastbutOneQues_FirstAttempt = false;
        Is_LastQues_FirstAttempt = false;

        Default_State();       
        //AtStart_TimeGapMethod();
        GenerateNumbsWords();
        SoundBtn_Img.enabled = true;
    }
        
    // Update is called once per frame
    void Update()
    {
        if ((Level_3_Manager.Instance.Check_GameState == Level_3_Manager.GameStates.InGame))
        {
            AtStart_TimeGapMethod();
        }
    }

    void Default_State()
    {
        PapersEffect.SetActive(false);

        ScoreCardMove.transform.localScale = new Vector3(0, 0, 0);

        Level_3_Manager.Instance.CoinsShow_Text.text = " " + PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);

        for (int i = 0; i < TargetObjs.Length; i++)
        {
            TargetObjs[i].SetActive(true);
        }
                               
        SoundBtn_Img.enabled = true;

        UnitsBoxs_Arr_Pos = new Vector3[9];
        TensBoxs_Arr_Pos = new Vector3[9];
        NumbersBoxs_Arr_Pos = new Vector3[9];

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
        for (int i = 0; i < NumbersImg_Arr.Length; i++) //Get Positions
        {
            NumbersBoxs_Arr_Pos[i] = NumbersImg_Arr[i].transform.localPosition;
            ////Debug.Log(":::::: default positions in start :::::: " + NumbersBoxs_Arr_Pos[i]);
        }

        CoinsTempPrev_s = PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);
                
    }
    void GenerateNumbsWords()
    {
        if (Level_3_Manager.Instance.Check_GameState == Level_3_Manager.GameStates.GameEnd)
            return;

        Is_UnitBoxsCardsPlaced = false;
        Is_TenBoxsCardPlaced = false;
        Is_NumbBoxsCardPlaced = false;
        Is_UnitBoxsCardsPlaced_Hint = false;
        Is_TenBoxsCardPlaced_Hint = false;        
        Is_NumbBoxsCardsPlaced_Hint = false;

        WrongAnsCount_i = 0;

        NumbersHintImgsPar.gameObject.SetActive(false);
        for (int i = 0; i < UnitsHintBoxsPar.Length; i++)
        {            
            UnitsHintBoxsPar[i].gameObject.SetActive(false);
            TensHintBoxsPar[i].gameObject.SetActive(false);             
        }
        for (int i = 0; i < NumbersImg_Arr.Length; i++)
        {
            NumbersImg_Arr[i].GetComponent<Image>().raycastTarget = false;
            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
        }

        DoneBtn_State(false,false,false);
                              
        QuesCount_i = QuesCount_i + 1;
        ////Debug.Log("QuesCount_i::::" + QuesCount_i + "::::::QuesNumb_i " + QuesNumb_i);

        StartCoroutine(ReArrangingPosofAllBoxes());        

        /*if (QuesCount_i > 9)
            return;*/

        if (!Is_LastQues_FirstAttempt || !Is_LastbutOneQues_FirstAttempt || !Is_LastbutSecQues_FirstAttempt)
        {       
            if (QuesCount_i == 10) //4
            {
                QuesCount_i = 7;//1
                //Debug.Log("::::::::QuesCount_i::::: " + QuesCount_i);

                Level_3_Manager.Instance.Update_ProgresBarValuee(-0.33f);   //Dec val for repeat ans again   

                Is_LastQues_FirstAttempt = false;
                Is_LastbutOneQues_FirstAttempt = false;
                Is_LastbutSecQues_FirstAttempt = false;
            }
        }

        QuesNumb_i = QuestionsNumbers_L[QuesCount_i - 1];
        
        //Debug.Log("QuesCount_i::::" + QuesCount_i + "::::::QuesNumb_i " + QuesNumb_i);

        for (int i=0; i< NumbersWords_Sprte.Length; i++)
        {
            NumberWord_Img.sprite = NumbersWords_Sprte[QuesNumb_i - 11];
        }
        
        NumberWord_Img.enabled = false;

        RepeatQuestion_VO(0f);

        //   iTween.ScaleTo(NumbersImg.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        //    iTween.ScaleTo(NumbersImg.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

    }
    ///////////////////////////////////
    private int UnitBoxCount_i;
    public void CheckUnitsBoxQuantity()
    {
        UnitBoxCount_i = TargetObjs[0].transform.childCount;

        if (UnitBoxCount_i >= 1)
        {
            Is_UnitBoxsCardsPlaced = true;
        }
        else if (UnitBoxCount_i == 0)
        {
            Is_UnitBoxsCardsPlaced = false;
        }
        ///////////////////////////////////
        if (UnitBoxCount_i >= (QuesNumb_i - 10))
        {
            if (WrongAnsCount_i >= 3)
            {
                Is_UnitBoxsCardsPlaced_Hint = true;
            }
            //Debug.Log("UnitBoxCount_i====:: " + UnitBoxCount_i);
        }

        SingleBoxsScrpts_Lvl3.SetPosnOfUnitsBoxs();
       
        DoneBtn_State(Is_UnitBoxsCardsPlaced,Is_TenBoxsCardPlaced,Is_NumbBoxsCardPlaced);

        //Debug.Log("UnitBoxCount_i:: " + UnitBoxCount_i);

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
    
    }
    //////////////////////////////////
    private int TenBoxCount_i;
    public void CheckTensBoxQuantity()
    {
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

        TensBoxsScrpts_Lvl3.SetPosnOfTensBoxs();
               
        DoneBtn_State(Is_UnitBoxsCardsPlaced, Is_TenBoxsCardPlaced, Is_NumbBoxsCardPlaced);
        //Debug.Log("TenBoxCount_i:: " + TenBoxCount_i);

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
    }//////////////////////////////////
    private int NumbrsBoxCount_i;
    private int Draged_ImgVal_i;
    public void CheckNumbrsBoxQuantity(int _ImgVal_i)
    {
        Draged_ImgVal_i = _ImgVal_i;
        NumbrsBoxCount_i = TargetObjs[2].transform.childCount;

        //Debug.Log("_ImgVal_i=======================:: " + _ImgVal_i);

        if (NumbrsBoxCount_i > 1)
        {
            GameObject PresImg = TargetObjs[2].transform.GetChild(1).gameObject;
            PresImg.gameObject.transform.localPosition = new Vector3(0, 0, 0);

            GameObject PrevImg = TargetObjs[2].transform.GetChild(0).gameObject;
            int ImgVol_temp = PrevImg.GetComponent<DragNumbImg_Level3>().ImgVal_i; //23-05
            PrevImg.transform.parent = DragBoxSet.transform;
            PrevImg.transform.localPosition = NumbersBoxs_Arr_Pos[ImgVol_temp - 11];

            //Debug.Log("ImgVol_temp =======================:: " + ImgVol_temp);
            //Debug.Log("PresImg.gameObject.transform.localPosition =======================:: " + PresImg.gameObject.transform.localPosition);
            //Debug.Log("PrevImg.transform.localPosition =======================:: " + PrevImg.transform.localPosition);
            //Debug.Log("_ImgVal_i - 10 =======================:: " + _ImgVal_i);            
        }
        if (NumbrsBoxCount_i >= 1)
        {
            Is_NumbBoxsCardPlaced = true;                                  

            if (WrongAnsCount_i >= 3)
            {
                Is_NumbBoxsCardsPlaced_Hint = true;
            }
        }
        if (NumbrsBoxCount_i == 0)
        {
            Is_NumbBoxsCardPlaced = false;
        }            
        DoneBtn_State(Is_UnitBoxsCardsPlaced, Is_TenBoxsCardPlaced, Is_NumbBoxsCardPlaced);
        //Debug.Log("TenBoxCount_i:: " + NumbrsBoxCount_i);

        if (Is_TenBoxsCardPlaced_Hint && Is_UnitBoxsCardsPlaced_Hint && Is_NumbBoxsCardPlaced)
        {
            for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
            {
                //TensBoxs_Arr[6].GetComponent<Image>().raycastTarget = false;               
            }
            for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
            {
                //UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
            }
            for (int i = 0; i < NumbersImg_Arr.Length; i++)
            {
                //NumbersImg_Arr[i].GetComponent<Image>().raycastTarget = false;
            }
        }
    }
    //////////////////////////
    void DoneBtn_State(bool is_unitplaced, bool is_tensplaced, bool is_numbcardplaced)
    {
        //Debug.Log("is_unitplaced::::::"+ is_unitplaced+ "||is_tensplaced:::::: "+ is_tensplaced+ "||is_numbcardplaced:::::: " + is_numbcardplaced);

        /*DoneBtn.gameObject.SetActive(is_show);
        DoneBtn.raycastTarget = is_show;*/

        if (is_unitplaced || is_tensplaced || is_numbcardplaced)
        {
            DoneBtn.gameObject.SetActive(true);
            DoneBtn.raycastTarget = true;
        }
        else if (!is_unitplaced || !is_tensplaced || !is_numbcardplaced)
        {
            DoneBtn.gameObject.SetActive(false);
            DoneBtn.raycastTarget = false;
        }
    }

    public void DoneBtn_Clicked()
    {
        DoneBtn_State(false,false,false);
                
        if ((TenBoxCount_i == 1) && (UnitBoxCount_i == (QuesNumb_i - 10)) && (QuesNumb_i == Draged_ImgVal_i)) //for getting the value of last digit
        { 
            NumberWord_Img.enabled = true;
            // NumberWord_Img.sprite = NumbersWords_Sprte[QuesCount_i - 1];
            // VoiceOver_AS.clip = NumbersVoices_AC[QuesCount_i - 1];
            VoiceOver_AS.clip = NumbersVoices_AC[(QuesNumb_i - 11)];
            VoiceOver_AS.PlayDelayed(0f);

            //DragUnits_Level1.Instance.InitPos_Set();

            StartCoroutine(After_CorrectAnsEvalution());
            //Debug.Log(":::::: correct answer :::::: ");
        }
        else
        {
            StartCoroutine(After_WrongAnsEvalution());
            //Debug.Log(":::::: wrong answer :::::: ");
        }        
    }

    private int AnsEval_i;
    private int WrongAnsCount_i;

    IEnumerator After_CorrectAnsEvalution()
    {
        VoiceOver_AS.clip = CorrectAnsSond_AC;
        VoiceOver_AS.Play();

        yield return new WaitForSeconds(0.5f);

        AnsEval_i = 1;

        Level_3_Manager.Instance.Update_ProgresBarValuee(0.11f);

        if (WrongAnsCount_i <= 0) //if correct in first attempt
        {
            //Level_3_Manager.Instance.Update_ProgresBarValuee();

            if ((QuesNumb_i == 17))
            {
                Is_LastbutSecQues_FirstAttempt = true;
            }
            if ((QuesNumb_i == 16))
            {
                Is_LastbutOneQues_FirstAttempt = true;
            }
            if ((QuesNumb_i == 19))
            {
                Is_LastQues_FirstAttempt = true;
            }
        }
        //Debug.Log("Is_LastQues_FirstAttempt :::: " + Is_LastQues_FirstAttempt + "====Is_LastbutOneQues_FirstAttempt :::: " + Is_LastbutOneQues_FirstAttempt + "====Is_LastbutSecQues_FirstAttempt ::::" + Is_LastbutSecQues_FirstAttempt);

        yield return new WaitForSeconds(0.5f);
        NumberWord_Img.enabled = true;
        VoiceOver_AS.clip = NumbersVoices_AC[QuesNumb_i - 11];
        VoiceOver_AS.Play();
        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.spring));
        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.spring));

        yield return new WaitForSeconds(3f);
        if (QuesCount_i <= 9)
        {
            //Debug.Log("QuesCount_i ::::::::::::::::::::::::" + QuesCount_i);

            if (!Is_LastQues_FirstAttempt || !Is_LastbutOneQues_FirstAttempt || !Is_LastbutSecQues_FirstAttempt)
            {
                GenerateNumbsWords();
            }
        }

        if (Is_LastQues_FirstAttempt && Is_LastbutOneQues_FirstAttempt && Is_LastbutSecQues_FirstAttempt)
        {
            Level_3_Manager.Instance.Check_GameState = Level_3_Manager.GameStates.GameEnd;
            Level_3_Manager.Instance.Levels_BarFill[2].fillAmount = 1f;
            PlayerPrefs.SetFloat(StaticVariables.Level3_BarVal, 1f);
            PlayerPrefs.Save();
            yield return new WaitForSeconds(0.5f);
            PapersEffect.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(ShowScoreCardEffect()); //6-3
            Level_3_Manager.Instance.Invoke("EndGame_PopUp_Show",4f);//7-3                        
        }
        
        StartCoroutine(ReArrangingPosofAllBoxes());

        /*CoinsScored_i = CoinsScored_i + Level_3_Manager.Instance.CoinsToAddPerQues;
        PlayerPrefs.SetInt(StaticVariables.Level3_Score, CoinsScored_i);
        PlayerPrefs.SetInt(StaticVariables.Coins_TotalScore, PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore) + PlayerPrefs.GetInt(StaticVariables.Level3_Score));*/

        // DragUnits_Level1.Instance.InitPos_Set();
        // DragTens_Level2.Instance.InitPos_Set();
    }
    IEnumerator After_WrongAnsEvalution()
    {
        VoiceOver_AS.clip = IncorrectAnsSond_AC;
        VoiceOver_AS.Play();

        yield return new WaitForSeconds(0f);
        WrongAnsCount_i = WrongAnsCount_i + 1;

        AnsEval_i = 0;
       
        yield return new WaitForSeconds(0.3f);
        VoiceOver_AS.clip = TryAgain_AC;
        VoiceOver_AS.Play();        
        yield return new WaitForSeconds(0.5f);       
        StartCoroutine(ReArrangingPosOfOnlyWrongAnsBoxes());
                
        yield return new WaitForSeconds(1f);
        RepeatQuestion_VO(0.3f);
        //Debug.Log("Voice call:::::::::TRY AGAIN::::::::::");

        if (WrongAnsCount_i >= 3)
        {
            //WrongAnsCount_i = 0;
            //Debug.Log("hint shown");
            _HintHighlight();
        }
        /////////////////////////////////////
        yield return new WaitForSeconds(2f);        
        if ((TenBoxCount_i == 1) || (UnitBoxCount_i == (QuesNumb_i - 10)) || (QuesNumb_i == Draged_ImgVal_i)) //for getting the value of last digit
        {
            DoneBtn_State(true, true, true); //on done btn after wrong answer
        }
        
    }
    IEnumerator ReArrangingPosOfOnlyWrongAnsBoxes()
    {
        yield return new WaitForSeconds(0f);

        NumberWord_Img.enabled = false;

        /*if ((TenBoxCount_i > 0))
        {
            for (int i = 0; i < TensBoxs_Arr.Length; i++)
            {
                TensBoxs_Arr[i].transform.parent = DragBoxSet.transform;
                TensBoxs_Arr[i].transform.localPosition = TensBoxs_Arr_Pos[i];

                TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
            }
        }*/
        if (((UnitBoxCount_i > QuesNumb_i - 10) || (UnitBoxCount_i < QuesNumb_i - 10)) || (TenBoxCount_i > 1))
        {
            Is_UnitBoxsCardsPlaced = false;
            Is_TenBoxsCardPlaced = false;

            Is_UnitBoxsCardsPlaced_Hint = false;
            Is_TenBoxsCardPlaced_Hint = false;

            for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
            {
                UnitsBoxs_Arr[i].transform.parent = DragBoxSet.transform;
                UnitsBoxs_Arr[i].transform.localPosition = UnitsBoxs_Arr_Pos[i];

                UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
            }
            ///////////////////////////
            for (int i = 0; i < TensBoxs_Arr.Length; i++)
            {
                TensBoxs_Arr[i].transform.parent = DragBoxSet.transform;
                TensBoxs_Arr[i].transform.localPosition = TensBoxs_Arr_Pos[i];

                TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
            }

        }
        if(QuesNumb_i != Draged_ImgVal_i)
        {
            Is_NumbBoxsCardPlaced = false;
            Is_NumbBoxsCardsPlaced_Hint = false;

            for (int i = 0; i < NumbersImg_Arr.Length; i++) //Get Positions
            {
                NumbersImg_Arr[i].transform.parent = DragBoxSet.transform;
                NumbersImg_Arr[i].transform.localPosition = NumbersBoxs_Arr_Pos[i];

                NumbersImg_Arr[i].GetComponent<Image>().raycastTarget = true;
            }
        }        

    }
    private int myquesno;
    void _HintHighlight()
    { 
        /*for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
        {
            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
        }
        /////////////////////////////////////////////////////////////////
        for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get 
        {
            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
        }
        TensBoxs_Arr[6].GetComponent<Image>().raycastTarget = true;
        ////////////////////////////////////////////////////////////////
        for (int i = 0; i < NumbersImg_Arr.Length; i++) //Get 
        {
            NumbersImg_Arr[i].GetComponent<Image>().raycastTarget = false;
        }
        NumbersImg_Arr[QuesNumb_i - 11].GetComponent<Image>().raycastTarget = true;*/

        ///////////////////////////////
        myquesno = QuesNumb_i - 10;
        ///////////////////////////////
        ShowHighliteOutlne_OnTensCad();
        ShowHighliteOutlne_OnSingleHintBox();
        //ShowHighliteOutlne_OnNumbImgCad();
    }
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
    ////////////    
    void ShowHighliteOutlne_OnTensCad()
    {
        if (Is_TenBoxsCardPlaced_Hint)
            return;

        TensHintBoxsPar[0].SetActive(true);

        Invoke("ShowHighliteOutlne_OffTensCad", 0.35f);
    }
    void ShowHighliteOutlne_OffTensCad()
    {
        if (Is_TenBoxsCardPlaced_Hint)
            return;

        TensHintBoxsPar[0].SetActive(false);

        Invoke("ShowHighliteOutlne_OnTensCad", 0.35f);
    }
    ////////////    
    void ShowHighliteOutlne_OnNumbImgCad()
    {
        if (Is_NumbBoxsCardsPlaced_Hint)
            return;


        NumbersHintImgsPar.SetActive(true);

        Invoke("ShowHighliteOutlne_OffNumbImgCad", 0.35f);
    }
    void ShowHighliteOutlne_OffNumbImgCad()
    {
        if (Is_NumbBoxsCardsPlaced_Hint)
            return;


        NumbersHintImgsPar.SetActive(false);

        Invoke("ShowHighliteOutlne_OnNumbImgCad", 0.35f);
    }
    //////////////////////////////////////////////////   

    IEnumerator ReArrangingPosofAllBoxes()
    {
        yield return new WaitForSeconds(0f);
                
        NumberWord_Img.enabled = false;

        for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
        {
            UnitsBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            UnitsBoxs_Arr[i].transform.localPosition = UnitsBoxs_Arr_Pos[i];

            //UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }
        for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
        {
            TensBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            TensBoxs_Arr[i].transform.localPosition = TensBoxs_Arr_Pos[i];

            //TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }
        for (int i = 0; i < NumbersImg_Arr.Length; i++) //Get Positions
        {
            NumbersImg_Arr[i].transform.parent = DragBoxSet.transform;
            NumbersImg_Arr[i].transform.localPosition = NumbersBoxs_Arr_Pos[i];

            //NumbersImg_Arr[i].GetComponent<Image>().raycastTarget = true;
        }       
    }
    public void ResetAllPositions()
    {
        NumberWord_Img.enabled = false;

        for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
        {
            UnitsBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            UnitsBoxs_Arr[i].transform.localPosition = UnitsBoxs_Arr_Pos[i];
        }
        for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
        {
            TensBoxs_Arr[i].transform.parent = DragBoxSet.transform;
            TensBoxs_Arr[i].transform.localPosition = TensBoxs_Arr_Pos[i];
        }
        for (int i = 0; i < NumbersImg_Arr.Length; i++) //Get Positions
        {
            NumbersImg_Arr[i].transform.parent = DragBoxSet.transform;
            NumbersImg_Arr[i].transform.localPosition = NumbersBoxs_Arr_Pos[i];
        }
    }
    ///////////////////////////////////////////////
    IEnumerator ShowScoreCardEffect()
    {        
        yield return new WaitForSeconds(0f);
        VoiceOver_AS.clip = RewardCollctSond_AC;
        VoiceOver_AS.Play();
        iTween.ScaleTo(ScoreCardMove.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 1f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.spring));
        yield return new WaitForSeconds(2f);
        iTween.ScaleTo(ScoreCardMove.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "time", 0.75f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(ScoreCardMove.gameObject, iTween.Hash("x", 450f, "y", 250f, "time", 0.75f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        yield return new WaitForSeconds(1f);
        CoinsChange_UpdateValue();
    }
    /////////////////////// Update Coins //////////////////////////////   
    private int CoinsTotalInGame_i;
    private int CoinsTempPrev_i;
    private int CoinsTempPrev_s;
    public void CoinsChange_UpdateValue()
    {
        CoinsScored_i = Level_3_Manager.Instance.CoinsToAddPerLvl;
        PlayerPrefs.SetInt(StaticVariables.Coins_TotalScore, PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore) + CoinsScored_i);
        PlayerPrefs.Save();
        //CoinsTempPrev_s =  0;//in Default_State()
        CoinsTempPrev_i = Level_3_Manager.Instance.CoinsAtInitialLvl;
        CoinsTotalInGame_i = PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);
        CoinsScoreEndShow_Text.text = CoinsScored_i.ToString();
        ////Debug.Log("CoinsTempPrev_i+++++++++++++++++++::" + CoinsTempPrev_i);
        iTween.ValueTo(Level_3_Manager.Instance.CoinsShow_Text.gameObject, iTween.Hash("from", CoinsTempPrev_i, "to", CoinsTotalInGame_i, "time", 0.5f, "delay", 0.35f, "onupdatetarget", gameObject, "onupdate", "UpdateCoinsTextValue", "easetype", iTween.EaseType.linear));
    }
    void UpdateCoinsTextValue(int CoinsTotalInGame_i)
    {
        Level_3_Manager.Instance.CoinsShow_Text.text = "" + CoinsTotalInGame_i.ToString();
    }
    ///////////////////////////////////////////////     
    public void RepeatQuestion_VO(float delayy)
    {
        if (Level_3_Manager.Instance.Check_GameState != Level_3_Manager.GameStates.InGame)
            return;

        for (int i = 0; i < NumbersImg_Arr.Length; i++)
        {
            NumbersImg_Arr[i].GetComponent<Image>().raycastTarget = false;
            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
        }

        VoiceOver_AS.clip = QuestionVoice_AC;
        VoiceOver_AS.PlayDelayed(delayy);
        
        SoundBtn_Img.raycastTarget = false;
        StartCoroutine(PlayTheNumberVoice_Sond());
    }
    IEnumerator PlayTheNumberVoice_Sond()
    {
        if (Level_3_Manager.Instance.Check_GameState == Level_3_Manager.GameStates.InGame)
        {
            yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.2f);
            VoiceOver_AS.clip = NumbersVoices_AC[QuesNumb_i - 11];// to play the ques number       
            VoiceOver_AS.Play();

            StartCoroutine(EnabllingTHeBtn_Sond());
        }
    }
    IEnumerator EnabllingTHeBtn_Sond()
    {
        yield return new WaitForSeconds(0.5f);
        SoundBtn_Img.raycastTarget = true;

        for (int i = 0; i < NumbersImg_Arr.Length; i++)
        {
            NumbersImg_Arr[i].GetComponent<Image>().raycastTarget = true;
            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = true;
        }
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

}
