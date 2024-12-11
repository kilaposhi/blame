using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.XR.Content.Interaction;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance { get; set; }

    public float CurrentSpeed = 0f;

    private void Awake()
    {
        if (Instance == null){

            Instance = this;

        }
        else
            Destroy(gameObject); // Singleton pattern
    }
}