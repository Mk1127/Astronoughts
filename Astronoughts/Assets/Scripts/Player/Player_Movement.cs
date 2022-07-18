using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Vector3 camF;
    Vector3 camR;

    Vector2 input;

    Vector3 intent;
    Vector3 velocityXZ;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public bool inGeyser = false;
    float startY;



    [Header("Movement")]
    [SerializeField] Transform cam;
    [SerializeField] CharacterController mover;
    [SerializeField] public float speed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float accel;
    [SerializeField] float grav;
    [HideInInspector] public float jumpSpeed;
    [HideInInspector] public bool isSprinting;
    [HideInInspector] public float startSpeed;

    [Header("Hover")]
    [SerializeField] ParticleSystem ps;
    [SerializeField] EmissionManager emissionManager;
    [SerializeField] float fadeTime;
    [SerializeField] public float thrustGauge = 100;
    [HideInInspector] public bool hovering = false;
    [HideInInspector] public bool canHover = true;

    [Header("Detection")]
    [SerializeField] public bool isGrounded = false;
    bool reverseGravity = false;
    [HideInInspector] public bool isGrabbing = false;

    [HideInInspector] Player_Interact playerIS;

    // Start is called before the first frame update
    void Start()
    {
        if (mover == false)
        {
            mover = GetComponent<CharacterController>();
        }

        playerIS = GetComponent<Player_Interact>();

        startSpeed = speed;
        hovering = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs(); //Checks horicontal and vertical inputs
        CalcCamera(); //Calculates cameras forwards and right based on it's rotation
        CheckGround();
        CalcMovement(); //Does all calculations for movement, doesn't move the character
        Gravity(); // Calculates and sets gravity for the character

        if(!isGrabbing)
        {
            if(canHover)
            {
                Hover();
            }
        }

        Jump();
        MoveCharacter();
    }

    void GetInputs()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
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
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * 0.5f, Color.cyan);
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 0.25f))
        {
            if(!hit.collider.isTrigger)
            {
                isGrounded = true;

                if (hit.collider.tag == "Geyser" || hit.collider.tag == "Enemy")
                {
                    isGrounded = false;
                }
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    void CalcMovement()
    {
        velocityXZ = velocity;
        velocityXZ.y = 0;

        if (isGrounded && !isGrabbing)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if(!hovering)
                {
                    isSprinting = true;

                    if(playerIS.isHidden)
                    {
                        speed = startSpeed * .75f;
                    }
                    else
                    {
                        speed = sprintSpeed;
                    }
                }
                else
                {
                    speed = startSpeed;
                    isSprinting = false;
                }
            }
            else
            {
                if(playerIS.isHidden)
                {
                    speed = startSpeed * .75f;
                    isSprinting = false;
                }
                else
                {
                    speed = startSpeed;
                    isSprinting = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if(!hovering)
                {
                    speed = startSpeed;
                    isSprinting = false;
                }
            }
        }

        if(!isGrounded)
        {
            speed = startSpeed * .75f;
        }

        if (isGrabbing)
        {
            isSprinting = false;
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                velocityXZ = Vector3.Lerp(velocity, cam.forward * input.y * speed, accel * Time.deltaTime);
            }
            else
            {
                velocityXZ = Vector3.Lerp(velocity, Vector3.zero, accel * Time.deltaTime);
            }

            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                velocityXZ = Vector3.Lerp(velocity, cam.right * input.x * speed, accel * Time.deltaTime);
            }
        }
        else
        {
            velocityXZ = Vector3.Lerp(velocity, transform.forward * input.magnitude * speed, accel * Time.deltaTime);
        }

        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
    }

    void Gravity()
    {
        if (!hovering)
        {
            if (isGrounded)
            {
                if (!reverseGravity)
                {
                    velocity.y = -1f; //If the player is not hovering, on the ground, and gravity is reversed set the Y velocity to 5. Lifts the player;
                }
            }
            else
            {
                velocity.y -= grav * Time.deltaTime; // If the player is not hovering and is not on the ground set Y velocity by gravity * time, Lowers the player.
            }
            velocity.y = Mathf.Clamp(velocity.y, -10, 50);
        }
        else
        {
            if (!reverseGravity)
            {
                velocity.y = 0.0f; //Player is hovering. Turns off gravity;
            }
        }
    }

    void Hover()
    {
        if (Input.GetKey(KeyCode.Space)) //On Press Space check for the following before starting to hover: Is not hovering, Is on the ground, and thrustGuage(fuel) is greater than 0. 
        {
            if (!hovering)
            {
                if (thrustGauge > 0)
                {
                    hovering = true;
                    StartCoroutine(StartHover());
                }
                
            }
        }
        else
        {
            hovering = false;
            reverseGravity = false;
        }
    }

    public void Jump()
    {
        if (inGeyser)
        {
            velocity.y = jumpSpeed;
            StartCoroutine(Geyser());
        }
    }

    void MoveCharacter()
    {
        mover.Move(velocity * Time.deltaTime);
    }

    IEnumerator DrainThrusters()
    {
        speed = startSpeed / 1.5f;
        while (thrustGauge > 0)
        {
            thrustGauge -= Time.deltaTime * 25;
            thrustGauge = Mathf.Clamp(thrustGauge, 0, 100);
            yield return null;

            if (!hovering)
            {
                break;
            }
        }

        hovering = false;
        reverseGravity = false;
        StartCoroutine(RefillThrusters());
    }

    IEnumerator RefillThrusters()
    {
        speed = startSpeed;
        float timer;
        if (thrustGauge == 0)
        {
            timer = 2f;
        }
        else
        {
            timer = 1f;
        }

        while (timer > 0)
        {
            if(isGrounded)
            {
                timer -= Time.deltaTime;
            }

            if (hovering)
            {
                hovering = true;
                StartCoroutine(DrainThrusters());
                yield break;
            }

            yield return null;
        }

        while (thrustGauge < 100)
        {
            if(isGrounded)
            {
                thrustGauge += Time.deltaTime * 20;
                thrustGauge = Mathf.Clamp(thrustGauge, 0, 100);
            }
            yield return null;

            if (hovering)
            {
                hovering = true;
                StartCoroutine(DrainThrusters());
                yield break;
            }
        }
    }

    IEnumerator StartHover() // Lifts the player and starts thruster drain and thruster VFX.
    {
        hovering = true;
        reverseGravity = true;

        startY = transform.position.y;

        StartCoroutine(DrainThrusters());
        StartCoroutine(SparksFade());

        if(isGrounded)
        {
            while (transform.position.y < startY + 1)
            {
                velocity.y = 3f;
                yield return null;
            }
        }

        reverseGravity = false;
    }

    IEnumerator Geyser()
    {
        while (velocity.y < jumpSpeed)
        {
            velocity.y += 0.8f;
            yield return null;
        }
        inGeyser = false;
    }

    public void OffGeyser()
    {
        inGeyser = false;
    }

    IEnumerator SparksFade()
    {
        var em = ps.emission;

        em.enabled = true;

        while (hovering)
        {
            yield return null;
        }

        em.enabled = false;
    }
}
