using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    [SerializeField] Player_Movement playerMovement;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Player_Interact playerIS;

    private bool isGrounded = false;

    [SerializeField] float rayDistance;
    private float startRayDistance;

    private int contactPoints = 0;

    private bool frontGrounded;
    private bool rearGrounded;
    private bool rightGrounded;
    private bool leftGrounded;

    private void Start()
    {
        if(rayDistance == 0)
        {
            rayDistance = 0.2f;
        }

        startRayDistance = rayDistance;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        UpdatedRayDistance();
        CastFrontRay();
        CastRearRay();
        CastRayRight();
        CastRayLeft();

        if(isGrounded)
        {
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if(playerMovement.isGrabbing)
                {
                    playerAnimator.Play("Pushing");
                    SetAnimatorSpeed(1.5f);
                }
                else
                {
                    if (playerIS.isHidden)
                    {
                        SetAnimatorSpeed(1.5f);
                        playerAnimator.Play("Crouch Walking");
                    }
                    else
                    {
                        if(playerMovement.isSprinting)
                        {
                            SetAnimatorSpeed(3f);
                        }
                        else
                        {
                            SetAnimatorSpeed(1.5f);
                        }

                        playerAnimator.Play("Running");
                    }
                }
            }
            else
            {
                if(playerIS.isHidden)
                {
                    SetAnimatorSpeed(1.5f);
                    playerAnimator.Play("Crouch Idle");
                }
                else
                {
                    SetAnimatorSpeed(1.5f);
                    playerAnimator.Play("Idle");
                }
            }
        }
        else
        {
            SetAnimatorSpeed(1.5f);
            playerAnimator.Play("Floating");
        }
    }

    private void UpdatedRayDistance()
    {
        if(!playerMovement.hovering)
        {
            if (isGrounded)
            {
                if (rayDistance == startRayDistance)
                {
                    rayDistance = startRayDistance * 6;
                }
            }
            else
            {
                if (rayDistance != startRayDistance)
                {
                    rayDistance = startRayDistance;
                }
            }
        }
        else
        {
            if (rayDistance != startRayDistance)
            {
                rayDistance = startRayDistance;
            }
        }
    }

    private void CastFrontRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.forward * 0.5f, Vector3.down, out hit, rayDistance))
        {
            if(!hit.collider.isTrigger)
            {
                Debug.DrawRay(transform.position + transform.forward * 0.5f, Vector3.down * rayDistance, Color.red);
                Debug.Log("Front Ray detected hit");

                if (!frontGrounded)
                {
                    contactPoints += 1;
                }

                frontGrounded = true;
            }
        }
        else
        {
            if(frontGrounded)
            {
                contactPoints -= 1;
            }

            frontGrounded = false;
        }
    }

    private void CastRearRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position - transform.forward * 0.5f, Vector3.down, out hit, rayDistance))
        {
            if(!hit.collider.isTrigger)
            {
                Debug.DrawRay(transform.position - transform.forward * 0.5f, Vector3.down * rayDistance, Color.red);
                Debug.Log("Back Ray detected hit");

                if (!rearGrounded)
                {
                    contactPoints += 1;
                }

                rearGrounded = true;
            }
        }
        else
        {
            if (rearGrounded)
            {
                contactPoints -= 1;
            }

            rearGrounded = false;
        }
    }

    private void CastRayRight()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.right * 0.5f, Vector3.down, out hit, rayDistance))
        {
            if(!hit.collider.isTrigger)
            {
                Debug.DrawRay(transform.position + transform.right * 0.5f, Vector3.down * rayDistance, Color.red);
                Debug.Log("Right Ray detected hit");

                if (!rightGrounded)
                {
                    contactPoints += 1;
                }

                rightGrounded = true;
            }
        }
        else
        {
            if (rightGrounded)
            {
                contactPoints -= 1;
            }

            rightGrounded = false;
        }
    }

    private void CastRayLeft()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position - transform.right * 0.5f, Vector3.down, out hit, rayDistance))
        {
            if(!hit.collider.isTrigger)
            {
                Debug.DrawRay(transform.position - transform.right * 0.5f, Vector3.down * rayDistance, Color.red);
                Debug.Log("Left Ray detected hit");

                if (!leftGrounded)
                {
                    contactPoints += 1;
                }

                leftGrounded = true;
            }
        }
        else
        {
            if (leftGrounded)
            {
                contactPoints -= 1;
            }

            leftGrounded = false;
        }
    }

    private void CheckGrounded()
    {
        if(contactPoints > 0)
        {
            isGrounded = true;

            if(playerMovement.hovering == true && contactPoints == 0)
            {
                playerMovement.hovering = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    private void SetAnimatorSpeed(float speed)
    {
        if (playerAnimator.speed != speed)
        {
            playerAnimator.speed = speed;
        }
    }
}
