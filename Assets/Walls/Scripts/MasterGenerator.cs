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

    private BoxCollider my_collider; // pour spawner les obstacles à différentes coordonnées

    //public float spawn_interval = 3f ; 
    private float timer = 3f;
    private float type = 0f;

     // types de préfabs d'obstacles à spawner
    public GameObject obstacle_01 ;
    public GameObject obstacle_02 ;
    public GameObject obstacle_03 ;

    void Start()
    {
        gen1 = generator_object1.GetComponent<Generator>();
        gen2 = generator_object2.GetComponent<Generator>();
        gen3 = generator_object3.GetComponent<Generator>();
        gen4 = generator_object4.GetComponent<Generator>();
        gen5 = generator_object5.GetComponent<Generator>();

        my_collider = generator_object1.GetComponent<BoxCollider>();

    }
    void Update()
    {
        Vector3 colliderMin = my_collider.bounds.min;
        Vector3 colliderMax = my_collider.bounds.max;

        if(timer <= 0 && SpeedManager.Instance.CurrentSpeed > 0)
        {
            timer = Random.Range(0.1f, 0.4f);//spawn_interval ; 
            Vector3 new_coords = new Vector3(Random.Range(colliderMin.x, colliderMax.x), Random.Range(colliderMin.y, colliderMax.y), generator_object1.transform.position.z);
            type = Random.Range(0f, 1f);
            if(type <= 0.9f)
            {
                SpawnAll(new_coords, obstacle_01);
            }
            else if(type > 0.9f && type <= 0.99f)
            {
                SpawnAll(new_coords, obstacle_02);
            }
            else
            {
                SpawnAll(new_coords, obstacle_03);
            }
        }
        else 
        {
            timer -= Time.deltaTime;
        }
    }

    // call all generators to spawn an object
    void SpawnAll(Vector3 coords, GameObject obstacle)
    {
        gen1.SpawnObstacle(coords, obstacle);
        gen2.SpawnObstacle(coords, obstacle);
        gen3.SpawnObstacle(coords, obstacle);
        gen4.SpawnObstacle(coords, obstacle);
        gen5.SpawnObstacle(coords, obstacle);
    }
}
