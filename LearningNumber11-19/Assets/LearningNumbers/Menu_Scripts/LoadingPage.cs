using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingPage : MonoBehaviour
{
    public static LoadingPage Instance;

    public string LoadingSceneName;
    public Slider slider;

    AsyncOperation async;

    private void Awake()
    {
        Instance = this;      
    }
    private void OnEnable()
    {
        slider.value = 0.1f;
        AudioListener.volume = 0;
    }
    public void LoadScreenStart(string NextSceneName)
    {
        LoadingSceneName = NextSceneName;
        StartCoroutine(LoadingScreen());
    }
    IEnumerator LoadingScreen()
    {
        async = SceneManager.LoadSceneAsync(LoadingSceneName);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            slider.value = async.progress + 0.1f;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;

                AudioListener.volume = 1;
            }
            yield return null;
        }
    }
}
