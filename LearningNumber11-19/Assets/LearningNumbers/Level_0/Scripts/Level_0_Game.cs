using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_0_Game : MonoBehaviour
{
    #region Getter
    private static Level_0_Game _instance;
    public static Level_0_Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_0_Game>();
            }
            return _instance;
        }
    }
    #endregion

    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;   
    [Header("AudioClips")]
    public AudioClip[] Intros_AC;
    public AudioClip[] NumbersVoices_AC;
    public AudioClip QuestionVoiceAfterTut_AC;
    public AudioClip PopupAtHomeClickedVoice_AC;
    public AudioClip PopupAtEndGameVoice_AC;
    public AudioClip PopupAtBackClickedVoice_AC;
    public AudioClip PopupAtSkipFrmLvl2ClikedVoice_AC;
    public AudioClip PopupAtSkipFrmLvl1ClikedVoice_AC;
    public AudioClip PopupAtSkipTutSameLvlVoice_AC;
    [Header("Ints")]
    public int QuesCount_i;
    private int answeredNumb;   
    [Header("Images")]
    public Image DragImg_Obj;
    public Image HighlightImg;    
    [Header("WordsNUmbersImages")]
    public Image NumberWord_Img;
    public Sprite[] NumbersWords_Sprte;
    [Header("DragNumbersImages")]
    public Image DragNumberImg;
    public Sprite[] DragNumbers_Sprte;
    [Header("GameObjects")]
    public GameObject[] NumbersSet;
    public GameObject TenBoxsSet;
    public GameObject UnitsBoxsSet_par;
    public GameObject[] UnitsBoxsSet;
    public GameObject UnitsBoxsDummyPlaces_par;
    public GameObject[] TargetObjs;
    public GameObject[] FinalNumbersContainBoxes;
    public GameObject SecModeGraphics;
    public GameObject PaperEffect_Show;
    [Header("Bools")]
    public bool Is_SingleCardPlaced;
    public bool Is_TenBoxsCardPlaced;
    public bool Is_UnitBoxsCardsPlaced;
    public bool Is_SecModeStartd;
    [Header("Vector3")]
    private Vector3 DragImg_Initial_Pos;
    private Vector3 TenBoxsDrag_Initial_Pos;

    void OnEnable()
    {
        Time.timeScale = 1;

        if(DragImg_Obj.GetComponent<iTween>())
        {
            Destroy(DragImg_Obj.GetComponent<iTween>());
        }

        DragImg_Initial_Pos = DragImg_Obj.transform.localPosition;
        TenBoxsDrag_Initial_Pos = TenBoxsSet.transform.localPosition;
        //Debug.Log("DragImg_Initial_Pos::" + DragImg_Initial_Pos);
   
        Is_SingleCardPlaced = false;
        Is_TenBoxsCardPlaced = false;
        Is_UnitBoxsCardsPlaced = false;
        Is_SecModeStartd = false;

        Default_State();

        ///////////////////////////////
        /*Skip_Btn_Tut_SecTime.enabled = false;
        SkipFrmLvl1Btn_Img.enabled = false;

        if (StaticVariables.Is_FromLvl1_Back)
        {
            SkipFrmLvl1Btn_Img.enabled = true;
            Is_Tutorial = false;
        }
       */
        ///////////////////////////////

        if (Level_0_Manager.Is_PlayAgain_Game_SameLvl)//25-04
        {
            QuesCount_i = 1;
            //Level_0_Manager.Is_PlayAgain_Game_SameLvl = false;
        }
        if (PlayerPrefs.GetInt(StaticVariables.Is_CompletedLvl0) == 1)///once level completed show skip btn
        {
            Level_0_Manager.Instance.SkipBtn_AftPlayAgainClicked_Img.enabled = true;
        }

        GenerateNumbsWords();        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            Count_MethodExecute_i_Display = 0;
        }

        if (StaticVariables.Is_RepeatSound)
        {
            RepeatQuestion_VO(0f);
            //Debug.Log("repeated the voiceover");
            StaticVariables.Is_RepeatSound = false;
        }
        
    }

    void Default_State()
    {
        for (int i = 0; i < NumbersSet.Length; i++)
        {
            NumbersSet[i].SetActive(false);
        }

        SecModeGraphics.gameObject.SetActive(false);
                
        PaperEffect_Show.gameObject.SetActive(false);
        
        for (int i = 0; i < TargetObjs.Length; i++)
        {
            TargetObjs[i].SetActive(true);
        } 
    }

    public float Up_Val;
    void GenerateNumbsWords()
    {
        Is_SingleCardPlaced = false;
        Is_TenBoxsCardPlaced = false;
        Is_UnitBoxsCardsPlaced = false;

        if (QuesCount_i > 9)
            return;

        //BlackBg_Img.CrossFadeAlpha(0f, 0.1f, true);

        DragImg_Obj.transform.localPosition = DragImg_Initial_Pos;
        TenBoxsSet.transform.localPosition = TenBoxsDrag_Initial_Pos;
        //Debug.Log("DragImg_Initial_Pos::"+ DragImg_Initial_Pos);
        NumberWord_Img.sprite = null;
        DragNumberImg.sprite = DragNumbers_Sprte[QuesCount_i - 1];
        DragImg_Obj.gameObject.SetActive(true);       
        DragImg_Obj.GetComponent<SingleCardNumberDrag>().enabled = true;
        DragImg_Obj.transform.localScale = new Vector3(0f,0f,0f);
        iTween.ScaleTo(DragImg_Obj.gameObject, iTween.Hash("x", 1f, "y",1f, "z", 1f, "time", 0.35f, "delay", 0f, "islocal", true, "easeType", iTween.EaseType.spring));

        TenBoxsSet.gameObject.SetActive(true);
        UnitsBoxsSet_par.gameObject.SetActive(true);
        UnitsBoxsDummyPlaces_par.gameObject.SetActive(true);

        Up_Val = 4f;
        for (int i = 0; i < UnitsBoxsSet.Length; i++)
        {
            UnitsBoxsSet[i].SetActive(false);

            UnitsBoxsSet[i].gameObject.transform.parent = UnitsBoxsSet_par.transform;

            UnitsBoxsDummyPlaces_par.transform.GetChild(i).gameObject.SetActive(false);

            if (i <= QuesCount_i - 1)
            {
                UnitsBoxsSet[i].SetActive(true);

                UnitsBoxsDummyPlaces_par.transform.GetChild(i).gameObject.SetActive(true);
            }

            UnitsBoxsSet[i].gameObject.transform.localPosition = new Vector3(Up_Val, 10f, 0);
            Up_Val = Up_Val + 28f;
        }

        //ShowRaycstStatusOfDragObjs("OnAllRays_SingleTensUnitsCards");

        RepeatQuestion_VO(0f);
    }

    private float y_val = 190f;
    private float delayy = 0f;
    public void SingleCardDraged_AnswersEvaluation()
    {
        //Debug.Log("QuesCount_i::::"+QuesCount_i);
        ShowRaycstStatusOfDragObjs("OffRay_SingleCard");
        DragImg_Obj.GetComponent<SingleCardNumberDrag>().enabled = false;

        Is_SingleCardPlaced = true;

        CheckStatus();
        //StartCoroutine(ShowNumbersSetMov());
    }

    IEnumerator ShowNumbersSetMov()
    {
        if (Level_0_Manager.Instance.Check_GameState == Level_0_Manager.GameStates.InGame)
        {

            //Debug.Log("its in the ShowNumbersSetMov");
            VoiceOver_AS.clip = NumbersVoices_AC[QuesCount_i - 1];
            VoiceOver_AS.PlayDelayed(0.5f);
            yield return new WaitForSeconds(0.5f);
            NumberWord_Img.sprite = NumbersWords_Sprte[QuesCount_i - 1];
            y_val = y_val - 55f;
            yield return new WaitForSeconds(2.5f);
            NumbersSet[QuesCount_i - 1].gameObject.SetActive(true);
            iTween.MoveTo(NumbersSet[QuesCount_i - 1].gameObject, iTween.Hash("x", -400f, "y", y_val, "time", 0.3f, "delay", 0f, "islocal", true, "easeType", iTween.EaseType.linear));
            iTween.ScaleTo(NumbersSet[QuesCount_i - 1].gameObject, iTween.Hash("x", 0.36f, "y", 0.36f, "z", 0.36f, "time", 0.3f, "delay", 0f, "islocal", true, "easeType", iTween.EaseType.linear));
            DragImg_Obj.gameObject.SetActive(false);
            TenBoxsSet.gameObject.SetActive(false);
            UnitsBoxsSet_par.gameObject.SetActive(false);
            UnitsBoxsDummyPlaces_par.gameObject.SetActive(false);
            for (int i = 0; i < UnitsBoxsSet.Length; i++)
            {
                UnitsBoxsSet[i].SetActive(false);
            }

            //yield return new WaitForSeconds(0.3f);
            //BlackBg_Img.CrossFadeAlpha(0.8f, 0.1f, true);
            NumberWord_Img.sprite = null;

            yield return new WaitForSeconds(0.5f);
            if (QuesCount_i >= 9)
            {
                SecMode_State();
            }
            else
            {
                QuesCount_i = QuesCount_i + 1;
                GenerateNumbsWords();
            }

            if (UnitsBoxsDrag.Instance)
            {
                UnitsBoxsDrag.Instance.SetValues();//childcount to 0;
                childcountofUnitsPar = 0;
                //Debug.LogError("::::::::::::::childcountofUnitsPar::::::::::::::" + childcountofUnitsPar);
            }
        }
    }
    public void UnitBoxCard_Placed()
    {
        childcountofUnitsPar = UnitsBoxsDrag.childcount;

        //Debug.Log(":::::childcountofUnitsPar:::::::" + childcountofUnitsPar + ":::::QuesCount_i+++++++" + QuesCount_i);

        if (childcountofUnitsPar == QuesCount_i)
        {
            Is_UnitBoxsCardsPlaced = true;
            //Debug.Log("yes.......they are equal+++++" + ":::::childcountofUnitsPar:::::::" + childcountofUnitsPar + ":::::QuesCount_i+++++++" + QuesCount_i);
            childcountofUnitsPar = 0;
            //Debug.LogError("childcountofUnitsPar:::" + childcountofUnitsPar);
        }

        if (Is_UnitBoxsCardsPlaced)
        {
            UnitsBoxsDrag.childcount = 0;
            CheckStatus();
        }
    }

    public void TensBoxsCard_Placed()
    {
        Is_TenBoxsCardPlaced = true;
        ShowRaycstStatusOfDragObjs("OffRay_TensBoxsCard");

        if (Is_TenBoxsCardPlaced)
        {
            CheckStatus();
        }
    }

    private int childcountofUnitsPar = 0;
    public void CheckStatus()
    {

        //Debug.Log("Is_SingleCardPlaced:: " + Is_SingleCardPlaced + "|| Is_TenBoxsCardPlaced::" + Is_TenBoxsCardPlaced + "|| Is_UnitBoxsCardsPlaced::" + Is_UnitBoxsCardsPlaced);
        if (Is_SingleCardPlaced && Is_TenBoxsCardPlaced && Is_UnitBoxsCardsPlaced)
        {            
            StartCoroutine(ShowNumbersSetMov());            
        }
    }
    void SecMode_State()
    {
        Count_MethodExecute_i_Display = 0;
        Is_SecModeStartd = true;

        DragImg_Obj.gameObject.SetActive(false);
        NumberWord_Img.gameObject.SetActive(false);
        DragNumberImg.gameObject.SetActive(false);
        SecModeGraphics.gameObject.SetActive(true);
        TenBoxsSet.gameObject.SetActive(false);
        UnitsBoxsSet_par.gameObject.SetActive(false);
        UnitsBoxsDummyPlaces_par.gameObject.SetActive(false);

        for (int i = 0; i < TargetObjs.Length; i++)
        {
            TargetObjs[i].SetActive(false);
        }

        Invoke("DefaltBoxesMoveRightAnim", 0.2f);
    }
    ////////////////////////////////////Move Animations   
    private float delaye = 0;
    void DefaltBoxesMoveRightAnim()
    {
        for (int i = 0; i < NumbersSet.Length; i++)
        {
            iTween.MoveTo(NumbersSet[i].gameObject, iTween.Hash("x", 66f, "time", 0.3f, "delay", delaye, "islocal", true, "easeType", iTween.EaseType.linear));
            delaye = delaye + 0.1f;
        }
        Invoke("MakeAlphaOfObj", 1f);
    }
    void MakeAlphaOfObj()
    {
        for (int i = 0; i < NumbersSet.Length; i++)
        {
            NumbersSet[i].GetComponent<Image>().CrossFadeAlpha(0.5f, 0.2f, true);
            //FinalNumbersContainBoxes[i].GetComponentInChildren<Image>().CrossFadeAlpha(0.5f, 0.2f, true);           
        }
        for (int f = 0; f < FinalNumbersContainBoxes.Length; f++)
        {
            FinalNumbersContainBoxes[f].GetComponent<CanvasGroup>().alpha = 0.6f;

            // FinalNumbersContainBoxes[f].GetComponentInChildren<Image>().CrossFadeAlpha(0.5f, 0.2f, true);
            //Debug.Log("FinalNumbersContainBoxes" + f);
        }
        Invoke("VOwithShowcase", 0.5f);
    }
    private int repeatcont = 0;
    private float Y_ValToMove = 182f;
    void VOwithShowcase()
    {
        NumbersSet[repeatcont].GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
        iTween.MoveTo(HighlightImg.gameObject, iTween.Hash("y", Y_ValToMove, "time", 0.2f, "delay", 0.5f, "islocal", true, "easeType", iTween.EaseType.linear));

        FinalNumbersContainBoxes[repeatcont].GetComponent<CanvasGroup>().alpha = 1f;

        VoiceOver_AS.clip = NumbersVoices_AC[repeatcont];
        VoiceOver_AS.PlayDelayed(1f);

        if (repeatcont < 8)
        {
            Invoke("VOwithShowcase", 2f);
        }
        else if (repeatcont >= 8)
        {
            Level_0_Manager.Instance.StartCoroutine("EndGameUI_PopUpShow");
        }

        repeatcont = repeatcont + 1;
        Y_ValToMove = Y_ValToMove - 55f;
    }

    public void ShowRaycstStatusOfDragObjs(string MyState)
    {
        switch (MyState)
        {
            case "OnRay_SingleCard":
                DragImg_Obj.GetComponent<Image>().raycastTarget = true;
                break;
            case "OffRay_SingleCard":
                DragImg_Obj.GetComponent<Image>().raycastTarget = false;
                break;
            case "OnRay_TensBoxsCard":
                TenBoxsSet.GetComponent<Image>().raycastTarget = true;
                break;
            case "OffRay_TensBoxsCard":
                TenBoxsSet.GetComponent<Image>().raycastTarget = false;
                break;            
            case "OffAllRays_SingleTensUnitsCards":
                DragImg_Obj.GetComponent<Image>().raycastTarget = false;
                TenBoxsSet.GetComponent<Image>().raycastTarget = false;
                for (int u = 0; u < UnitsBoxsSet.Length; u++)
                {
                    UnitsBoxsSet[u].GetComponent<Image>().raycastTarget = false;
                }
                break;
            case "OnAllRays_SingleTensUnitsCards":
                DragImg_Obj.GetComponent<Image>().raycastTarget = true;
                TenBoxsSet.GetComponent<Image>().raycastTarget = true;
                for (int u = 0; u < UnitsBoxsSet.Length; u++)
                {
                    UnitsBoxsSet[u].GetComponent<Image>().raycastTarget = true;
                }
                break;
        }

    }    
    ///////////////////////////////////////////////

    [Header("Repeat_VO")]
    public int Count_MethodExecute_i_Display;
    public int CountDownTimer_15;

    void AtStart_TimeGapMethod()
    {
        Count_MethodExecute_i_Display = 0;

        InvokeRepeating("Check_ClickOnAnswer", 1f, 1f);
    }
    void Check_ClickOnAnswer()
    {
        if (Is_SecModeStartd)
            return;

        Count_MethodExecute_i_Display++;

        if (Count_MethodExecute_i_Display > CountDownTimer_15)
        {
            Count_MethodExecute_i_Display = 0;

            RepeatQuestion_VO(0f);
            //    Debug.Log("Check whether clicked after 10sec::repeat sound");
        }
    }
    public void RepeatQuestion_VO(float delayy)
    {       
        VoiceOver_AS.clip = Intros_AC[5];
        VoiceOver_AS.PlayDelayed(delayy);

        ShowRaycstStatusOfDragObjs("OffAllRays_SingleTensUnitsCards");

        //Debug.Log("OffAllRays_SingleTensUnitsCards........its in the enabling the btn sond");

        StartCoroutine(EnabllingTHeBtn_Sond());

        Invoke("EnabllingTHeBtn_Sond",3f);
        //Debug.Log("OffAllRays_SingleTensUnitsCards........its in the enabling the btn sond"+ delTame);
    }
    IEnumerator EnabllingTHeBtn_Sond()
    {     
        yield return new WaitForSeconds(VoiceOver_AS.clip.length);

        ShowRaycstStatusOfDragObjs("OnAllRays_SingleTensUnitsCards");

        //Debug.Log("its in the enabling the btn sond");
        
    }
   
}
