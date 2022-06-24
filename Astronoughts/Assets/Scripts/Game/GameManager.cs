using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager gameManager;
    
    // player statistics
    public int playerFuel;
    public int currentFuel;
    public int crewNumber;
    public int currentCrew;
    public int partsToFind;
    public int currentParts;
    public int repairsCompleted;

    // inventory statistics
    public GameObject slot;
    public GameObject[] slotsToFill;
    public Image[] collectedParts;
    public SortedList Parts;
 
    //game statistics
    public int sceneCounter;
    public string currentScene;
    public string shipState;

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
        playerFuel = 100;
        currentFuel = 100;
        crewNumber = 0;
        currentCrew = 0;
        partsToFind = 5;
        currentParts = 0;
        repairsCompleted = 0;

        //game statistics
        sceneCounter = 0;
        currentScene = "";
        shipState = "Incapacitated";
    }

}
