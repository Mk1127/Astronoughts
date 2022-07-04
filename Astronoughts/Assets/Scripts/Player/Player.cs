using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player:MonoBehaviour
{
    #region Variables
    public static Player instance;
    public static Player Ps;
    //private Animator anim;

    [HideInInspector] public float currentFuel;
    [HideInInspector] public int currentCrew;
    [HideInInspector] public int currentParts;
    [HideInInspector] public string currentShipState;

    private float maxFuel = 100f;
    public Player_Movement script;
    public AudioSource jetpackSource;

    private GameObject UIController;
    private UIControllerScript UIScript;
    private GameObject gm;
    private GameManager gmScript;

    public GameObject convoPanel;
    private Rigidbody rb;

    // Shows the player what he needs to do
    public Text convoText;

    public Text fuelText;
    public Image fuelImage;
    public Text partsText;
    public Text crewText;

    [SerializeField] private AudioClip[] prizeClips;
    [SerializeField] private AudioSource prizeSource;
    [SerializeField] private AudioClip[] grassClips;
    [SerializeField] private AudioSource grassSource;

    public Button inventoryButton;
    private GameObject inventory;
    private Inventory invScript;

    public List<Item> items = new List<Item>();

    //private bool allCrew;
    //private bool allParts;
    //private bool fixedShip;
    //private bool didWin;
    #endregion

    #region Properties
    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GetReady();
        convoPanel.SetActive(true);
        convoText.text = "I should find the parts that fell off of my ship. If I can find my crew, they can do the repairs.";
        StartCoroutine(Wait());
        currentFuel = maxFuel;
    }

    private void GetReady()
    {
        UIController = GameObject.FindGameObjectWithTag("UIController");
        UIScript = UIController.GetComponent<UIControllerScript>();
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmScript = gm.GetComponent<GameManager>();
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        invScript = inventory.GetComponent<Inventory>();
        fuelText = GameObject.Find("FuelText").GetComponent<Text>();
        fuelImage = GameObject.Find("FuelImage").GetComponent<Image>();
        convoText = GameObject.Find("convoText").GetComponent<Text>();
        //crewText = GameObject.Find("crewText").GetComponent<Text>();
        //partsText = GameObject.Find("partsText").GetComponent<Text>();
        partsText.text = "Parts: " + gmScript.Parts;
        crewText.text = "Crew: " + gmScript.Crew;
    }

    private void Update()
    {
        CheckFuel();
    }

    #region Functions

    public void CheckWin()
    {
        //return;

        if(gmScript.Parts == 5)
        {
            convoText.text = "You've collected all the ship parts!";
            gmScript.spaceshipWholeEnabled = true;
            //didWin = true;
            StartCoroutine(Wait());
            //SceneManager.LoadScene("GameOver");
        }
    }
    public void CheckFuel()
    {
        fuelImage.fillAmount = currentFuel / maxFuel;
        currentFuel = (float)(Math.Truncate(script.thrustGauge * 100) / 100);
        fuelText.text = "JetPack: " + currentFuel;
        if(currentFuel < 25f)
        {
            fuelImage.color = Color.red;
        }
        else
        {
            fuelImage.color = Color.white;
        }
        if(script.hovering == false)
        {
            jetpackSource.GetComponent<AudioSource>().mute = true;
            return;
        }
        else if(script.hovering == true && currentFuel > 0)
        {
            jetpackSource.GetComponent<AudioSource>().mute = false;
        }
        else if(script.hovering == true && currentFuel <= 0)
        {
            jetpackSource.GetComponent<AudioSource>().mute = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Grass")
        {
            grassSource.mute = false;
            grassSource.volume = 0.1f;
            GrassStep();
            grassSource.loop = true;
            convoPanel.SetActive(true);
            convoText.text = "I think I'm hidden.";
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            gameObject.GetComponent<AudioSource>().Play();
            //convoPanel.SetActive(true);
            //convoText.text = "Argh!";
            //StartCoroutine(Wait());
        }

        if(other.tag == "Friend")
        {
            print("Collided with " + other.gameObject.name);
            //other.gameObject.GetComponent<AudioSource>().Play();
            convoPanel.SetActive(true);
            convoText.text = "It's my friend!";
            gmScript.crew++;
            gmScript.GatherStats();
            crewText.text = "Crew: " + gmScript.Crew;
            StartCoroutine(Wait());
        }

        if(other.tag == "Item") // If we collide with an item that we can pick up
        {
            //print("Collided with " + other.gameObject.name);
            Grab();
            convoPanel.SetActive(true);
            convoText.text = "I picked up my " + other.gameObject.name;
            gmScript.parts++;
            gmScript.GatherStats();
            partsText.text = "Parts: " + gmScript.Parts;
            if(other.gameObject.name == "Panel")
            {
                gmScript.panelButton.interactable = true;
                gmScript.panelEnabled = true;
                invScript.invPanel.interactable = true;
                other.gameObject.SetActive(false);
            }
            if(other.gameObject.name == "Solar1")
            {
                gmScript.solar1Button.interactable = true;
                gmScript.solar1Enabled = true;
                invScript.invSolar1.interactable = true;
                gmScript.spaceshipBrokenDownEnabled = false;
                gmScript.spaceshipBrokenUpEnabled = true;
                gmScript.shipsolar1Enabled = true;
                other.gameObject.SetActive(false);
            }
            if(other.gameObject.name == "Solar2")
            {
                gmScript.solar2Button.interactable = true;
                gmScript.solar2Enabled = true;
                invScript.invSolar2.interactable = true;
                gmScript.spaceshipBrokenDownEnabled = false;
                gmScript.spaceshipBrokenUpEnabled = true;
                gmScript.shipsolar2Enabled = true;
                other.gameObject.SetActive(false);
            }
            if(other.gameObject.name == "Engine")
            {
                gmScript.engineButton.interactable = true;
                gmScript.engineEnabled = true;
                invScript.invEngine.interactable = true;
                gmScript.spaceshipBrokenDownEnabled = false;
                gmScript.spaceshipBrokenUpEnabled = true;
                gmScript.shipengineEnabled = true;
                other.gameObject.SetActive(false);
            }
            if(other.gameObject.name == "Cockpit")
            {
                gmScript.cockpitButton.interactable = true; // button
                gmScript.cockpitEnabled = true; // bool
                invScript.invCockpit.interactable = true;
                gmScript.spaceshipBrokenDownEnabled = false;
                gmScript.spaceshipBrokenUpEnabled = true;
                gmScript.shipcockpitEnabled = false; //model
                gmScript.gotCockpit = true;
                gmScript.spaceshipBrokenUpEnabled = false;
                other.gameObject.SetActive(false);

            }
            StartCoroutine(Wait());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Grass")
        {
            grassSource.loop = false;
            grassSource.mute = true;
            convoText.text = "";
            convoPanel.SetActive(false);
        }
    }

    public void Grab()
    {
        AudioClip clip = GetPrizeClip();
        prizeSource.PlayOneShot(clip);
    }

 
    public void GrassStep()
    {
        AudioClip clip = GetGrassClip();
        grassSource.PlayOneShot(clip);
    }

    private AudioClip GetPrizeClip()
    {
        return prizeClips[UnityEngine.Random.Range(0,prizeClips.Length)];
    }
    private AudioClip GetGrassClip()
    {
        return grassClips[UnityEngine.Random.Range(0,grassClips.Length)];
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        convoPanel.SetActive(false);
    }
    #endregion

}
