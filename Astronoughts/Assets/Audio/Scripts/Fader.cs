using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Fader : MonoBehaviour
{
    public Image introScreen;
    public GameObject introPanel;
    public GameObject mainButtonPanel;
    float fadeTime = 3f;
    Color colorToFadeTo;

    public Animator startAnimator;
//    public Animator settings Button;
    public bool isHidden;

    public Button startButton;
    public Button creditsButton;
    public Button helpButton;
    public Button quitButton;
    
    void Start()
    {
        StartCoroutine(FadeOut());
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

    IEnumerator FadeOut()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        colorToFadeTo = new Color(1f, 1f, 1f, 0f);
        introScreen.CrossFadeColor(colorToFadeTo, fadeTime, true, true);
        yield return new WaitForSeconds(5);

        introPanel.SetActive(false);
        mainButtonPanel.SetActive(true);
    }

}
