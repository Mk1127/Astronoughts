using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Switch : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] List<Door_Door> targetDoors = new List<Door_Door>();
    [SerializeField] List<Material> materials = new List<Material>();

    [Header("Lever (Only set if this object will be a lever)")]
    [SerializeField] bool isLever;
    [SerializeField] Transform leverHandle;
    [SerializeField] Animator leverHandleAnimator;

    private GameObject playerGO;

    private bool toggled = false;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        if(playerGO == null)
        {
            playerGO = GameObject.Find("Player");
        }

        SetMaterial(0);
    }

    private void Update()
    {
        if (isLever)
        {
            if (Vector3.Distance(transform.position, playerGO.transform.position) < 5f)
            {
                RaycastHit hit;

                Vector3 raydir = ((playerGO.transform.position - transform.position) + (Vector3.up * .5f)).normalized;

                if (Physics.Raycast(transform.position, raydir, out hit, Vector3.Distance(transform.position, playerGO.transform.position)))
                {
                    Debug.DrawRay(transform.position, raydir * Vector3.Distance(transform.position, playerGO.transform.position), Color.red);

                    if (hit.collider.tag == "Player")
                    {
                        SetMaterial(1);

                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if (!isMoving)
                            {
                                StartCoroutine(ToggleHandle());
                                ToggleDoors();
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !isLever)
        {
            ToggleDoors();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && !isLever)
        {
            ToggleDoors();
        }
    }

    public void ToggleDoors()
    {
        for (int i = 0; i < targetDoors.Count; i++)
        {
            targetDoors[i].ToggleDoor();
        }

        if (toggled)
        {
            SetMaterial(0);
            toggled = false;
        }
        else
        {
            SetMaterial(1);
            toggled = true;
        }
    }

    private void SetMaterial(int index)
    {
        if (materials.Count > 1)
        {
            GetComponent<MeshRenderer>().material = materials[index];
        }
    }

    IEnumerator ToggleHandle()
    {
        isMoving = true;

        if(toggled)
        {
            leverHandleAnimator.Play("LeverUp");
        }
        else
        {
            leverHandleAnimator.Play("LeverDown");
        }

        float timeBase = 0;

        foreach(Door_Door door in targetDoors)
        {
            if(door.lowerDistance > timeBase)
            {
                timeBase = door.lowerDistance;
            }
        }

        yield return new WaitForSeconds(timeBase);

        Debug.Log("Lever Is Ready Again");

        isMoving = false;
    }
}
