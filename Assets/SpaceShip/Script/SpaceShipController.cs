using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityEngine.XR.Content.Interaction{

public class SpaceShipController : MonoBehaviour
{

    public XRSlider moveSpeedSlider;
    public XRJoystick joystick;
    public float moveSpeed = 2;
    public float upThrust = 2;
    public float strafeThrust = 3;
    public float responseTime;

    private float moveValueX;
    private float moveValueY;
    // private float thrust;
    private float moveInput;
    private Rigidbody rb;
    private Animator animator;

    void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();        

        moveSpeedSlider.onValueChange.AddListener(SetMoveSpeed);
        joystick.onValueChangeX.AddListener(SetMoveX);
        joystick.onValueChangeY.AddListener(SetMoveY);
    }

    // Physics should be independent from  the framerate
    void FixedUpdate() {
        HandleMovement();
    }

    void HandleMovement(){

        // move forward
        // transform.position += -transform.forward * thrustSpeed * Time.deltaTime;
        
        if (moveValueX < 0){
            animator.SetBool("turnR", true);
            animator.SetBool("turnL", false);
        } else if (moveValueX > 0){
            animator.SetBool("turnL", true);
            animator.SetBool("turnR", false);
        } else {
            animator.SetBool("turnR", false);
            animator.SetBool("turnL", false);
        }

        if (moveValueY > 0){
            animator.SetBool("up", true);
            animator.SetBool("down", false);
        } else if (moveValueY < 0){
            animator.SetBool("down", true);
            animator.SetBool("up", false);
        }
        else {
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }

        transform.position += transform.right * moveValueX * strafeThrust * Time.deltaTime;
        transform.position += transform.up * moveValueY * upThrust * Time.deltaTime;


    }

    // void OnMove(InputValue value){
    //     moveValue = value.Get<Vector2>();
    // }

    // void OnThrust(InputValue value){
    //     moveInput = value.Get<float>();
    //     Debug.Log("Thrust: " + moveInput);
    // }

        // void ConnectControlEvents(){
        // }
    void SetMoveSpeed(float sliderValue)
    {
        moveSpeed = sliderValue;
        Debug.Log("Move Speed: " + moveSpeed);
    }

    void SetMoveX(float value)
    {
        moveValueX = value;
        Debug.Log("Move X: " + moveValueX);
    }

    void SetMoveY(float value)
    {
        moveValueY = value;
        Debug.Log("Move Y: " + moveValueY);
    }

}

}