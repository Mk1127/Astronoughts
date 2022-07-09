using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crew_Wait : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float rotateSpeed;
    [SerializeField] float speed;

    private Vector3 startPos;
    //private bool playing = false;

    void Awake()
    {
        startPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //playing = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
    }


    private void LookAtTarget()
    {
        Vector3 target = (player.position + Vector3.up) - transform.position;
        float singleStep = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward,target,singleStep,0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
