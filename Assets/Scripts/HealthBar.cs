using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	
	//public Transform cameraPosition;

	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		
		mainCamera = Camera.main;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//transform.LookAt(cameraPosition); //CHANGE IN PREFAB TI GET CAMERA
		
		///////////////////transform.LookAt(mainCamera.transform);
		///
		 transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back, mainCamera.transform.rotation * Vector3.up);
		
		//transform.position
		
	}
}
