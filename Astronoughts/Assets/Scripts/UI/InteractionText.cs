using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionText : MonoBehaviour
{
    [SerializeField] public Transform lookAt;
    [SerializeField] Vector3 offset;

    [SerializeField] Camera cam;

    [SerializeField] GameManager gm;

    [Header("For Ship Only")]
    [SerializeField] bool isShip;
    [SerializeField] bool isFinished;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gameManager;

        cam = Camera.main;

        if (isShip)
        {
            if(gm.parts < 5)
            {
                if(isFinished)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }
            else
            {
                if(isFinished)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
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

        if(!isShip)
        {
            if (lookAt.gameObject.activeSelf == false)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
