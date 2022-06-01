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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<Player_Interact>().isHidden)
            {
                follow.enabled = true;
                patrol.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            follow.enabled = false;
            patrol.enabled = true;
        }
    }
}
