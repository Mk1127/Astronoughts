using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StateMachine : MonoBehaviour
{
    [Header("AI Scripts")]
    [SerializeField] Enemy_Patrol patrol;
    [SerializeField] Enemy_Follow follow;

    [Header("GameObjects")]
    [SerializeField] GameObject player;


    [Header("Raycast")]
    [SerializeField] float rayLength;

    private bool playerFound = false;

    public LayerMask ignoreLayer;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        follow.enabled = false;
        patrol.enabled = true;
    }

    private void Update()
    {
        RaycastHit hit;

        Vector3 raydir = ((player.transform.position - transform.position) + (Vector3.up * .5f)).normalized;

        if (Physics.Raycast(transform.position, raydir, out hit, rayLength, ~ignoreLayer))
        {
            if (hit.collider.tag == "Player" || hit.collider.tag == "Player")
            {
                playerFound = true;
                ToggleFollow(true, false);
            }
            else
            {
                ToggleFollow(false, true);
            }
        }
        else
        {
            if (playerFound)
            {
                ToggleFollow(false, true);
            }

        }


        Debug.DrawRay(transform.position, raydir * rayLength, Color.red);
    }

    public void ToggleFollow(bool toggleFollow, bool togglePatrol)
    {
        follow.enabled = toggleFollow;
        patrol.enabled = togglePatrol;
    }
}
