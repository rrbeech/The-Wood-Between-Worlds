using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XRLocationManager : MonoBehaviour
{

    private void Start()
    {
        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the StartPositionManager singleton has a saved start position for the current scene
        if (StartPositionManager.Instance.startPositions.ContainsKey(currentSceneName))
        {
            Debug.Log("A start position found for scene " + currentSceneName);
            // If a saved start position exists, set the position of the XR origin to the saved value
            Vector3 startPosition = StartPositionManager.Instance.startPositions[currentSceneName];
            transform.position = startPosition;
        }
        else
        {
            Debug.Log("No start position found for scene " + currentSceneName);
        }
    }
}