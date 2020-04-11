using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public int stage;
    public Text buildText;
    public Text upgradeText;
    public Text shootText;
    public Transform[] positions;
    // Start is called before the first frame update
    void Start()
    {
        stage = 0;
        SetStage(stage);
        StartCoroutine(TutorialTimeout());
    }

    // Update is called once per frame
    void Update()
    {
        if (stage == 3)
        {
            return;
        }
        
        if (stage == 2 && Floor.overFloor)
        {
            stage = 3;
            StartCoroutine(disable());
        }
    }

    public void SetStage(int i)
    {
        stage = i;
        transform.position = positions[i].position;

        if (i == 1)
        {
            ActivateUpgrade();
        }
        else if (i == 2)
        {
            ActivateShoot();
        }
    }
    
    public void ActivateUpgrade()
    {
        buildText.gameObject.SetActive(false);
        upgradeText.gameObject.SetActive(true);
    }
    public void ActivateShoot()
    {
        upgradeText.gameObject.SetActive(false);
        shootText.gameObject.SetActive(true);
    }
    
    IEnumerator disable()
    {
        yield return new WaitForSeconds(2f);
        WaveSpawner.tutorialComplete = true;
        gameObject.SetActive(false); 
    }

    IEnumerator TutorialTimeout()
    {
        yield return new WaitForSeconds(30);
        stage = 3;
        StartCoroutine(disable());
    }
}
