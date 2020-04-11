using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;
    public float START_HEALTH;
    public  float startHealth  = 100f;
    private float health;
    public int value = 50;
    public GameObject DestroyEffect;
    public bool alive = false;
    public bool deathAudioPlayed;
    private BuildManager buildManager;
    
    [Header("Unity Stuff")] public Image healthBar;

    public void Start()

    {
        if (WaveSpawner.waveIndex >= 80)
        {
            int difference = WaveSpawner.waveIndex - 80;
            startHealth = START_HEALTH + (((80-difference)*(80-difference))/START_HEALTH)*((80-difference)*2);
        }
        else
        {
            startHealth = START_HEALTH + ((WaveSpawner.waveIndex*WaveSpawner.waveIndex)/START_HEALTH)*(WaveSpawner.waveIndex*2);
        }
        
        health = startHealth;
        speed = startSpeed;
        alive = true;
        deathAudioPlayed = false;
        speed = speed + (WaveSpawner.waveIndex * .05f);
        buildManager = BuildManager.instance;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            DestroyGameObject();
        }
    }

    void DestroyGameObject()
    {
        if(alive)
        {
            WaveSpawner.enemiesAlive -= 1;
            alive = false;
            PlayerStats.Money += value;
            GameObject.FindGameObjectWithTag("gamemanager").GetComponent<PlayerStats>().SetChargedBar(.07f);
            GameObject effect = Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2.5f);
            Destroy(gameObject);
        }
    }

    public void Slow(float slowReduction)
    {
        speed = startSpeed * slowReduction;
    }
    
    void OnMouseDown()
    {
        if (IsPointerOverUIObject())
        {
            Floor.overFloor = false;
            return;
        }

        if (buildManager.nodeComponent)
        {
            buildManager.DeselectNode(0f);
        }
        
        Floor.overFloor = true;
        Floor.onlyOverFloor = true;
    }
    
    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
