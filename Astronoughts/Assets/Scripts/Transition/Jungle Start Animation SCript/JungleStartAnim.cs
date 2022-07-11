using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleStartAnim : MonoBehaviour
{
    [SerializeField] float timer;

    [Header("Enemy")]
    [SerializeField] Enemy_Patrol enemyPatrol;

    [Header("Cam")]
    [SerializeField] CamPivot_Follow camFS;
    [SerializeField] CamPivot_Rotation camRS;

    [Header("Player")]
    [SerializeField] Player_Movement playerMS;
    [SerializeField] Player_Rotation playerRS;
    [SerializeField] Player_Animations playerAS;
    [SerializeField] Animator playerAnim;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(Animation());
        }
    }

    IEnumerator Animation()
    {
        Animtoggle(true);

        yield return new WaitForSeconds(timer);

        Animtoggle(false);

        gameObject.SetActive(false);
    }

    private void Animtoggle(bool hasStarted)
    {
        if(hasStarted)
        {
            camFS.target = enemyPatrol.gameObject.transform;
        }
        else
        {
            camFS.target = playerMS.gameObject.transform;
        }

        camRS.enabled = !hasStarted;
        playerMS.enabled = !hasStarted;
        playerRS.enabled = !hasStarted;
        playerAnim.enabled = !hasStarted;
        playerAS.enabled = !hasStarted;
        enemyPatrol.enabled = hasStarted;

    }
}
