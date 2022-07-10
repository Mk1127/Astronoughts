using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [Header("Objects")]
    public GameObject geyserVFX;

    [Header("Floats")]
    public float geyserForce;
    public float startGeyserForce;

    [Header("Bools")]
    public bool isactive;

    private BoxCollider collider;

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

            if (other.tag == "Block")
            {
                StartCoroutine(ToggleGeyser(0, false));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            StartCoroutine(ToggleGeyser(0, false));
            StartCoroutine(SetParent(other));
            UpdateActiveGeysers(-1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Block")
        {
            StartCoroutine(ToggleGeyser(2, true));
            UpdateActiveGeysers(1);
        }
    }

    private void UpdateActiveGeysers(int changeBy)
    {
        transform.parent.GetComponent<Geyser_System>().activeGeysers += changeBy;
        transform.parent.GetComponent<Geyser_System>().UpdateGeysersForce();
    }

    IEnumerator SetParent(Collider other)
    {
        other.transform.parent = null;
        other.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(.2f);
        other.transform.position = transform.position + Vector3.up;
    }

    IEnumerator ToggleGeyser(float timer, bool setActive)
    {
        yield return new WaitForSeconds(timer);
        isactive = setActive;
        geyserVFX.SetActive(setActive);
    }
}
