using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    public RaycastHit currentHit;
    public EnemyMovement targetEnemy;
    public GameObject cornerObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHit = InputManager.currentHit;
    }
}
