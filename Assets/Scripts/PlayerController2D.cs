using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController2D : MonoBehaviour
{
    public int playerIndex = 0;


    [SerializeField]
    private string VerticalAxis = "Vertical";
    [SerializeField]
    private string HorizontalAxis = "Horizontal";
    [SerializeField]
    private string JumpButton = "Jump";
    [SerializeField]
    private string CrouchButton = "Crouch";
    [SerializeField]
    private string DashButton = "Dash";


    [SerializeField]
    private float speed = 25f;

    [SerializeField]
    private float jumpStartupSpeed = 20f;
    [SerializeField]
    private float jumpHeight = 4f;
    [SerializeField]
    private float jumpSpeedDecay = 0.1f;
    [SerializeField]
    private float jumpGravityMultiplier = 1.005f;

    [SerializeField]
    private float dashForce = 30f;
    [SerializeField]
    private float smoothTime = 0.8f;
    [SerializeField]
    private float stopThresh = 0.3f;

    private float facing = 1f;

    private bool bEnable = true;

    private Rigidbody2D rb;
    private CapsuleCollider2D cl;

    private float vel;

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

    private float height = -1f;
    // Update is called once per frame
    void Update()
    {
        bool bOnGround = DetectGround();

        // jump
        // upward acceleration
        if (bEnable && bOnGround && Input.GetButtonDown(playerIndex + JumpButton))
        {
            height = transform.position.y;
            rb.AddForce(Vector2.up * jumpStartupSpeed, ForceMode2D.Impulse);
        }
        float vx = rb.velocity.x;
        float vy = rb.velocity.y;
        // acceleration decay
        if (transform.position.y >= height + jumpHeight && vy > 0f)
            vy *= 1f - jumpSpeedDecay;
        // downward acceleration (gravity multiplier)
        if (rb.velocity.y <= 0f)
            vy *= jumpGravityMultiplier;
        Mathf.Clamp(vy, -15f, speed);


        // crouch
        transform.localScale = new Vector3(1, 1f - Mathf.Abs(Input.GetAxis(playerIndex + CrouchButton)) / 2f, 1);

        // right and left
        if (bOnGround)
        {
            if (!bEnable)
            {
                vx = 0;
            }else
            {
                if (Input.GetAxisRaw(playerIndex + HorizontalAxis) != 0)
                    facing = Input.GetAxisRaw(playerIndex + HorizontalAxis);
                if (Mathf.Abs(Input.GetAxis(playerIndex + HorizontalAxis)) > stopThresh)
                    vx = Mathf.SmoothDamp(rb.velocity.x, facing * speed, ref vel, smoothTime);
                else
                    vx = Input.GetAxis(playerIndex + HorizontalAxis) * speed; //faster stop
            }
        }
        else
        {
            vx = rb.velocity.x + 0.001f * Input.GetAxis(playerIndex + HorizontalAxis) * speed;  //restrict air movements
        }
        vx = Mathf.Clamp(vx, -speed, speed);
        rb.velocity = new Vector2(vx, vy);

        // dash
        if (bEnable && Input.GetButtonDown(playerIndex + DashButton))
            rb.AddForce(Vector2.right * facing * dashForce, ForceMode2D.Impulse); // add horizontal force 


        //facing
        transform.localScale = new Vector3(transform.localScale.x * facing,
            transform.localScale.y,
            transform.localScale.z);
    }

    bool DetectGround()
    {
        return Physics2D.CircleCast(cl.bounds.center, cl.bounds.size.x / 2, Vector2.down, cl.bounds.size.y / 2);
    }

    public float GetFacing()
    {
        return facing;
    }

    public void disable()
    {
        bEnable = false;
    }
    public void enable()
    {
        bEnable = true;
    }
}
