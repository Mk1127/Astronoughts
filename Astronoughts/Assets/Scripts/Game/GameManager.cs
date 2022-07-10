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
    [HideInInspector] public bool spaceshipBrokenUpEnabled;
    [HideInInspector] public bool shipsolar1Enabled;
    [HideInInspector] public bool shipsolar2Enabled;
    [HideInInspector] public bool shipengineEnabled;
    [HideInInspector] public bool shipcockpitEnabled;
    [HideInInspector] public bool spaceshipWholeEnabled;

    //spaceship model part is enabled/disabled variables
    [HideInInspector] public bool GreenFound;
    [HideInInspector] public bool YellowFound;
    [HideInInspector] public bool RedFound;
    [HideInInspector] public bool BlueFound;
    [HideInInspector] public bool OrangeFound;

    // Hub specific bools
    public bool grassCheck;
    public bool viewCheck;
    public bool blockInteract;
    public bool gotCockpit;

    // player statistics variables that are needed by other scripts
    [HideInInspector] public float playerFuel;
    [HideInInspector] public float currentFuel;
    [HideInInspector] public int crewNumber;
    public int parts;
    public int crew;

    // Inventory slots (buttons). Public for manual assignment
    public Button panelButton;
    public Button solar1Button;
    public Button solar2Button;
    public Button engineButton;
    public Button cockpitButton;

    // Inventory bools. Button sprite swap changes empty icon (not interactable) to object (interactable)
    public bool panelEnabled;
    public bool solar1Enabled;
    public bool solar2Enabled;
    public bool engineEnabled;
    public bool cockpitEnabled;

    // public lists
    public List<GameObject> partList = new List<GameObject>();
    public List<bool> astronoughtsFound = new List<bool>();

    // which scene
    private string scene;

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
        get
        {
            return parts;
        }
        set
        {
            parts = value;
        }
    }

    public int Crew
    {
        get
        {
            return crew;
        }
        set
        {
            crew = value;
        }
    }
    #endregion

    // Start is called before the first frame update
    
    // Ensure gm survives scenes changes
    void Awake()
    {
        // identify the scene name
        scene = SceneManager.GetActiveScene().name;

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
        
        // Find the inventory slots and add them to the partsList
        GetSlots();
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

    public List<GameObject> GetSlots()
    {
        foreach(GameObject slot in GameObject.FindGameObjectsWithTag("Slot"))
        {
            partList.Add(slot);
        }
        // Return the slots array
        return partList;
    }

    public List<GameObject> UpdateSlots()
    {
        foreach(GameObject slot in partList)
        {
            partList.Remove(slot);
        }
        foreach(GameObject slot in GameObject.FindGameObjectsWithTag("Slot"))
{
            partList.Add(slot);
        }
        // Return the slots array
        return partList;
    }

}
