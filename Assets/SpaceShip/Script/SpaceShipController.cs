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
    public float moveSpeedMultiplicator = 25;
    public float animationSpeedMultiplicator = 25;
    public float upThrust = 4;
    public float strafeThrust = 3;
    public float responseTime;

    private float moveValueX = 0;
    private float moveValueY = 0;
    private Rigidbody rb;
    private Animator animator;
    private float moveSpeed;

    void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();        

        joystick.onValueChangeX.AddListener(SetMoveX);
        joystick.onValueChangeY.AddListener(SetMoveY);
        moveSpeedSlider.onValueChange.AddListener(SetMoveSpeed);
    }

    // Physics should be independent from  the framerate
    void FixedUpdate() {
        HandleMovement();
    }

    void HandleMovement(){

        float animX = 0f;
        float animY = 0f;


        if (moveValueX != 0f){
            animX += moveValueX * Time.deltaTime * animationSpeedMultiplicator ;
        } else if (animX > 0.05f){
            animX -= Time.deltaTime * 10;
        }
        else if (animX < -0.05f)
        {
            animX -= Time.deltaTime * -10;
        }
        else
        {
            animX = 0f;
        }


        if (moveValueY != 0f){
            animY += moveValueY * Time.deltaTime * animationSpeedMultiplicator;
        }
        else if (animY > 0.01f)
        {
            animY -= Time.deltaTime * 10;
        }
        else if (animY < -0.01f)
        {
            animY -= Time.deltaTime * -10;
        }
        else
        {
            animY = 0f;
        }
 
        if (animX > 1f)
        {
            animX = 1f;
        }
        if (animX < -1) { animX = -1; }
        if (animY > 1f) { animX = 1f; }
        if (animY < -1f) { animX = -1f; }


        transform.position += transform.right * moveValueX * strafeThrust * Time.deltaTime;
        transform.position += transform.up * moveValueY * upThrust * Time.deltaTime;

        // For the blender animation
        Debug.Log("AnimX" + animX);
        Debug.Log("AnimY" + animY);
        animator.SetFloat("X", animX);
        animator.SetFloat("Y", animY);

    }

    void OnMove(InputValue value){
        Vector2 moveValue = value.Get<Vector2>();
        moveValueX = moveValue.x;
        moveValueY = moveValue.y;
        Debug.Log("Move: " + moveValue);
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
