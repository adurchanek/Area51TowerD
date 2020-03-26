using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{
    
    public GameObject ui;



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

            if (r < 0)
            {
                r = 0;
            }

            roundsText.text = r.ToString();
        }
        Time.timeScale = 0f;
        
        


    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))

        {
			
            //ToggleTime();
			
        }
    }


    public void Retry()

    {
        //sceneFader.FadeTo(SceneManager.GetActiveScene().name);
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

        //SceneFader.FadeTo(levelToLoad);
    }
    
    public void Resume()

    {

        Time.timeScale = 1f;
        ui.SetActive(!ui.activeSelf);
        //ToggleTime();

        //SceneFader.FadeTo(levelToLoad);
    }
    
    public void ToggleTime()

    {
		
		
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)

        {

            Time.timeScale = 0f;
            
            
			
        }

        else
        {
            Time.timeScale = 1f;

        }


    }
}