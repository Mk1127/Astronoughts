using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMUIController : MonoBehaviour
{
    public GameObject mainButtonPanel;

    public Button startButton;
    public Button creditsButton;
    public Button helpButton;
    public Button quitButton;

    public Animator startAnimator;
    public bool isHidden;

    void Start()
    {
    }

    public void OpenSettings()
    {
        startAnimator.SetBool("isHidden", true);
    }

    public void OnStartButtonClick()
    {
        // This button was clicked.
        Debug.Log("You clicked the start button");
        // run the button animation
        startAnimator.SetBool("isHidden", true);
        SceneManager.LoadScene("level 01");
    }

    public void OnCreditsButtonClick()
    {
        Debug.Log("You clicked the credits button");
        SceneManager.LoadScene("credits");
    }

    public void OnHelpButtonClick()
    {
        Debug.Log("You clicked the help button");
        SceneManager.LoadScene("help");
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("You clicked the quit button");
        Application.Quit();
    }

}
