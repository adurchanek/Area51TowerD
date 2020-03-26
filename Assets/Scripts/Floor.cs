using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Floor : MonoBehaviour

{
    
    
    


    public static bool overFloor;
    public static bool onlyOverFloor;
    
    private BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        overFloor = false;

        onlyOverFloor = false;
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }




    void OnMouseDown()

    {
        //Debug.Log("1");
        //if(EventSystem.current.IsPointerOverGameObject())
        
        /*
        if(EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("UI elemtn clicked");
            overFloor = false;
            Debug.Log("Floor clicked");
            
            //Debug.Log("Not over!!!"+ overFloor);
            return;

        }
        */
        

        
 
//        if (Input.touchCount > 0 &&  !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || BuildManager.instance.clickingUiElement)
//        {
//            Debug.Log("Floor clicked");
//            overFloor = false;
//            return;
//        }


        if (IsPointerOverUIObject())
        {
            overFloor = false;
            return;
        }



        //buildManager.DeselectNode();
        
        if (buildManager.nodeComponent)
        {
            //StartCoroutine(buildManager.DeselectNodeUI());
            //Debug.Log("1");
            buildManager.DeselectNode(0f);
        }

 
        

        //check touch

        //if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
        {
            //overFloor = false;
            //return;

        }
        //if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            //overFloor = false;
            //return;
        }

        
        //Debug.Log("over!!!"+ overFloor);

        overFloor = true;
        onlyOverFloor = true;


    }
    
    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
