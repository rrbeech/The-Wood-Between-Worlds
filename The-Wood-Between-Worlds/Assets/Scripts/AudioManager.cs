using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip defaultAmbience; //background music
    public AudioClip transitionSound;
    private AudioSource track1, track2;
    private bool isPlayingTrack1;

    public static AudioManager instance;

    private void Awake() //not sure what this does
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start() // Assign initial tracks
    {
        track1 = gameObject.AddComponent<AudioSource>();
        track2 = gameObject.AddComponent<AudioSource>();
        track1.loop = true;
        track2.loop = true;
        isPlayingTrack1 = true;
             
        track1.clip = defaultAmbience; /// play the ambient track (if any) at the Start 
        track1.Play();
        track2.clip = transitionSound;  //transporter sound effect     
    }

    public void SwapTrack(AudioClip newClip)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip));

        isPlayingTrack1 = !isPlayingTrack1;
    }
    
    public void ReturnToDefault()
    {
        SwapTrack(defaultAmbience);
    }
    
    private IEnumerator FadeTrack(AudioClip newClip)
    {
        float timeToFade = 01.25f;
        float timeElapsed = 0, pct;

        if (isPlayingTrack1) //If default Ambience it playing...
        {
            track2.clip = newClip;
            track2.Play();
            while (timeElapsed < timeToFade)
            {
                pct = timeElapsed / timeToFade;
                track2.volume = Mathf.Lerp(0, 1, pct);
                track1.volume = Mathf.Lerp(1, 0, pct);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
                track1.Stop();
        }

        /*
        else   //Returning to the woods (don't need this for now)
        {
            track1.clip = newClip;
            track1.Play();
            while (timeElapsed < timeToFade)
            {
                pct = timeElapsed / timeToFade;
                track1.volume = Mathf.Lerp(0, 1, pct);
                track2.volume = Mathf.Lerp(1, 0, pct);
                timeElapsed += Time.deltaTime;
                yield return null;
                
            }
            track2.Stop();

            //Stop interior (burning Bush) dialog tracks
            //GodsPart.Stop();
            //MosesPart.Stop();

        }
        */
        
    }


}
