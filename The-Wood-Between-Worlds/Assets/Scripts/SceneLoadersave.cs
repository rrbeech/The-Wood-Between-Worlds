using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadersave
    : MonoBehaviour
{
    // can be set in the Inspector for this script
    //public string sceneName;
    // alternatively, you can do something like
     private const string sceneName = "Charn";

    // time after this script initializes, in seconds,
    // that the scene transition will happen
    private const float TIME_LIMIT = 10F; //seconds

    // timer variable
    private float timer = 0F;
    // alternatively, you can set this in an Awake() function,
    // which is automatically called when the script initializes

    // automatically called many times every second
    void Update()
    {
        // deltaTime is the time (measured in seconds) since the previous Update step
        // it's typically very small, e.g. 1/60th of a second ~= 0.0167F
        this.timer += Time.deltaTime;

        // check if it's time to switch scenes
        if (this.timer >= TIME_LIMIT)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}