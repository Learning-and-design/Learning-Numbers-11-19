using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_1_Tutorial : MonoBehaviour
{
    #region Getter
    private static Level_1_Tutorial _instance;
    public static Level_1_Tutorial Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_1_Tutorial>();
            }
            return _instance;
        }
    }
    #endregion


    [Header("ForTutorial")]
    public GameObject GirlChar_Set;
    public Image GirlChar_Img;
    public Sprite[] GirlChar_Sprites;
    public Image Speechbubble_Img;
    public GameObject HandPic;
    public Image BG_CharIntro_Img;
    public GameObject DragBoxSet;
    public GameObject[] UnitsBoxs_Arr;
    public GameObject[] TensBoxs_Arr;
    public GameObject[] TargetObjs;
    public GameObject[] SlotsWhiteImgs;
    public GameObject TrainObj;
    [Header("Imges")]
    public Image NumbersImg;
    public Image NumberWord_Img;
    public Image DoneBtn;
    public Image SoundBtn_Img;
    [Header("Bools")]
    public static bool Is_FromHintBtn;
    public static bool Is_HintShown;   
    [Header("Vectors")]
    public Vector3 SingleBoxCard_Tut_Pos;
    public Vector3 TenBoxsCard_Tut_Pos;   
    public Vector3[] UnitsBoxs_Arr_Pos;
    public Vector3[] TensBoxs_Arr_Pos;    
    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;
    public AudioSource PopUpsVO_AS;
    [Header("AudioClips")]
    public AudioClip[] Intros_AC;
    public AudioClip ThisEleven_AC;

    void OnEnable()
    {
        StartCoroutine(TutorealBegin());
    }

    void Start()
    {
        //StartCoroutine(TutorealBegin());        
    }
   
    // Update is called once per frame
    void Update()
    {

    }

    #region TrainAnim_InOut
    void TrainIn_Anim()
    {
        //TrainObj.transform.localPosition = new Vector3(1000f, -80f, 0f);
        if(TrainObj.transform.localPosition.x != 700f)
        iTween.MoveTo(TrainObj.gameObject, iTween.Hash("x", 700f, "time", 2f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
    }
    void TrainOut_Anim()
    {
        iTween.MoveTo(TrainObj.gameObject, iTween.Hash("x", -1050f, "time", 2f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
    }
    #endregion TrainAnim_InOut    

    IEnumerator TutorealBegin()
    {
        TrainObj.transform.localPosition = new Vector3(1050f, -80f, 0f);

        for (int i = 0; i < SlotsWhiteImgs.Length; i++)
        {
            SlotsWhiteImgs[i].SetActive(false);
        }

        StaticVariables.Is_Tutorial = true;
                
        SoundBtn_Img.enabled = false;

        UnitsBoxs_Arr_Pos = new Vector3[9];
        TensBoxs_Arr_Pos = new Vector3[9];
        for (int i = 0; i < UnitsBoxs_Arr.Length; i++) //Get Positions
        {
            UnitsBoxs_Arr_Pos[i] = UnitsBoxs_Arr[i].transform.localPosition;
        }
        for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
        {
            TensBoxs_Arr_Pos[i] = TensBoxs_Arr[i].transform.localPosition;
        }

        for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
        {
            UnitsBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
        }
        for (int i = 0; i < TensBoxs_Arr.Length; i++) //Get Positions
        {
            TensBoxs_Arr[i].GetComponent<Image>().raycastTarget = false;
        }

        GirlChar_Set.transform.localPosition = new Vector3(500f,-180f,0f);
        GirlChar_Set.transform.localScale = new Vector3(1f, 1f, 1f);
        NumberWord_Img.enabled =false;
        HandPic.transform.localPosition = new Vector3(0f,0f, 0f);

       
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", 0f, "time", 0.5f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.35f, "delay", 0.8f, "islocal", true, "easetype", iTween.EaseType.linear));

        HandPic.GetComponent<Image>().enabled = false;

        GirlChar_Img.enabled = true;
        GirlChar_Img.sprite = GirlChar_Sprites[0];

        yield return new WaitForSeconds(1f);
        VoiceOver_AS.clip = Intros_AC[0];
        VoiceOver_AS.Play();

        Invoke("TrainIn_Anim", 5f);      

        yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
        iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        VoiceOver_AS.clip = Intros_AC[1];
        VoiceOver_AS.Play();
        BG_CharIntro_Img.gameObject.SetActive(false);
        GirlChar_Img.sprite = GirlChar_Sprites[1];
        GirlChar_Img.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
               
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", -400f, "y", -205f, "time", 0.5f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(GirlChar_Set.gameObject, iTween.Hash("x", 0.8f, "y", 0.8f, "time", 0.5f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < UnitsBoxs_Arr.Length; i++)
        {
            iTween.ScaleTo(UnitsBoxs_Arr[i].gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "z", 1.1f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(UnitsBoxs_Arr[i].gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

            iTween.ScaleTo(TensBoxs_Arr[i].gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "z", 1.1f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(TensBoxs_Arr[i].gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        }
        yield return new WaitForSeconds(1.5f);
        iTween.ScaleTo(NumbersImg.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "z", 1.2f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(NumbersImg.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

        yield return new WaitForSeconds(2f);
        VoiceOver_AS.clip = Intros_AC[2];
        VoiceOver_AS.Play();
       
        yield return new WaitForSeconds(4f);       
        iTween.ScaleTo(UnitsBoxs_Arr[8].gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "z", 1.2f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));
        iTween.ScaleTo(UnitsBoxs_Arr[8].gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 0.35f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));
       
        iTween.ScaleTo(TensBoxs_Arr[6].gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "z", 1.2f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));
        iTween.ScaleTo(TensBoxs_Arr[6].gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 0.35f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));

        yield return new WaitForSeconds(2f);
        for(int i=0; i < SlotsWhiteImgs.Length; i++)
        {
            SlotsWhiteImgs[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < SlotsWhiteImgs.Length; i++)
        {
            SlotsWhiteImgs[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < SlotsWhiteImgs.Length; i++)
        {
            SlotsWhiteImgs[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < SlotsWhiteImgs.Length; i++)
        {
            SlotsWhiteImgs[i].SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        iTween.ScaleTo(DoneBtn.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(DoneBtn.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        
        yield return new WaitForSeconds(2f);
        VoiceOver_AS.clip = Intros_AC[3];
        VoiceOver_AS.Play();

        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", -700f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        
        iTween.ScaleTo(NumbersImg.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(NumbersImg.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));


        yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.75f);
        VoiceOver_AS.clip = Intros_AC[4];
        VoiceOver_AS.Play();

        StartCoroutine(TensBoxCardDragShow());
    }
    IEnumerator TensBoxCardDragShow()
    {
        yield return new WaitForSeconds(0.5f);
        HandPic.GetComponent<Image>().enabled = true;
        HandPic.transform.localPosition = new Vector3(93f, -5f,0f);       
        HandPic.transform.localEulerAngles = new Vector3(0f, 0f, 50f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 93f, "y", -5f, "time", 0.5f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(1f);
        //iTween.MoveTo(TensBoxs_Arr[6].gameObject, iTween.Hash("x", 86.9f, "y", -296.2f, "time", 0.5f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(TensBoxs_Arr[6].gameObject, iTween.Hash("x", -86f, "y", -327.6f, "time", 0.5f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", -132f, "y", -296f, "time", 0.5f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(0.8f);
        StartCoroutine(SingleBoxDragShow());
    }   
    //////////////////////////////////////////////////
    IEnumerator SingleBoxDragShow()
    {
        yield return new WaitForSeconds(0.2f);
        HandPic.GetComponent<Image>().enabled = true;
        HandPic.transform.localPosition = new Vector3(175f, -10f, 0f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 165f, "y", 38, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(1f);
        iTween.MoveTo(UnitsBoxs_Arr[8].gameObject, iTween.Hash("x", 290.7f, "y", -329.3f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 264f, "y", -296f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(2f);        
        StartCoroutine(TouchDoneButtonShow());
    }
    IEnumerator TouchDoneButtonShow()
    {
        yield return new WaitForSeconds(0f);

        VoiceOver_AS.clip = Intros_AC[5];
        VoiceOver_AS.Play();

        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 402f, "y", -50, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.RotateTo(HandPic.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.ScaleTo(DoneBtn.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(DoneBtn.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
                
        yield return new WaitForSeconds(2f);
        HandPic.GetComponent<Image>().enabled = false;

        yield return new WaitForSeconds(1f);
        NumberWord_Img.enabled = true;//for 11   
        yield return new WaitForSeconds(0.5f);
        VoiceOver_AS.clip = ThisEleven_AC;
        VoiceOver_AS.Play();

        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.35f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.55f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        
        yield return new WaitForSeconds(3f);
        VoiceOver_AS.clip = Intros_AC[6];
        VoiceOver_AS.Play();
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", -400f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        StartCoroutine(ForSoundBtnIntroShow());
    }
    IEnumerator ForSoundBtnIntroShow()
    {
        yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", -700f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        yield return new WaitForSeconds(0.75f);
        SoundBtn_Img.enabled = true;
        Invoke("TrainOut_Anim", 5f);

        VoiceOver_AS.clip = Intros_AC[7];
        VoiceOver_AS.Play();

        yield return new WaitForSeconds(0.2f);
        HandPic.GetComponent<Image>().enabled = true;
        HandPic.transform.localPosition = new Vector3(460f, 0f, 0f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 465f, "y", 34f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.RotateTo(HandPic.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", -8f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.ScaleTo(SoundBtn_Img.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.3f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(SoundBtn_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.38f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

        yield return new WaitForSeconds(6f);
        HandPic.GetComponent<Image>().enabled = false;

        StartCoroutine(StarttheGame());
    }
    //////////////////////////////////////////////////     
    IEnumerator StarttheGame()
    {        
        yield return new WaitForSeconds(1f);
           
        if (Is_FromHintBtn)
        {
            Is_HintShown = true;
           // SkipHintBtn_Img.enabled = false;
        }   
        
        StaticVariables.Is_Tutorial = false;
        Level_1_Manager.Instance.AfterTut_PopUp_Show();//9-3
        StartCoroutine(ReArrangingPosofAllBoxes());

    }
    IEnumerator ReArrangingPosofAllBoxes()
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
    }
}
