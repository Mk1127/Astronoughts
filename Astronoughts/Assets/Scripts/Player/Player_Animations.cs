using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    [SerializeField] Player_Movement playerMovement;
    [SerializeField] Animator playerAnimator;

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.isGrounded)
        {
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                playerAnimator.Play("Running");
            }
            else
            {
                playerAnimator.Play("Idle");
            }
        }
        else
        {
            playerAnimator.Play("Floating");
        }
    }
}
