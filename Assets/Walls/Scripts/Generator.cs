using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public void SpawnObstacle(Vector3 coords, GameObject obstacle)
    {
        // on génère l'obstacle aux coordonées passées par Master Generator mais on offset en y pour que les obstacles soient bien en face de leur propre générateur
        Instantiate(obstacle, new Vector3(coords.x, coords.y+gameObject.transform.position.y, coords.z), transform.rotation);
        //Debug.Log("SPAWNED OBSTACLE");
    }
}
