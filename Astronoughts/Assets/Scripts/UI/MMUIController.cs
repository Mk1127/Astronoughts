using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
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

    [SerializeField] GameObject[] creditsPanels;
    public GameObject mainButtonPanel;

    public Button startButton;
    public Button creditsButton;
    public Button controlsButton;
    public Button helpButton;
    public Button quitButton;

    int panelIndex = 0;

    public Text text;
    public Text text2;
    public Text text3;

    public Animator startAnimator;
    public Animator panelAnimator;
    public bool isHidden;
    public bool isPlaying;
    private 
    #endregion

    void Start()
    {
        if(scene == "SplashScreen")
        {
            text.text = "next";
            text2.text = "credits";
            text3.text = "controls";
            mainMenu.SetActive(false);
            controlsMenu.SetActive(false);
            instructionsMenu.SetActive(true);

            startButton.enabled = false;
            startButton.gameObject.SetActive(false);
            creditsButton.enabled = false;
            creditsButton.gameObject.SetActive(false);
            controlsButton.enabled = false;
            controlsButton.gameObject.SetActive(false);
        }
        else if(scene == "Menu")
        {
            isPlaying = true;
            panelAnimator.SetBool("isPlaying",true);
            text.text = "help";
            text2.text = "credits";
            text3.text = "controls";
            mainMenu.SetActive(true);
            controlsMenu.SetActive(false);
            instructionsMenu.SetActive(false);
        }
        else if(scene == "Credits")
        {
            text.text = "next";
            text2.text = "back";
            text3.text = "return";

            helpButton.enabled = true;
            helpButton.gameObject.SetActive(true);
            creditsButton.enabled = true;
            creditsButton.interactable = true;
            creditsButton.gameObject.SetActive(true);
            controlsButton.enabled = true;
            controlsButton.interactable = true;
            controlsButton.gameObject.SetActive(true);

            SwitchPanel();
        }
    }

    #region Functions
    public void OnStartButtonClick()
    {
        // This button was clicked.
        Debug.Log("You clicked the start button");
        // run the animation
        panelAnimator.SetBool("isPlaying", false);
        SceneManager.LoadScene("Hub");
    }

    public void OnCreditsButtonClick()
    {
        if(scene == "Credits")
        {
            Debug.Log("You clicked the back button");
            if(panelIndex == 0)
            {
                creditsButton.interactable = false;
            }
            else
            {
                creditsButton.interactable = true;
                panelIndex--;
                SwitchPanel();
            }
        }
        else
        {
            Debug.Log("You clicked the credits button");
            SceneManager.LoadScene("Credits");
        }
    }

    public void OnHelpButtonClick()
    {
        if(scene == "SplashScreen")
        {
            Debug.Log("You clicked the skip button");
            SceneManager.LoadScene("Menu");
        }
        else if(scene == "Credits")
        {
            Debug.Log("You clicked the next button");

            if(panelIndex == creditsPanels.Length - 1)
            {
                helpButton.interactable = false;
            }
            else
            {
                helpButton.interactable = true;
                panelIndex++;
                SwitchPanel();
            }
        }
        else
        {
            Debug.Log("You clicked the help button");
            text.text = "Help";
            instructionsMenu.SetActive(true);
            mainMenu.SetActive(false);
            controlsMenu.SetActive(false);
        }
    }

    public void OnControlsButtonClick()
    {
        if(scene == "Credits")
        {
            Debug.Log("You clicked the return button");
            SceneManager.LoadScene("Menu");
        }
        else
        {
            Debug.Log("You clicked the controls button");
            mainMenu.SetActive(false);
            controlsMenu.SetActive(true);
            instructionsMenu.SetActive(false);
        }
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("You clicked the quit button");
        Application.Quit();
    }

    void SwitchPanel()
    {
        GameObjectExt.SetAllActive(creditsPanels,false);
        creditsPanels[panelIndex].SetActive(true);
    }
    #endregion

}
