using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    public float geyserForce;
    public float startGeyserForce;
    public bool isactive;

    private void Awake()
    {
        startGeyserForce = geyserForce;
        isactive = true;
    }
    private void Start()
    {


    }
    private void OnTriggerStay(Collider other)
    {
        if (isactive)
        {
            if (other.tag == "Player")
            {
                Player_Movement playerMovement = other.GetComponent<Player_Movement>();
                playerMovement.jumpSpeed = geyserForce;
                playerMovement.inGeyser = true;
            }
        }

        if (other.tag == "Block")
        {

        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            isactive = false;
            transform.parent.GetComponent<Geyser_System>().activeGeysers--;
            transform.parent.GetComponent<Geyser_System>().UpdateGeysersForce();
            other.transform.parent = null;
            other.transform.position = transform.position + (Vector3.up / 2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Block")
        {
            isactive = true;
            transform.parent.GetComponent<Geyser_System>().activeGeysers++;
            transform.parent.GetComponent<Geyser_System>().UpdateGeysersForce();
        }
    }
}
