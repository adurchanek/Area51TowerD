using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    private void OnEnable()
    {
        if (GameController.gameWon)
        {
            string roundsTxt = "All 100";
            roundsText.text = roundsTxt.ToString();
        }
        else
        {
            int r = PlayerStats.rounds - 1;
            roundsText.text = r.ToString();
        }
    }
    
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
