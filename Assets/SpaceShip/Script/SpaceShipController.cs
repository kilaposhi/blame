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
    public Collider shipModelCollider;
    public float moveSpeedMultiplicator = 25;
    public float upThrust = 4;
    public float strafeThrust = 3;
    public float responseTime;


    public float topCeilingY = 200f;
    public float lowCeilingY = -200f; 
    public float leftWallX = 24.608f;
    public float rightWallX = 64.208f;



    private float moveValueX = 0;
    private float moveValueY = 0;
    private Rigidbody rb;
    private float moveSpeed;


    void Start(){
        rb = GetComponent<Rigidbody>();

        joystick.onValueChangeX.AddListener(SetMoveX);
        joystick.onValueChangeY.AddListener(SetMoveY);
        moveSpeedSlider.onValueChange.AddListener(SetMoveSpeed);
    }

    // Physics should be independent from  the framerate
    void FixedUpdate() {
        HandleMovement();
    }

    void HandleMovement(){

        Vector3 newPosition = transform.position;
        newPosition += transform.right * moveValueX * strafeThrust * Time.deltaTime;
        newPosition += transform.up * moveValueY * upThrust * Time.deltaTime;


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

        Debug.Log("Move Speed input: " + speedInput);
        moveSpeed = speedInput * moveSpeedMultiplicator;
        SpeedManager.Instance.CurrentSpeed = moveSpeed; 
        Debug.Log("Move Speed: " + moveSpeed);
    }

}
