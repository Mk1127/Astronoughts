using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private string shipState;


    // inventory statistics
    public GameObject slot;
    public GameObject[] slotsToFill;
    public Image[] collectedParts;
    public SortedList SortedParts;
 
    //game statistics
    public int sceneCounter;
    public string currentScene;

    //sources
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject UIController;
    public Player playerScript;
    public UIControllerScript UIScript;

    //display
    [SerializeField] public Text partsText;
    [SerializeField] public Text crewText;
    [SerializeField] public Text fuelText;
    [SerializeField] public Text shipText;
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
            partsText.text = "Parts: " + value;
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
            crewText.text = "Crew: " + value;
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
        playerScript = player.GetComponent<Player>();
        UIScript = UIController.GetComponent<UIControllerScript>();

        //game statistics
        sceneCounter = 0;
        currentScene = "";

    }

    private void Update()
    {

    }

    public void GatherStats()
    {
        parts = parts + playerScript.currentParts;
        crew = crew + playerScript.currentCrew;
        shipState = "My Ship is " + playerScript.currentShipState;
        currentScene = UIScript.scene;

        shipText.text = "My ship is " + shipState;
        partsText.text = "Parts: " + parts;
        crewText.text = "Crew: " + crew;
    }

}
