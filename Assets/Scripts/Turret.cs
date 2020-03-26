using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : MonoBehaviour
{

	public Transform target;
	public float damageMultiplier;


	private EnemyMovement targetEnemy;

	//private GameObject playerTargetObject;
	public PlayerTarget playerTarget;
//	
	[Header("GENERAL")]
//
	public float range = 15f;

	public bool moneyTurret;

	public int moneyTurretDividend;
//	
	
	
	[Header("USE BULLETS (default)")]
	
	public GameObject bulletPrefab;
	public float fireRate;
	public float FIRE_RATE;
//
	private float fireCountdown = 0f;
	
	[Header("USE LASER")]
	
	public  bool useLaser = false;

	public LineRenderer lineRenderer;

	public ParticleSystem impactEffect;
	public ParticleSystem firePointEffect;
	//TODO unity custom editor episode 14 at 19:40

	public float damageOverTime;
	public float DAMAGE_OVER_TIME;

	public float slowReduction = .5f;
	private float mag;
	public bool laserSound;
	private AudioManager am;
	
	
//	
//	
	[Header("Unity Setup Fields")]
//
	public string enemyTag = "Enemy";
//
	public Transform partToRotate;
//
	public float turnSpeed = 10f;
//

	public Transform firePoint;
	public Transform[] firePoints;
	private int currentFirePoint;

	public Transform midFirePoint;

	private bool updatingTarget;

	public bool nodeIsPressed;
	

	
	
	
	
	

	//public Light impactLight;
	//public Light firePointLight;
//	
//
//	
//
	// Use this for initialization
	void Start ()
	{
		UpdateTarget();
		//InvokeRepeating("UpdateTarget", 0f,.5f);
		updatingTarget = false;
		playerTarget = GameObject.FindWithTag("PlayerTarget").GetComponent<PlayerTarget>();

		damageOverTime = DAMAGE_OVER_TIME;
		fireRate = FIRE_RATE;
		//fireCountdown = fireRate;
		laserSound = true;
		
		am = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<AudioManager>();

		slowReduction = slowReduction / (damageMultiplier*1.25f);
		
		if (moneyTurret)
		{
			StartCoroutine(GivePlayerMoney());
		}

		nodeIsPressed = false;
		currentFirePoint = 0;


	}
//	
	void UpdateTarget ()
	{

		if (target != null)
		{
			//return;
		}




		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		
		foreach (GameObject enemy in enemies)
		{

			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			if (distanceToEnemy < shortestDistance)
			{
				
				
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)

		{
			target = nearestEnemy.transform;
			targetEnemy = target.GetComponent<EnemyMovement>();
		}
		else
		{
			
			
			target = null;
			targetEnemy = null;
		}
	}
	
	
	// Update is called once per frame
	void Update()
	{

		if (moneyTurret)
		{
			return;
		}

		if (target == null)
		{
			UpdateTarget();
		}

		if (GameController.gameEnded)
		{
			return;
		}

		
		if (InputManager.currentlyShooting)
		{

			fireRate  = FIRE_RATE*5f;

			if (Input.GetMouseButtonDown(0))
			{
				//fireCountdown = fireRate/2;
				//fireCountdown = .1f;
				///////fireCountdown /= 3;
				//fireCountdown = 1f / fireRate;
				
				
			}

			if (fireCountdown > 1f / fireRate)
			{
				fireCountdown = 1f / fireRate;
			}

			//Debug.Log("currentlyShooting and mouse down");

			
			CancelInvoke("UpdateTarget");
			updatingTarget = false;
			
			target = playerTarget.transform;
			
			Vector3 tempVec = new Vector3(
				InputManager.currentHit.point.x-firePoint.position.x,
				InputManager.currentHit.point.y-firePoint.position.y,
				InputManager.currentHit.point.z-firePoint.position.z);

			float distanceScalar = 1;
			if ((InputManager.currentHit.point - playerTarget.cornerObject.transform.position).magnitude > 6.5)
			{
				distanceScalar = ((InputManager.currentHit.point - playerTarget.cornerObject.transform.position).magnitude - 6.5f) * .55f;
			}

			float m = .4f*distanceScalar;

			if (m < .4f)
			{
				m = .4f;
			}

			mag = tempVec.magnitude - m;

			if (mag < 0)
			{
				mag = .05f;
			}


			target.position = firePoint.position + tempVec.normalized*(mag) ;
			
			
			
			

			
			/*
			if (InputManager.currentHit.transform.gameObject.tag == "Enemy")
			{
				targetEnemy = InputManager.currentHit.transform.gameObject.GetComponent<EnemyMovement>();
			}
			else
			{
				targetEnemy = null;
			}
			
			*/
			
			//Debug.Log("cancel invoking");
			
		}
		else if (!updatingTarget)
		{
			fireRate  = FIRE_RATE;
			InvokeRepeating("UpdateTarget", 0f,.3f);
			updatingTarget = true;
		}
		


		if (target == null)
		{
			if (useLaser)
			{

				if (lineRenderer.enabled) ;
				{

					lineRenderer.enabled = false;
					impactEffect.Stop();
					firePointEffect.Stop();
					//impactLight.enabled = false;
					//firePointLight.enabled = false;
					am.stopLaser();
				}
			}

			return;
		}
		
		//Vector3 dir = target.position - transform.position;
		LockOnTarget();


		if (useLaser)
		{
			Laser();
		}
		else
		{
			if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			
			}

			fireCountdown -= Time.deltaTime;
			
		}
		
		//Debug.Log("countdown " + fireCountdown);
	}

	private void Laser()
	{

		
		
		
		if (!InputManager.currentlyShooting)
		{
			//damageOverTime /= 4;
			damageOverTime = DAMAGE_OVER_TIME;
			if (targetEnemy != null)
			{
				targetEnemy.TakeDamage(damageOverTime*Time.deltaTime*damageMultiplier);
				targetEnemy.Slow(slowReduction);
				am.playLaserHit();

			}
		}
		else
		{
			//damageOverTime *= 4;
			damageOverTime = DAMAGE_OVER_TIME*8;
			RaycastHit hit;
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
			//Vector3 fwd = transform.TransformDirection(Vector3.forward);


				
			if (Physics.Raycast(firePoint.position, target.transform.position - firePoint.position,  out hit,(InputManager.currentHit.point-firePoint.position).magnitude))
			{
				//Debug.DrawRay(transform.position, transform.TransformDirection(playerTarget.transform.position - transform.position) * hit.distance, Color.yellow);
				
				
				//Debug.Log("HITTTT SOMETHING YAY!: " + hit.transform.name);

				if (hit.transform.tag == "Enemy")
				{
					targetEnemy = hit.transform.GetComponent<EnemyMovement>();
					if (targetEnemy != null)
					{
						targetEnemy.TakeDamage(damageOverTime*Time.deltaTime*damageMultiplier);
						targetEnemy.Slow(slowReduction);
						
						am.playLaserHit();
						//impactEffect.Play();
						//firePointEffect.Play();
						
					}

					if ((hit.transform.position-firePoint.position).magnitude < mag)
					{
						target = hit.transform;
					}


					
				}
				else
				{


					//impactEffect.Stop();
					//firePointEffect.Stop();
					//impactLight.enabled = false;
					//firePointLight.enabled = false;
					
					
				}



			}


			/*
			
			if (Physics.Raycast(ray, out hit)) {
				Transform objectHit = hit.transform;
            
				//Debug.Log("hit plane1" + objectHit.tag);
                
				//Instantiate(enemy, hit.point, hit.transform.rotation);
				//currentCoords = hit.point;
				currentHit = hit;
				playerTarget.transform.position = hit.point;
				currentlyShooting = true;

			}
			*/
		}
		


		if(!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			//firePointEffect.Play();

			firePointEffect.Play();
			impactEffect.Play();

			//impactEffect.Play();
			//firePointEffect.Play(); //possibly the particle systems getting hit over and over which causes the  target to come out towards the screen, use layer mask, because if it hits anything it will come towards the screen
			//impactLight.enabled = true;
			//firePointLight.enabled = true;

		}
		//Debug.DrawRay(transform.position, , Color.red);
		
		//Debug.DrawLine(transform.position, target.transform.position, Color.red);
		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);



		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position;// +  dir.normalized*target.transform.localScale.x/2f;

		impactEffect.transform.rotation = Quaternion.LookRotation(dir);


		if (laserSound)
		{
			am.playLaser();
		}



		StartCoroutine(LaserSound());





	}

	private void LockOnTarget()
	{
		
		//Debug.Log("Distance from target to node: "+ Vector3.Distance(target.position, firePoint.position));




//		if (Vector3.Distance(target.position, firePoint.position) < .1)
//		{
//			
//			
//			Vector3 dir2 = target.position - firePoint.position; //change to parent transform to avoid .6 Y offset?
//			Quaternion lookRotation2 = Quaternion.LookRotation(dir2);
//			Vector3 rotation2 = Quaternion.Lerp(partToRotate.rotation,lookRotation2,Time.deltaTime*(2)).eulerAngles;
//			
//
//			partToRotate.rotation = Quaternion.Euler(rotation2.x,rotation2.y,rotation2.z); //TargetLockOn
//			return;
//
//		}

		
		//Debug.Log("node is pressed" + nodeIsPressed);
		


		Vector3 dir = target.position - firePoint.position; //change to parent transform to avoid .6 Y offset?
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime*turnSpeed).eulerAngles;
		//Debug.Log("rotation: " + lookRotation);
		partToRotate.rotation = Quaternion.Euler(rotation.x,rotation.y,rotation.z); //TargetLockOn


	}

	private void Shoot()
	{

		GameObject bulletGO;
		if (firePoints.Length == 0)
		{
			bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		}
		else
		{
			bulletGO = Instantiate(bulletPrefab, firePoints[currentFirePoint].position, firePoints[currentFirePoint].rotation);
			currentFirePoint++;

			if (currentFirePoint >= firePoints.Length)
			{
				currentFirePoint = 0;
			}

		}


		Bullet bullet = bulletGO.GetComponent<Bullet>();




		//bulletGO.GetComponent<Bullet>().Seek(target);
		if (bullet != null)
		{
			bullet.SetDamage(bullet.GetDamage() * damageMultiplier);


			if (InputManager.currentlyShooting)
			{
				
				bullet.NoSeek(InputManager.currentHit.point);
				//bullet.NoSeek(target.transform.position);
				//Debug.Log("WORKSSSSS");
			}
			else
			{
				
				bullet.Seek(target);
			}


		}
		
		Destroy(bullet.gameObject, 4);

		//Debug.Log("Shoot");
		
		

	}




//	
//	// Update is called once per frame
//	void Update () {
//
//		if (target == null)
//
//		{
//			return;
//		}
//
//		Vector3 dir = target.position - transform.position;
//		Quaternion lookRotation = Quaternion.LookRotation(dir);
//		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime*turnSpeed).eulerAngles;
//		partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f); //TargetLockOn
//
//		if (fireCountdown <= 0f)
//		{
//
//
//			Shoot();
//			fireCountdown = 1f / fireRate;
//			
//		}
//
//		fireCountdown -= Time.deltaTime;
//	}
//
//	private void Shoot()
//	{
//		GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
//		Bullet bullet = bulletGO.GetComponent<Bullet>();
//
//
//		if (bullet != null)
//
//		{
//			
//			
//			bullet.Seek(target);
//		}
//
//	}
//
	private void OnDrawGizmosSelected()
	{
		
		
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
		
	}


	IEnumerator LaserSound()
	{
		laserSound = false;
		
		yield return new WaitForSeconds(.1f);
		laserSound = true;
	}




	IEnumerator GivePlayerMoney()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
		
			PlayerStats.Money += (int)moneyTurretDividend*(int) damageMultiplier;
			
		}

	}








}
