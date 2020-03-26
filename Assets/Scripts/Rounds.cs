using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rounds : MonoBehaviour
{
    
    
    public Text roundsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnEnable()
    {
        int r = PlayerStats.rounds + 1;
        roundsText.text = "ROUND " + r.ToString();
    }
    
    
    
    
}
