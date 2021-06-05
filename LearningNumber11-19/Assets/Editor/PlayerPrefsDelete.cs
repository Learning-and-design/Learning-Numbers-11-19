using UnityEngine;
using UnityEditor;

public class PlayerPrefsDelete : EditorWindow
{

	[MenuItem ("PlayerPrefs/Delete")]
	public static void DeletePrefs ()
	{
        PlayerPrefs.DeleteAll();       
	}

}
