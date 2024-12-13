using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraControls : MonoBehaviour
{
    // des contrôles de caméra de bases pour tester les mécaniques de téléportation 
    public float moveSpeed = 50f; // Speed of the camera movement up and down
    public float speedIncrease = 15f ; // Acceleration when hitting space
    public float slowThreshold = 5f ;
    public float horizontalSpeed = 10f; 
    public float topCeiling = 200f;
    public float lowCeiling = -200f; 

    // Update is called once per frame
    void Update()
    {
        
        // Get the input from the legacy input system
        float horizontal = 0f; // Default to no horizontal movement
        float vertical = 0f;   // Default to no vertical movement

        // Check for input and adjust movement values
        if (Input.GetKey(KeyCode.Z))
        {
            vertical = 1f; // Move up
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1f; // Move down
        }

        if (Input.GetKey(KeyCode.Q))
        {
            horizontal = -1f; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f; // Move right
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            SpeedManager.Instance.CurrentSpeed += speedIncrease * Time.deltaTime; // acceleration
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // must check that decceleration don't go below a certain threshold of slowness
            if(SpeedManager.Instance.CurrentSpeed > slowThreshold)
            {
                SpeedManager.Instance.CurrentSpeed -= speedIncrease * Time.deltaTime; // decceleration
            }
        }

        // Calculate the translation vector
        Vector3 translation = new Vector3(horizontal, vertical, 0f);

        // Apply the movement to the camera
        transform.Translate(translation * moveSpeed * Time.deltaTime);


        // Under the LOW ceiling we teleport the player to: just under the TOP ceiling
        if(transform.position.y < lowCeiling)
        {
            transform.position = new Vector3(transform.position.x, topCeiling-0.3f, transform.position.z) ;
        }
        // Above the TOP ceiling we teleport the player to: just above the LOW ceiling
        else if(transform.position.y > topCeiling)
        {
            transform.position = new Vector3(transform.position.x, lowCeiling+0.3f, transform.position.z) ;
        }
    }
}
