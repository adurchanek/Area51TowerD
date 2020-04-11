using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	//public SceneFader SceneFader;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string levelToLoad = "MainLevel";
	//public int levelToLoad = 1;

	public void Quit()

	{
		Debug.Log("Quit");
		Application.Quit();
	}
	
	public void Play()
	{
		Debug.Log("Play");
		SceneManager.LoadScene("SampleScene");
	}
}
