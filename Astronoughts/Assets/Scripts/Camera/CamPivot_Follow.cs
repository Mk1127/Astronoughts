using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPivot_Follow : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] float smoothSpeed;
    [SerializeField] Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null)
        {
            target = transform;
        }

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothdPosition = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothdPosition;
    }
}
