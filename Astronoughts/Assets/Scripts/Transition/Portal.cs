using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] string nextScene;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.gameManager;

        if (nextScene == "Menu")
        {
            if(gm.parts < 5)
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
