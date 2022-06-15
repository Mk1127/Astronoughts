using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    [SerializeField] Player_Movement playerMovement;
    [SerializeField] Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.isGRounded)
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
    }
}
