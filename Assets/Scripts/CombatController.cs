using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class CombatController : MonoBehaviour
{
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
    private GameObject NSimpleAttack;
    [SerializeField]
    private GameObject FSimpleAttack;
    [SerializeField]
    private GameObject DSimpleAttack;
    [SerializeField]
    private GameObject NSpecialAttack;
    [SerializeField]
    private GameObject FSpecialAttack;
    [SerializeField]
    private GameObject DSpecialAttack;

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
        animator.Play(attackName);

        // application needs to be 60 fps limited
        if (Application.targetFrameRate > 0)
        {
            for (int i = 0; i < animFrameDuration + 1; ++i)
                yield return new WaitForEndOfFrame();

            animator.enabled = false;
        }

        yield return null;
    }

    private void Update()
    {
        if (InAnimation())
            return;

        float facing = playerController.GetFacing();
        float forward = Input.GetAxisRaw(HorizontalAxis);
        float down = Input.GetAxisRaw(VerticalAxis);

        // TODO : upgrade (use array to store the gameobjects)
        if (Input.GetButtonDown(SimpleAttackButton))
        {
            GameObject attack = null;
            if (down < 0)
                attack = Instantiate(DSimpleAttack, transform);
            else if (Mathf.Abs(forward) > 0)
                attack = Instantiate(FSimpleAttack, transform);
            else
                StartCoroutine(StartAttackAnim("NSimpleAttack", 60));
        }
        else if (Input.GetButtonDown(SpecialAttackButton))
        {
            GameObject attack = null;
            if (down < 0)
                attack = Instantiate(DSpecialAttack, transform);
            else if (Mathf.Abs(forward) > 0)
                attack = Instantiate(FSpecialAttack, transform);
            else
                attack = Instantiate(NSpecialAttack, transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }

    public void ResetAnimator()
    {
        animator.enabled = false;
    }

    public bool InAnimation() => animator.enabled;
}