using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;


public class Lift : MonoBehaviour
{
    public AudioClip newTrack; //this will be the transition sound
    private Animator drop = null;  //animator for the lift
    private Animator woodExit = null; //Animator for the stargate

    private bool alreadyTriggered = false;  // this is so that triggering happes only once.                                        
    private const string destinationScene = "Charn";  //Destination Scene.
    private IEnumerator coroutine;

    // time after this script initializes, in seconds,
    // that the scene transition will happen
    private const float TIME_LIMIT = 10F; //seconds

    private void Awake()
    {
        woodExit = GameObject.Find("Stargate").GetComponent<Animator>(); //Animation for the Stargate
        drop = GetComponent<Animator>(); //Drop below the surface of the pond - animation on THIS game object
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter occured");
        if (other.CompareTag("Player") && alreadyTriggered == false) //if this is the first time the lift has been triggered...
        {
            //Debug.Log("Playing animation NOW");

            drop.Play("goDown", 0, 0.0f); 
            woodExit.Play("WoodExit", 0, 0.0f);
            AudioManager.instance.SwapTrack(newTrack); //Play the transition Sound effect
           
            alreadyTriggered = true; //set flag so we don't drop a second time.

            coroutine = SceneLoader(); //Wait TIME_LIMIT seconds then switch scenes
            StartCoroutine(coroutine);
        }
    }


    private IEnumerator SceneLoader()
    {
            yield return new WaitForSeconds(TIME_LIMIT);
            SceneManager.LoadScene(destinationScene);
    }
}