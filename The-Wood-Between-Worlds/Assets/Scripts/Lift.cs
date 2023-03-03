using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;


public class Lift : MonoBehaviour
{
    private Animator drop = null;
    private bool alreadyTriggered = false;  // this is so that triggering happes only once.                                        
    private const string sceneName = "Transition Scene";  //Scene name we are going to.
    private IEnumerator coroutine;

    // time after this script initializes, in seconds,
    // that the scene transition will happen
    private const float TIME_LIMIT = 1F; //seconds

    private void Awake()
    {
        drop = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter occured");
        if (other.CompareTag("Player") && alreadyTriggered == false) //if this is the first time the lift has been triggered...
        {
            //Debug.Log("Playing animation NOW");
            drop.Play("goDown", 0, 0.0f);
            alreadyTriggered = true; //set flag so we don't drop a second time.

            coroutine = SceneLoader();
            StartCoroutine(coroutine);
        }
    }


    private IEnumerator SceneLoader()
    {
            yield return new WaitForSeconds(TIME_LIMIT);
            SceneManager.LoadScene(sceneName);
    }
}