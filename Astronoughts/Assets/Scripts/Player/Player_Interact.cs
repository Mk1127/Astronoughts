using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Interact : MonoBehaviour
{
    [SerializeField] float rayDistance;
    [SerializeField] Player_Rotation playerRot;
    [SerializeField] InteractionText interactionText;
    [SerializeField] Vector3 lastCheckpoint;
    public Text convoText;
    [SerializeField] AudioSource playerSource;
    public AudioClip[] grassClips;

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
        //playerSource = GameObject.FindWithTag("AudioSource").GetComponent<AudioSource>();

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

            if (hit.collider.tag == "Block")
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
                        interactionText.lookAt = hit.transform;
                        interactionText.gameObject.SetActive(true);
                    }
                }
            }

            if (hit.collider.tag == "Totem")
            {
                interactionText.lookAt = hit.transform;
                interactionText.gameObject.SetActive(true);
                

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    hit.collider.gameObject.GetComponent<Sequence_Totem>().ToggleTotem();
                }
            }

            if(hit.collider.tag == "Lever")
            {
                interactionText.lookAt = hit.transform;
                interactionText.gameObject.SetActive(true);

                Lever lever = hit.collider.gameObject.GetComponent<Lever>();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if(lever.raise)
                    {
                        lever.RaiseDoor();
                    }
                    else
                    {
                        lever.LowerDoor();
                    }
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Grass")
        {
            isHidden = true;
            //playerSource.mute = false;
            //playerSource.volume = 0.1f;
            //GrassStep();
            //playerSource.loop = true;
            //convoText.text = "I think I'm hidden now.";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
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
            //playerSource.loop = false;
            //playerSource.mute = true;
            //convoText.text = "";
        }

        if (other.tag == "Checkpoint")
        {
            lastCheckpoint = other.transform.position;
        }
    }

    private void GrassStep()
    {
        AudioClip clip = GetRandomClip();
        playerSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return grassClips[UnityEngine.Random.Range(0,grassClips.Length)];
    }
}
