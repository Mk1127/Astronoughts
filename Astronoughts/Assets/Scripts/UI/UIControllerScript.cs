using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControllerScript : MonoBehaviour
{
    #region Variables
    public static UIControllerScript instance;

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

    // Use this for initialization
    void Awake()
    {
        /*if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return;
        }
        //Persist this instance through level change.
        DontDestroyOnLoad(gameObject);*/
    }

    // Start is called before the first frame update
    void Start()
    {
        overlayHidden = true;
        statusHidden = true;
        viewHidden = true;
        //convoHidden = false;
        //allHidden = false;

        overlay.SetActive(false);
        statusPanel.SetActive(false);
        viewPanel.SetActive(false);
        convoPanel.SetActive(false);
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
            viewPanel.SetActive(false);
            overlayHidden = true;
            statusHidden = true;
            viewHidden = true;
        }
        else
        {
            overlay.SetActive(true);
            statusPanel.SetActive(false);
            viewPanel.SetActive(false);
            overlayHidden = false;
            statusHidden = false;
            viewHidden = false;
        }
    }

    public void OnCloseViewButtonClick()
    {
        Debug.Log("You clicked the close view button");
        if(viewHidden == true)
        {
            viewPanel.SetActive(true);
            viewHidden = false;
        }
        else
        {
            viewPanel.SetActive(false);
            viewHidden = true;
        }
    }

    public void OnStatusButtonClick()
    {
        Debug.Log("You clicked the status button");
        if(statusHidden == true)
        {
            statusPanel.SetActive(true);
            statusHidden = false;
        }
        else
        {
            statusPanel.SetActive(false);
            statusHidden = true;
        }
    }
    #endregion

}
