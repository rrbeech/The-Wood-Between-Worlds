using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glimmer : MonoBehaviour
{

    public ParticleSystem particles;

    private void onTriggerEnter(Collider other)
    {
        if (other.tag == "Rabbit")
        {
            particles.Play();
        }
    }

    private void onTriggerExit(Collider other)
    {
        if (other.tag == "Rabbit")
        {
            particles.Stop();
        }
    }
}
