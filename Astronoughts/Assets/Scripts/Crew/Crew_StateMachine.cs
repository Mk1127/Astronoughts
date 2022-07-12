using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crew_StateMachine : MonoBehaviour
{
    [Header("AI Scripts")]
    [SerializeField] Crew_MoveToDestination go;
    [SerializeField] Crew_Wait wait;

    [Header("GameObjects")]
    [SerializeField] GameObject player;

    [Header("Raycast")]
    [SerializeField] float rayLength;

    [SerializeField] Animator crewAnimator;

    private bool playerFound = false;
    [SerializeField] bool inHub = false;

    public LayerMask ignoreLayer;

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if(inHub)
        {
            ToggleMove(false, true);
            playerFound = true;
        }

        wait.enabled = true;
        go.enabled = false;
    }

    private void Update()
    {
        AnimateCrew();
        RaycastHit hit;

        Vector3 raydir = ((player.transform.position - transform.position) + (Vector3.up * .5f)).normalized;

        if(Physics.Raycast(transform.position,raydir,out hit,rayLength,~ignoreLayer))
        {
            if(hit.collider.tag == "Player" || hit.collider.tag == "Player")
            {
                playerFound = true;
                ToggleMove(false,true);
            }
            else
            {
                ToggleMove(true,false);
            }
        }
        else
        {
            if(playerFound)
            {
                ToggleMove(false,true);
            }

        }


        Debug.DrawRay(transform.position,raydir * rayLength,Color.red);
    }

    public void ToggleMove(bool toggleWait, bool toggleGo)
    {
        go.enabled = toggleGo;
        wait.enabled = toggleWait;
    }

    public void AnimateCrew()
    {
        if(playerFound == false)
        {
            crewAnimator.Play("Idle");
        }
        else
        {
            if(wait.enabled == true)
            {
                crewAnimator.Play("Idle");
            }
            else
            {
                crewAnimator.Play("Running");
            }
        }

    }
}
