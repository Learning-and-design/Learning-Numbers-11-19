using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_3_Tutorial : MonoBehaviour
{
    #region Getter
    private static Level_3_Tutorial _instance;
    public static Level_3_Tutorial Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_3_Tutorial>();
            }
            return _instance;
        }
    }
    #endregion

    [Header("ForTutorial")]
    public GameObject DragBoxSet;
    public GameObject GirlChar_Set;
    public Image GirlChar_Img;
    public Sprite[] GirlChar_Sprites;
    public Image Speechbubble_Img;
    public GameObject HandPic;    
    public GameObject[] UnitsBoxsPar;
    public GameObject[] TensBoxsPar;
    public GameObject[] NumberImgsPar;
    [Header("====Images====")]    
    public GameObject TargetImg_NunberCard;
    [Header("NumbersImages")]
    public Image OneBoxQuant_Img;
    public Image TensBoxQuant_Img;
    public Image Number_Img;   
    public Image NumberWord_Img;
    public Image DoneBtn_Img;
    public Image SoundBtn_Img;
    [Header("Bools")]
    private bool Is_SingleBoxPlaced_Tut;
    private bool Is_TensBoxPlaced_Tut;
    private bool Is_NumbreImgBoxPlaced_Tut;
    [Header("Vectors")]
    public Vector3 SingleBoxCard_Tut_Pos;
    public Vector3 TensBoxCard_Tut_Pos;
    public Vector3 NumbrBoxCard_Tut_Pos;    
    public Vector3[] NumbersBoxs_Arr_Pos;
    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;
    [Header("AudioClips")]
    public AudioClip[] Intros_AC;
    public AudioClip Eleven_AC;

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

    IEnumerator TutorealBegin()
    {
        SoundBtn_Img.enabled = false;

        NumbersBoxs_Arr_Pos = new Vector3[9];
                
        for (int i = 0; i < NumberImgsPar.Length; i++) //Get Positions
        {
            NumbersBoxs_Arr_Pos[i] = NumberImgsPar[i].transform.localPosition;            
        }

        NumberWord_Img.enabled = false;
        for (int i = 0; i < UnitsBoxsPar.Length; i++)
        {
            UnitsBoxsPar[i].transform.localScale = new Vector3(0f, 0f, 0f);
            TensBoxsPar[i].transform.localScale = new Vector3(0f, 0f, 0f);
            NumberImgsPar[i].transform.localScale = new Vector3(0f, 0f, 0f);

        }
        /////////////////////////////////////////////////
        for (int all=0; all < UnitsBoxsPar.Length; all++)
        {
            UnitsBoxsPar[all].GetComponent<DragUnits_Level3>().enabled = false;
            TensBoxsPar[all].GetComponent<DragTens_Level3>().enabled = false;
            NumberImgsPar[all].GetComponent<DragNumbImg_Level3>().enabled = false;
        }

        SingleBoxCard_Tut_Pos = new Vector3(195f,10f,0f);
        TensBoxCard_Tut_Pos = new Vector3(206.6f,-83.9f,0f);
        NumbrBoxCard_Tut_Pos = new Vector3(-231f,-210f,0f);

        OneBoxQuant_Img.transform.localPosition = SingleBoxCard_Tut_Pos;
        TensBoxQuant_Img.transform.localPosition = TensBoxCard_Tut_Pos;
        Number_Img.transform.localPosition = NumbrBoxCard_Tut_Pos;

        StaticVariables.Is_Tutorial = true;

        Is_SingleBoxPlaced_Tut = false;
        Is_TensBoxPlaced_Tut = false;
        Is_NumbreImgBoxPlaced_Tut = false;

        DoneBtn_Img.raycastTarget = false;
        HandPic.GetComponent<Image>().enabled = false;

        Level_3_Manager.Instance.CoinsShow_Text.text = " " + PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);

        yield return new WaitForSeconds(0f);
        GirlChar_Set.transform.localPosition = new Vector3(65f, -180f, 0f);
        iTween.MoveFrom(GirlChar_Set.gameObject, iTween.Hash("x", 500f, "time", 0.5f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.35f, "delay", 0.8f, "islocal", true, "easetype", iTween.EaseType.linear));
               
        GirlChar_Img.enabled = true;
        GirlChar_Img.sprite = GirlChar_Sprites[0];

        yield return new WaitForSeconds(1f);
        VoiceOver_AS.clip = Intros_AC[0];
        VoiceOver_AS.Play();

        yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
        iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        VoiceOver_AS.clip = Intros_AC[1];
        VoiceOver_AS.Play();
        
        yield return new WaitForSeconds(1.5f);
        GirlChar_Img.sprite = GirlChar_Sprites[1];
        GirlChar_Img.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < UnitsBoxsPar.Length; i++)
        {
            UnitsBoxsPar[i].transform.localScale = new Vector3(1f, 1f, 1f);
            TensBoxsPar[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }
        for (int i=0; i < UnitsBoxsPar.Length; i++)
        {
            iTween.ScaleTo(UnitsBoxsPar[i].gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "z", 1.1f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(UnitsBoxsPar[i].gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

            iTween.ScaleTo(TensBoxsPar[i].gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "z", 1.2f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(TensBoxsPar[i].gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        }
       
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < NumberImgsPar.Length; i++)
        {            
            NumberImgsPar[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }
        for (int i = 0; i < NumberImgsPar.Length; i++)
        {
            iTween.ScaleTo(NumberImgsPar[i].gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "z", 1.1f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(NumberImgsPar[i].gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        }
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 102f, "y", -168f, "time", 0.35f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 342f, "y", -136f, "time", 0.35f, "delay", 1f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.RotateTo(HandPic.gameObject, iTween.Hash("z", 130f, "time", 0.35f, "delay", 1f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(0.8f);       
        GirlChar_Img.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
        NumberWord_Img.enabled = true;
        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
       
        yield return new WaitForSeconds(1f);
        VoiceOver_AS.clip = Intros_AC[2];
        VoiceOver_AS.Play();
        GirlChar_Img.sprite = GirlChar_Sprites[0];

        yield return new WaitForSeconds(4f);
        iTween.ScaleTo(OneBoxQuant_Img.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));
        iTween.ScaleTo(OneBoxQuant_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));
                
        iTween.ScaleTo(TensBoxQuant_Img.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));
        iTween.ScaleTo(TensBoxQuant_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));

        yield return new WaitForSeconds(1f);
        iTween.ScaleTo(Number_Img.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));
        iTween.ScaleTo(Number_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));

        yield return new WaitForSeconds(1.2f);
        iTween.ScaleTo(DoneBtn_Img.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.spring));
        iTween.ScaleTo(DoneBtn_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.spring));
        NumberWord_Img.enabled = false;
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", 700f, "time", 0.75f, "delay", 1f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(3f);
        VoiceOver_AS.clip = Eleven_AC;
        VoiceOver_AS.Play();

        StartCoroutine(QuantitiesDragShow());
    }

    IEnumerator QuantitiesDragShow()
    {
        yield return new WaitForSeconds(1.5f);
        VoiceOver_AS.clip = Intros_AC[3];
        VoiceOver_AS.Play();

        yield return new WaitForSeconds(0.5f);
        HandPic.GetComponent<Image>().enabled = true;
        HandPic.transform.localEulerAngles = new Vector3(0f, 0f, 55f);
        HandPic.transform.localPosition = new Vector3(148f, -26f, 0f);
        iTween.MoveTo(TensBoxQuant_Img.gameObject, iTween.Hash("x", 405f, "y", -318f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 347f, "y", -247f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
                
        yield return new WaitForSeconds(2f);
        VoiceOver_AS.clip = Intros_AC[4];
        VoiceOver_AS.Play();
        HandPic.transform.localPosition = new Vector3(96f, 69f, 0f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 430f, "y", -243f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(OneBoxQuant_Img.gameObject, iTween.Hash("x", 531f, "y", -315f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(2f);
        VoiceOver_AS.clip = Intros_AC[5];
        VoiceOver_AS.Play();
        HandPic.transform.localPosition = new Vector3(-316f, -157f, 0f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 477f, "y", -275f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.RotateTo(HandPic.gameObject, iTween.Hash("z", 0f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(Number_Img.gameObject, iTween.Hash("x", 678f, "y", -246f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
              
        yield return new WaitForSeconds(3f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 450f, "y", -108f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        VoiceOver_AS.clip = Intros_AC[8];
        VoiceOver_AS.Play();
        yield return new WaitForSeconds(1f);
        iTween.ScaleTo(HandPic.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.spring));
        iTween.ScaleTo(HandPic.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.spring));

        yield return new WaitForSeconds(1f);
        HandPic.GetComponent<Image>().enabled = false;

        yield return new WaitForSeconds(1f);
        NumberWord_Img.enabled = true;
      
        VoiceOver_AS.clip = Eleven_AC;
        VoiceOver_AS.Play();

        StartCoroutine(LastConclusionAUdioplay());
    } 
    IEnumerator LastConclusionAUdioplay()
    {
        yield return new WaitForSeconds(VoiceOver_AS.clip.length);
        VoiceOver_AS.clip = Intros_AC[6];
        VoiceOver_AS.Play();

        iTween.ScaleTo(Speechbubble_Img.gameObject, iTween.Hash("x", 0.65f, "y", 0.65f, "z", 0.65f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        GirlChar_Img.sprite = GirlChar_Sprites[3];
        
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", 30f, "y", -190f, "time", 0.5f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(GirlChar_Set.gameObject, iTween.Hash("x", 0.9f, "y", 0.9f, "time", 0.5f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

        StartCoroutine(ForSoundBtnIntroShow());
    }
    IEnumerator ForSoundBtnIntroShow()
    {
        yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", 700f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(0.5f);
        SoundBtn_Img.enabled = true;       

        VoiceOver_AS.clip = Intros_AC[9];
        VoiceOver_AS.PlayDelayed(0.5f);
        
        HandPic.GetComponent<Image>().enabled = true;
        HandPic.transform.localPosition = new Vector3(460f, 0f, 0f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 468f, "y", 32.3f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.RotateTo(HandPic.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", -8f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.ScaleTo(SoundBtn_Img.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.3f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(SoundBtn_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.6f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        
        yield return new WaitForSeconds(4f);
        HandPic.GetComponent<Image>().enabled = false;

        StartCoroutine(StarttheGame());
    }
    IEnumerator StarttheGame()
    {        
        yield return new WaitForSeconds(3.5f);
                
        StaticVariables.Is_Tutorial = false;

        Level_3_Manager.Instance.AfterTut_PopUp_Show();//7-3
        
        OneBoxQuant_Img.transform.parent = DragBoxSet.transform;
        TensBoxQuant_Img.transform.parent = DragBoxSet.transform;
        Number_Img.transform.parent = DragBoxSet.transform;
    }   
    ////////////////////////////////////////////////////
}
