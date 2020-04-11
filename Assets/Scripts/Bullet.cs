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
	public GameObject impactEffect;
	public float explosionRadius = 0f;
	public Transform firePoint;
	public string enemyTag = "Enemy";
	public float damage = 5f;
	public Light flamesLight;
	public bool useLight = false;
	public Vector3 currentDirection;
	public string lockOnString;
	public AudioSource audioSource;
	private AudioManager am;
	private bool alive;
	public bool lockOn;
	public bool fighterMissile;
	
	public void Seek(Transform target)
	{
		if(!lockOn)
		{
			NoSeek(target.position);
			return;
		}

		lockOn = true;
		this.target = target;
		currentDirection = target.position - transform.position;
		UpdateTarget();
	}
	
	public void NoSeek(Vector3 target)

	{
		currentDirection = target - transform.position;
		lockOn = false;
		transform.LookAt(target);
	}

	void Start()
	{
		am = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<AudioManager>();
		
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
	}
	
	// Update is called once per frame
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
			if (!lockOn)
			{
				speed *= 2;				
				float distanceThisFrame2 = speed * Time.deltaTime;
				transform.Translate(currentDirection.normalized * distanceThisFrame2, Space.World);
				return; // check collisions
			}
			else
			{
				float distanceThisFrame2 = speed * Time.deltaTime;
				transform.Translate(currentDirection.normalized * distanceThisFrame2, Space.World);
				return;
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

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}

	private void HitTarget()
	{
		if (target != null)
		{
			if (!alive)
			{
				return;
			}

			GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

			var targetColor = target.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0]
				.GetColor("_BaseColor");
			
			if (explosionRadius <= 0)
			{
				ParticleSystem ps = effectIns.transform.GetChild(0).GetComponent<ParticleSystem>();
				var col = ps.colorOverLifetime;
				col.enabled = true;
				Gradient grad = new Gradient();
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
			
			if(useLight)
			{
				flamesLight.enabled = false;
			}
		}
		Destroy(gameObject);
	}

	private void Explode()
	{
		Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
		
		foreach (Collider collider in hitObjects)
		{
			if (collider.tag == "Enemy")
			{
				Damage(collider.transform);
			}
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
			this.target = other.transform;
			HitTarget();
		}
		
		if(target == null)
		{
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
		damage = d;
	}
	
	public float GetDamage()
	{
		return damage;
	}
}
