using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip defaultAmbience; //might not need this
    private AudioSource track1, track2, GodsPart, MosesPart;
    public GameObject God; //game object that has the audio clip component we want to play
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
        
        GodsPart = God.GetComponent<AudioSource>();
   
        Debug.Log("GodsPart.clip = " + GodsPart.clip);
        
        track1.clip = defaultAmbience; /// play the ambient track (if any) at the Start 
        track1.Play();
     
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

        if (isPlayingTrack1) //Entering the Grotto
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

            //Play interior (Burning Bush) dialog tracks
            GodsPart.Play();
            MosesPart.Play();

        }
        else   //Exiting the Grotto
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
            GodsPart.Stop();
            MosesPart.Stop();

        }
        
    }


}
