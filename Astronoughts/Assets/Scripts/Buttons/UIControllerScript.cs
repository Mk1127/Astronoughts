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
    //private bool convoHidden;
    private bool viewHidden;
    private bool allHidden;

    public string Scene;

    public GameObject overlay;
    public GameObject statusPanel;
    public GameObject convoPanel;
    public GameObject viewPanel;
    public GameObject[] allPanels;

    public Button startButton;
    public Button overlayButton;
    public Button inventoryButton;
    public Button statusPanelButton;
    public Button viewPanelButton;
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
        viewHidden = true;
        //convoHidden = false;
        //allHidden = false;

        overlay.SetActive(true);
        statusPanel.SetActive(false);
        viewPanel.SetActive(false);
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

    public void OnCloseViewButtonClick()
    {
        Debug.Log("You clicked the close view button");
        if(viewHidden == false)
        {
            viewPanel.SetActive(false);
            viewHidden = true;
        }
        else
        {
            viewPanel.SetActive(true);
            viewHidden = false;
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
