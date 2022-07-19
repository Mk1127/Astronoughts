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
    [SerializeField] private GameObject GMPrefab;

    //public GameObject astroRed;
    //public GameObject astroBlue;
    //public GameObject astroYellow;
    //public GameObject redPrefab;
    //public GameObject bluePrefab;
    //public GameObject yellowPrefab;

    public Animator startAnimator;
    public Animator panelAnimator;
    public bool isHidden;
    public bool isPlaying;
    #endregion
    private void Awake()
    {
        // identify the scene name
        scene = SceneManager.GetActiveScene().name;
        switch(scene)
        {
            case "Hub":
                restartText.text = "Restart Game";
                break;
            case "Jungle":
                restartText.text = "Reload Level";
                break;
            case "Volcano":
                restartText.text = "Reload Level";
                break;
            default:
                restartText.text = "Restart Game";
                break;
        }
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
    public void ReloadLevel()
    {
        // This button was clicked.
        Debug.Log("You clicked the reload button");
        switch(scene)
        {
            case "Hub":
                //astroRed = GameObject.FindGameObjectWithTag("Red");
                //ResetRedAstro();
                //astroBlue = GameObject.FindGameObjectWithTag("Blue");
                //ResetBlueAstro();
                //astroYellow = GameObject.FindGameObjectWithTag("Yellow");
                //ResetYellowAstro();

                gmScript.YellowFound = false;
                gmScript.RedFound = false;
                gmScript.BlueFound = false;

                gmScript.panelButton.interactable = false;
                gmScript.engineButton.interactable = false;
                gmScript.solar1Button.interactable = false;
                gmScript.solar2Button.interactable = false;
                gmScript.cockpitButton.interactable = false; // button

                gmScript.solar1Enabled = false;
                gmScript.solar2Enabled = false;
                gmScript.panelEnabled = false;
                gmScript.engineEnabled = false;
                gmScript.cockpitEnabled = false; // bool

                invScript.invSolar2.interactable = false;
                invScript.invSolar1.interactable = false;
                invScript.invPanel.interactable = false;
                invScript.invEngine.interactable = false;
                invScript.invCockpit.interactable = false;

                gmScript.spaceshipBrokenDownEnabled = true;
                gmScript.spaceshipBrokenUpEnabled = false;
                gmScript.shipcockpitEnabled = false; //model
                gmScript.gotCockpit = false;

                // run the button animation
                panelAnimator.SetBool("isPlaying",false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;

            case "Jungle":
                //ResetYellowAstro();
                //ResetBlueAstro();

                gmScript.solar1Button.interactable = false;
                gmScript.solar2Button.interactable = false;

                gmScript.solar1Enabled = false;
                gmScript.solar2Enabled = false;

                invScript.invSolar2.interactable = false;
                invScript.invSolar1.interactable = false;

                gmScript.YellowFound = false;
                gmScript.BlueFound = false;

                // run the button animation
                panelAnimator.SetBool("isPlaying",false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;

            case "Volcano":
                //ResetRedAstro();
                gmScript.panelButton.interactable = false;
                gmScript.engineButton.interactable = false;

                gmScript.panelEnabled = false;
                gmScript.engineEnabled = false;

                invScript.invPanel.interactable = false;
                invScript.invEngine.interactable = false;

                gmScript.RedFound = false;

                // run the button animation
                panelAnimator.SetBool("isPlaying",false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            default:
                // run the button animation
                panelAnimator.SetBool("isPlaying",false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }


    public void RestartGame()
    {
        // This button was clicked.
        Debug.Log("You clicked the restart button");
        // run the button animation
        panelAnimator.SetBool("isPlaying",false);
        gmScript.Awake();
        SceneManager.LoadScene(scene);
    }

    public void ResetJungle()
    {

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
