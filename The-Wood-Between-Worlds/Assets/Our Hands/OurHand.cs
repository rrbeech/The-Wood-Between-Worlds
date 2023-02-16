using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



public class OurHand : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ourHandPrefab;
    public InputDeviceCharacteristics ourControllerCharacteristics;

    private InputDevice ourDevice;
    private Animator ourHandAmimator;

    void Start()
    {
        InitializeOurHand();
    }

    void InitializeOurHand()
    {
        //Check for our controller characteristics
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(ourControllerCharacteristics, devices);

        //If Device identified, Instantiate a Hand
        if(devices.Count > 0)
        {
            ourDevice = devices[0];
            GameObject newHand = Instantiate(ourHandPrefab, transform);
            ourHandAmimator = newHand.GetComponent<Animator>(); //get the Animator component from newHand.
        }


    }

    // Update is called once per frame
    void Update()
    {
        //Change Animate position or re-initialize
        if (ourDevice.isValid)
        {
            UpdateOurHand();
        }
        else
        {
            InitializeOurHand();
        }
    }

    void UpdateOurHand()
    {
        if (ourDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            //Debug.Log("Trigger Value = " + triggerValue);
            ourHandAmimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            //Debug.Log("Trigger not Active");
            ourHandAmimator.SetFloat("Trigger",0);
        }

        if (ourDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            //Debug.Log("Grip Value = " + gripValue);
            ourHandAmimator.SetFloat("Grip", gripValue);
        }
        else
        {
            //Debug.Log("Grip not Active");
            ourHandAmimator.SetFloat("Grip", 0);
        }
    }
    
}
