using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicAudioManager : MonoBehaviour
{

    public static MusicAudioManager myScript;

    public AudioSource MusicBg_AS;
    public GameObject Btn_Sound;

    public Sprite BtnTex_On, BtnTex_Off;

  //  public Text Text_Sound;

    [HideInInspector]
    public string Is_Sound;
    [HideInInspector]
    public string SoundString;

    void Awake()
    {
        myScript = this;

        if (!PlayerPrefs.HasKey(SoundString))
        {
            PlayerPrefs.SetString(SoundString, "On");
        }

        Is_Sound = PlayerPrefs.GetString(SoundString);

        CheckSound();
    }

    public void SoundBtnAct()
    {
        if (Is_Sound == "On")
        {
            Is_Sound = "Off";
            PlayerPrefs.SetString(SoundString, Is_Sound);
            CheckSound();
        }
        else
        {
            Is_Sound = "On";
            PlayerPrefs.SetString(SoundString, Is_Sound);
            CheckSound();
        }
    }
    void CheckSound()
    {
        if (Is_Sound == "Off")
        {
            Btn_Sound.GetComponent<Image>().sprite = BtnTex_Off;
           /* if (Text_Sound != null)
            {
                Text_Sound.text = "Sound Off";
            }*/
         //   AudioListener.volume = 0;
            MusicBg_AS.mute = true;
        }
        else
        {
            /*if (Text_Sound != null)
            {
                Text_Sound.text = "Sound On";
            }*/
            Btn_Sound.GetComponent<Image>().sprite = BtnTex_On;
          //  AudioListener.volume = 1;
            MusicBg_AS.mute = false;

            MusicBg_AS.volume = 0.2f;
        }
    }
}
