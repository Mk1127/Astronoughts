using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronoughts : MonoBehaviour
{
    public int astroIndex;
    [SerializeField] bool inHub;
    [SerializeField] GameManager gm;
    [SerializeField] Animator anim;
    [SerializeField] string[] anims;
    [SerializeField] string animationName;
    [SerializeField] AudioClip[] crewClips;
    [SerializeField] AudioSource crewSource;
    private GameObject zeroFourGreen;

    private void Start()
    {
        gm = GameManager.gameManager;
        zeroFourGreen = GameObject.FindGameObjectWithTag("Green");

        if(inHub)
        {
            if(!zeroFourGreen)
            {
                if(gm.astronoughtsFound[astroIndex])
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
                GetAnim(animationName);
            }
        }
        else
        {
            if(gm.astronoughtsFound[astroIndex])
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }


    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Pause());
            if(zeroFourGreen)
            {
                UpdateGM();
            }
            else if(!zeroFourGreen)
            {
                if(!inHub)
                {
                    UpdateGM();
                }
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

    public string GetAnim(string animationName)
    {
        return anims[UnityEngine.Random.Range(0,anims.Length)];
    }

    public AudioClip GetCrewClip()
    {
        return crewClips[UnityEngine.Random.Range(0,crewClips.Length)];
    }

    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(2);
    }
}
