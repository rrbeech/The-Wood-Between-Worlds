using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class triggerScript : MonoBehaviour
{
    private AudioSource newTrack;
    private Collider soundTrigger;

    private void Awake()
    {
        newTrack = GetComponent<AudioSource>();
        soundTrigger = GetComponent<BoxCollider>();
    }


    void OnTriggerEnter(Collider other)
    {
        newTrack.Play();
        //Debug.Log("Crossed the trigger");
    }
    
}
