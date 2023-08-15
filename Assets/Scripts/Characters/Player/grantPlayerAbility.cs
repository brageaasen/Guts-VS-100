using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grantPlayerAbility : MonoBehaviour
{
    [SerializeField] GameObject grantAbilityUI;
    [SerializeField] PlayerMovement playerMovement;
    private GameObject climbUI;
    private GameObject crouchUI;
    // Start is called before the first frame update
    void Start()
    {
        climbUI = transform.GetChild(0).gameObject;
        crouchUI = transform.GetChild(1).gameObject;
    }

    public void grantClimb()
    {
        climbUI.SetActive(true);
        playerMovement.canClimb = true;
    }
    public void grantCrouch()
    {
        crouchUI.SetActive(true);
        playerMovement.canCrouch = true;
    }
}
