using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 25f;
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
        if (Input.GetButtonDown("Jump") && DetectGround())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange); // add vertical force without mass

        //rb.AddForce(Input.GetAxis("Horizontal") * Vector3.right * speed, ForceMode.VelocityChange);
        rb.velocity = new Vector3 (Input.GetAxis("Horizontal") * speed,rb.velocity.y,0f);
    }

    bool DetectGround()
    {
        //return Physics.Raycast(transform.position, Vector3.down, 0.5f);
        return Physics.BoxCast(cl.bounds.center, transform.localScale/4f, Vector3.down, transform.rotation, 0.5f);
    }
}
