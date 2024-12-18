using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        // lorsqu'un obstacle entre en contact avec le Destructor (au bout du couloir derrière le joueur), 
        // il est détruit pour libérer de la mémoire

        if(c.gameObject.CompareTag("Obstacle"))
        {
            // Destroy the obstacle 
            Destroy(c.gameObject);
            //Debug.Log("OBSTACLE DESTROYED");
        }

    }
}
