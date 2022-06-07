using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionText : MonoBehaviour
{
    [SerializeField] public Transform lookAt;
    [SerializeField] Vector3 offset;

    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(lookAt != null)
        {
            Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);
            if (transform.position != pos)
            {
                transform.position = pos;
            }
        }
    }
}
