using UnityEngine;

public class CombatController : MonoBehaviour
{
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

    [SerializeField]
    private bool bStartInverted = false;
    private float inverted = 1.0f;


    private void Start()
    {
        if (bStartInverted)
            inverted *= -1.0f;
    }

    private void Update()
    {
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
                attack = Instantiate(NSimpleAttack, transform);

            attack.transform.position = new Vector3(attack.transform.position.x + (inverted * attackOffset),
                attack.transform.position.y,
                0f);
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

            attack.transform.position = new Vector3(attack.transform.position.x + (inverted * attackOffset),
                attack.transform.position.y,
                0f);
        }
        // TODO : use animation with already assigned trigger

        if (forward != 0f)
            inverted = forward;
    }
}