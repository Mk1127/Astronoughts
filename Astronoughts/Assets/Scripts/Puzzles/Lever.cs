using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] Transform door;
    [SerializeField] float lowerDistance;
    [SerializeField] public bool raise;
    [SerializeField] bool isLever;
    bool isToggled;
    [SerializeField] MeshRenderer renderer;
    [SerializeField] List<Material> materials = new List<Material>();
    [SerializeField] Collider collider;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void LowerDoor()
    {
        if(!raise ||!isLever && !isToggled)
        {
            StartCoroutine(lowerDoor());
        }

        if(isLever)
        {
            transform.tag = "Untagged";
            collider.enabled = false;
            renderer.material = materials[1];
            collider.enabled = true;
        }
    }

    public void RaiseDoor()
    {
        if(raise || !isLever && !isToggled)
        {
            StartCoroutine(raiseDoor());
        }

        if(isLever)
        {
            transform.tag = "Untagged";
            collider.enabled = false;
            renderer.material = materials[1];
            collider.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isLever)
        {
            LowerDoor();
            renderer.material = materials[1];
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isLever)
        {
            RaiseDoor();
            renderer.material = materials[0];
        }
    }

    IEnumerator lowerDoor()
    {
        for (float i = 0; i < lowerDistance; i += 0.1f)
        {
            door.position -= new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator raiseDoor()
    {
        for (float i = 0; i < lowerDistance; i += 0.1f)
        {
            door.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
