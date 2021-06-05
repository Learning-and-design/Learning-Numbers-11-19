using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu_UI, Loading_UI;

    // Start is called before the first frame update
    void Start()
    {
        Menu_UI.SetActive(true);
        Loading_UI.SetActive(false);

        haskeys_declare();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerPrefs.HasKey(StaticVariables.Coins_TotalScore))
        {
            PlayerPrefs.SetInt(StaticVariables.Coins_TotalScore, 0);
        }
        //////////////////////////////////////////////////////
        if (!PlayerPrefs.HasKey(StaticVariables.Level1_Score))
        {
            PlayerPrefs.SetInt(StaticVariables.Level1_Score, 0);
        }
        if (!PlayerPrefs.HasKey(StaticVariables.Level2_Score))
        {
            PlayerPrefs.SetInt(StaticVariables.Level2_Score, 0);
        }
        if (!PlayerPrefs.HasKey(StaticVariables.Level3_Score))
        {
            PlayerPrefs.SetInt(StaticVariables.Level3_Score, 0);
        }        
        if (!PlayerPrefs.HasKey(StaticVariables.Level4_Score))
        {
            PlayerPrefs.SetInt(StaticVariables.Level4_Score, 0);
        }        
        ///////////////////////////////////////////////////////
        if (!PlayerPrefs.HasKey(StaticVariables.Level1_BarVal))
        {
            PlayerPrefs.GetFloat(StaticVariables.Level1_BarVal, 0);
        }
        if (!PlayerPrefs.HasKey(StaticVariables.Level2_BarVal))
        {
            PlayerPrefs.GetFloat(StaticVariables.Level2_BarVal, 0);
        }
        if (!PlayerPrefs.HasKey(StaticVariables.Level3_BarVal))
        {
            PlayerPrefs.GetFloat(StaticVariables.Level3_BarVal, 0);
        }
        if (!PlayerPrefs.HasKey(StaticVariables.Level4_BarVal))
        {
            PlayerPrefs.GetFloat(StaticVariables.Level4_BarVal, 0);
        }
        ///////////////////////////////////////////////////////
        if (!PlayerPrefs.HasKey(StaticVariables.Is_CompletedLvl0))
        {
            PlayerPrefs.GetInt(StaticVariables.Is_CompletedLvl0, 0);
        }
    }
    void haskeys_declare()
    {

    }
    public void Play_Clicked()
    {
        Menu_UI.SetActive(false);
        Loading_UI.SetActive(true);

        if (LoadingPage.Instance)
            LoadingPage.Instance.LoadScreenStart("Level_0");
    }
    public void Level_0_Clicked()
    {
        Menu_UI.SetActive(false);
        Loading_UI.SetActive(true);

        if (LoadingPage.Instance)
            LoadingPage.Instance.LoadScreenStart("Level_0");
    }
    public void Level_1_Clicked()
    {
        Menu_UI.SetActive(false);
        Loading_UI.SetActive(true);

        if (LoadingPage.Instance)
            LoadingPage.Instance.LoadScreenStart("Level_1");
    }
    public void Level_2_Clicked()
    {
        Menu_UI.SetActive(false);
        Loading_UI.SetActive(true);

        if (LoadingPage.Instance)
            LoadingPage.Instance.LoadScreenStart("Level_2");
    }
    public void Level_3_Clicked()
    {
        Menu_UI.SetActive(false);
        Loading_UI.SetActive(true);

        if (LoadingPage.Instance)
            LoadingPage.Instance.LoadScreenStart("Level_3");
    }
    public void Level_4_Clicked()
    {
        Menu_UI.SetActive(false);
        Loading_UI.SetActive(true);

        if (LoadingPage.Instance)
            LoadingPage.Instance.LoadScreenStart("Level_4");
    }
}
