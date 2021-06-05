using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_2_Tutorial : MonoBehaviour
{
    #region Getter
    private static Level_2_Tutorial _instance;
    public static Level_2_Tutorial Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_2_Tutorial>();
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
    public GameObject TwentyBosPar;
    public Image BG_CharIntro_Img;
    [Header("NumbersImages")]
    public Image[] Number_Img;
    public Image FakeNumber_Img;
    public Image NumberWord_Img;
    public Image SoundBtn_Img;
    [Header("====HintImages====")]
    public GameObject NumbersHintImgsPar;       
    [Header("Vectors")]
    public Vector3 SingleBoxCard_Tut_Pos;
    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;
    [Header("AudioClips")]
    public AudioClip[] Intros_AC;
    public AudioClip Nineteen_AC;

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
        NumbersHintImgsPar.gameObject.SetActive(false);
        SoundBtn_Img.enabled = false;

        StaticVariables.Is_Tutorial = true;

        for (int i = 0; i < Number_Img.Length; i++)
        {
            Number_Img[i].enabled = true;
            Number_Img[i].GetComponent<DragNumber_Level2>().enabled = false;           
        }
        Number_Img[5].transform.localPosition = new Vector3(557.8f, -332f, 0f);
               
        TwentyBosPar.transform.parent.transform.localPosition = new Vector3(800f, 36f, 0f);

        Level_2_Manager.Instance.CoinsShow_Text.text = " " + PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);

        yield return new WaitForSeconds(0f);
        FakeNumber_Img.enabled = false;
        NumberWord_Img.enabled = false;

        GirlChar_Set.transform.localPosition = new Vector3(600f, -180f, 0f);
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", 0f, "time", 0.5f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.linear));
        //iTween.ScaleTo(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.35f, "delay", 0.8f, "islocal", true, "easetype", iTween.EaseType.linear));

        HandPic.GetComponent<Image>().enabled = false;

        GirlChar_Img.enabled = true;
        GirlChar_Img.sprite = GirlChar_Sprites[0];

        yield return new WaitForSeconds(1f);
        VoiceOver_AS.clip = Intros_AC[0];
        VoiceOver_AS.Play();

        CardBordIn_Anim();

        yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
        iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        VoiceOver_AS.clip = Intros_AC[1];
        VoiceOver_AS.Play();
        GirlChar_Img.sprite = GirlChar_Sprites[1];
        GirlChar_Img.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
        BG_CharIntro_Img.enabled = false;
               
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", -366f, "y", -192f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(GirlChar_Set.gameObject, iTween.Hash("x", 0.9f, "y", 0.9f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        
        yield return new WaitForSeconds(1f);
        HandPic.GetComponent<Image>().enabled = false; //6-3
        HandPic.transform.localPosition = new Vector3(520f, -134f, 0f);
        HandPic.transform.localEulerAngles = new Vector3(0f, 0f, 105f);

        iTween.ScaleTo(TwentyBosPar.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(TwentyBosPar.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

        yield return new WaitForSeconds(2f);       
        iTween.ScaleTo(Number_Img[5].gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(Number_Img[5].gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

        yield return new WaitForSeconds(1f);
        iTween.ScaleTo(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.3f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        GirlChar_Img.sprite = GirlChar_Sprites[2];
        iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", -700f, "time", 0.5f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInBack));//addded 6-3

        yield return new WaitForSeconds(1f);
        VoiceOver_AS.clip = Intros_AC[2];
        VoiceOver_AS.Play();     
        iTween.ScaleTo(Speechbubble_Img.gameObject, iTween.Hash("x", 0.65f, "y", 0.65f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        GirlChar_Img.sprite = GirlChar_Sprites[3];
        
        StartCoroutine(SingleCardDragShow()); 
    }
    //////////////////////////////////////////////////
    IEnumerator SingleCardDragShow()
    {
        yield return new WaitForSeconds(2f);
        HandPic.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(3f);       
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 178f, "y", 56, "time", 0.35f, "delay", 1f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(Number_Img[5].gameObject, iTween.Hash("x", 145f, "y", -92f, "time", 0.35f, "delay", 1f, "islocal", true, "easetype", iTween.EaseType.linear));

        yield return new WaitForSeconds(3f);
        VoiceOver_AS.clip = Intros_AC[3];
        VoiceOver_AS.Play();

        HandPic.GetComponent<Image>().enabled = false;
        iTween.ScaleTo(Number_Img[5].gameObject, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(Number_Img[5].gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

        yield return new WaitForSeconds(1.2f);
        Number_Img[5].enabled = false;
        NumberWord_Img.enabled = true;
        FakeNumber_Img.enabled = true;

        yield return new WaitForSeconds(0.5f);
        VoiceOver_AS.clip = Intros_AC[4];
        VoiceOver_AS.Play();        

        StartCoroutine(ForSoundBtnIntroShow());
    }
    IEnumerator ForSoundBtnIntroShow()
    {
        yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
        SoundBtn_Img.enabled = true;

        Invoke("CardBordOut_Anim", 5f);

        VoiceOver_AS.clip = Intros_AC[5];
        VoiceOver_AS.Play();

        yield return new WaitForSeconds(0.2f);
        HandPic.GetComponent<Image>().enabled = true;
        HandPic.transform.localPosition = new Vector3(460f, 0f, 0f);
        iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 468f, "y", 43.5f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
        iTween.RotateTo(HandPic.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", -8f, "time", 0.35f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

        iTween.ScaleTo(SoundBtn_Img.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.3f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        iTween.ScaleTo(SoundBtn_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.3f, "delay", 0.38f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
        
        yield return new WaitForSeconds(6f);
        HandPic.GetComponent<Image>().enabled = false;

        StartCoroutine(StarttheGame());
    }
    IEnumerator StarttheGame()
    {
        yield return new WaitForSeconds(1f);
       
        StaticVariables.Is_Tutorial = false;

        Level_2_Manager.Instance.AfterTut_PopUp_Show();//7-3

    }
    //////////////////////////////////////////
    #region CardBordAnim_InOut
    void CardBordIn_Anim()
    {
        TwentyBosPar.transform.parent.transform.localPosition = new Vector3(800f, 36f, 0f);
        iTween.MoveTo(TwentyBosPar.transform.parent.gameObject, iTween.Hash("x", 0f, "y", 36f, "time", 1.5f, "delay", 10f, "islocal", true, "easetype", iTween.EaseType.linear));
    }
    void CardBordOut_Anim()
    {
        iTween.MoveTo(TwentyBosPar.transform.parent.gameObject, iTween.Hash("x", -800f, "y", 36f, "time", 1f, "delay",0f, "islocal", true, "easetype", iTween.EaseType.linear));
    }
    #endregion CardBordAnim_InOut        
    /////////////////////////////////////////////////////////////    
       
}
