using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;

public class SpaceShipController : MonoBehaviour
{

    public XRSlider moveSpeedSlider;
    public XRJoystick joystick;

    public float moveSpeedMultiplier = 70f;
    public float accelerationTime = 8f;
    public float decelerationTime = 11f;


    public float verticalSpeedMultiplier = 13f;
    public float horizontalSpeedMultiplier = 8f;


    public float topCeilingY = 200f;
    public float lowCeilingY = -200f; 
    public float leftWallX = -20f;
    public float rightWallX = 20f;



    private float moveValueX = 0f;
    private float moveValueY = 0f;
    private Collider shipModelCollider;
    private float currentSpeed = 0f;
    private float targetSpeed = 0f;
    private float moveFactor = 0f;
    private AudioSource audioSource;
    private Rigidbody rb;



    void Start(){
        shipModelCollider = GetComponentInChildren<Collider>();
        rb = GetComponentInChildren<Rigidbody>();

        joystick.onValueChangeX.AddListener(SetMoveX); 
        joystick.onValueChangeY.AddListener(SetMoveY);
        moveSpeedSlider.onValueChange.AddListener(SetMoveSpeed);
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        HandleMovement();
        HandleSpeed();
    }

    // For the Animation controller
    public bool IsShipStill()
    {
        return currentSpeed == 0f;
    }
    void HandleMovement(){
        // if (currentSpeed == 0)
        // {
        //     return;
        // }

        Vector3 newPosition = transform.position;
        // Move the ship
        newPosition += -transform.right * moveValueX * horizontalSpeedMultiplier * Time.deltaTime;
        newPosition += transform.up * moveValueY * verticalSpeedMultiplier * Time.deltaTime;


        // Under the LOW ceiling we teleport the player to: just under the TOP ceiling
        if(newPosition.y < lowCeilingY)
        {
            newPosition = new Vector3(transform.position.x, topCeilingY-0.3f, transform.position.z) ;
        }
        // Above the TOP ceiling we teleport the player to: just above the LOW ceiling
        else if(newPosition.y > topCeilingY)
        {
            newPosition = new Vector3(transform.position.x, lowCeilingY+0.3f, transform.position.z) ;
        }
        
        // Stop the ship from going out of bounds
        float rotatedExtentX = GetRotatedExtentX();
        newPosition.x = Mathf.Clamp(newPosition.x, leftWallX + rotatedExtentX, rightWallX - rotatedExtentX);
        
        // Apply the new position
        transform.position = newPosition;
        // rb.MovePosition(newPosition);
    }

    void HandleSpeed()
    {
        if (currentSpeed == targetSpeed)
        {
            return;
        }

        // Delay time to use
        float smoothTime;
        if (targetSpeed > currentSpeed)
        {
            smoothTime = accelerationTime;
            audioSource.Play(); // Play the Acceleration Sound
        }
        else
        {
            smoothTime = decelerationTime;
        }

        // To make the ship accelarate/decelerate more and more
        moveFactor += Time.deltaTime / smoothTime;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, moveFactor);

        // Reset Lerp Factor when end of acceleration/decelaration
        if (Mathf.Abs(currentSpeed - targetSpeed) < 0.01f)
        {
            currentSpeed = targetSpeed;
            moveFactor = 0;
        }

        // Debug.Log("Move Speed: " + currentSpeed);
        SpeedManager.Instance.CurrentSpeed = currentSpeed;
    }



    // Get the max x position of the ship when rotated
    private float GetRotatedExtentX()
    {
        // Get the bounds of the collider in world space
        Bounds bounds = shipModelCollider.bounds;

        // We consider only the top-front-left and top-front-right corners of the bounds
        Vector3 topFrontLeftCorner = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        Vector3 topFrontRightCorner = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);

        // Transform corners to local space to calculate X extents
        Vector3 localLeftCorner = transform.InverseTransformPoint(topFrontLeftCorner);
        Vector3 localRightCorner = transform.InverseTransformPoint(topFrontRightCorner);
        
        // return the maximum absolute X value
        return Mathf.Max(Mathf.Abs(localLeftCorner.x), Mathf.Abs(localRightCorner.x));
    }
    
    // To test movement with Keyboard
    void OnMove(InputValue value){
        Vector2 moveValue = value.Get<Vector2>();
        moveValueX = moveValue.x;
        moveValueY = moveValue.y;
    }

    void SetMoveX(float value)
    {
        moveValueX = value;
        // right [0, -1]
    }

    void SetMoveY(float value)
    {
        moveValueY = value;
        // up [0, 1]
    }

    void SetMoveSpeed(float speedInput) // speedInput [0, 1]
    {
        targetSpeed = speedInput * moveSpeedMultiplier;       
    }

}
