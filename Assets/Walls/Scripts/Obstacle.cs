using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Update()
    {
        // les obstacles sont Translaté à la vitesse de SpeedManager vers le joueur pour donner une impression de vitesse
        transform.Translate(new Vector3(0, 0, -SpeedManager.Instance.CurrentSpeed*Time.deltaTime));
        //Debug.Log(transform.rotation);
    }
}
