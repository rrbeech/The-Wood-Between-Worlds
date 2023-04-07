using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerCharn : MonoBehaviour
{
    public AudioClip defaultAmbience; //background music
    public AudioClip transitionSound;
    public AudioClip transitionExitSound;
    public float transitionVolume;
    public float ambientDelay;
    private AudioSource track1, track2;
    private bool isPlayingTrack1 = true;


    public static AudioManagerCharn instance;

    private void Awake() //not sure what this does
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start() // Assign initial tracks and play them
    {
        track1 = gameObject.AddComponent<AudioSource>();
        track2 = gameObject.AddComponent<AudioSource>();
        track1.loop = false;
        track2.loop = true;


        track1.clip = transitionSound; /// play the Transition sound at the start
        track1.volume = transitionVolume;
        track1.Play();
        track2.clip = defaultAmbience;  //background music   
        track2.PlayDelayed(ambientDelay);
    }

    private void PlayExitSound() //stop playing ambient music and play trasit
    {
        SwapTrack(transitionSound);
    } 

    public void SwapTrack(AudioClip newClip)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip));
        //isPlayingTrack1 = !isPlayingTrack1;
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
    }

}
    