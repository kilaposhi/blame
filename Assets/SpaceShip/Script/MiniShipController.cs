using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniShipController : MonoBehaviour
{
    public Transform spaceShipModel; // The ship model to track the rotation of
    private Quaternion rotationOffset; 
    private Vector3 lastRotationEuler; // Tracks the last rotation of the ship in Euler angles

        
    void Start()
    {
        // Initialize the last rotation to match the ship's current rotation
        lastRotationEuler = spaceShipModel.rotation.eulerAngles;
   
    }
    // Update is called once per frame
    void Update()
    {
        // Calculate the delta rotation based on the ship's current and last rotation
        Vector3 currentRotationEuler = spaceShipModel.rotation.eulerAngles;
        Vector3 deltaRotation = currentRotationEuler - lastRotationEuler;

        // Adjust delta rotation 
        deltaRotation.x *= -1; // Invert pitch (up/down movement)

        // Apply the delta rotation to the mini ship
        transform.Rotate(deltaRotation);
        
        // update the last rotation
        lastRotationEuler = currentRotationEuler;
    }
}
