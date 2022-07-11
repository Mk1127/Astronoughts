using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Door : MonoBehaviour
{
    [SerializeField] public float lowerDistance;
    [SerializeField] bool isLowered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ToggleDoor()
    {
        if (isLowered)
        {
            StartCoroutine(RaiseDoor());
        }
        else
        {
            StartCoroutine(LowerDoor());
        }
    }

    IEnumerator LowerDoor()
    {
        isLowered = true;
        for (float i = 0; i < lowerDistance; i += 0.1f)
        {
            transform.position -= new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator RaiseDoor()
    {
        isLowered = false;
        for (float i = 0; i < lowerDistance; i += 0.1f)
        {
            transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
