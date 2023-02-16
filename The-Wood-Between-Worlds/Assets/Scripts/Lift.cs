using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Lift : MonoBehaviour
{
    [SerializeField] private Animator drop = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            drop.Play("Down", 0, 0.0f);
        }

    }
}
