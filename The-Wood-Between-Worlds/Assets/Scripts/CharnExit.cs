using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharnExit : MonoBehaviour
{
    public AudioClip TransitionSound; //this will be the transition sound
    private Animator blackCubeExit = null;  //animator for the black cube to become opaque
    private Animator charnExit = null; //Animator for the stargate

    public bool ExitInitiated = false;  // Are we exiting?                                     
    public string destinationScene = "Wood Between Worlds";  //Destination Scene.
    private IEnumerator coroutine;

    // time after this script initializes, in seconds,
    // that the scene transition will happen
    private const float TIME_LIMIT = 10F; //seconds
    private GameObject yellowRing;  //Only the Yellow rings will return Player to the Woods.
    private GameObject audioManager; //Get Audio Manager for this scene

    public bool ringTouched;

    private RingCollisionDetect yellowRingCollisionDetect;

    private void Start()
    {
        // Get the RingCollisionDetect component from the greenRing GameObject
        yellowRingCollisionDetect = yellowRing.GetComponent<RingCollisionDetect>();
    }
    private void Awake()
    {
        charnExit = GetComponent<Animator>(); //Animation for the Stargate
        yellowRing = GameObject.Find("Ring-Yellow");
        blackCubeExit = GameObject.FindWithTag("BlackCubeExit").GetComponent<Animator>(); //Drop below the surface of the pond - animation on THIS game object
        audioManager = GameObject.Find("AudioManager");//find the audio manager for this project
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!ExitInitiated) // If we are not already in the process of exiting, check the yellow ring
        {
            //ringTouched = yellowRing.GetComponent<RingCollisionDetect>().ringTouched;
            ringTouched =yellowRingCollisionDetect.ringTouched;

            if (ringTouched)
            {
                ExitInitiated = true;  //set to true so we don't check again.  We are leaving.
                ExitCharn();
            }
                
        }
    }

    void ExitCharn() //Do the stuff to exit this scene
    {
        blackCubeExit.Play("BlackCubeExit", 0, 0.0f); //Turn on the cube opacity animation
        charnExit.Play("CharnExit", 0, 0.0f); // turn on the stargate exit effect
        TransitionSound = audioManager.GetComponent<AudioManagerCharn>().transitionExitSound;//make local pointer to the transition sound
        audioManager.GetComponent<AudioManagerCharn>().SwapTrack(TransitionSound); //Play the transition Sound effect

        coroutine = SceneLoader(); //Wait TIME_LIMIT seconds then switch scenes
        StartCoroutine(coroutine);
    }


    private IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(TIME_LIMIT);
        SceneManager.LoadScene(destinationScene);
    }
}
