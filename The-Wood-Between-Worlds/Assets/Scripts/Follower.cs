using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 1.0f;
    float distanceTravelled;


    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);

        // this line of code not needed as it will change the rotation of the rodent
        //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled); 
    }
}