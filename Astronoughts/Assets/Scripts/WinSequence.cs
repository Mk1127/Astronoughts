using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSequence : MonoBehaviour
{
    private string scene;
    public int duration;
    // Start is called before the first frame update

    private void Awake()
    {
        scene = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        if(scene == "SpaceShipWinSequence")
        {
            StartCoroutine(PauseBeforeMenu());

        }
    }

    // Update is called once per frame
    IEnumerator PauseBeforeMenu()
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("Menu");
    }
}
