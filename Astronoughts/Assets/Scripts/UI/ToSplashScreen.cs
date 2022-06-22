using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ToSplashScreen : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private string splashScene;
#pragma warning restore 0649
    private void Start()
    {
        StartCoroutine(WaitForEnd());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(splashScene);
        }

    }
    IEnumerator WaitForEnd()
    {
        //Print the time of when the function is first called.

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(64f);

        //After we have waited 64 seconds print the time again.

        SceneManager.LoadScene(splashScene);
    }
}
