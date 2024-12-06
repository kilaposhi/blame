using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterGenerator : MonoBehaviour
{
    // references vers les différents générateurs
    public GameObject generator_object1 ;
    public GameObject generator_object2 ;
    public GameObject generator_object3 ;
    public GameObject generator_object4 ;
    public GameObject generator_object5 ;

    private Generator gen1;
    private Generator gen2;
    private Generator gen3;
    private Generator gen4;
    private Generator gen5;

    private BoxCollider collider; // pour spawner les obstacles à différentes coordonnées

    //public float spawn_interval = 3f ; 
    private float timer = 3f; 

    void Start()
    {
        gen1 = generator_object1.GetComponent<Generator>();
        gen2 = generator_object2.GetComponent<Generator>();
        gen3 = generator_object3.GetComponent<Generator>();
        gen4 = generator_object4.GetComponent<Generator>();
        gen5 = generator_object5.GetComponent<Generator>();

        collider = generator_object1.GetComponent<BoxCollider>();

    }
    void Update()
    {
        Vector3 colliderMin = collider.bounds.min;
        Vector3 colliderMax = collider.bounds.max;

        if(timer <= 0)
        {
            timer = Random.Range(0.1f, 0.4f);//spawn_interval ; 
            Vector3 new_coords = new Vector3(Random.Range(colliderMin.x, colliderMax.x), Random.Range(colliderMin.y, colliderMax.y), generator_object1.transform.position.z); 
            SpawnAll(new_coords);
        }
        else 
        {
            timer -= Time.deltaTime;
        }
    }

    // call all generators to spawn an object
    void SpawnAll(Vector3 coords)
    {
        gen1.SpawnObstacle(coords);
        gen2.SpawnObstacle(coords);
        gen3.SpawnObstacle(coords);
        gen4.SpawnObstacle(coords);
        gen5.SpawnObstacle(coords);
    }
}
