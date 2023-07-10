using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;
    [SerializeField] private float smoothTime = 0.25f;

    private Vector3 velocity;

    Renderer r1;
    Renderer r2;

    // Start is called before the first frame update
    void Start()
    {
        r1 = P1.GetComponent<Renderer>();
        r2 = P2.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //1/4 = z/vision
        Vector3 CameraPos = new Vector3 ((P1.transform.position.x + P2.transform.position.x) / 2f, 
            (P1.transform.position.y + P2.transform.position.y) / 2f, CamZ());
 
        transform.position = Vector3.SmoothDamp(transform.position, CameraPos, ref velocity, smoothTime); //smoothen the movement
    }

    //move z position to see both players
    private float CamZ()
    {
        float z = -(Mathf.Abs(P1.transform.position.x-P2.transform.position.x)) / 2f;

        print(z);
        if (z<=-10 && z >= -25)
        {
            
            return z;
        }
        if (z > -10)
        {
            return -10;
        } else
        {
            return -25;
        }
        
    } 
}
