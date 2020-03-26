using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputManager : MonoBehaviour



{
    public PlayerTarget playerTarget;
    //public static Vector3 currentCoords;
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


        //Debug.Log("over floor: " + Floor.overFloor);
        if(Input.GetMouseButton(0) && Floor.overFloor)
        {
            //Your code here
            //Debug.Log("hit plane YES");
            //if ( ! EventSystem.current.IsPointerOverGameObject())
            

            
            
            
            //Input.mousePosition
            RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            //Debug.Log(firstFingerDown);
            if (Physics.Raycast(ray, out hit) && (hit.transform.tag == "FLOOR" || hit.transform.tag == "Enemy" || hit.transform.tag == "Node")) {

                //Debug.Log("down: " + Input.GetMouseButtonDown(0));

                if (hit.transform.name != "Node" && Input.GetMouseButtonDown(0))
                {
                    //currentlyShooting = false;
                    //return;
                }

//                if (firstFingerDown && hit.transform.name != "FLOOR")
//                {
//                    currentlyShooting = false;
//                    return;
//                }

                firstFingerDown = false;

                Transform objectHit = hit.transform;
            
                //Debug.Log("hit plane1" + objectHit.tag);
                
                    //Instantiate(enemy, hit.point, hit.transform.rotation);
                    //currentCoords = hit.point;
                    currentHit = hit;
                    playerTarget.transform.position = hit.point;
                    currentlyShooting = true;
                    //playerTarget.transform.position = new Vector3(
                    //    playerTarget.transform.position.x,
                    //    playerTarget.transform.position.y,
                    //    playerTarget.transform.position.z);
                    
                    //target.position = new Vector3(
                    //	firePoint.position.x + (target.position.x - firePoint.position.x) / 1.25f,
                    //	firePoint.position.y + (target.position.y - firePoint.position.y) / 1.25f,
                    //	firePoint.position.z + (target.position.z - firePoint.position.z) / 1.25f); //should be possible like a ring around finger...something is off..instead of based on distance to target do fixed distance


            }
            else
            {
                //Debug.Log("SECOND");
                firstFingerDown = true;
                currentlyShooting = false;
            }
            
        }
        else
        {
            //Debug.Log("THIRD");
            firstFingerDown = true;
            currentlyShooting = false;
        }
  

    }

 
    




}
