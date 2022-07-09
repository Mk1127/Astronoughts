using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronoughts : MonoBehaviour
{
    public int astroIndex;
    [SerializeField] bool inHub;
    [SerializeField] GameManager gm;

    private void Start()
    {
        gm = GameManager.gameManager;

        if (inHub)
        {
            if (gm.astronoughtsFound[astroIndex])
            {
                gameObject.SetActive(true);
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
                gameObject.SetActive(false);
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
