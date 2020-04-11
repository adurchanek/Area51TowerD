using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public static int enemiesAlive;
	public Wave[] waves;
	public float timeBetweenWaves = 5f;
	private float countdown = 4f;
	public static int waveIndex = 0;
	public Transform spawnPoint;
	public Transform[] spawnPoints;
	private int currentSpawnPoint;
	public GameObject roundsUI;
	public static bool tutorialComplete;
	public float spawnSpeed = .15f;

	public void Start()
	{
		enemiesAlive = 0;
		currentSpawnPoint = 0;
		waveIndex = 0;
		tutorialComplete = false;
	}
	
	private void Update()
	{
		if (!tutorialComplete)
		{
			return;
		}

		if (enemiesAlive > 0)
		{
			return;
		}
		
		if(!GameController.gameEnded && waveIndex != waves.Length)
		{

		}
		else
		{
			Debug.Log("Level won");
			GameController.gameWon  = true;
			enabled = false;
		}

		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}
		
		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
	}

	IEnumerator SpawnWave()
	{
		StartCoroutine(DisplayRounds());
		PlayerStats.rounds += 1;
		Wave wave = waves[waveIndex];

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemies[i%wave.enemies.Length]);  
			
			yield return new WaitForSeconds(1f/wave.rate);
		}

		waveIndex += 1;
	}

	private void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, spawnPoints[currentSpawnPoint].position, spawnPoints[currentSpawnPoint].rotation);
		currentSpawnPoint++;

		enemiesAlive += 1;
		if (currentSpawnPoint >= spawnPoints.Length)
		{
			currentSpawnPoint = 0;
		}
	}
	
	public void SpawnEnemy(Transform location, int nextWaypoint)
	{
		Wave wave = waves[waveIndex];
	
		int r = Random.Range(0,  wave.enemies.Length);
		GameObject enemy =    Instantiate(wave.enemies[r], location.position, location.rotation);
		EnemyMovementController emc = enemy.GetComponent<EnemyMovementController>();
		emc.SetWayPointIndex(nextWaypoint); 
		
		enemiesAlive += 1;
	}
	
	IEnumerator DisplayRounds()
	{
		while(enemiesAlive > 0)
		{
			yield return new WaitForSeconds(.1f);
		}

		roundsUI.SetActive(true);
		yield return new WaitForSeconds(2f);
		
		roundsUI.SetActive(false);
	}
}
