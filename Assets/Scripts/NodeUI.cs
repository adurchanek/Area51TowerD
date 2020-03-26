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

    //public Button upgradeButton;

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
        
        //Debug.Log("UI WORKS" + target);
        
        
        //grandChild = gameObject.transform.GetChild(0).GetChild(0);

        //////////////////////transform.position = target.turret.transform.position;
        /////transform.position = transform.position + target.transform.forward*-1*4;
        ///
        ///
        transform.position = transform.position;
        
        //partToRotate.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles.x,target.transform.rotation.eulerAngles.y,target.transform.rotation.eulerAngles.z);
        //partToRotate.transform.rotation = target.transform.rotation
        
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x-target.transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y-target.transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z-target.transform.rotation.eulerAngles.z);
        //partToRotate.rotation = Quaternion.Euler(target.turret.transform.rotation.eulerAngles.x,target.turret.transform.rotation.eulerAngles.y,target.turret.transform.rotation.eulerAngles.z);
        
        ///partToRotate.position = target.GetBuildPosition() + target.transform.forward*-1*6;
        
        //partToRotate.transform.rotation = target.transform.rotation;
        //transform.position = target.GetBuildPosition() ;
        
        //transform.LookAt(cameraPosition);
        //transform.rotation = target.GetBuildRotation();
        //child = gameObject.transform.GetChild(0);
        
        /* ///////////////

        if (!target.isUpgraded)
        {
            
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "COMPLETE";
            upgradeButton.interactable = false;

        }
        
        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();
*/
        //transform.LookAt(Camera.main.transform);
        //transform.LookAt(Camera.main.transform.position);

        string multiplier = "\n<i><b>x</b></i>" + (target.GetComponent<Node>().turret.GetComponent<Turret>().damageMultiplier * 2).ToString();
        
        
        
        
        sellButton.GetComponentInChildren<Text>().text =  "<b>Sell</b>\n"  + '$' + (target.GetComponent<Node>().turretBlueprint.GetSellAmount() + ((target.GetComponent<Node>().upgradedLevel-1)*target.GetComponent<Node>().turretBlueprint.upgradeCost)).ToString();
        upgradeButton.GetComponentInChildren<Text>().text = "<b>Upgrade</b>\n" + '$' +
                                                            target.GetComponent<Node>().turretBlueprint.upgradeCost
                                                                .ToString() + multiplier;// + " Power";

        
        //Debug.Log(upgradeButton.GetComponentInChildren<Text>());

        
        
        ui.SetActive(true);



    }

    void Update()

    {
        

        //Debug.Log("clicking UI ELEMENT-----" + BuildManager.instance.clickingUiElement);

        
        //moneyText.text = '$' + PlayerStats.Money.ToString();



        //partToRotate.position = target.GetBuildPosition() + target.transform.forward*-1*6;
        ////transform.position = target.transform.position + target.transform.forward * -1 * 6;
        //partToRotate.rotation = target.transform.rotation;
        //////transform.LookAt(cameraPosition);
        ///
        //Vector3 dir = cameraPosition.position - transform.position;

        //Quaternion lookRotation = Quaternion.LookRotation(dir);

        //canvasTransform.position = target.GetBuildPosition() + target.transform.forward*-1*6;



        //////transform.LookAt(cameraPosition);
        //partToRotate.rotation = Quaternion.Euler(partToRotate.rotation.eulerAngles.x,target.transform.rotation.eulerAngles.y,partToRotate.rotation.eulerAngles.z);
        //partToRotate.rotation = target.transform.rotation;
        //transform.LookAt(transform.position + cameraPosition.transform.rotation * Vector3.forward,cameraPosition.transform.rotation * Vector3.up);


        /////////////transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
        //transform.LookAt(camera.transform.position);
        //transform.position = target.GetBuildPosition();
        //Debug.Log("test");

        ////Vector3 v = camera.transform.position - transform.position;
        ////v.x = v.z = 0.0f;
        ////transform.LookAt( camera.transform.position - v ); 
        ////transform.Rotate(0,180,0);

        //transform.rotation = (camera.transform.rotation);

        //transform.LookAt(Camera.main.transform);


        //tempTransform.rotation = target.transform.GetChild(0).rotation;

        //tempTransform.LookAt(camera.transform);
        //transform.Rotate(target.GetBuildRotation().eulerAngles);
        //transform.rotation = tempTransform.rotation;



    }


    public void Hide()

    {
        
        ui.SetActive(false);
    }
    
    public void UpgradeTurret()

    {
        
        
        
        target.UpgradeTurret();
        //Debug.Log("4");
        BuildManager.instance.DeselectNode(.1f);
    }
    
    
    
    public void SellTurret()

    {
        //Debug.Log("3");
        BuildManager.instance.clickingUiElement = true;
        BuildManager.instance.DeselectNode(.1f);
        target.SellTurret();


        
    }
}
