using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJet : MonoBehaviour
{
    public Transform[] locations;

    public float speed;
    public float MAX_SPEED;
    

    public bool airStrikeActive;
    // Start is called before the first frame update
    void Start()
    {
        airStrikeActive = false;
        //speed = 0;
        Debug.Log("fighter jet active");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("fighter jet updating");

        if (speed < MAX_SPEED)
        {
            speed = speed + (speed*speed*.3f*.16f);
        }

        Vector3 dir = locations[1].position-transform.position;
        
        transform.Translate(dir.normalized*speed*Time.deltaTime,Space.World);
        transform.LookAt(dir);

        if (Vector3.Distance(transform.position, locations[1].position) < 2f)

        {

            airStrikeActive = false;

            transform.position = locations[0].position;
            this.gameObject.SetActive(false);

            //Debug.Log("TESTING");


        }
        
    }



}
