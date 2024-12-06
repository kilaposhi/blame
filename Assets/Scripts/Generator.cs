using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject obstacle_01 ;
    //public float spawn_interval = 3f ; 
    //private float timer = 3f; 

    /*
    void Update()
    {
        if(timer <= 0)
        {
            timer = spawn_interval ; 
            Instantiate(obstacle_01, transform.position, transform.rotation);
        }
        else 
        {
            timer -= Time.deltaTime;
        }

    }
    */

    public void SpawnObstacle(Vector3 coords) // TODO: add spacial + temporal + type parameters
    {
        Instantiate(obstacle_01, new Vector3(coords.x, coords.y+gameObject.transform.position.y, coords.z), transform.rotation);
        //Debug.Log("SPAWNED OBSTACLE");
    }
}
