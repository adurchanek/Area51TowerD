using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


	public GameObject ui;
	
	public SceneFader sceneFader;

	public string menuSceneName = "MainMenu";
	
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))

		{
			
			ToggleTime();
			
		}

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


	public void Retry()

	{

		ToggleTime();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}
	
	public void Menu()

	{
		ToggleTime();
		sceneFader.FadeTo(menuSceneName);
	}





}
