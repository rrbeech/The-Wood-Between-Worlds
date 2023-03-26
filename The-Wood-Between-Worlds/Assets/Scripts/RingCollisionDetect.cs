using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollisionDetect : MonoBehaviour
{ 
    public bool ringTouched = false;
    private AudioSource ringCollision;

    private void Start()
    {
        ringCollision = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
        {     
        if (other.CompareTag("Player"))
            {
            ringCollision.Play();
            ringTouched = true;
            }
        }

    private void OnTriggerExit(Collider other)
        {      
        ringTouched = false;
        }
}
