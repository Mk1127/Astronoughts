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
    
    // player statistics
    [HideInInspector] public float playerFuel;
    [HideInInspector] public float currentFuel;
    [HideInInspector] public int crewNumber;
    public int parts;
    public int crew;

    // inventory statistics
    public List<GameObject> partList = new List<GameObject>();
    //public SortedList SortedParts;

    //game statistics
    public int sceneCounter;
    private string currentScene;

    //sources
    private GameObject player;
    private GameObject UIController;
    [SerializeField] public Player playerScript;
    [SerializeField] public UIControllerScript UIScript;

    //display
    [SerializeField] public Text partsText;
    [SerializeField] public Text crewText;
    [SerializeField] public Text fuelText;
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
        player = GameObject.FindGameObjectWithTag("Player");
        UIController = GameObject.FindGameObjectWithTag("UIController");
        //playerScript = player.GetComponent<Player>();
        //UIScript = UIController.GetComponent<UIControllerScript>();

        //game statistics
        sceneCounter = 0;
        currentScene = SceneManager.GetActiveScene().name;

    }

     private void Update()
    {
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
            //GameObject Clone = Instantiate()

        }
        // Return the slots array
        return partList;
    }

}
