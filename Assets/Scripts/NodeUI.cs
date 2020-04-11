using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public float verticalOffset;
    public Transform cameraPosition;
    public Text upgradeCost;
    public Text sellAmount;
    public Transform child;
    public Transform grandChild;
    public Transform partToRotate;
    public Transform canvasTransform;
    public Camera camera;
    public Transform tempTransform;
    public Button sellButton;
    public Button upgradeButton;
    
    public void SetTarget(Node target, Camera camera)
    {
        this.target = target;
        this.camera = camera;
        GameObject emptyGO = new GameObject();
        tempTransform = emptyGO.transform;
        transform.position = transform.position;
        
        string multiplier = "\n<i><b>x</b></i>" + (target.GetComponent<Node>().turret.GetComponent<Turret>().damageMultiplier * 2).ToString();
        sellButton.GetComponentInChildren<Text>().text =  "<b>Sell</b>\n"  + '$' + (target.GetComponent<Node>().turretBlueprint.GetSellAmount() + ((target.GetComponent<Node>().upgradedLevel-1)*target.GetComponent<Node>().turretBlueprint.upgradeCost)).ToString();
        upgradeButton.GetComponentInChildren<Text>().text = "<b>Upgrade</b>\n" + '$' +
                                                            target.GetComponent<Node>().turretBlueprint.upgradeCost
                                                                .ToString() + multiplier;// + " Power";
        
        ui.SetActive(true);
    }

    void Update()
    {
        
    }
    
    public void Hide()
    {
        ui.SetActive(false);
    }
    
    public void UpgradeTurret()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode(.1f);
    }
    
    public void SellTurret()
    {
        BuildManager.instance.clickingUiElement = true;
        BuildManager.instance.DeselectNode(.1f);
        target.SellTurret();
    }
}
