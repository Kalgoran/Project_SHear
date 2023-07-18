using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController2D : MonoBehaviour
{
    [SerializeField]
    private float speed = 25f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float gravity = 4f;
    private Rigidbody2D rb;
    private CapsuleCollider2D cl;

    private float v0 = 0;
    private float vel;
    private float inputRL;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<CapsuleCollider2D>();
        Physics2D.queriesStartInColliders = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //- s'accroupir
        //- dash
        //- (courir)

        float vx;
        float vy;

        bool a = DetectGround();
        if (a && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // add vertical force without mass
            v0 = rb.velocity.x;
        }

        //faster fall 
        vy = rb.velocity.y;
        if (rb.velocity.y <0)
        {
            rb.gravityScale = gravity;
        } else
        {
            rb.gravityScale = 1f;
        }
        Mathf.Clamp(vy, -15, speed);

        //right and left
        if (a)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5)
            {
                vx = Mathf.SmoothDamp(rb.velocity.x, Input.GetAxis("Horizontal") * speed, ref vel, 0.6f);
            } else
            {
                vx = Input.GetAxis("Horizontal") * speed; //faster stop
            }           
        }
        else
        {
            vx = rb.velocity.x + 0.007f * Input.GetAxis("Horizontal") * speed;  //restrict air movements
        }
        vx = Mathf.Clamp(vx, -speed, speed);
        rb.velocity = new Vector2(vx, vy);
    }


    bool DetectGround()
    {
        return Physics2D.CircleCast(cl.bounds.center, cl.bounds.size.x/2, Vector2.down, cl.bounds.size.y/2);
    }
}