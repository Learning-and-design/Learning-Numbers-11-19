# Learning-Numbers-11-19
 Techframe Unity Project - LearningNumbers11-19(04-06)
# Game Link
[Play Game](https://kreedo-education.github.io/Learning-Numbers-11-19/webbuild)


# LearningNumbers11-19(04-06)-AllFiles

================================================

1.Unity Version : 2020.30f1

----------------------------------------

2.Contains Six Scenes in Build Settings:

  (1) 0_MenuScene

  (2) Level_0

  (3) Level_1

  (4) Level_2

  (5) Level_3

  (6) Level_4

-------------------------------

3.Folder Sructure as per Levels

  Ex: Level_0 -> Audios

              -> Individual_Asset_Images

              -> Scripts

------------------------

4.Global Folders like:

    Menu_Scripts contains

    Scenes folder contains all the scenes

    SoundFiles folder contains all the global sounds of numbers questionaries and popups

    UI_Global folder contains all the Images of Ingame,Mascot,Boxes,Backgrounds.

----------------------------------------------------------------------------------------------

5.Class Files:

   (1) StaticVariables.cs == is a Scriptable Object with all tha data which controls and contains data types of each Level

 

   (2) MenuManager.cs == has declared the PlayerPrefs (data) of Coins as per Levels and Progress Bar values

 

   (3) In a Level, Class Files Used are:

       Level_1_Manager.cs  --> Controls the Level - Contains the Enums of switching Tutorial to Game to Popups and Progress Bar.

       Level_1_Game.cs     --> Controls the Game - (Main Logic) Contains the Question Generation as per document,Correct or Wrong answerd estimation.

       Level_1_Tutorial.cs --> Controls the Presentation before Level Start with Mascot

       DragTens_Level1.cs  --> Drag Controls only for Tens Boxes (Original Position to Target Position)

       DragUnits_Level1.cs  --> Drag Controls only for Units Boxes (Original Position to Target Position)

       SingleBoxsPlaces_Level.cs  --> Makes the position depends up on the slots in game background image.

       TensBoxsPlaces_Level.cs  --> Makes the position depends up on the slots in game background image.

------------------------------------------------------------------------------------------------

6.In every Manager.cs --> used 'enums' to get the game state like InGame,InTutorial,Popshow.

  Debugs are commented in all scripts (If needed,can On lines)

  Coins Score is stored in PlayerPrefs.GetInt(StaticVariables.Coins_TotalScore);

 