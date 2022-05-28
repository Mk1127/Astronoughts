using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    [SerializeField] float rayDistance;
    [SerializeField] Player_Rotation playerRot;

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
                    //playerRot.enabled = false;
                }
                else
                {
                    hit.collider.gameObject.transform.parent = null;
                    //playerRot.enabled = true;
                }
            }

            if (hit.collider.tag == "Totem")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<Sequence_Totem>().ToggleTotem();
                }
            }
        }
    }
}
