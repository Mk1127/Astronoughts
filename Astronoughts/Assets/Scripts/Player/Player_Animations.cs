using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    [SerializeField] Player_Movement playerMovement;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Player_Interact playerIS;

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.isGrounded)
        {
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if(playerIS.isHidden)
                {
                    playerAnimator.Play("Crouch Walking");
                }
                else
                {
                    playerAnimator.Play("Running");
                }
            }
            else
            {
                if(playerIS.isHidden)
                {
                    playerAnimator.Play("Crouch Idle");
                }
                else
                {
                    playerAnimator.Play("Idle");
                }
            }
        }
        else
        {
            playerAnimator.Play("Floating");
        }
    }
}
