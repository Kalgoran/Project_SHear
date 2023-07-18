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

    private void Update()
    {
        float forward = Input.GetAxisRaw(HorizontalAxis);
        float down = Input.GetAxisRaw(VerticalAxis);

        // TODO : upgrade (use array)
        if (Input.GetButtonDown(SimpleAttackButton))
        {
            if (down < 0)
                Instantiate(DSimpleAttack);
            else if (Mathf.Abs(forward) > 0)
                Instantiate(FSimpleAttack);
            else
                Instantiate(NSimpleAttack);
        }
        if (Input.GetButtonDown(SpecialAttackButton))
        {
            if (down < 0)
                Instantiate(DSpecialAttack);
            else if (Mathf.Abs(forward) > 0)
                Instantiate(FSpecialAttack);
            else
                Instantiate(NSpecialAttack);
        }
    }
}