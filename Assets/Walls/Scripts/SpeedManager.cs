using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.XR.Content.Interaction;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance { get; set; }
    // public XRSlider moveSpeedSlider;
    // public float speedMultiplicator = 15;
    public float CurrentSpeed = 0f;

    private void Awake()
    {
        if (Instance == null){

            Instance = this;

        }
        else
            Destroy(gameObject); // Singleton pattern
    }

    // private void Start()
    // {
    //     moveSpeedSlider.onValueChange.AddListener(SetSpeed);
    // }

    // private void SetSpeed(float speedInput)
    // {
    //     CurrentSpeed = speedInput * speedMultiplicator ;
    //     Debug.Log("Move Speed: " + CurrentSpeed);
    // }

}