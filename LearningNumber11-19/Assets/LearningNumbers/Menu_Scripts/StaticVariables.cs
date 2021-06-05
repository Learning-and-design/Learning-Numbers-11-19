using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables : MonoBehaviour
{   
    /////////////// ** Bools ** /////////////   
    public static bool Is_Tutorial;
    public static bool Is_HintShownTutorial;
    public static bool Is_FromHintBtn; 
    public static bool Is_RepeatSound;
        
    /////////////// ** Strings ** /////////////   
    public static string Is_CompletedLvl0 = "Is_CompletedLvl0";
    public static string Coins_TotalScore = "Coins_TotalScore";
    /////////////// ** Level Score ** /////////////   
    public static string Level1_Score = "Level1_Score";
    public static string Level2_Score = "Level2_Score";
    public static string Level3_Score = "Level3_Score";
    public static string Level4_Score = "Level4_Score";
    ////////////////////////////////////////////////
    public static bool Is_FromLvl1_Back;    
    public static bool Is_FromLvl2_Back;
    public static bool Is_FromLvl3_Back;
    public static bool Is_FromLvl4_Back;
    /////////////////////////////////////////////////////////
    public static bool Is_FromLvl0_CameFrmLvl1_GameSkipToLvl1;
    public static bool Is_FromLvl1_CameFrmLvl2_GameSkipToLvl2;
    public static bool Is_FromLvl2_CameFrmLvl3_GameSkipToLvl3;
    public static bool Is_FromLvl3_CameFrmLvl4_GameSkipToLvl4;    
    /////////////// ** Levels Progress Bar ** /////////////   
    public static string Level1_BarVal = "Level1_BarVal";
    public static string Level2_BarVal = "Level2_BarVal";
    public static string Level3_BarVal = "Level3_BarVal";
    public static string Level4_BarVal = "Level4_BarVal";
}
