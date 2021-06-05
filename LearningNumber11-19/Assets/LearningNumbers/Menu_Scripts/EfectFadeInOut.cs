using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EfectFadeInOut : MonoBehaviour
{

    public Image EffecctImg;
    
    void OnEnable()
    {
        EffecctImg = this.gameObject.GetComponent<Image>();
        EffecctImg.CrossFadeAlpha(0f, 0f, true);
        Invoke("ShowIn", 0.3f);
    }

    void ShowIn()
    {
        EffecctImg.CrossFadeAlpha(1f, 0.3f, true);

        Invoke("ShowOut", 1.8f);
    }

    void ShowOut()
    {
        EffecctImg.CrossFadeAlpha(0f, 0.35f, true);
    }
}
