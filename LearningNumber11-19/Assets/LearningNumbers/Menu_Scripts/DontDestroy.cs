using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour 
{
	void Awake () 
	{
		DontDestroyOnLoad (this.gameObject);
	}

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
