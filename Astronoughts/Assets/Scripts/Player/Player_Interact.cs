using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    [SerializeField] float rayDistance;
    [SerializeField] Player_Rotation playerRot;
    [SerializeField] InteractionText interactionText;
    [SerializeField] Vector3 lastCheckpoint;

    public bool isHidden;

    private void Start()
    {
        /*if(playerRot == null)
        {
            playerRot = gameObject.GetComponent<Player_Rotate>();
            playerRot.enabled = true;
        }
        else
        {
            playerRot.enabled = true;
        }*/

        if(interactionText == null)
        {
            if(GameObject.Find("InteractionText"))
            {
                interactionText = GameObject.Find("InteractionText").GetComponent<InteractionText>();
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, rayDistance))
        {

            Debug.Log(hit.collider.tag);

            if (hit.collider.tag == "Castable")
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDistance;
                Debug.DrawRay(transform.position, forward, Color.red);

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    hit.collider.gameObject.transform.parent = gameObject.transform;
                    
                    if(interactionText != null)
                    {
                        interactionText.gameObject.SetActive(false);
                    }
                }
                else
                {
                    hit.collider.gameObject.transform.parent = null;

                    if (interactionText != null)
                    {
                        interactionText.gameObject.SetActive(true);
                        interactionText.lookAt = hit.transform;
                    }
                }
            }

            if (hit.collider.tag == "Totem")
            {
                interactionText.gameObject.SetActive(true);
                interactionText.lookAt = hit.transform;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<Sequence_Totem>().ToggleTotem();
                }
            }
        }
        else
        {
            if (interactionText.gameObject.activeSelf == true)
            {
                interactionText.gameObject.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = lastCheckpoint;
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Grass")
        {
            isHidden = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            lastCheckpoint = other.transform.position;
        }

        if (other.tag == "Enemy")
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = lastCheckpoint;
            gameObject.GetComponent<CharacterController>().enabled = true;

            if(other.transform.childCount > 0)
            {
                other.gameObject.GetComponentInChildren<Enemy_StateMachine>().ToggleFollow(false, true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Grass")
        {
            isHidden = false;
        }
    }
}
