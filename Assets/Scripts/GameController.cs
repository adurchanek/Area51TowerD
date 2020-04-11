using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	
	public static bool gameEnded;
	public static bool gameWon;
	public GameObject pauseMenu;
	public GameObject gameOverUI;
	private float viewPort;
	private float viewPortFovRatio;
	public CameraSetupPoint[] cameraPoints;
	
	void Awake()
	{
		viewPort = 185f/90f;
		viewPortFovRatio = viewPort/ Camera.main.aspect;
		Application.targetFrameRate = 30;
		QualitySettings.vSyncCount = 2;
		
		if (Camera.main.aspect <= 1.334) //   4/3
		{
			Camera.main.fieldOfView = cameraPoints[cameraPoints.Length-1].fov;
			Camera.main.transform.position = cameraPoints[cameraPoints.Length-1].t.position;
			Camera.main.transform.rotation = cameraPoints[cameraPoints.Length-1].t.rotation;
		}
		else if (Camera.main.aspect <= 1.51) //   3/2
		{
			Camera.main.fieldOfView = cameraPoints[5].fov;
			Camera.main.transform.position = cameraPoints[5].t.position;
			Camera.main.transform.rotation = cameraPoints[5].t.rotation;
		}
		
		else if (Camera.main.aspect <= 1.61) //   16/10
		{
			Camera.main.fieldOfView = cameraPoints[4].fov;
			Camera.main.transform.position = cameraPoints[4].t.position;
			Camera.main.transform.rotation = cameraPoints[4].t.rotation;
		}
		else if (Camera.main.aspect <= 1.67) //   5/3
		{
			Camera.main.fieldOfView = cameraPoints[3].fov;
			Camera.main.transform.position = cameraPoints[3].t.position;
			Camera.main.transform.rotation = cameraPoints[3].t.rotation;
		}
		else if (Camera.main.aspect <= 1.78) //   16/9
		{
			Camera.main.fieldOfView = cameraPoints[2].fov;
			Camera.main.transform.position = cameraPoints[2].t.position;
			Camera.main.transform.rotation = cameraPoints[2].t.rotation;
		}
		else if (Camera.main.aspect <= 1.91) //   19/10
		{
			Camera.main.fieldOfView = cameraPoints[1].fov;
			Camera.main.transform.position = cameraPoints[1].t.position;
			Camera.main.transform.rotation = cameraPoints[1].t.rotation;
		}
		else // > 1.68
		{
			//Debug.Log("Set Camera SetupPoint: " + 2);
			//Debug.Log("RATIO: " + "normal");
		}

		int newHeight = Screen.currentResolution.height;
		int newWidth = Screen.currentResolution.width;
		
		if (Screen.currentResolution.height > 1000f)
		{
			float resolutionRatio;
			resolutionRatio = Screen.currentResolution.height / 1000f;
			newHeight = (int)(Screen.currentResolution.height / resolutionRatio);
			newWidth = (int)(Screen.currentResolution.width / resolutionRatio);
		}
		Screen.SetResolution(newWidth,newHeight  , true, 30);
	}


	void Start()
	{
		gameEnded = false;
		gameWon = false;
		Time.timeScale = 1f;
	}

	// Update is called once per frame
	void Update ()
	{
		if (gameEnded)
		{
			PlayerStats.Lives = 0;
			return;
		}

		if (Input.GetKeyDown("e"))
		{
			Cursor.visible = true;
		}
		
		if (Input.GetKeyDown("p"))
		{
			Cursor.visible = false;
		}

		if (PlayerStats.Lives <= 0 || gameWon)
		{
			EndGame();
			return;
		}
	}

	public void EndGame()
	{
		Time.timeScale = .05f;
		Debug.Log("Game Over");

		gameEnded = true;
		
		gameOverUI.SetActive(true);
	}
}

	[System.Serializable]
	public class CameraSetupPoint
	{
		public Transform t;
		public float fov;
	};
