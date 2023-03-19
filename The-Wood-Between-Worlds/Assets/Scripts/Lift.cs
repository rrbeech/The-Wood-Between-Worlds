using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;


public class Lift : MonoBehaviour
{
    public AudioClip newTrack; //this will be the transition sound
    private Animator drop = null;
    private Animator Stargate1 = null;
    private Animator Stargate2 = null;
    private bool alreadyTriggered = false;  // this is so that triggering happes only once.                                        
    private const string sceneName = "Transition Scene";  //Scene name we are going to.
    private IEnumerator coroutine;

    // time after this script initializes, in seconds,
    // that the scene transition will happen
    private const float TIME_LIMIT = 10F; //seconds

    private void Awake()
    {
        Stargate1 = GameObject.Find("Stargate").GetComponent<Animator>();
        //Stargate2 = GameObject.Find("Star Gate Number two (1)").GetComponent<Animator>();

        drop = GetComponent<Animator>(); //Drop below the surface of the pond
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter occured");
        if (other.CompareTag("Player") && alreadyTriggered == false) //if this is the first time the lift has been triggered...
        {
            //Debug.Log("Playing animation NOW");

            drop.Play("goDown", 0, 0.0f); 
            Stargate1.Play("Stargate", 0, 0.0f);
            AudioManager.instance.SwapTrack(newTrack); //Play the transition Sound effect
           
            alreadyTriggered = true; //set flag so we don't drop a second time.

            coroutine = SceneLoader(); //Wait TIME_LIMIT seconds then switch scenes
            StartCoroutine(coroutine);
        }
    }


    private IEnumerator SceneLoader()
    {
            yield return new WaitForSeconds(TIME_LIMIT);
            SceneManager.LoadScene(sceneName);
    }
}