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

    public string currentInteraction = "";

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

        if (interactionText.gameObject.activeSelf == true)
        {
            interactionText.gameObject.SetActive(false);
        }


    }
    // Update is called once per frame
    void Update()
    {
        /*RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, rayDistance))
        {

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
        }*/
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
