using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_0_Tutorial : MonoBehaviour
{
    #region Getter
    private static Level_0_Tutorial _instance;
    public static Level_0_Tutorial Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Level_0_Tutorial>();
            }
            return _instance;
        }
    }
    #endregion

    void OnEnable()
    {
        StartCoroutine(TutorealBegin());
    }
    //////////////////// TUTORIAL ///////////////////////////////////////////////
    #region Tutorial    
    [Header("AudioSource")]
    public AudioSource VoiceOver_AS;
    [Header("GameObject")]
    public GameObject GirlChar_Set;
    public Image GirlChar_Img;
    public Sprite[] GirlChar_Sprites;
    public GameObject TenBoxsSet;
    public GameObject[] UnitsBoxsSet;  
    public Image Speechbubble_Img;
    public GameObject HandPic;
    public Image BG_CharIntro_Img;
    public Image DragImg_Obj;
    [Header("DotAnim")]
    public Animator DotAnim_Obj;
    [Header("AudioClips")]
    public AudioClip[] Intros_AC;
    public AudioClip[] NumbersVoices_AC;
    [Header("WordsNUmbersImages")]
    public Image NumberWord_Img;
    public Sprite[] NumbersWords_Sprte;

    IEnumerator TutorealBegin()
    {
        if (Level_0_Manager.Instance.Check_GameState != Level_0_Manager.GameStates.PopUpShow)
        {

            StaticVariables.Is_Tutorial = true;

            DragImg_Obj.GetComponent<Image>().raycastTarget = false;
            TenBoxsSet.GetComponent<Image>().raycastTarget = false;
            for (int u = 0; u < UnitsBoxsSet.Length; u++)
            {
                UnitsBoxsSet[u].GetComponent<Image>().raycastTarget = false;
            }

            //SoundBtn_Img.GetComponent<Image>().enabled = false;       
            ////////////////////////////////////////////////////////

            //BlackBg_Img.CrossFadeAlpha(0f, 0f, true);        
            iTween.MoveFrom(GirlChar_Set.gameObject, iTween.Hash("x", 500f, "time", 0.75f, "delay", 0, "islocal", true, "easetype", iTween.EaseType.linear));
            iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.35f, "delay", 0.8f, "islocal", true, "easetype", iTween.EaseType.linear));
            HandPic.GetComponent<Image>().enabled = false;

            GirlChar_Img.enabled = true;
            GirlChar_Img.sprite = GirlChar_Sprites[0];

            yield return new WaitForSeconds(1f);
            VoiceOver_AS.clip = Intros_AC[0];
            VoiceOver_AS.Play();

            yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
            iTween.ScaleFrom(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0.3f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            VoiceOver_AS.clip = Intros_AC[1];
            VoiceOver_AS.Play();
            GirlChar_Img.sprite = GirlChar_Sprites[1];
            GirlChar_Img.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
            iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", -250f, "time", 0.5f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            yield return new WaitForSeconds(0.5f);
            BG_CharIntro_Img.CrossFadeAlpha(0f, 0.5f, true);

            StartCoroutine(SingleCardDragShow());
        }
    }

    IEnumerator SingleCardDragShow()
    {
        if (Level_0_Manager.Instance.Check_GameState != Level_0_Manager.GameStates.PopUpShow)
        {
            NumberWord_Img.enabled = false;
            yield return new WaitForSeconds(VoiceOver_AS.clip.length + 0.5f);
            iTween.ScaleTo(Speechbubble_Img.gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 0f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
            iTween.ScaleTo(Speechbubble_Img.gameObject, iTween.Hash("x", 0.65f, "y", 0.65f, "time", 0.3f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.linear));

            iTween.MoveTo(GirlChar_Set.gameObject, iTween.Hash("x", -700f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

            VoiceOver_AS.clip = Intros_AC[2];
            VoiceOver_AS.Play();
            yield return new WaitForSeconds(1.5f);
            //Invoke("ShowHighliteOutlne_OnSingleCad", 0f);
            HandPic.GetComponent<Image>().enabled = true;
            HandPic.transform.localPosition = new Vector3(60f, -330f, 0f);
            iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 140f, "y", -100, "time", 0.75f, "delay", 2f, "islocal", true, "easetype", iTween.EaseType.linear));
            iTween.MoveTo(DragImg_Obj.gameObject, iTween.Hash("x", 106f, "y", 50, "time", 0.75f, "delay", 2f, "islocal", true, "easetype", iTween.EaseType.linear));

            yield return new WaitForSeconds(5f);
            iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 260f, "y", -300f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));

            StartCoroutine(TensBoxCardDragShow());
        }
    }
    IEnumerator TensBoxCardDragShow()
    {
        if (Level_0_Manager.Instance.Check_GameState != Level_0_Manager.GameStates.PopUpShow)
        {
            BG_CharIntro_Img.CrossFadeAlpha(0f, 0.5f, true);

            yield return new WaitForSeconds(1f);
            VoiceOver_AS.clip = Intros_AC[3];
            VoiceOver_AS.Play();

            iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 260f, "y", -70, "time", 0.75f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
            iTween.MoveTo(TenBoxsSet.gameObject, iTween.Hash("x", 245f, "y", 60, "time", 0.75f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
            yield return new WaitForSeconds(2f);
            iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 282f, "y", -293, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
            iTween.RotateTo(HandPic.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 50f, "time", 0.5f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
            StartCoroutine(UnitsBoxCardDragShow());
        }
    }
    IEnumerator UnitsBoxCardDragShow()
    {
        if (Level_0_Manager.Instance.Check_GameState != Level_0_Manager.GameStates.PopUpShow)
        {
            yield return new WaitForSeconds(0f);
            BG_CharIntro_Img.CrossFadeAlpha(0f, 0.5f, true);
            iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 282f, "y", -65, "time", 0.75f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
            iTween.MoveTo(UnitsBoxsSet[0].gameObject, iTween.Hash("x", -5f, "y", 240, "time", 0.75f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
            StartCoroutine(NumberWordBoxCardShow());
        }
    }
    IEnumerator NumberWordBoxCardShow()
    {
        if (Level_0_Manager.Instance.Check_GameState != Level_0_Manager.GameStates.PopUpShow)
        {
            yield return new WaitForSeconds(3f);
            VoiceOver_AS.clip = NumbersVoices_AC[0];
            VoiceOver_AS.Play();

            NumberWord_Img.enabled = true;
            NumberWord_Img.sprite = NumbersWords_Sprte[0];
            iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "time", 0.35f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.easeInBack));
            iTween.ScaleTo(NumberWord_Img.gameObject, iTween.Hash("x", 1f, "y", 1f, "time", 0.35f, "delay", 0.65f, "islocal", true, "easetype", iTween.EaseType.easeInBack));

            iTween.MoveTo(HandPic.gameObject, iTween.Hash("x", 200f, "y", -230, "time", 0.35f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.linear));
            iTween.RotateTo(HandPic.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "time", 0.35f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.linear));

            yield return new WaitForSeconds(1.5f);
            VoiceOver_AS.clip = Intros_AC[4];
            VoiceOver_AS.Play();

            HandPic.GetComponent<Image>().enabled = false;

            StartCoroutine(StarttheLevel());
        }
    }

    IEnumerator StarttheLevel()
    {
        yield return new WaitForSeconds(4f);
        StaticVariables.Is_Tutorial = false;

        Level_0_Manager.Instance.AfterTutorialPage_Active();
    }
    #endregion
}

