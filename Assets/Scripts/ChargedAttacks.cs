using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedAttacks : MonoBehaviour
{
    public FighterJet fighter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CallAirStrike()
    {
        Debug.Log("airstrike called");
        fighter.gameObject.SetActive(true);
    }
}
