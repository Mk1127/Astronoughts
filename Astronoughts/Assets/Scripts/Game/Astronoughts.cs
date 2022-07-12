using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronoughts : MonoBehaviour
{
    public int astroIndex;
    [SerializeField] bool inHub;
    [SerializeField] GameManager gm;
    [SerializeField] Animator anim;
    [SerializeField] string animationName;

    private void Start()
    {
        gm = GameManager.gameManager;

        if (inHub)
        {
            if (gm.astronoughtsFound[astroIndex])
            {
                gameObject.SetActive(true);
                anim.Play(animationName);
            }
            else
            {
                gameObject.SetActive(!true);
            }
        }
        else
        {
            if (gm.astronoughtsFound[astroIndex])
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Pause());
            if(!inHub)
            {
                UpdateGM();
            }
        }
    }

    public void UpdateGM()
    {
        gm.astronoughtsFound[astroIndex] = true;
    }

    private void ToggleAstro()
    {

    }

    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(10);
    }
}
