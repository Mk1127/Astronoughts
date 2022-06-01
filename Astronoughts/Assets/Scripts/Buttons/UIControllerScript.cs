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

public class UIControllerScript : MonoBehaviour
{
    #region Variables
    private bool overlayHidden;
    private bool statusHidden;
    private bool settingsHidden;
    private bool convoHidden;
    private bool allHidden;

    public GameObject overlay;
    public GameObject statusPanel;
    public GameObject settingsPanel;
    public GameObject convoPanel;
    public GameObject[] allPanels;

    public Button startButton;
    public Button overlayButton;
    public Button inventoryButton;
    public Button settingsPanelButton;
    public Button quitButton;

    public Animator startAnimator;
    public Animator panelAnimator;
    public bool isHidden;
    public bool isPlaying;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        overlayHidden = false;
        statusHidden = false;
        settingsHidden = false;
        //convoHidden = true;
        //allHidden = false;

        overlay.SetActive(true);
        statusPanel.SetActive(true);
        settingsPanel.SetActive(true);
        convoPanel.SetActive(false);

        startButton.enabled = true;
        startButton.gameObject.SetActive(true);
        inventoryButton.enabled = true;
        inventoryButton.gameObject.SetActive(true);
        overlayButton.enabled = true;
        overlayButton.gameObject.SetActive(true);
    }

    public void OnRestartClicked()
    {
        // This button was clicked.
        Debug.Log("You clicked the restart button");
        // run the button animation
        panelAnimator.SetBool("isPlaying",false);
        SceneManager.LoadScene("Hub");
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("You clicked the quit button");
        panelAnimator.SetBool("isPlaying",false);
        Application.Quit();
    }

    public void OnOverlayButtonClick()
    {
        Debug.Log("You clicked the overlay button");
        if(overlayHidden == false)
        {
            overlay.SetActive(false);
            overlayHidden = true;
        }
        else
        {
            overlay.SetActive(true);
            overlayHidden = false;
        }
    }

    public void OnStatusButtonClick()
    {
        Debug.Log("You clicked the status button");
        if(statusHidden == false)
        {
            statusPanel.SetActive(false);
            statusHidden = true;
        }
        else
        {
            statusPanel.SetActive(true);
            statusHidden = false;
        }
    }

    public void OnSettingsButtonClick()
    {
        Debug.Log("You clicked the settings button");
        if(settingsHidden == false)
        {
            settingsPanel.SetActive(false);
            settingsHidden = true;
        }
        else
        {
            settingsPanel.SetActive(true);
            settingsHidden = false;
        }
    }
}
