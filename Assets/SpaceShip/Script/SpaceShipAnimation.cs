using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;

public class SpaceShipAnimation : MonoBehaviour
{

    public XRJoystick joystick;

    public float animationSpeedMultiplicator = 0.4f;

    // Input values [-1, 1]
    private float moveValueX = 0;
    private float moveValueY = 0;
    private Animator animator;
    // Animator values for the blender [-1, 1]
    private float animX = 0f;
    private float animY = 0f;
    private SpaceShipController spaceShipController;

    void Start()
    {
        animator = GetComponent<Animator>();
        spaceShipController = GetComponentInParent<SpaceShipController>();


        joystick.onValueChangeX.AddListener(SetMoveX);
        joystick.onValueChangeY.AddListener(SetMoveY);
        
    }

    // Update is called once per frame
    void Update()
    {
        // We do not allow movement if the ship isn't started
        if (spaceShipController.IsShipStill())
        {
            return;
        }

        // When the ship is moving we update smoothly animX towards the moveValue which update the animation
        if (moveValueX != 0f){
             animX = Mathf.MoveTowards(animX, moveValueX, animationSpeedMultiplicator * Time.deltaTime);
        }
        else
        {
            animX = Mathf.MoveTowards(animX, 0f, Time.deltaTime); // Go towards idle
        }


        if (moveValueY != 0f){
            animY = Mathf.MoveTowards(animY, moveValueY, animationSpeedMultiplicator * Time.deltaTime);
        }
        else
        {
            animY = Mathf.MoveTowards(animY, 0f, Time.deltaTime);
        }
 
        // Keep the values between -1 and 1
        animX = Mathf.Clamp(animX, -1f, 1f);
        animY = Mathf.Clamp(animY, -1f, 1f);
        // Debug.Log($"Animation Variables - animX: {animX}, animY: {animY}");
        
        // Set the anim coordinates in the blender animator
        animator.SetFloat("X", animX);
        animator.SetFloat("Y", animY);
    }

    void SetMoveX(float value)
    {
        moveValueX = value;
        // right [0, -1] left [0, 1]
    }

    void SetMoveY(float value)
    {
        moveValueY = value;
        // up [0, 1] down [0, -1]
    }
}
