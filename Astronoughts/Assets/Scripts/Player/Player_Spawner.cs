using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Spawner : MonoBehaviour
{
    [HideInInspector] public string lastScene;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.gameManager;

        lastScene = gm.lastScene;

        if(lastScene != "")
        {
            if (SceneManager.GetActiveScene().name == "Hub")
            {
                gameObject.GetComponent<CharacterController>().enabled = false;
                gameObject.transform.position = GameObject.Find(lastScene).transform.position;
                gameObject.GetComponent<CharacterController>().enabled = true;
            }
        }
    }
}
