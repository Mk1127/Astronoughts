using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] CharacterController mover;

    Vector3 camF;
    Vector3 camR;

    Vector2 input;

    Vector3 intent;
    Vector3 velocityXZ;
    Vector3 velocity;

    [SerializeField] float speed;
    [SerializeField] float accel;
    [SerializeField] float grav;
    [SerializeField] float jumpSpeed;

    [SerializeField] bool isGRounded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (mover == false)
        {
            mover = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs(); //Checks horicontal and vertical inputs
        CalcCamera(); //Calculates cameras forwards and right based on it's rotation
        CheckGround(); //Self explanatory
        CalcMovement(); //Does all calculations for movement, doesn't move the character
        Gravity(); // Calculates and sets gravity for the character
        Jump();
        MoveCharacter();
    }

    void GetInputs()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 2);
    }

    void CalcCamera()
    {
        camF = cam.forward;
        camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 0.2f))
        {
            isGRounded = true;
        }
        else
        {
            isGRounded = false;
        }
    }

    void CalcMovement()
    {
        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocity, transform.forward * input.magnitude * speed, accel * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
    }

    void Gravity()
    {
        if (isGRounded)
        {
            velocity.y = -0.5f;
        }
        else
        {
            velocity.y -= grav * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10, 10);
    }

    void Jump()
    {
        if (isGRounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump Pressed");
                speed = 4;
                velocity.y = jumpSpeed;
            }
            else
            {
                speed = 5;
            }
        }
    }

    void MoveCharacter()
    {
        mover.Move(velocity * Time.deltaTime);
    }
}
