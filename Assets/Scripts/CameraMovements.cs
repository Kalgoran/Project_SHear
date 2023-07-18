using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;
    [SerializeField] private float smoothTime = 0.25f;

    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraPos = new Vector3 ((P1.transform.position.x + P2.transform.position.x) / 2f, 
            (P1.transform.position.y + P2.transform.position.y) / 2f, transform.position.z);
 
        transform.position = Vector3.SmoothDamp(transform.position, CameraPos, ref velocity, smoothTime); //smoothen the movement
    }

    //move z position to see both players
    private float CamZ()
    {
        float z = -(Mathf.Abs(P1.transform.position.x-P2.transform.position.x)) / 2f;

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
