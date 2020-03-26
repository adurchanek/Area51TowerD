using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class TurretBlueprint
{



    public GameObject[] prefabs;

    public GameObject prefab;
    public int cost;
    
    public GameObject upgradedPrefab;
    public GameObject upgradedPrefab2;
    public GameObject upgradedPrefab3;
    public int upgradeCost;

    public int currentCost;





    public int GetSellAmount()
    {

        return cost;
        //return cost/2 + cost/4;
    }


}
