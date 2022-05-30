//////////////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Julian Davis
//Section: (2022SU.SGD.289)
//Instructor: Amber Johnson
//////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMUIController : MonoBehaviour
{
    #region Variables
    public string scene;

    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject instructionsMenu;
    public GameObject creditsMenu;

    public GameObject mainButtonPanel;
    public Button startButton;
    public Button creditsButton;
    public Button controlsButton;
    public Button helpButton;
    public Button quitButton;

    public Text text;

    public Animator startAnimator;
    public Animator panelAnimator;
    public bool isHidden;
    public bool isPlaying;
    #endregion

    void Start()
    {
        isPlaying = true;
        panelAnimator.SetBool("isPlaying",true);
        if(scene == "Splash")
        {
            text.text = "Skip";
            mainMenu.SetActive(false);
            controlsMenu.SetActive(false);
            instructionsMenu.SetActive(true);
            creditsMenu.SetActive(false);

            startButton.enabled = false;
            startButton.gameObject.SetActive(false);
            creditsButton.enabled = false;
            creditsButton.gameObject.SetActive(false);
            controlsButton.enabled = false;
            controlsButton.gameObject.SetActive(false);
        }
        else if(scene == "Menu")
        {
            text.text = "Help";
            mainMenu.SetActive(true);
            controlsMenu.SetActive(false);
            instructionsMenu.SetActive(false);
            creditsMenu.SetActive(false);
        }
    }

    public void OnStartButtonClick()
    {
        // This button was clicked.
        Debug.Log("You clicked the start button");
        // run the animation
        startAnimator.SetBool("isPlaying", false);
        SceneManager.LoadScene("Hub");
    }

    public void OnCreditsButtonClick()
    {
        Debug.Log("You clicked the credits button");
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
    }

    public void OnHelpButtonClick()
    {
        Debug.Log("You clicked the skip button");
        if(scene == "Splash")
        {
            // run the animation
            panelAnimator.SetBool("isPlaying",false);
            SceneManager.LoadScene("Menu");
        }
        else if(scene == "Menu")
        {
            Debug.Log("You clicked the help button");
            text.text = "Help";
            instructionsMenu.SetActive(true);
            mainMenu.SetActive(false);
            controlsMenu.SetActive(false);
            creditsMenu.SetActive(false);
        }
    }

    public void OnControlsButtonClick()
    {
        Debug.Log("You clicked the controls button");
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
        instructionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("You clicked the quit button");
        Application.Quit();
    }

}
