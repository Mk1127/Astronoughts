using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    [SerializeField] Transform camPivot;
    [HideInInspector] public bool isGrabbing = false;
    // Update is called once per frame
    void Update()
    {
        if(!isGrabbing)
        {
            if (Input.GetAxisRaw("Vertical") > 0) //Up
            {
                transform.rotation = camPivot.rotation;
            }

            if (Input.GetAxisRaw("Vertical") < 0) //Down
            {
                transform.rotation = Quaternion.Euler(0, camPivot.rotation.eulerAngles.y + 180, 0);
            }

            if (Input.GetAxisRaw("Horizontal") > 0) //Right
            {
                transform.rotation = Quaternion.Euler(0, camPivot.rotation.eulerAngles.y + 90, 0);
            }

            if (Input.GetAxisRaw("Horizontal") < 0) //Left
            {
                transform.rotation = Quaternion.Euler(0, camPivot.rotation.eulerAngles.y - 90, 0);
            }

            if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") < 0) //Up + Left
            {
                transform.rotation = Quaternion.Euler(0, camPivot.rotation.eulerAngles.y - 45, 0);
            }

            if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") > 0) //Up + Right
            {
                transform.rotation = Quaternion.Euler(0, camPivot.rotation.eulerAngles.y + 45, 0);
            }

            if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") < 0) //Down + Left
            {
                transform.rotation = Quaternion.Euler(0, camPivot.rotation.eulerAngles.y - 135, 0);
            }

            if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") > 0) //Down + Right
            {
                transform.rotation = Quaternion.Euler(0, camPivot.rotation.eulerAngles.y + 135, 0);
            }
        }
    }
}
