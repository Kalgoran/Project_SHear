using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 8f;

    private Rigidbody rb = null;
    private Collider cl = null;

    // Start is called before the first frame update
    void Start()
    {
        cl = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")&&DetectGround())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange); // add vertical force without mass

        rb.AddForce(Input.GetAxis("Horizontal") * Vector3.right * speed, ForceMode.VelocityChange); //pk il va si vite
    }

    bool DetectGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f);
        //return Physics.BoxCast(cl.bounds.center, transform.localScale, Vector3.down, transform.rotation, 200f);
    }
}
