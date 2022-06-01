using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Follow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float rotateSpeed;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, (player.position + Vector3.up), speed * 0.005f);
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector3 target = (player.position + Vector3.up) - transform.position;
        float singleStep = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target, singleStep, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
