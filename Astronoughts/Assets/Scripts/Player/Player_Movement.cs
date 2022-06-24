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



    [Header("Movement")]
    [SerializeField] Transform cam;
    [SerializeField] CharacterController mover;
    [SerializeField] float speed;
    [SerializeField] float accel;
    [SerializeField] float grav;
    [HideInInspector] public float jumpSpeed;

    float startSpeed;

    [Header("Hover")]
    [SerializeField] ParticleSystem ps;
    [SerializeField] EmissionManager emissionManager;
    [SerializeField] float fadeTime;
    [SerializeField] float thrustGauge = 100;
    [HideInInspector] public bool hovering = false;

    [Header("Detection")]
    [SerializeField] public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (mover == false)
        {
            mover = GetComponent<CharacterController>();
        }

        startSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs(); //Checks horicontal and vertical inputs
        CalcCamera(); //Calculates cameras forwards and right based on it's rotation
        CheckGround(); //Self explanatory
        CalcMovement(); //Does all calculations for movement, doesn't move the character
        Gravity(); // Calculates and sets gravity for the character
        Hover();
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
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 0.275f))
        {
            isGrounded = true;
            hovering = false;

            if (hit.collider.tag == "Geyser")
            {
                isGrounded = false;
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
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = startSpeed * 1.33f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = startSpeed;
            }

        }
        velocityXZ = Vector3.Lerp(velocity, transform.forward * input.magnitude * speed, accel * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
    }

    void Gravity()
    {
        if (!hovering)
        {
            if (isGrounded)
            {
                velocity.y = -0.5f;
            }
            else
            {
                velocity.y -= grav * Time.deltaTime;
            }

            velocity.y = Mathf.Clamp(velocity.y, -10, 50);
        }
        else
        {
            velocity.y = 0.0f;
        }
    }

    void Hover()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!hovering)
            {
                if (isGrounded)
                {
                    if (thrustGauge > 0)
                    {
                        hovering = true;
                        StartCoroutine(LiftPlayer());
                    }
                }
            }
            else
            {
                hovering = false;
            }
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
            Debug.Log("Thrusters: Draining");
            thrustGauge -= Time.deltaTime * 15;
            thrustGauge = Mathf.Clamp(thrustGauge, 0, 100);
            yield return null;

            if (!hovering)
            {
                StartCoroutine(RefillThrusters());
                yield break;
            }
        }

        Debug.Log("Thrusters: Empty");
        hovering = false;
        StartCoroutine(RefillThrusters());
    }

    IEnumerator RefillThrusters()
    {
        speed = startSpeed;
        if (thrustGauge == 0)
        {
            yield return new WaitForSeconds(3.0f);
        }

        while (thrustGauge < 100)
        {
            Debug.Log("Thrusters: Refilling");
            thrustGauge += Time.deltaTime * 10;
            thrustGauge = Mathf.Clamp(thrustGauge, 0, 100);
            yield return null;

            if (hovering)
            {
                StartCoroutine(DrainThrusters());
                yield break;
            }
        }

        Debug.Log("Thrusters: Full");

    }

    IEnumerator LiftPlayer()
    {
        mover.enabled = false;

        for (int i = 0; i < 10; i++)
        {
            Vector3 newposition = transform.position + Vector3.up;
            transform.position = Vector3.MoveTowards(transform.position, newposition, 0.1f);

            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(DrainThrusters());
        StartCoroutine(SparksFade());
        hovering = true;
        mover.enabled = true;
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
