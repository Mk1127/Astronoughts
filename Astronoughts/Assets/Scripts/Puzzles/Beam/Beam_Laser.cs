using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Laser : MonoBehaviour
{
    [SerializeField] bool toggled;
    [SerializeField] float rayDistance;
    [SerializeField] Transform lightRay;
    private float startRayDisctance;
    // Start is called before the first frame update
    void Start()
    {
        startRayDisctance = rayDistance;
        lightRay.localScale = new Vector3(rayDistance / this.transform.localScale.x, lightRay.localScale.y, lightRay.localScale.z);
        lightRay.localPosition = new Vector3(lightRay.localPosition.x, lightRay.localPosition.y, rayDistance / 2);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, rayDistance))
        {
            if (hit.collider.tag == "Block" || hit.collider.tag == "Untagged")
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDistance;
                Debug.DrawRay(transform.position, forward, Color.green);

                lightRay.localScale = new Vector3(rayDistance / this.transform.localScale.x, lightRay.localScale.y, lightRay.localScale.z);
                lightRay.localPosition = new Vector3(lightRay.localPosition.x, lightRay.localPosition.y, rayDistance / (2 * 0.5f));

                rayDistance = Vector3.Distance(hit.transform.position, transform.position);
            }
        }
        else
        {
            rayDistance = startRayDisctance;
            lightRay.localScale = new Vector3(rayDistance / this.transform.localScale.x, lightRay.localScale.y, lightRay.localScale.z);
            lightRay.localPosition = new Vector3(lightRay.localPosition.x, lightRay.localPosition.y, rayDistance / (2 * 0.5f));
        }

        Vector3 straight = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, straight, Color.green);
    }
}
