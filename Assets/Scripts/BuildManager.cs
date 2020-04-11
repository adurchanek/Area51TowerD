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
	private GameObject nodeUiInstance;
	public GameObject nodeUi;
	public NodeUI nodeComponent;
	private GameObject nodeui;
	public bool clickingUiElement;
	public GameObject buildEffect;
	public GameObject sellEffect;


	// Use this for initialization
	void Awake () {

		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene");
			return;
		}

		instance = this;
	}

	private void Start()
	{
		clickingUiElement = false;
	}


	public bool CanBuild
	{
		get
		{
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
		
		if(nodeComponent!=null)
		{
			Destroy(nodeui);
		}
		
		nodeui = Instantiate(nodeUi, node.GetBuildPosition(), node.GetBuildRotation());
		
		if (nodeui != null)
		{
			nodeComponent = nodeui.GetComponent<NodeUI>();
			nodeComponent.SetTarget(node,Camera.main);

			if (node.GetComponent<Node>().upgradedLevel >= 4)
			{
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
		StartCoroutine(DeselectNodeUI(delay));
	}
	
	public IEnumerator DeselectNodeUI(float delay)
	{
		yield return new WaitForSeconds(0);
		
		selectedNode = null;
		if(nodeComponent != null)
		{
			Destroy(nodeui);
		}

		clickingUiElement = false;
	}
	
	public void SelectTurretToBuild(TurretBlueprint turretToBuild)
	{
		this.turretToBuild = turretToBuild;
		DeselectNode(0f);
	}



	public TurretBlueprint GetTurretToBuild()
	{
		return turretToBuild;
	}
}
