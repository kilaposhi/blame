using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {

        if(c.gameObject.CompareTag("Obstacle"))
        {
            // Destroy the obstacle 
            Destroy(c.gameObject);
            //Debug.Log("OBSTACLE DESTROYED");
        }

    }
}
