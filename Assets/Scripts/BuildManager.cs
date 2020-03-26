using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;
	public TurretBlueprint turretToBuild;
	private Node selectedNode;
	//public NodeUI nodeUiInstance;
	
	private GameObject nodeUiInstance;

	public GameObject nodeUi;
	public NodeUI nodeComponent;

	private GameObject nodeui;

	public bool clickingUiElement;

	
	
	
	
	
	//public GameObject missileLauncherPrefab;
	//public GameObject standardTurretPrefab;

	public GameObject buildEffect;
	public GameObject sellEffect;
	//Vector3 positionOffset

	// Use this for initialization
	void Awake () {

		if (instance != null)

		{
			Debug.LogError("More than one BuildManager in scene");
			return;
		}

		instance = this;


		//SelectStandardTurret();
	}

	private void Start()
	{
		clickingUiElement = false;
	}


	public bool CanBuild
	{
		get
		{
			//Debug.Log(turretToBuild);
			return turretToBuild != null;
		}
	}
	
	
	public bool HasMoney
	{
		get
		{
			return PlayerStats.Money >= turretToBuild.cost;
		}
	}


	public void SelectedNode(Node node)

	{

		if (node == selectedNode)

		{
			//Debug.Log("2");
			DeselectNode(0f);
			return;
		}
		



		selectedNode = node;
		//turretToBuild = null;
		
		if(nodeComponent!=null)

		{
			
			//nodeComponent.Hide();
			Destroy(nodeui);
		}
		
		
		
		nodeui = Instantiate(nodeUi, node.GetBuildPosition(), node.GetBuildRotation());
	
		
		//NodeUI nodeComponent = nodeui.GetComponent<NodeUI>();

		
		
		//GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		//Bullet bullet = bulletGO.GetComponent<Bullet>();



		//bulletGO.GetComponent<Bullet>().Seek(target);
		if (nodeui != null)

		{
			

			nodeComponent = nodeui.GetComponent<NodeUI>();
			nodeComponent.SetTarget(node,Camera.main);

			if (node.GetComponent<Node>().upgradedLevel >= 4)
			{
				//nodeComponent.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
				nodeComponent.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Button>().enabled = false;
				nodeComponent.transform.GetChild(0).GetChild(0).GetChild(0).gameObject
					.GetComponent<UnityEngine.UI.Image>().enabled = false;

				nodeComponent.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Text>()
					.enabled = false;
			}



		}
	}

	public void DeselectNode(float delay)
	{

		
		//Debug.Log("CO ROUTINE STARTED - Deselect Node -----test");
		StartCoroutine(DeselectNodeUI(delay));
	}
	
	public IEnumerator DeselectNodeUI(float delay)
	{
		
		
		yield return new WaitForSeconds(0);
		//DeselectNode();
		
		
		selectedNode = null;
		if(nodeComponent!=null)

		{
			
			//nodeComponent.Hide();
			Destroy(nodeui);
		}

		clickingUiElement = false;

	}



	public void SelectTurretToBuild(TurretBlueprint turretToBuild)
	{
		//Debug.Log("10");
		this.turretToBuild = turretToBuild;
		DeselectNode(0f);
	}



	public TurretBlueprint GetTurretToBuild()

	{

		return turretToBuild;
	}

//	public void BuildTurretOn(Node node)
//	{
//
//		
//		DeselectNode();
//		if (PlayerStats.Money < turretToBuild.cost)
//		{
//			
//			Debug.Log("Not enough money");
//
//			return;
//		}
//
//		PlayerStats.Money -= turretToBuild.cost;
//
//
//		//Vector3 positionOffset = new Vector3( 0, node.transform.localScale.y/2,0); CHANGE poisition offset to node.positionoffset. updated position offset in node and added getbuildposition. deleted node.transform.position +...etc and replaced with getbuild...
//		
//		node.positionOffset = new Vector3( 0, node.transform.localScale.y/2f+turretToBuild.prefab.transform.position.y,0);
//		
//		GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), node.transform.rotation);
//		node.turret = turret;
//
//		GameObject effect = Instantiate(buildEffect, node.transform.position, Quaternion.identity);
//		
//		Destroy(effect, 1.3f);
//
//		//Debug.Log("Turret Built: money left" + PlayerStats.Money);
//
//	}
}
