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
    
    

    //public ParticleSystem oneShotVfx;
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
    
    

//    public void SpawnClickedVFX()
//    {
////        if (clickedVFX != null)
////        {
////
////            var vfx = Instantiate(clickedVFX, origin.transform.position, Quaternion.identity);
////            vfx.transform.SetParent(origin.transform);
////            var ps = vfx.GetComponent<ParticleSystem>();
////            Destroy(vfx, );
////        }
//
//        //oneShotVfx.gameObject.SetActive(false);
////        oneShotVfx.gameObject.SetActive(false);
////        oneShotVfx.gameObject.SetActive(true);
////        //oneShotVfx.loop = false;
////
////
////        
//////        oneShotVfx.Stop();
////
////        //mainVfx.Stop();
////        mainVfx.Clear();
////        mainVfx.Play();
////        oneShotVfx.Clear();
////        oneShotVfx.Play();
//
//        if (isActive)
//        {
//
//            return;
//
//
//        }
//        
//        Debug.Log("PRessed vfx");
//
//
//        //mainVfx.gameObject.SetActive(false);
//        mainVfx.gameObject.SetActive(true);
//        oneShotVfx.gameObject.SetActive(true);
//
//
//
//    }


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
        //Debug.Log("SET CHARGE");

//        if (chargedBarPercent == 0)
//        {
//            chargeButton.transform.gameObject.SetActive(false);
//        }

        if (chargedBarFull)
        {
            //Debug.Log("WTF CHARGE");
            return;
        }

        chargedBarPercent += increase;

        if (chargedBarPercent >= 1) //TODO change so only the percent goes to 1 if and nothing else if isActive == true
        {
            chargedBarPercent = 1f;
            chargedBarFull = true;
            //slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = fullChargeColor;
            chargeButton.gameObject.SetActive(true);
            chargetButtonOneShot.gameObject.SetActive(false);

            if (!isActive)
            {
                fullChargeAudio.PlayOneShot(fullChargeAudio.clip);
            }


            chargeBar.GetComponent<Animation>().Play();
            
            //Debug.Log("FULLY CHARGED OVER");
            
        }

        
        if (chargedBarPercent == 0)
        {
            chargeButton.transform.gameObject.SetActive(false);
        }
        
        
        if (isActive)
        {

            return;


        }
        
        //slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color =
            Color.Lerp(originalColor, fullChargeColor, chargedBarPercent);

        
        
        //chargedBar.transform.GetComponent<ChargeBar>().set;
        //////slider.value = chargedBarPercent;
        fillAmount.fillAmount = chargedBarPercent;






        //Debug.Log("END CHARGE" + chargedBarPercent);


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

        Debug.Log("PRessed");

        isActive = true;
        
        chargeButton.GetComponent<Animation>().Stop();
        chargeButton.GetComponent<Animation>().Play();

            

        
        activateAudio.PlayOneShot(activateAudio.clip);
        
        slider.transform.GetChild(1).gameObject.SetActive(false);
        
        //chargetButtonOneShot.gameObject.SetActive(false);

        chargedBarFull = false;
        chargedBarPercent = 0f;
        
        //mainVfx.gameObject.SetActive(false);
        mainVfx.Clear();
        mainVfx.gameObject.SetActive(true);
        mainVfx.Simulate(0.0f, true, true);
        mainVfx.Play();
        //mainVfx.Clear();
        //oneShotVfx.gameObject.SetActive(true);
        chargetButtonOneShot.Clear();
        chargetButtonOneShot.gameObject.SetActive(true);
        //chargetButtonOneShot.Emit(1);
        
        chargetButtonOneShot.Simulate(0.0f, true, true);
        chargetButtonOneShot.Play();
        //chargetButtonOneShot.Clear();

        

        //////slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = originalColor;

        //////slider.value = chargedBarPercent;
        /////fillAmount.fillAmount = chargedBarPercent;
        //chargeButton.transform.gameObject.SetActive(false);


        
        StartCoroutine(deactivateChargeBar());

        GetComponent<ChargedAttacks>().CallAirStrike();


    }

    IEnumerator deactivateChargeBar()

    {
        
        yield return new WaitForSeconds(.99f);
        slider.transform.GetChild(1).gameObject.SetActive(true);
        
        
        
        chargeButton.transform.gameObject.SetActive(false);

        isActive = false;
        
        //slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color =
            Color.Lerp(originalColor, fullChargeColor, chargedBarPercent);


        fillAmount.fillAmount = chargedBarPercent;
        //chargedBar.transform.GetComponent<ChargeBar>().set;
        //////slider.value = chargedBarPercent;
        //slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().
        
        chargeBar.GetComponent<Animation>().Rewind();
        chargeBar.GetComponent<Animation>().Play();
        //chargeBar.GetComponent<Animation>().Stop();
        
        
        if (chargedBarPercent >= 1) //TODO this is the issue, its playing twice when it normall would be fine, find out why this is needed or a bool to get around this. like if bool false, the already played
        {
            chargedBarPercent = 1f;
            chargedBarFull = true;
            //slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = fullChargeColor;
            chargeButton.gameObject.SetActive(true);
            chargetButtonOneShot.gameObject.SetActive(false);
            fullChargeAudio.PlayOneShot(fullChargeAudio.clip);
            chargeBar.GetComponent<Animation>().Play();
            
            //Debug.Log("FULLY CHARGED OVER");
            
        }


    }
    
    






}
