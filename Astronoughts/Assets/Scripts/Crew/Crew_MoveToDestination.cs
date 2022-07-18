using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crew_MoveToDestination:MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform destination;
    [SerializeField] float rotateSpeed;
    [SerializeField] float speed;
    [SerializeField] List<Vector3> checkpoints = new List<Vector3>(); //Offset from the start position
    [SerializeField] bool showCheckpoints = true;

    private string scene;
    private Vector3 startPos;
    private Vector3 nextCheckPoint;

    private int checkpointIndex = 0;
    private bool playing = false;


    private void Awake()
    {
        scene = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        startPos = transform.position;

        if(checkpoints.Count > 0)
        {
            nextCheckPoint = startPos + checkpoints[checkpointIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        SelectCheckpoint();
        MoveToCheckpoint();
        DebugLogs();
    }

    private void SelectCheckpoint()
    {
        if(Vector3.Distance(transform.position,nextCheckPoint) < 0.1f)
        {
            nextCheckPoint = startPos + checkpoints[checkpointIndex];
            if(checkpointIndex != checkpoints.Count - 1)
            {
                checkpointIndex++;
            }
            else
            {
                checkpointIndex = 0;
            }
        }
    }

    private void MoveToCheckpoint()
    {
        transform.position = Vector3.MoveTowards(transform.position,nextCheckPoint,speed * 0.005f);
        LookAtTarget(nextCheckPoint);
    }

    private void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 target = (targetPosition + Vector3.up) - transform.position;
        float singleStep = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target, singleStep, 0f);
        Quaternion nextDirection = Quaternion.LookRotation(newDirection);
        nextDirection.x = 0;
        nextDirection.z = 0;
        transform.rotation = nextDirection;
    }

    private void DebugLogs()
    {
        Debug.Log(checkpointIndex);
    }

    private void OnDrawGizmosSelected()
    {
        if(showCheckpoints)
        {
            Gizmos.color = Color.yellow;
            Vector3 gizmoPosItion;

            if(checkpoints.Count > 0)
            {
                for(int i = 0;i < checkpoints.Count;i++)
                {
                    if(!playing)
                    {
                        gizmoPosItion = transform.position + checkpoints[i];
                        Gizmos.DrawCube(gizmoPosItion,Vector3.one);
                    }
                    else
                    {
                        gizmoPosItion = new Vector3(startPos.x,startPos.y,startPos.z) + checkpoints[i];
                        Gizmos.DrawCube(gizmoPosItion,Vector3.one);
                    }
                }
            }
        }


    }
}
