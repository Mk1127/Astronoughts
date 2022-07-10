using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Astronoughts : MonoBehaviour
{
    public int astroIndex;
    [SerializeField] bool inHub;
    [SerializeField] GameManager gm;
    [SerializeField] AudioClip[] crewClips;
    [SerializeField] AudioSource crewSource;

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
            GetCrewClip();
            StartCoroutine(Pause());
            if(!inHub)
            {
                UpdateGM();
                //gameObject.SetActive(false);
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

    private AudioClip GetCrewClip()
    {
        return crewClips[UnityEngine.Random.Range(0,crewClips.Length)];
    }

    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(10);
    }
}
