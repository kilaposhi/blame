using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipController : MonoBehaviour
{

    public float yawTorque = 120;
    public float pitchTorque = 120;
    public float thrust = 5;
    public float upThrust = 120;
    public float strafeThrust = 120;
    public float responseTime;
    public float thrustGlideReduction = 0.4f;
    // public float roll;

    private Vector2 moveValue;
    // private float thrust;
    private float thrustInput;
    private float glide;
    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    // Physics should be independent from  the framerate
    void FixedUpdate() {
        HandleMovement();
    }

    void HandleMovement(){

        // move forward
        // transform.position += -transform.forward * thrustSpeed * Time.deltaTime;
        // more realist thrust for later
        
        // log the thrust input
        Debug.Log(thrustInput); // never 0 doesnt work

        if (thrustInput > 0 || thrustInput < 0) {
            float currentThrust = thrust; 
            rb.AddRelativeForce(Vector3.forward * thrustInput * currentThrust * Time.deltaTime);
            glide = thrust;
        }
        else {
            rb.AddRelativeForce(Vector3.forward * glide  * Time.deltaTime);
            glide *= thrustGlideReduction; 
        }

        // inputs
        float horizontalInput = moveValue.x;
        float verticalInput = moveValue.y;


        // // yaw, pitch, roll
        // Yaw += horizontalInput * YawAmount * Time.deltaTime * 1/4;
        // float pitch = Mathf.Lerp(0, 20, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        // float roll = Mathf.Lerp(0, 30, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        // // apply rotation
        // transform.localRotation = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * pitch + Vector3.forward * roll);

        // Pitch
        rb.AddRelativeTorque(Vector3.right * Math.Clamp(verticalInput, -1f, 1f) * pitchTorque *Time.deltaTime);
        // Yaw 
        rb.AddRelativeTorque(Vector3.up * Math.Clamp(horizontalInput, -1f, 1f) * yawTorque *Time.deltaTime);



    }

    void OnMove(InputValue value){
        moveValue = value.Get<Vector2>();
    }

    void OnThrust(InputValue value){
        thrustInput = value.Get<float>();
    }

}
