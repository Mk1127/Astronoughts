using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager gameManager;
    
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
    public Button energyButton;

    public bool panelEnabled;
    public bool solar1Enabled;
    public bool solar2Enabled;
    public bool engineEnabled;
    public bool energyEnabled;

    public List<GameObject> partList = new List<GameObject>();

    //game statistics
    private string currentScene;

    //sources
    private GameObject player;
    private GameObject UIController;
    private Player playerScript;
    private UIControllerScript UIScript;

    //display
    [HideInInspector] public Text partsText;
    [HideInInspector] public Text crewText;
    [HideInInspector] public Text fuelText;
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
        parts = 0;
        crew = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        UIController = GameObject.FindGameObjectWithTag("UIController");
        playerScript = player.GetComponent<Player>();
        UIScript = UIController.GetComponent<UIControllerScript>();

        crewText = GameObject.FindGameObjectWithTag("crewText").GetComponent<Text>();
        partsText = GameObject.FindGameObjectWithTag("partsText").GetComponent<Text>();
        fuelText = GameObject.Find("fuelText").GetComponent<Text>();
    }

    public void GatherStats()
    {
        Parts = parts + playerScript.currentParts;
        Crew = crew + playerScript.currentCrew;
        // get shipState from number of pairs implemented (method not yet established) 
        partsText.text = "Parts: " + Parts;
        crewText.text = "Crew: " + Crew;
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
