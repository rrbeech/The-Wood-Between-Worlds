using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Lift : MonoBehaviour
{
    [SerializeField] private Animator drop = null;

    private void Awake()
    {
        drop = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter occured");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Playing animation");
            drop.Play("Down", 0, 0.0f);
        }

    }
}
