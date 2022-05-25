using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] string nextScene;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("hitting Player");
            if(nextScene != "")
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
