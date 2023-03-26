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

    public bool onThePlatform = false;  // Are we standing on the platform?                                      
    public string destinationScene = "Charn";  //Destination Scene.
    private IEnumerator coroutine;

    // time after this script initializes, in seconds,
    // that the scene transition will happen
    private const float TIME_LIMIT = 10F; //seconds
    private GameObject greenRing;  //Only the green rings will draw player out of the Wood Between Worlds.

    public bool ringTouched;

    private void Awake()
    {
        woodExit = GameObject.Find("Stargate").GetComponent<Animator>(); //Animation for the Stargate
        greenRing = GameObject.Find("Ring-Green");
        drop = GetComponent<Animator>(); //Drop below the surface of the pond - animation on THIS game object
    }

    private void OnTriggerEnter(Collider other) //If player moves onto the platform while touching green ring
    {
          ringTouched = greenRing.GetComponent<RingCollisionDetect>().ringTouched;

        if (other.CompareTag("Player")) //If colliding with Player (hands)         
        {
            onThePlatform = true; //set flag 
            if (ringTouched) //exit the woods if touching the green ring
                ExitTheWoods();            
        }
    }

    private void OnTriggerExit(Collider other)
    {      
            onThePlatform = false; //No longer on the platform   
    }

    private IEnumerator SceneLoader()
    {
            yield return new WaitForSeconds(TIME_LIMIT);
            SceneManager.LoadScene(destinationScene);
    }

    void ExitTheWoods() //Do the stuff to exit this scene
    {
        drop.Play("goDown", 0, 0.0f); //lower the platform
        woodExit.Play("WoodExit", 0, 0.0f); // turn on the stargate effect
        AudioManager.instance.SwapTrack(newTrack); //Play the transition Sound effect

        coroutine = SceneLoader(); //Wait TIME_LIMIT seconds then switch scenes
        StartCoroutine(coroutine);
    }
    
    private void Update() // if already on the platform THEN player touches green ring...
    {
        ringTouched = greenRing.GetComponent<RingCollisionDetect>().ringTouched;

        if (ringTouched && onThePlatform)
            ExitTheWoods();
    }
   
}