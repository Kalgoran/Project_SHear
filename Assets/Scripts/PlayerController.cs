using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 5f;

    private Rigidbody rb = null;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange); // add vertical force without mass

        rb.AddForce(Input.GetAxis("Horizontal") * Vector3.right * speed);
    }
}
