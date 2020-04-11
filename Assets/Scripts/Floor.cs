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
        if (IsPointerOverUIObject())
        {
            overFloor = false;
            return;
        }

        if (buildManager.nodeComponent)
        {
            buildManager.DeselectNode(0f);
        }

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
