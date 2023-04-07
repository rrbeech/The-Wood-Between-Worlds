using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play("Door", 0, 0.0f);
        }
       
    }
}
