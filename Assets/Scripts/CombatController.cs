using Unity.VisualScripting;
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
    private float attackOffset = 5f;

    private PlayerController2D P;

    void Start()
    {
        P = GetComponent<PlayerController2D>();
    }

    private void Update()
    {
        float facing = P.GetFacing();
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

            //attack.transform.position = new Vector3(facing * (attack.transform.position.x + attackOffset),
            //    attack.transform.position.y,
            //    0f); 
            attack.transform.position = new Vector3(transform.position.x + facing * attackOffset,
                transform.position.y,
                0f); //the referential for attack.transform has to be the gameobject, not the attack object
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

            attack.transform.position = new Vector3(transform.position.x + facing * attackOffset,
                transform.position.y,
                0f);
        }
        // TODO : use animation with already assigned trigger
    }
}