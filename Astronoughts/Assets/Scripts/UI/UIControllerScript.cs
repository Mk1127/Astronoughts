using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIControllerScript : MonoBehaviour
{
    #region Variables
    public static UIControllerScript instance;

    private string scene;
    private GameObject gm;
    private GameManager gmScript;

    private bool overlayHidden;
    private bool miniMapHidden;
    private bool viewHidden;
    [HideInInspector] public bool clickedView;
    //private bool convoHidden;
    //private bool allHidden;

    public TMP_Text restartText;
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject miniMapPanel;
    [SerializeField] public GameObject convoPanel;
    [SerializeField] GameObject viewPanel;
    [SerializeField] GameObject[] allPanels;

    [SerializeField] Button startButton;
    [SerializeField] Button overlayButton;
    [SerializeField] Button inventoryButton;
    [SerializeField] Button miniMapPanelButton;
    [SerializeField] Button viewPanelButton;
    [SerializeField] Button quitButton;
    [SerializeField] private GameObject inventory;
    [SerializeField] private Inventory invScript;


    public Animator startAnimator;
    public Animator panelAnimator;
    public bool isHidden;
    public bool isPlaying;
    #endregion
    private void Awake()
    {
        // identify the scene name
        scene = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    void Start()
    {
        overlayHidden = true;
        miniMapHidden = true;
        viewHidden = true;
        //convoHidden = true;
        //allHidden = false;

        overlay.SetActive(false);
        miniMapPanel.SetActive(false);
        viewPanel.SetActive(false);
        convoPanel.SetActive(false);
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmScript = gm.GetComponent<GameManager>();
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        invScript = gm.GetComponent<Inventory>();
    }

    #region Functions

    public void OnQuitButtonClick()
    {
        Debug.Log("You clicked the quit button");
        panelAnimator.SetBool("isPlaying",false);
        Application.Quit();
    }

    public void OnOverlayButtonClick()
    {
        Debug.Log("You clicked the overlay button");
        if(overlayHidden == true)
        {
            overlay.SetActive(true);
            miniMapPanel.SetActive(true);
            viewPanel.SetActive(true);
            overlayHidden = false;
            miniMapHidden = false;
            viewHidden = false;
            //convoHidden = true;
        }
        else
        {
            overlay.SetActive(false);
            miniMapPanel.SetActive(false);
            viewPanel.SetActive(false);
            overlayHidden = true;
            miniMapHidden = true;
            viewHidden = true;
            //convoHidden = true;

        }
    }

    public void OnCloseViewButtonClick()
    {
        Debug.Log("You clicked the close view button");
        gmScript.viewCheck = true;
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

    public void OnMiniMapButtonClick()
    {
        Debug.Log("You clicked the miniMap button");
        if(miniMapHidden == true)
        {
            miniMapPanel.SetActive(true);
            miniMapHidden = false;
        }
        else
        {
            miniMapPanel.SetActive(false);
            miniMapHidden = true;
        }
    }
    #endregion

}
