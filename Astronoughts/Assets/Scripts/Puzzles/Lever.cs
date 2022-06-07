using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] Transform door;
    [SerializeField] float lowerDistance;
    [SerializeField] bool isLever;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void LowerDoor()
    {
        StartCoroutine(lowerDoor());
    }

    public void RaiseDoor()
    {
        StartCoroutine(raiseDoor());
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isLever)
        {
            LowerDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isLever)
        {
            RaiseDoor();
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
