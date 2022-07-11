using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Interact : MonoBehaviour
{
    [SerializeField] float rayDistance;
    [SerializeField] Player_Rotation playerRot;
    [SerializeField] Player_Movement playerMove;
    [SerializeField] CamPivot_Rotation camPivot;
    [SerializeField] InteractionText interactionText;
    [SerializeField] Vector3 lastCheckpoint;

    public bool isHidden;
    private bool isHolding = false;

    private void Start()
    {
        if (interactionText == null)
        {
            if (!GameObject.Find("InteractionText"))
            {
                Debug.Log("Interaction Text was not found");
            }
            else
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

            if (hit.collider.tag == "Totem")
            {
                isHolding = false;

                AutoToggleInteractionText(hit.transform);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    hit.collider.gameObject.GetComponent<Sequence_Totem>().ToggleTotem();
                }
            }

            /*if (hit.collider.tag == "Lever")
            {
                isHolding = false;

                AutoToggleInteractionText(hit.transform);


                if (hit.collider.GetComponent<Lever>())
                {
                    Lever lever = hit.collider.gameObject.GetComponent<Lever>();

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if (lever.raise)
                        {
                            lever.RaiseDoor();
                        }
                        else
                        {
                            lever.LowerDoor();
                        }
                    }
                }
                else
                {
                    Door_Switch doorSwitch = hit.collider.gameObject.GetComponent<Door_Switch>();

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        doorSwitch.ToggleDoors();
                    }
                }

            }*/
        }
        else
        {
            if (interactionText != null)
            {
                if (interactionText.gameObject.activeSelf == true)
                {
                    interactionText.gameObject.SetActive(false);
                }
            }
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
        if (other.tag == "Enemy")
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = lastCheckpoint;
            gameObject.GetComponent<CharacterController>().enabled = true;

            if (other.transform.childCount > 0)
            {
                other.gameObject.GetComponentInChildren<Enemy_StateMachine>().ToggleFollow(false, true);
            }
        }

        if (other.tag == "Rock")
        {
            other.gameObject.GetComponent<LavaRock>().LowerRock();
            transform.parent = other.gameObject.transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grass")
        {
            isHidden = false;
        }

        if (other.tag == "Checkpoint")
        {
            lastCheckpoint = other.transform.position;
        }

        if (other.tag == "Rock")
        {
            transform.parent = null;
        }
    }

    public void AutoToggleInteractionText(Transform hit)
    {
        if (isHolding)
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            if (interactionText != null)
            {
                interactionText.lookAt = hit.transform;
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    public void LockPlayer()
    {
        playerRot.isGrabbing = true;
        playerMove.isGrabbing = true;
        playerMove.speed = playerMove.startSpeed / 1.5f;
        camPivot.enabled = false;


        isHolding = true;
    }

    public void ReleasePlayer()
    {
        playerRot.isGrabbing = false;
        playerMove.isGrabbing = false;
        playerMove.speed = playerMove.startSpeed;
        camPivot.enabled = true;

        isHolding = false;
    }
}
