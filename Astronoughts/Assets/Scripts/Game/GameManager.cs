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

    //spaceship model
    public bool spaceshipBrokenDownEnabled;
    public bool spaceshipBrokenUpEnabled;
    public bool shipsolar1Enabled;
    public bool shipsolar2Enabled;
    public bool shipengineEnabled;
    public bool shipcockpitEnabled;
    public bool spaceshipWholeEnabled;

    // player statistics
    [HideInInspector] public float playerFuel;
    [HideInInspector] public float currentFuel;
    [HideInInspector] public int crewNumber;
    public int parts;
    public int crew;

    // inventory statistics
    public Button panelButton;
    public Button solar1Button;
    public Button solar2Button;
    public Button engineButton;
    public Button cockpitButton;

    public bool panelEnabled;
    public bool solar1Enabled;
    public bool solar2Enabled;
    public bool engineEnabled;
    public bool cockpitEnabled;

    public List<GameObject> partList = new List<GameObject>();
    public List<bool> astronoughtsFound = new List<bool>();

    //game statistics
    private string currentScene;

    //sources
    private GameObject player;
    private GameObject UIController;
    private Player playerScript;
    private UIControllerScript UIScript;

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
    void Awake()
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

        Prepare();
        GetSlots();

        //game statistics
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void Prepare()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UIController = GameObject.FindGameObjectWithTag("UIController");
        playerScript = player.GetComponent<Player>();
        UIScript = UIController.GetComponent<UIControllerScript>();
        spaceshipBrokenDown.gameObject.SetActive(true);
        spaceshipBrokenUp.gameObject.SetActive(false);
        solar1.gameObject.SetActive(false);
        solar2.gameObject.SetActive(false);
        engine.gameObject.SetActive(false);
        spaceshipWhole.gameObject.SetActive(false);
    }

    public void GatherStats()
    {
        Parts = parts;
        Crew = crew;
        // get shipState from number of pairs implemented (method not yet established) 
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
