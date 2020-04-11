using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;
    public static int Lives;
    public int startLives = 20;
    public static int rounds;
    public float upgradeMultiplier;
    public float chargedBarPercent;
    public bool chargedBarFull;
    public static GameObject chargedBar;
    public Slider slider;
    public Color originalColor;
    public Color fullChargeColor;
    public Button chargeButton;
    public ParticleSystem chargetButtonOneShot;
    public GameObject chargeBar;
    public ParticleSystem mainVfx;
    public AudioSource fullChargeAudio;
    public AudioSource activateAudio;
    public bool isActive;
    public Image fillAmount;
    public GameObject clickedVFX;
    public GameObject glowVFX;
    public GameObject UIvfx;
    public GameObject origin;
    public GameObject destination;
    
    void Start()

    {
        Money = startMoney;
        Lives = startLives;
        rounds = 0;
        upgradeMultiplier = 1f;
        chargedBarPercent = 0f;
        chargedBarFull = false;
        isActive = false;
        fillAmount.fillAmount = 0;
    }
    
    public void SetChargedBar(float increase)
    {
        if (chargedBarFull)
        {
            return;
        }

        chargedBarPercent += increase;

        if (chargedBarPercent >= 1) //TODO change so only the percent goes to 1 if and nothing else if isActive == true
        {
            chargedBarPercent = 1f;
            chargedBarFull = true;
            chargeButton.gameObject.SetActive(true);
            chargetButtonOneShot.gameObject.SetActive(false);

            if (!isActive)
            {
                fullChargeAudio.PlayOneShot(fullChargeAudio.clip);
            }
            
            chargeBar.GetComponent<Animation>().Play();
        }
        
        if (chargedBarPercent == 0)
        {
            chargeButton.transform.gameObject.SetActive(false);
        }
        
        if (isActive)
        {
            return;
        }
        
        Color.Lerp(originalColor, fullChargeColor, chargedBarPercent);
            
        fillAmount.fillAmount = chargedBarPercent;
    }
    
    
    public void ActivateChargeBar()
    {
        if (!chargedBarFull)
        {
            return;
        }
        
        if (isActive)
        {
            return;
        }

        Debug.Log("Pressed");
        isActive = true;
        chargeButton.GetComponent<Animation>().Stop();
        chargeButton.GetComponent<Animation>().Play();
        activateAudio.PlayOneShot(activateAudio.clip);
        slider.transform.GetChild(1).gameObject.SetActive(false);
        chargedBarFull = false;
        chargedBarPercent = 0f;
        mainVfx.Clear();
        mainVfx.gameObject.SetActive(true);
        mainVfx.Simulate(0.0f, true, true);
        mainVfx.Play();
        chargetButtonOneShot.Clear();
        chargetButtonOneShot.gameObject.SetActive(true);
        chargetButtonOneShot.Simulate(0.0f, true, true);
        chargetButtonOneShot.Play();
        StartCoroutine(deactivateChargeBar());
        GetComponent<ChargedAttacks>().CallAirStrike();
    }

    IEnumerator deactivateChargeBar()
    {
        yield return new WaitForSeconds(.99f);
        slider.transform.GetChild(1).gameObject.SetActive(true);
        chargeButton.transform.gameObject.SetActive(false);
        isActive = false;
        Color.Lerp(originalColor, fullChargeColor, chargedBarPercent);
        fillAmount.fillAmount = chargedBarPercent;
        chargeBar.GetComponent<Animation>().Rewind();
        chargeBar.GetComponent<Animation>().Play();

        if (chargedBarPercent >= 1) //TODO this is the issue, its playing twice when it normall would be fine, find out why this is needed or a bool to get around this. like if bool false, the already played
        {
            chargedBarPercent = 1f;
            chargedBarFull = true;
            chargeButton.gameObject.SetActive(true);
            chargetButtonOneShot.gameObject.SetActive(false);
            fullChargeAudio.PlayOneShot(fullChargeAudio.clip);
            chargeBar.GetComponent<Animation>().Play();
        }
    }
}
