using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Book1Collision : MonoBehaviour
{
    public GameObject bookCanvas;
    
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with book");
            bookCanvas.gameObject.SetActive(true);
        }
    }
}