using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StateMachine : MonoBehaviour
{
    [SerializeField] Enemy_Patrol patrol;
    [SerializeField] Enemy_Follow follow;

    private void Start()
    {
        follow.enabled = false;
        patrol.enabled = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<Player_Interact>().isHidden)
            {
                ToggleFollow(true, false);
            }
            else
            {
                ToggleFollow(false, true);
            }
        }
        else
        {
            ToggleFollow(false, true);
        }
    }

    public void ToggleFollow(bool toggleFollow, bool togglePatrol)
    {
        follow.enabled = toggleFollow;
        patrol.enabled = togglePatrol;
    }
}
