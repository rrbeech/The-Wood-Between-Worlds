using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerCharn : MonoBehaviour
{
    public AudioClip defaultAmbience; //background music
    public AudioClip transitionSound;
    public float transitionVolume;
    public float ambientDelay;
    private AudioSource track1, track2;


    public static AudioManagerCharn instance;

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
        track1.loop = false;
        track2.loop = true;


        track1.clip = transitionSound; /// play the Transition sound at the start
        track1.volume = transitionVolume;
        track1.Play();
        track2.clip = defaultAmbience;  //background music   
        track2.PlayDelayed(ambientDelay);
    }
}
    