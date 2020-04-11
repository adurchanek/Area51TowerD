using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
	public Color hoverColor;
	//public Vector3 positionOffset;
	public Vector3 positionOffset;
	private Renderer rend;
	private Color startColor;
	private BuildManager buildManager;
	public GameObject tutorial;
	public Color notEnoughMoneyColor;
	public Color fullyUpgradedColor;
	public Vector3 rotationOffset;
	public Transform offsetTransform;
	public int upgradedLevel;
	
	
	//[HideInInspector]
	public GameObject turret;
	public TurretBlueprint turretBlueprint;

	//[HideInInspector] 
	
	
	[HideInInspector] 
	//public bool isUpgraded = false;
	
	void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.GetColor("_BaseColor");
		turret = null;
		buildManager = BuildManager.instance;
		upgradedLevel = 1;
	}





	void OnMouseEnter()
	{
		if (!buildManager.CanBuild)
			
			return;
	}
    
    
	void OnMouseExit()
	{
		if (upgradedLevel >= 4)
		{
			rend.material.SetColor("_BaseColor", fullyUpgradedColor);
		}
		else
		{
			rend.material.SetColor("_BaseColor", startColor);
		}
	}


	IEnumerator resetColor()
	{
		yield return new WaitForSeconds(.05f);
		rend.material.SetColor("_BaseColor", startColor);
	}
	
	void OnMouseDown()
	{
		if (IsPointerOverUIObject())
		{
			Floor.overFloor = false;
			return;
		}

		if (buildManager.nodeComponent)
		{
			buildManager.DeselectNode(0f);
		}
		
		Floor.overFloor = true;

		if (turret != null)
		{
			this.turret.GetComponent<Turret>().nodeIsPressed = true;
		}
		
		Floor.onlyOverFloor = true;
	}
	
	void OnMouseUpAsButton()
	{
		if (IsPointerOverUIObject())
		{
			return;
		}

		if (turret != null)
		{
			buildManager.SelectedNode(this);
			return;
		}
		
		if (!buildManager.CanBuild)
		{
			Debug.Log("no money");
			return;
		}
		
		if(buildManager.HasMoney)
		{
			rend.material.SetColor("_BaseColor", hoverColor);
			StartCoroutine(resetColor());
		}
		else
		{
			rend.material.SetColor("_BaseColor", notEnoughMoneyColor);
			StartCoroutine(resetColor());
		}
		
		BuildTurret(buildManager.GetTurretToBuild());
	}
	

	void BuildTurret(TurretBlueprint blueprint)
	{
		if (!WaveSpawner.tutorialComplete &&   GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Tutorial>().stage == 0)
		{
			GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Tutorial>().SetStage(1);
		}

		buildManager.DeselectNode(0f);
		
		if (PlayerStats.Money < blueprint.cost)
		{
			return;
		}

		PlayerStats.Money -= blueprint.cost;
		GameObject _turret = Instantiate(blueprint.prefabs[0], this.GetBuildPosition(), GetBuildRotation());
		this.turret = _turret;
		turretBlueprint = blueprint;
		GameObject effect = Instantiate(buildManager.buildEffect, this.transform.position, GetBuildRotation());
		Destroy(effect, 1.3f);
		this.turret.GetComponent<Turret>().nodeIsPressed = true;
	}



	public void UpgradeTurret()
	
	{
		if (!WaveSpawner.tutorialComplete && GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Tutorial>().stage == 1)
		{
			GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Tutorial>().SetStage(2);
		}
		
		if (upgradedLevel >= 4)
		{
			rend.material.SetColor("_BaseColor", fullyUpgradedColor);
			return;
		}
		
		if (PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			return;
		}
		
		Destroy(turret);
		GameObject _turret = Instantiate(turretBlueprint.prefabs[upgradedLevel], this.GetBuildPosition(), GetBuildRotation());
		this.turret = _turret;
		Turret t = turret.GetComponent<Turret>();
		t.damageMultiplier = t.damageMultiplier * 2;// + upgradedLevel*2;
		upgradedLevel += 1;
		PlayerStats.Money -= turretBlueprint.upgradeCost;
		GameObject effect = Instantiate(buildManager.buildEffect, this.GetBuildPosition(), GetBuildRotation());
		Destroy(effect, 1.3f);
		
		if (upgradedLevel >= 4)
		{
			rend.material.SetColor("_BaseColor", fullyUpgradedColor);
		}
	}
	
	public Vector3 GetBuildPosition ()
	{
		return transform.position + positionOffset;
	}
	
	public Quaternion GetBuildRotation ()
	{
		return offsetTransform.rotation;
	}
	
	public void SellTurret()
	{
		PlayerStats.Money += turretBlueprint.GetSellAmount() + ((upgradedLevel-1)*turretBlueprint.upgradeCost);
		GameObject effect = Instantiate(buildManager.sellEffect, this.GetBuildPosition(), GetBuildRotation());
		Destroy(effect, 1.3f);
		Destroy(turret);
		turretBlueprint = null;
		StartCoroutine(resetColor());
		upgradedLevel = 1;
	}
	
	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}
