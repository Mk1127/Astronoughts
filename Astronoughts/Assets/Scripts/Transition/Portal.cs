using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Portal : MonoBehaviour
{
    [SerializeField] string nextScene;
    GameManager gm;
    private AudioSource source;
    private AudioClip clip;

    private void Start()
    {
        gm = GameManager.gameManager;

        if (nextScene == "SpaceShipWinSequence")
        {
            if(gm.Parts < 5 && gm.Crew < 4)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("hitting Player");
            if(nextScene != "")
            {
                gm.lastScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
