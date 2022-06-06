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
    private bool convoHidden;
    private bool allHidden;

    public GameObject overlay;
    public GameObject statusPanel;
    public GameObject convoPanel;
    public GameObject[] allPanels;

    public Button startButton;
    public Button overlayButton;
    public Button inventoryButton;
    public Button statusPanelButton;
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
        //convoHidden = true;
        //allHidden = false;

        overlay.SetActive(true);
        statusPanel.SetActive(true);
        convoPanel.SetActive(true);
    }

    #region Functions
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
            statusPanel.SetActive(false);
            overlayHidden = true;
            statusHidden = true;
        }
        else
        {
            overlay.SetActive(true);
            statusPanel.SetActive(true);
            overlayHidden = false;
            statusHidden = false;
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
    #endregion

}
