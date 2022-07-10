using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronoughtGreen : MonoBehaviour
{
    public int astroIndex;
    [SerializeField] GameManager gm;
    [SerializeField] AudioClip[] crewClips;
    [SerializeField] AudioSource crewSource;

    private void Start()
    {
        gm = GameManager.gameManager;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gm.astronoughtsFound[astroIndex])
            {
                GetCrewClip();
            }
            else
            {
                GetComponent<AudioSource>().Play();
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

    private AudioClip GetCrewClip()
    {
        return crewClips[UnityEngine.Random.Range(0,crewClips.Length)];
    }

    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(10);
    }
}
