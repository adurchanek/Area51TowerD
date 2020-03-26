using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelLoader : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingScreen;
    public Slider slider;

    public Text progressText;
    void Awake()
    {
        //LoadLevel(1);
    }

    public void LoadLevel(int sceneIndex)

    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }


    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        mainMenu.SetActive(false);
        

        while (!operation.isDone)
        {
            
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            slider.value = progress;
            progressText.text = (int)(progress * 100) + "%";
            yield return null;
        }


    }


}
