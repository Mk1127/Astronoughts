using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence_Totem : MonoBehaviour
{
    [SerializeField] float detectDistance;
    [SerializeField] private bool toggled = false;
    [SerializeField] GameObject flame;

    [SerializeField] List<Material> materials = new List<Material>();

    private GameObject playerGO;
    private Player_Interact playerIS;

    private void Start()
    {
        if (playerGO == null)
        {
            playerGO = GameObject.Find("Player");
        }

        if(playerIS == null)
        {
            playerIS = playerGO.GetComponent<Player_Interact>();
        }

        SetMaterial(0);
    }

    private void Update()
    {
        if (!toggled)
        {
            if (Vector3.Distance(transform.position, playerGO.transform.position) < detectDistance)
            {
                RaycastHit hit;

                Vector3 raydir = ((playerGO.transform.position - transform.position) + (Vector3.up * .5f)).normalized;

                if (Physics.Raycast(transform.position, raydir, out hit, Vector3.Distance(transform.position, playerGO.transform.position)))
                {
                    Debug.DrawRay(transform.position, raydir * Vector3.Distance(transform.position, playerGO.transform.position), Color.red);

                    if (hit.collider.tag == "Player")
                    {
                        if (playerIS.currentInteraction == "")
                        {
                            playerIS.currentInteraction = this.name;
                        }

                        if (playerIS.currentInteraction == this.name)
                        {
                            SetMaterial(1);

                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                ToggleTotem();
                            }
                        }
                    }
                    else
                    {
                        if (playerIS.currentInteraction == this.name)
                        {
                            playerIS.currentInteraction = "";

                            SetMaterial(0);
                        }
                    }
                }
                else
                {
                    if (playerIS.currentInteraction == this.name)
                    {
                        playerIS.currentInteraction = "";
                        SetMaterial(0);
                    }
                }
            }
            else
            {
                if (playerIS.currentInteraction == this.name)
                {
                    playerIS.currentInteraction = "";
                    SetMaterial(0);
                }
            }
        }
        else
        {
            if (playerIS.currentInteraction == this.name)
            {
                playerIS.currentInteraction = "";
                SetMaterial(0);
            }
        }
    }

    public void ToggleTotem()
    {
        if (!toggled)
        {
            //gameObject.GetComponent<MeshRenderer>().material.color = totemColor;
            flame.SetActive(true);
            GameObject parent = transform.parent.gameObject;
            parent.GetComponent<Sequence_Door>().addToSequence(this.name);
        }

        toggled = true;
    }

    public void ResetTotem()
    {
        //gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        flame.SetActive(false);
        toggled = false;
    }

    private void SetMaterial(int index)
    {
        if (materials.Count > 1)
        {
            GetComponent<MeshRenderer>().material = materials[index];
        }
    }
}
