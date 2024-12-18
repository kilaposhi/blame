using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    //public float speed = 10f; // deprecated, we use the cool singleton thingy now
    public GameObject wall1; 
    public GameObject wall2; 

    private Renderer rend1;
    private Renderer rend2;
    private float scaleFactor;
    private float textureOffsetX = 0f;

    void Start()
    {
        // on récupère les Renderer des murs pour pouvoir mettre les textures à jour
        rend1 = wall1.GetComponent<Renderer>();
        rend2 = wall2.GetComponent<Renderer>();

        // on utilise un facteur basé sur la taille du mur pour que les pixels de la texture se déplace à la même vitesse que SpeedManager
        scaleFactor = wall1.transform.localScale.z / rend1.material.mainTextureScale.x ;
    }

    void Update()
    {
        // on déplace la texture des murs vers le joueur pour donner une impression de mouvement
        textureOffsetX += SpeedManager.Instance.CurrentSpeed/scaleFactor * Time.deltaTime;
        Vector2 offset = new Vector2(textureOffsetX, 0);
        rend1.material.mainTextureOffset = offset;
        rend2.material.mainTextureOffset = offset;
    }

}
