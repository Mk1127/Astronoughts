using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager gameManager;

    //for player hub spawning
    [HideInInspector] public string lastScene = "";

    //spaceship model part is enabled/disabled variables
    public bool spaceshipBrokenDownEnabled;
    public bool spaceshipBrokenUpEnabled;
    public bool spaceshipWholeEnabled;
    public bool shipPanelEnabled;
    public bool shipsolar1Enabled;
    public bool shipsolar2Enabled;
    public bool shipengineEnabled;
    public bool shipcockpitEnabled;


    //crew found variables
    public bool GreenFound;
    public bool YellowFound;
    public bool RedFound;
    public bool BlueFound;
    public bool OrangeFound;

    public List<bool> astronoughtsFound = new();

    // Hub specific bools
    [HideInInspector] public bool grassCheck;
    [HideInInspector] public bool viewCheck;
    [HideInInspector] public bool blockInteract;
    [HideInInspector] public bool gotCockpit;

    // player statistics variables that are needed by other scripts
    [HideInInspector] public float playerFuel;
    [HideInInspector] public float currentFuel;
    [HideInInspector] public int crewNumber;
    private int parts;
    private int crew;

    // Inventory slots (buttons). Public for manual assignment
    public Button panelButton;
    public Button solar1Button;
    public Button solar2Button;
    public Button engineButton;
    public Button cockpitButton;

    // Game Manager bools to determine if something has been picked up
    public bool panelEnabled;
    public bool solar1Enabled;
    public bool solar2Enabled;
    public bool engineEnabled;
    public bool cockpitEnabled;

    public List<GameObject> partList = new();
    public List<GameObject> partsFound = new();



    // sources for imported information. What scripts and gameObjects must we access?
    // the GameObjects that have scripts we need
    private GameObject player;
    private GameObject UIController;
    
    // The scripts themselves that have variables we need to access
    private Player playerScript;
    private UIControllerScript UIScript;

    // The location of the jetpack fuel gauge
    public Transform contentContainer;

    #endregion

    #region Properties

    public int Parts
    {
        get => parts;
        set => parts = value;
    }
    public int Crew
    {
        get => crew;
        set => crew = value;
    }
    #endregion

    // Start is called before the first frame update

    // Ensure gm survives scenes changes
    public void Awake()
    {

        if(gameManager == null)
        {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }
        else if(gameManager != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // find the player and UIController GameObjects (and their scripts) onLoad from the scene
        Prepare();
        
        // Find the parts and add them to the partsList
        GetParts();
    }

    public void Update()
    {
        if(gotCockpit == true)
        {
            cockpitButton.interactable = true;
        }
    }

    private void Prepare()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UIController = GameObject.FindGameObjectWithTag("UIController");
        playerScript = player.GetComponent<Player>();
        UIScript = UIController.GetComponent<UIControllerScript>();
    }

    // assigning value of the public variables to private variables for safekeeping
    public void GatherStats()
    {
        Parts = parts;
        Crew = crew;
    }

    public List<GameObject> GetParts()
    {
        foreach(GameObject Part in GameObject.FindGameObjectsWithTag("Item"))
        {
            partList.Add(Part);
        }
        // Return the slots array
        return partList;
    }

}
