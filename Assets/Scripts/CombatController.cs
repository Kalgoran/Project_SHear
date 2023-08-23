using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class CombatController : MonoBehaviour
{
    public int playerIndex = 0;


    public PlayerController2D playerController;
    private Animator animator;


    [SerializeField]
    private string SimpleAttackButton = "Attack0";
    [SerializeField]
    private string SpecialAttackButton = "Attack1";
    [SerializeField]
    private string BlockButton = "Block";
    [SerializeField]
    private string HorizontalAxis = "Horizontal";
    [SerializeField]
    private string VerticalAxis = "Vertical";

    // TODO : cooldown

    // neutral
    // forward
    // down
    [SerializeField]
    private string NSimpleAttack = "NSimpleAttack";
    [SerializeField]
    private int NSimpleFrameDuration = 60;
    [SerializeField]
    private string FSimpleAttack = "FSimpleAttack";
    [SerializeField]
    private int FSimpleFrameDuration = 60;
    [SerializeField]
    private string DSimpleAttack = "DSimpleAttack";
    [SerializeField]
    private int DSimpleFrameDuration = 60;
    [SerializeField]
    private string NSpecialAttack = "NSpecialAttack";
    [SerializeField]
    private int NSpecialFrameDuration = 60;
    [SerializeField]
    private string FSpecialAttack = "FSpecialAttack";
    [SerializeField]
    private int FSpecialFrameDuration = 60;
    [SerializeField]
    private string DSpecialAttack = "DSpecialAttack";
    [SerializeField]
    private int DSpecialFrameDuration = 60;

    [SerializeField]
    private float attackOffset = 1f;


    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    IEnumerator StartAttackAnim(string attackName, int animFrameDuration)
    {
        animator.enabled = true;
        playerController.disable();
        animator.Play(attackName);

        // application needs to be 60 fps limited
        if (Application.targetFrameRate > 0)
        {
            for (int i = 0; i < animFrameDuration + 1; ++i)
                yield return new WaitForEndOfFrame();

            animator.enabled = false;
            playerController.enable();
        }
                
        yield return null;
    }

    private void Update()
    {
        if (InAnimation())
            return;

        float forward = Input.GetAxisRaw(playerIndex + HorizontalAxis);
        float down = Input.GetAxisRaw(playerIndex + VerticalAxis);

        // TODO : upgrade (use array to store the gameobjects)
        if (Input.GetButtonDown(playerIndex + SimpleAttackButton))
        {
            if (down < 0)
                StartCoroutine(StartAttackAnim(DSimpleAttack, DSimpleFrameDuration));
            else if (Mathf.Abs(forward) > 0)
                StartCoroutine(StartAttackAnim(FSimpleAttack, FSimpleFrameDuration));
            else
                StartCoroutine(StartAttackAnim(NSimpleAttack, NSimpleFrameDuration));
        }
        else if (Input.GetButtonDown(playerIndex + SpecialAttackButton))
        {
            if (down < 0)
                StartCoroutine(StartAttackAnim(DSpecialAttack, DSpecialFrameDuration));
            else if (Mathf.Abs(forward) > 0)
                StartCoroutine(StartAttackAnim(FSpecialAttack, FSpecialFrameDuration));
            else
                StartCoroutine(StartAttackAnim(NSpecialAttack, NSpecialFrameDuration));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }

    public void ResetAnimator()
    {
        animator.enabled = false;
        playerController.enable();
    }

    public bool InAnimation() => animator.enabled;
}