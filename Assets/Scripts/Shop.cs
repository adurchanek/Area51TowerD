using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	private BuildManager buildManager;
	public TurretBlueprint  standardTurret;
	public TurretBlueprint  missileLauncher;
	public TurretBlueprint  laserBeamer;
	public TurretBlueprint  moneyTurret;
	public Text moneyTextStandardTurret;
	public Text moneyTextMissile;
	public Text moneyTextLaserBeamer;
	public Text moneyTextMoneyTurret;
	public Button standardTurretButton;
	public Button missileTurretButton;
	public Button laserBeamerButton;
	public Button moneyTurretButton;
	public Button currentAnimating;
	public GameObject pauseMenu;
	public Color defaultColor;
	
	private void Start()
	{
		buildManager = BuildManager.instance;
		moneyTextStandardTurret.text = '$' + standardTurret.cost.ToString();
		moneyTextMissile.text = '$' + missileLauncher.cost.ToString();
		moneyTextLaserBeamer.text = '$' + laserBeamer.cost.ToString();
		moneyTextMoneyTurret.text = '$' + moneyTurret.cost.ToString();
		buildManager.turretToBuild = standardTurret;
		standardTurretButton.transform.GetChild(0).GetComponent<Animation>().Play();
		currentAnimating = standardTurretButton;
		currentAnimating.transform.GetChild(0).GetComponent<Image>().color = Color.green;
	}
	
	// Update is called once per frame
	void Update ()
	{
		moneyTextStandardTurret.text = '$' + standardTurret.cost.ToString();
		moneyTextMissile.text = '$' + missileLauncher.cost.ToString();
		moneyTextLaserBeamer.text = '$' + laserBeamer.cost.ToString();
		moneyTextMoneyTurret.text = '$' + moneyTurret.cost.ToString();
		
		if ( Input.GetKeyDown(KeyCode.Escape))
		{
			if (!pauseMenu.active)
			{
				Pause();
			}
			else
			{
				pauseMenu.GetComponent<Paused>().Resume();
			}
		}
	}
	
	public void SelectStandardTurret()
	{
		buildManager.SelectTurretToBuild(standardTurret);
		currentAnimating.transform.GetChild(0).GetComponent<Animation>().Stop();
		currentAnimating.transform.GetChild(0).transform.localScale =  new Vector3(1,1,1);
		currentAnimating.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
		standardTurretButton.transform.GetChild(0).GetComponent<Animation>().Play();
		currentAnimating = standardTurretButton;
		currentAnimating.transform.GetChild(0).GetComponent<Image>().color = Color.green;
	}
	
	public void SelectMissileLauncher()
	{
		buildManager.SelectTurretToBuild(missileLauncher);
		currentAnimating.transform.GetChild(0).GetComponent<Animation>().Stop();
		currentAnimating.transform.GetChild(0).transform.localScale =  new Vector3(1,1,1);
		currentAnimating.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
		missileTurretButton.transform.GetChild(0).GetComponent<Animation>().Play();
		currentAnimating = missileTurretButton;
		currentAnimating.transform.GetChild(0).GetComponent<Image>().color = Color.green;
	}
	
	public void SelectLaserBeamer()
	{
		buildManager.SelectTurretToBuild(laserBeamer);
		currentAnimating.transform.GetChild(0).GetComponent<Animation>().Stop();
		currentAnimating.transform.GetChild(0).transform.localScale =  new Vector3(1,1,1);
		currentAnimating.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
		laserBeamerButton.transform.GetChild(0).GetComponent<Animation>().Play();
		currentAnimating = laserBeamerButton;
		currentAnimating.transform.GetChild(0).GetComponent<Image>().color = Color.green;
	}
	
	public void SelectMoneyTurret()
	{
		buildManager.SelectTurretToBuild(moneyTurret);
		currentAnimating.transform.GetChild(0).GetComponent<Animation>().Stop();
		currentAnimating.transform.GetChild(0).transform.localScale =  new Vector3(1,1,1);
		currentAnimating.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
		moneyTurretButton.transform.GetChild(0).GetComponent<Animation>().Play();
		currentAnimating = moneyTurretButton;
		currentAnimating.transform.GetChild(0).GetComponent<Image>().color = Color.green;
	}
	
	public void Pause()
	{
		pauseMenu.SetActive(true);
	}
}
