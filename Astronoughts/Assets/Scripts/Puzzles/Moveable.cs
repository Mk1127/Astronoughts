using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] List<Material> materials = new List<Material>();

    [Header("Player")]
    [SerializeField] GameObject playerGO;
    [SerializeField] Player_Interact playerIS;
    [SerializeField] bool lockPlayer;
    [SerializeField] MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        if (playerGO == null)
        {
            playerGO = GameObject.Find("Player");
        }

        if (playerIS == null)
        {
            playerIS = playerGO.GetComponent<Player_Interact>();
        }

        if (renderer == null)
        {
            renderer = GetComponent<MeshRenderer>();
        }

        SetMaterial(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerGO.transform.position) < 3f)
        {
            RaycastHit hit;

            Vector3 raydir = ((playerGO.transform.position - transform.position) + (Vector3.up * .5f)).normalized;

            if (Physics.Raycast(transform.position, raydir, out hit, Vector3.Distance(transform.position, playerGO.transform.position)))
            {
                Debug.DrawRay(transform.position, raydir * Vector3.Distance(transform.position, playerGO.transform.position), Color.red);

                if (hit.collider.tag == "Player")
                {
                    if(playerIS.currentInteraction == "")
                    {
                        playerIS.currentInteraction = this.name;
                    }

                    if(playerIS.currentInteraction == this.name)
                    {
                        SetMaterial(1);

                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if (lockPlayer)
                            {
                                playerIS.LockPlayer();
                            }
                            else
                            {
                                GetComponent<Rigidbody>().isKinematic = true;
                            }

                            transform.position = playerGO.transform.position + (playerGO.transform.forward * 1.5f) + Vector3.up;
                            transform.parent = playerGO.transform;
                        }

                        if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            SetMaterial(1);

                            if (lockPlayer)
                            {
                                playerIS.ReleasePlayer();
                            }
                            else
                            {
                                GetComponent<Rigidbody>().isKinematic = false;
                            }
                            transform.parent = null;
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
        }
        else
        {
            if(playerIS.currentInteraction == this.name)
            {
                playerIS.currentInteraction = "";
                SetMaterial(0);
            }
        }
    }

    private void SetMaterial(int index)
    {
        if (materials.Count < 2)
        {
            Debug.LogError(transform.name + " needs 2 materials in the Moveable script");
        }
        else
        {
            if (renderer.material != materials[index])
            {
                renderer.material = materials[index];
            }
        }
    }
}
