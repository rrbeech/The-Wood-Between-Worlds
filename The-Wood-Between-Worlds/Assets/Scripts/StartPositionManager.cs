using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionManager : MonoBehaviour
{
    // A singleton instance of the StartPositionManager class. static things will be shared   
    // across all instances of a class.
    public static StartPositionManager Instance;

    // A dictionary that maps scene names to start positions
    public Dictionary<string, Vector3> startPositions = new Dictionary<string, Vector3>();

    private void Awake()
    {
        // If an instance of the StartPositionManager class doesn't exist, set it to this instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // If an instance already exists, destroy this game object to ensure only one instance is created
            Destroy(gameObject);
        }

        // Mark this game object as persistent between scenes
        DontDestroyOnLoad(gameObject);
    }
}