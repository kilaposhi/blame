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
    // Start is called before the first frame update
    void Start()
    {
        rend1 = wall1.GetComponent<Renderer>();
        rend2 = wall2.GetComponent<Renderer>();
        Debug.Log("Running");
        scaleFactor = wall1.transform.localScale.z / rend1.material.mainTextureScale.x ;
    }

    // Update is called once per frame
    void Update()
    {
        // float offset = Time.time * speed;
        // rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

        //Vector2 textureOffset = new(Time.deltaTime * SpeedManager.Instance.CurrentSpeed/scaleFactor, 0);
        //rend1.material.mainTextureOffset = textureOffset;
        //rend2.material.mainTextureOffset = textureOffset;

        textureOffsetX += (SpeedManager.Instance.CurrentSpeed/scaleFactor) * Time.deltaTime;
        Vector2 offset = new Vector2(textureOffsetX, 0);
        rend1.material.mainTextureOffset = offset;
        rend2.material.mainTextureOffset = offset;
    }

}
