using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    private Transform target;
    public WaveSpawner waveSpawner;
    private bool headingToWaypoint;
    public Transform[] desertWaypoints;
    private int waveIndex;
    private float speed;
    private float MAX_SPEED = 1;
    private int currentNumSpawned;
    private int maxNumSpawnBeforeReset;

    void Start()
    {
        headingToWaypoint = true;
        waveIndex = 0;
        target = Waypoints.waypoints[waveIndex];
        speed = MAX_SPEED;
        currentNumSpawned = 0;
        maxNumSpawnBeforeReset = 3;
    }
    
    void Update()
    {
        if (!WaveSpawner.tutorialComplete)
        {
            return;
        }
        
        if (WaveSpawner.enemiesAlive <= 0)
        {
            speed = .25f;
        }
        else
        {
            speed = MAX_SPEED;
        }
        
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized*speed*Time.deltaTime,Space.World);

        if (Vector3.Distance(transform.position, target.position) < .15f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if (headingToWaypoint)
        {
            //int randomSpawnCount = Random.Range(1,  4);
            int i = 0;
            while (i < 5)
            {
                waveSpawner.SpawnEnemy(target.transform, waveIndex);
                i++;
            }
            
            if (currentNumSpawned >= maxNumSpawnBeforeReset)
            {
                int r = Random.Range(0,  desertWaypoints.Length);
                target = desertWaypoints[r];
                currentNumSpawned = 0;
            }
            currentNumSpawned++;
            headingToWaypoint = false;
        }
        else
        {
            headingToWaypoint = true;
            int r = Random.Range(0,  Waypoints.waypoints.Length-11);
            target = Waypoints.waypoints[r];
            waveIndex = r;
        }
    }
}
