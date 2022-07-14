using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    LineRenderer lr;

    [Header("Collision (Only Set If This Is A Laser)")]
    [SerializeField] bool hasCollision;
    [SerializeField] BoxCollider colliderObject;

    [Header("Transform")]
    [SerializeField] Transform startPoint;

    [Header("Light")]
    [SerializeField] int maxBounces;
    [SerializeField] bool reflectOffMirror;
    [SerializeField] float lineWidth;
    [SerializeField] float lineLength;

    [Header("Debug")]
    [SerializeField] bool debug;

    float firstLength;
    float lastLength;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = maxBounces + 1;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.SetPosition(0, startPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        CastLaser(transform.position, transform.forward);
    }

    void CastLaser(Vector3 position, Vector3 direction)
    {
        lr.SetPosition(0, startPoint.position);



        for (int i = 0; i < maxBounces; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, lineLength, 1))
            {
                firstLength = Vector3.Distance(lr.GetPosition(1), startPoint.position);
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal/*new Vector3(hit.normal.x, hit.normal.y, hit.normal.z)*/);
                lr.SetPosition(i + 1, hit.point);

                if (!hasCollision)
                {
                    if (hit.collider.tag == "Melt")
                    {
                        hit.collider.gameObject.SetActive(false);
                    }

                    if (hit.transform.tag != "Mirror" && reflectOffMirror)
                    {
                        for (int j = (i + 1); j <= maxBounces; j++)
                        {
                            lr.SetPosition(j, hit.point);

                        }
                        break;
                    }
                }
                else
                {
                    if (hit.transform.tag == "Block")
                    {
                        for (int j = (i + 1); j <= maxBounces; j++)
                        {
                            lr.SetPosition(j, hit.point);
                        }
                        break;
                    }
                }
            }
        }

        if (hasCollision)
        {
            if (lastLength != firstLength)
            {
                if (debug)
                {
                    Debug.Log(firstLength);
                }

                colliderObject.size = new Vector3(1, 1, (firstLength * 10) - 1);
                colliderObject.center = new Vector3(0, 0, (colliderObject.size.z / 2) - 1);
                lastLength = firstLength;
            }
        }
    }
}
