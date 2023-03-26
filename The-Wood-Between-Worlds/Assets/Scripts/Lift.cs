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

    private bool onThePlatform = false;  // Are we standing on the platform?                                      
    private const string destinationScene = "Charn";  //Destination Scene.
    private IEnumerator coroutine;

    // time after this script initializes, in seconds,
    // that the scene transition will happen
    private const float TIME_LIMIT = 10F; //seconds
    public GameObject greenRing;
    private bool ringTouched;

    private void Awake()
    {
        woodExit = GameObject.Find("Stargate").GetComponent<Animator>(); //Animation for the Stargate
        greenRing = GameObject.Find("Ring-Green");
        drop = GetComponent<Animator>(); //Drop below the surface of the pond - animation on THIS game object
    }

    private void OnTriggerEnter(Collider other) //If player moves onto the platform while touching green ring
    {
        //Debug.Log("OnTriggerEnter occured");
        ringTouched = greenRing.GetComponent<RingCollisionDetect>().ringTouched;

        if (other.CompareTag("Player") &&  //If colliding with Player (hands)
            (onThePlatform == false) && //if this is the first time the lift has been triggered
            ringTouched) //We are touching the green ring
        {
            onThePlatform = true; //set flag so we don't drop a second time.
            ExitTheWoods();            
        }
    }

    private void OnTriggerExit(Collider other)
    {      
            onThePlatform = false; //set flag so we don't drop a second time    
    }

    private IEnumerator SceneLoader()
    {
            yield return new WaitForSeconds(TIME_LIMIT);
            SceneManager.LoadScene(destinationScene);
    }

    void ExitTheWoods() //Do the stuff to exit this scene
    {
        drop.Play("goDown", 0, 0.0f);
        woodExit.Play("WoodExit", 0, 0.0f);
        AudioManager.instance.SwapTrack(newTrack); //Play the transition Sound effect

        coroutine = SceneLoader(); //Wait TIME_LIMIT seconds then switch scenes
        StartCoroutine(coroutine);
    }
    /*
    private void Update() // if already on the platform THEN player touches green ring...
    {
        if(ringTouched && onThePlatform)
            ExitTheWoods();
    }
    */
}