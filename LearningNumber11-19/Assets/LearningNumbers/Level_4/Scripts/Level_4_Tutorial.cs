using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_4_Tutorial : MonoBehaviour
{
    #region Getter
    private static Level_4_Tutorial _instance;
    public static Level_4_Tutorial Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_4_Tutorial>();
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
    public GameObject[] BallsImgsPar;
    public Image BG_CharIntro_Img;
    [Header("NumbersImages")]   
    public Image Number_Img;   
    public Image NumberWord_Img;
    public Image DoneBtn_Img;
    public Image SoundBtn_Img;
    [Header("Bools")]
    private bool Is_SingleBoxPlaced_Tut;
    private bool Is_TensBoxPlaced_Tut;
    private bool Is_NumbreImgBoxPlaced_Tut;
    private bool Is_BallsImgBoxPlaced_Tut;    
    [Header(":::: Vectors ::::")]
    public Vector3[] UnitsBoxs_Arr_Pos;
    public Vector3 TensBoxs_Pos;
    public Vector3 NumberThirteenBoxs_Pos;
    public Vector3 Balls_All_Pos;
    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;
    [Header("AudioClips")]
    public AudioClip[] Intros_AC;
    public AudioClip Thirteen_AC;
    public AudioClip TouchDone_AC;

    private float xtemp;

    void OnEnable()
    {
        StartCoroutine(TutorealBegin());
    }

    void Start()
    {       
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TutorealBegin()
    {
        xtemp = -412f;
        SoundBtn_Img.enabled = false;

        for (int all=0; all < UnitsBoxsPar.Length; all++)
        {           
            UnitsBoxsPar[all].transform.localScale = new Vector3(0,0,0);
            TensBoxsPar[all].transform.localScale = new Vector3(0, 0, 0);
            NumberImgsPar[all].transform.localScale = new Vector3(0, 0, 0);           
        }
        for (int all = 0; all < BallsImgsPar.Length; all++)
        {
            BallsImgsPar[all].transform.localScale = new Vector3(0, 0, 0);
        }
        
        StaticVariables.Is_Tutorial = true;

        Is_SingleBoxPlaced_Tut = false;
        Is_TensBoxPlaced_Tut = false;
        Is_NumbreImgBoxPlaced_Tut = false;
        Is_BallsImgBoxPlaced_Tut = false;
        
        NumberWord_Img.enabled = false;
        DoneBtn_Img.enabled = false;
        DoneBtn_Img.raycastTarget = false;

        Level_4_Manager.Instance.CoinsShow_Text.text = " " + PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);

        yield return new WaitForSeconds(0f);
        GirlChar_Set.transform.localPosition = new Vector3(500f, -192f, 0f);
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", 0f, "time", 0.5f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.35f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));

        HandPic.GetComponent<Image>().enabled = false;

        GirlChar_Img.enabled = true;
        GirlChar_Img.sprite = GirlChar_Sprites[0];
        Speechbubble_Img.transform.localScale = new Vector3(0.85f, 0.85f, 1f);
        ////////////////////////////////////////
        yield return new WaitForSeconds(1f);
        VoiceOver_AS.clip = Intros_AC[0];
        VoiceOver_AS.Play();
      
        yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
        iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        VoiceOver_AS.clip = Intros_AC[1];
        VoiceOver_AS.Play();

        GirlChar_Img.sprite = GirlChar_Sprites[3];
     
        BG_CharIntro_Img.enabled = false;
               
        yield return new WaitForSeconds(4f);
        GirlChar_Img.sprite = GirlChar_Sprites[1];
        GirlChar_Img.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

        for (int all = 0; all < BallsImgsPar.Length; all++)
        {
            BallsImgsPar[all].transform.localScale = new Vector3(1, 1, 1);           
        }
        
        yield return new WaitForSeconds(1.5f);        
        GirlChar_Img.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
        for (int all = 0; all < UnitsBoxsPar.Length; all++)
        {            
            UnitsBoxsPar[all].transform.localScale = new Vector3(1, 1, 1);
            TensBoxsPar[all].transform.localScale = new Vector3(1, 1, 1);            
        }

        yield return new WaitForSeconds(1.5f);
        GirlChar_Img.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

        for (int all = 0; all < UnitsBoxsPar.Length; all++)
        {  
            NumberImgsPar[all].transform.localScale = new Vector3(1, 1, 1);
        }
       
        yield return new WaitForSeconds(1f);
        GirlChar_Img.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
        DoneBtn_Img.enabled = true;
        iTween.ScaleTo(DoneBtn_Img.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.3f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));
        iTween.ScaleTo(DoneBtn_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.65f, "islocal", true, "easetype", iTween.EaseType.spring));

        HandPic.transform.localPosition = new Vector3(-289f,-92f, 0f);

        yield return new WaitForSeconds(2.5f);
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", 700f, "y", -192f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        
        yield return new WaitForSeconds(2f);
        VoiceOver_AS.clip = Thirteen_AC;
        VoiceOver_AS.Play();

        yield return new WaitForSeconds(2f);
        VoiceOver_AS.clip = Intros_AC[2];
        VoiceOver_AS.Play();
        yield return new WaitForSeconds(0.5f);
        HandPic.GetComponent<Image>().enabled = true;
        HandPic.transform.localEulerAngles = new Vector3(0f, 0f, -235f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", -290f, "y", 114f, "time", 0.2f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        for(int b=0; b <= 13; b++)
        {
            iTween.MoveTo(BallsImgsPar[b].gameObject, iTween.Hash("x", -412f, "y", 12f, "time", 0.2f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        }
        yield return new WaitForSeconds(0.5f);
        for (int b = 0; b < 10; b++)
        {              
            iTween.MoveTo(BallsImgsPar[b].gameObject, iTween.Hash("x", xtemp, "y", 12f, "time", 0.2f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.linear));
            xtemp = xtemp + 32f;           
        }
        iTween.MoveTo(BallsImgsPar[10].gameObject, iTween.Hash("x", -412, "y", -18.4f, "time", 0.2f, "delay", 0.25f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(BallsImgsPar[11].gameObject, iTween.Hash("x", -380, "y", -18.4f, "time", 0.2f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(BallsImgsPar[12].gameObject, iTween.Hash("x", -348, "y", -18.4f, "time", 0.2f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(2.5f);
        VoiceOver_AS.clip = Intros_AC[3];
        VoiceOver_AS.Play();
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 314f, "y", -68f, "time", 0.2f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 314f, "y", 148f, "time", 0.2f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(TensBoxsPar[0].gameObject, iTween.Hash("x", 233f, "y", 43f, "time", 0.2f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        
        yield return new WaitForSeconds(3f);
        VoiceOver_AS.clip = Intros_AC[4];
        VoiceOver_AS.Play();
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 202f, "y", -109f, "time", 0.2f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 172f, "y", 97f, "time", 0.2f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(UnitsBoxsPar[0].gameObject, iTween.Hash("x", 64.7f, "y", -14.5f, "time", 0.2f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(UnitsBoxsPar[1].gameObject, iTween.Hash("x", 104.5f, "y", -14.5f, "time", 0.2f, "delay", 0.6f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(UnitsBoxsPar[2].gameObject, iTween.Hash("x", 142.7f, "y", -14.5f, "time", 0.2f, "delay", 0.7f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(3f);
        VoiceOver_AS.clip = Intros_AC[5];
        VoiceOver_AS.Play();

        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 70f, "y", 24f, "time", 0.2f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", -68f, "y", 225f, "time", 0.2f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(NumberImgsPar[2].gameObject, iTween.Hash("x", -218f, "y", 98f, "time", 0.2f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(2f);
        VoiceOver_AS.clip = TouchDone_AC;
        VoiceOver_AS.Play();
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 380f, "y", -172f, "time", 0.3f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.RotateTo(HandPic.gameObject, iTween.Hash("z",55f,  "time", 0.3f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.ScaleTo(DoneBtn_Img.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.3f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));
        iTween.ScaleTo(DoneBtn_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.65f, "islocal", true, "easetype", iTween.EaseType.spring));
             
        yield return new WaitForSeconds(2f);
        NumberWord_Img.enabled = true;        
        VoiceOver_AS.clip = Thirteen_AC;
        VoiceOver_AS.PlayDelayed(0.3f);

        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.3f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.spring));
        iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.8f, "islocal", true, "easetype", iTween.EaseType.spring));
        
        //iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", -700f, "y", -190f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        HandPic.GetComponent<Image>().enabled = false;
        Speechbubble_Img.transform.localScale = new Vector3(0.65f, 0.65f, 1f);

        StartCoroutine(LastConclusionAUdioplay());
    }

    IEnumerator LastConclusionAUdioplay()
    {
        yield return new WaitForSeconds(2f);

        VoiceOver_AS.clip = Intros_AC[6];
        VoiceOver_AS.Play();

        iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        GirlChar_Img.sprite = GirlChar_Sprites[0];
        GirlChar_Img.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
        
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", 83f, "y", -192f, "time", 0.5f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(GirlChar_Set.gameObject, iTween.Hash("x", 0.9f, "y", 0.9f, "time", 0.5f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

        StartCoroutine(ForSoundBtnIntroShow());
    }
    IEnumerator ForSoundBtnIntroShow()
    {
        yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", 700f, "y", -192f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(0.5f);
        SoundBtn_Img.enabled = true;

        VoiceOver_AS.clip = Intros_AC[8];
        VoiceOver_AS.PlayDelayed(0.5f);

        HandPic.GetComponent<Image>().enabled = true;
        HandPic.transform.localPosition = new Vector3(460f, 0f, 0f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 460f, "y", 34f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.RotateTo(HandPic.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", -8f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.ScaleTo(SoundBtn_Img.gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(SoundBtn_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.6f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        
        yield return new WaitForSeconds(5f);       
        StartCoroutine(StarttheGame());
    }
    IEnumerator StarttheGame()
    {
        yield return new WaitForSeconds(3f);

        HandPic.GetComponent<Image>().enabled = false;

        StaticVariables.Is_Tutorial = false;

        Level_4_Manager.Instance.AfterTut_PopUp_Show();//7-3
          
        SetPos_Original();
    }
    void SetPos_Original()
    {
        for (int all = 0; all < UnitsBoxsPar.Length; all++)
        {
            UnitsBoxsPar[all].transform.parent = DragBoxSet.transform;
        }
        TensBoxsPar[0].transform.parent = DragBoxSet.transform;
        NumberImgsPar[2].transform.parent = DragBoxSet.transform;
        for (int all = 0; all < BallsImgsPar.Length; all++)
        {
            BallsImgsPar[all].transform.parent = DragBoxSet.transform;
        }
        ///////////////
        UnitsBoxsPar[0].transform.localPosition = UnitsBoxs_Arr_Pos[0];
        UnitsBoxsPar[1].transform.localPosition = UnitsBoxs_Arr_Pos[1];
        UnitsBoxsPar[2].transform.localPosition = UnitsBoxs_Arr_Pos[2];

        TensBoxsPar[0].transform.localPosition = TensBoxs_Pos;
        NumberImgsPar[2].transform.localPosition = NumberThirteenBoxs_Pos;
        for (int all = 0; all < BallsImgsPar.Length; all++)
        {
            BallsImgsPar[all].transform.localPosition = Balls_All_Pos;
        }
    }   
}
