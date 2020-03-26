using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]
public class EnemyMovementController : MonoBehaviour {

	private Transform target;
	public int wavepointIndex = 0;

	private EnemyMovement enemy;

	void Awake()

	{

		enemy = GetComponent<EnemyMovement>();
		target = Waypoints.waypoints[wavepointIndex];
	}

	
	void Update()

	{
		Vector3 dir = target.position - transform.position;
        
		transform.Translate(dir.normalized*enemy.speed*Time.deltaTime,Space.World);

		if (Vector3.Distance(transform.position, target.position) < .15f)

		{


			GetNextWayPoint();
			//Debug.Log("TESTING");


		}

		enemy.speed = enemy.startSpeed;
	}

	private void GetNextWayPoint()
	{

		if (wavepointIndex == Waypoints.waypoints.Length-1)

		{  
			


			EndPath();
			return;
		}

		wavepointIndex += 1;
		target = Waypoints.waypoints[wavepointIndex];

	}


	void EndPath()

	{
		
		PlayerStats.Lives -= 1; 
		WaveSpawner.enemiesAlive -= 1;
		Destroy(gameObject);
	}
	
	public void SetWayPointIndex(int index)

	{

		wavepointIndex = index;
		
		target = Waypoints.waypoints[wavepointIndex];
		

	}
	
	
}
