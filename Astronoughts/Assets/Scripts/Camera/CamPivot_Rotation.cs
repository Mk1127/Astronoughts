using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPivot_Rotation : MonoBehaviour
{
    bool canRotate = true;
    [SerializeField] float degreesToRotate;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //Scroll Up
        {
            if (canRotate)
            {
                StartCoroutine(RotateCam(new Vector3(0, 1, 0), degreesToRotate, 0.5f));
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //Scroll Down
        {
            if (canRotate)
            {
                StartCoroutine(RotateCam(new Vector3(0, 1, 0), -degreesToRotate, 0.5f));
            }
        }
    }

    IEnumerator RotateCam(Vector3 axis, float angle, float duration)
    {
        canRotate = false;

        Quaternion from = transform.rotation;

        Quaternion to = transform.rotation;

        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to; //set transforms rotation to new rotation

        canRotate = true;
    }
}
