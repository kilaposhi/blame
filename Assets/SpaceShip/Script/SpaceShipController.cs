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

    public float moveSpeedMultiplier = 25f;
    public float accelerationTime = 3f; // Time to reach final speed
    public float decelerationTime = 4f; // Time to stop


    public float verticalSpeedMultiplier = 4f;
    public float horizontalSpeedMultiplier = 3f;


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



    void Start(){
        shipModelCollider = GetComponentInChildren<Collider>();

        joystick.onValueChangeX.AddListener(SetMoveX); 
        joystick.onValueChangeY.AddListener(SetMoveY);
        moveSpeedSlider.onValueChange.AddListener(SetMoveSpeed);
    }

    void FixedUpdate() {
        HandleMovement();
        HandleSpeed();
    }

    void HandleMovement(){
        //Debug.Log("Movevalue X:" + moveValueX);
        //Debug.Log("Movevalue Y :" + moveValueY);

        if (currentSpeed == 0)
        {
            return;
        }

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
    }

    void HandleSpeed()
    {
        if (currentSpeed == targetSpeed)
        {
            return;
        }

        // Delay time to use
        float smoothTime = targetSpeed > currentSpeed ? accelerationTime : decelerationTime;

        moveFactor += Time.deltaTime / smoothTime;
        // Debug.Log("LerpFactor"+ MoveFactor);
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
    
    // To test movement
    void OnMove(InputValue value){
        Vector2 moveValue = value.Get<Vector2>();
        moveValueX = moveValue.x;
        moveValueY = moveValue.y;
        // Debug.Log("Move: " + moveValue);
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

    void SetMoveSpeed(float speedInput)
    {
        targetSpeed = speedInput * moveSpeedMultiplier;       
    }

}
