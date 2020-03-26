using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{


    public AudioSource explosion1;
    public AudioSource missleExplosion1;
    public AudioSource laser;
    
    public AudioSource missileFire;
    public AudioSource missileFighterHit;
    public AudioSource laserHit;
    
    public AudioSource bulletFire;
    public AudioSource bulletHit;
    
    public AudioSource music1;
    public AudioSource[] musicList;
    
    private int numSoundsBulletHit;
    private int numSoundsExplosions;
    private int numSoundsLasers;
    private int numSoundsLargeExplosions;

    public int currentMusicIndex;
    
    



    public int c;
    // Start is called before the first frame update
    void Start()
    {
        numSoundsExplosions = 0;
        numSoundsLasers = 0;
        numSoundsBulletHit = 0;
        //music1.Play();
        //currentMusicIndex = musicList.Length-1;
        currentMusicIndex = 0;
        //music1.Play();
        musicList[currentMusicIndex].Play();
        
        
        InvokeRepeating("SwitchMusic", 2f,2);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log("num sounds"  + numSoundsExplosions);



        




    }

    
    public void playFighterExplosion1()

    {

        if (numSoundsLargeExplosions < 2 )
        {
            missileFighterHit.PlayOneShot(missileFighterHit.clip);
            numSoundsLargeExplosions++;
            StartCoroutine(reduceCountBigExplosions(missileFighterHit.clip.length/3));
        }

    }

    public void playExplosion1()

    {

        if (numSoundsExplosions < 11 )
        {
            explosion1.PlayOneShot(explosion1.clip);
            numSoundsExplosions++;
            StartCoroutine(reduceCountExplosions(explosion1.clip.length/3));
        }
        else
        {
            //Debug.Log("NOT PLAYED");
        }
    }
    public void playBulletHit()

    {

        if (numSoundsBulletHit < 7 )
        {
            bulletHit.PlayOneShot(bulletHit.clip);
            numSoundsBulletHit++;
            StartCoroutine(reduceCountBulletHits(bulletHit.clip.length));
        }
        else
        {
            Debug.Log("NOT PLAYED");
        }
    }
    
    public void playBulletFire()

    {

        if (numSoundsBulletHit < 7 )
        {
            bulletFire.PlayOneShot(bulletFire.clip);
            numSoundsBulletHit++;
            StartCoroutine(reduceCountBulletHits(bulletFire.clip.length));
        }
        else
        {
            //Debug.Log("NOT PLAYED");
        }
    }
    
    public void playMissileFire()

    {

        if (numSoundsExplosions < 10 )
        {
            missileFire.PlayOneShot(missileFire.clip);
            numSoundsExplosions++;
            StartCoroutine(reduceCountExplosions(missileFire.clip.length));
        }
        else
        {
            //Debug.Log("NOT PLAYED");
        }
    }
    
    

    
    
    public void playLaser()

    {

        if (!laser.isPlaying)
        {
            laser.Play();
            laser.loop = true;
            //laser.
        }
    }
    
    public void playLaserHit()

    {

        if (numSoundsLasers < 10 )
        {
            laserHit.PlayOneShot(laserHit.clip);
            numSoundsLasers++;
            StartCoroutine(reduceCountLasers(laserHit.clip.length/3));
        }
        else
        {
            //Debug.Log("NOT PLAYED");
        }
    }
    
    public void stopLaser()

    {

        if (laser.isPlaying)
        {

            laser.loop = false;
            laser.Pause();
        }

    }

    IEnumerator reduceCountExplosions(float length)
    {
        
        yield return new WaitForSeconds(length);

        //Debug.Log("not reached");
        numSoundsExplosions--;
        
    }
    IEnumerator reduceCountBigExplosions(float length)
    {
        
        yield return new WaitForSeconds(length);

        //Debug.Log("not reached");
        numSoundsLargeExplosions--;
        
    }
    
    IEnumerator reduceCountLasers(float length)
    {
        
        yield return new WaitForSeconds(length);

        //Debug.Log("not reached");
        numSoundsLasers--;
        
    }
    
    IEnumerator reduceCountBulletHits(float length)
    {
        
        yield return new WaitForSeconds(length);

        //Debug.Log("not reached");
        numSoundsBulletHit--;
        
    }
    
    
    
    
    
    void SwitchMusic()
    {
        //Debug.Log("currentIndex Music: " + currentMusicIndex);
        if (!musicList[currentMusicIndex].isPlaying)
        {
            //currentMusicIndex++;
            //Debug.Log("played");
            
            //currentMusicIndex = (currentMusicIndex++) % musicList.Length;
            //(++currentMusicIndex) % (musicList.Length - 1);

            currentMusicIndex = (++currentMusicIndex % (musicList.Length - 1));
            
            //if (currentMusicIndex > musicList.Length - 1)
            {
                //currentMusicIndex = 0;
            }
            
            
            musicList[currentMusicIndex].Play();



        }
        else
        {
            //Debug.Log("is not played: " + currentMusicIndex);
                
            
        }
        
        



        //Debug.Log("not reached");
        //numSoundsBulletHit--;
        
    }



}
