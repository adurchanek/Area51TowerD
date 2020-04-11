using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public PlayerTarget playerTarget;
    public static RaycastHit currentHit;
    public GameObject enemy;
    public static bool currentlyShooting;
    public bool firstFingerDown;

    // Start is called before the first frame update
    void Start()
    {
        firstFingerDown = true;
        currentlyShooting = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetMouseButton(0) && Floor.overFloor)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit) && (hit.transform.tag == "FLOOR" || hit.transform.tag == "Enemy" || hit.transform.tag == "Node")) {

                if (hit.transform.name != "Node" && Input.GetMouseButtonDown(0))
                {
                    //currentlyShooting = false;
                    //return;
                }

                firstFingerDown = false;

                Transform objectHit = hit.transform;
                
                currentHit = hit;
                playerTarget.transform.position = hit.point;
                currentlyShooting = true;
            }
            else
            {
                firstFingerDown = true;
                currentlyShooting = false;
            }
        }
        else
        {
            firstFingerDown = true;
            currentlyShooting = false;
        }
    }
}
