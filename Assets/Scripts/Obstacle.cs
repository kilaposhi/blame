using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   //public float speed = 3f; // deprecated, now we use the cool singleton stuff

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, -SpeedManager.Instance.CurrentSpeed*Time.deltaTime));
        //Debug.Log(transform.rotation);
    }
}
