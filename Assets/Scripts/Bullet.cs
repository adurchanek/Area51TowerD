using System;
using System.Collections;
using System.Collections.Generic;
using MirzaBeig.ParticleSystems.Demos;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	private Transform target;

	public float speed = 70f;

	public float maxSpeed;
//
	public GameObject impactEffect;
	//public GameObject flamesEffect;
	

	public float explosionRadius = 0f;
	
	public Transform firePoint;
	//public GameObject flames;
	
	public string enemyTag = "Enemy";
	public float damage = 5f;

	public Light flamesLight;
	//public bool hasTarget;
	
	public bool useLight = false;
	//public bool heatSeeking;

	public Vector3 currentDirection;

	public string lockOnString;
	
	public AudioSource audioSource;

	private AudioManager am;

	private bool alive;
	
	
	
	

	public bool lockOn;

	public bool fighterMissile;

//
	public void Seek(Transform target)

	{

		//if (lockOnString == "No")
		if(!lockOn)
		{
			NoSeek(target.position);
			return;
		}

		lockOn = true;
		//hasTarget = true;
		this.target = target;
		currentDirection = target.position - transform.position;
		UpdateTarget();
		//GetComponent<SphereCollider>().enabled = false
	}
	
	public void NoSeek(Vector3 target)

	{
		//GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		//sphere.transform.position = target;
		//sphere.transform.localScale = sphere.transform.localScale / 5;
		currentDirection = target - transform.position;
		lockOn = false;
		transform.LookAt(target);
		

	}

	void Start()

	{

		am = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<AudioManager>();
		//am.playExplosion1();
		//audioSource.Play();
		//Debug.Log("lockOn: " + lockOn);
		
		//flames = (GameObject) Instantiate(flamesEffect, firePoint.position, firePoint.rotation);
		if(useLight)


		{
			flamesLight.enabled = true;
		}
		
		
		if (explosionRadius > 0f)
		{

				
			am.playMissileFire();
		}
		else
		{

			am.playBulletFire();
		}

		alive = true;

		//hasTarget = false;
		//heatSeeking = false;
	}

//
//	// Update is called once per frame
	void Update()
	{


		if (transform.position.y <= 0)
		{
			alive = false;
			Destroy(gameObject);
			return;
		}

		speed = speed +(speed*.07f);
		if (speed >= maxSpeed)
		{
			speed = maxSpeed;
		}
		

		if (target == null)
		{
			//Destroy(flames, .1f);
			//Destroy(gameObject);
			//return; //Takes a while to process and this prevents it from going further down below
			//target = GameObject.FindGameObjectsWithTag(enemyTag)[0].transform;


			if (!lockOn)
			{

				speed *= 2;				
				float distanceThisFrame2 = speed * Time.deltaTime;
				//Vector3 dir2 = target.position - transform.position;
				transform.Translate(currentDirection.normalized * distanceThisFrame2, Space.World);
				//Debug.Log("!lockOn");
				
				

				return; // check collisions
			}
			else
			{
				
				
				float distanceThisFrame2 = speed * Time.deltaTime;
				//Vector3 dir2 = target.position - transform.position;
				transform.Translate(currentDirection.normalized * distanceThisFrame2, Space.World);
				
				//Debug.Log("explode");
				//target = 
				////////HitTarget();
				
				//Explode();
				return;
			}

			//if (!UpdateTarget())
			{
			//	return;
			}
		}



		Vector3 dir = target.position - transform.position;
		currentDirection = dir;
		
		
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)

		{

			HitTarget();
			return;
		}

		//flames.transform.position = transform.position;
		//flames.transform.rotation = transform.rotation;

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);


		//if (flamesEffect != null)
		{
			//GameObject effectIns = (GameObject) Instantiate(flamesEffect, firePoint.position, firePoint.rotation);
			//Destroy(effectIns, 5f);
		}
	}

	private void HitTarget()
	{
		
		
		
		if (target != null)
		{
			//AudioMana

			if (!alive)
			{
				return;
			}

			GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
			
			
			//effectIns.GetComponent<ParticleSystem>().colorOverLifetime.color.gradient.
/*
			ParticleSystem ps = effectIns.transform.GetChild(0).GetComponent<ParticleSystem>();
			
			
			Debug.Log(effectIns.transform.GetChild(0).GetComponent<ParticleSystem>().main.st);

			//ParticleSystem ps = getComponent<ParticleSystem>();
			var psMain = ps.main;
			var grad = psMain.startColor.gradient;
			grad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );

                 
			psMain.startColor = grad;
			*/

			var targetColor = target.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0]
				.GetColor("_BaseColor");


			if (explosionRadius <= 0)
			{
				ParticleSystem ps = effectIns.transform.GetChild(0).GetComponent<ParticleSystem>();
				var col = ps.colorOverLifetime;
				col.enabled = true;

			
	
				Gradient grad = new Gradient();
				//grad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.black, 0.0f), new GradientColorKey(target.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].GetColor("_BaseColor"), 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );
				grad.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.black, 0.0f), new GradientColorKey(targetColor, .2f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 0.7f), new GradientAlphaKey(0.6f, 1.0f) } );

				col.color = grad;

			}
			else
			{
				
				
				ParticleSystem ps = effectIns.transform.GetChild(0).GetComponent<ParticleSystem>();
				var col = ps.colorOverLifetime;

				col.enabled = true;

			

				Gradient grad = new Gradient();
				
				grad.SetKeys( new GradientColorKey[] { new GradientColorKey(targetColor, 0.0f), new GradientColorKey(targetColor, .2f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.6f, 1.0f) } );

				col.color = grad;
				
				
				ps.GetComponent<Renderer>().materials[1].SetColor("_BaseColor", target.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].GetColor("_BaseColor"));



			}




			float timeToDestroy = 2f;

			if (fighterMissile)
			{
				timeToDestroy = 5f;
			}


			Destroy(effectIns, timeToDestroy);

			if (explosionRadius > 0f)
			{
				Explode();
				
				if (fighterMissile)
				{
					am.playFighterExplosion1();
					//Camera.main.GetComponent<CameraShake>().
				}
				else
				{
				
					am.playExplosion1();
				
				}
				

			}
			else
			{
				Damage(target);
				am.playBulletHit();
			}

			//Destroy(flames, 1f);
			if(useLight)
			{
				flamesLight.enabled = false;
			}
			
		}


		Destroy(gameObject);
		
	}

	private void Explode()
	{
		//Debug.Log("COLLISION SHOULD PLAY");
		Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);


		//bool playAudio = false;
		foreach (Collider collider in hitObjects)

		{


			if (collider.tag == "Enemy")

			{

				//EnemyMovement e = collider.gameObject.GetComponent<EnemyMovement>();
				//if (!e.deathAudioPlayed)
				{
					//playAudio = true;
					//e.deathAudioPlayed = true;
				}

				Damage(collider.transform);

				
			}
		}

		//if (playAudio)
		{
			//am.playExplosion1();
		}

		alive = false;

	}


	void Damage(Transform enemy)

	{
		EnemyMovement e = enemy.GetComponent<EnemyMovement>();

		if (e != null)
		{
			e.TakeDamage(damage);
			
		}

		//Destroy(enemy.gameObject);	
	}


	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,explosionRadius);
		
	}
	
	bool UpdateTarget ()
	{
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

		if (nearestEnemy != null )

		{
			target = nearestEnemy.transform;
		}
		else
		{
			
			//Destroy(flames, .001f);
			//Destroy(flames);
			Destroy(gameObject);
			return false; //Takes a while to process and this prevents it from going further down below
		}

		return true;

	}


	private void OnTriggerEnter(Collider other)
	{



		if(!lockOn)
		{
			explosionRadius *= 2;
			damage = 1000f;
			//Debug.Log("HITTTTTTTTTTTTTTTTTTTT: " + other.gameObject.name);
			this.target = other.transform;
			HitTarget();

		}
		
		if(target == null)
		{
			//explosionRadius *= 2;
			//damage = 1000f;
			//Debug.Log("HITTTTTTTTTTTTTTTTTTTT: " + other.gameObject.name);
			this.target = other.transform;
			HitTarget();

		}


	}
	
	


	void SetHeatSeeking(bool b)
	{
		//heatSeeking = b;
		
	}
	
	
	
	public void SetDamage(float d)
	{
		
		//Debug.Log("passedInDmg: "+ d);
		damage = d;

	}
	
	public float GetDamage()
	{
		return damage;

	}
	


}
