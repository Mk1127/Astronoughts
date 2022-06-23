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

    private int currentFuel;
    public int maxFuel = 100;

    public GameObject player;
    [SerializeField]
    private Rigidbody rb;

    // Shows the player what he needs to do
    public Text convoText;
    public Text[] allText;
    public Sprite fullSlot;
    private int parts;
    private int crew;
    //private int shipState;

    [SerializeField]
    private Text partsText;
    [SerializeField]
    private Text crewText;
    [SerializeField]
    private Text fuelText;
    //[SerializeField]
    //private Text shipText;

    [SerializeField]
    private AudioClip[] prizeClips;
    [SerializeField]
    private AudioSource prizeSource;
    [SerializeField]
    private AudioClip[] grassClips;
    [SerializeField]
    private AudioSource grassSource;

    public Button inventoryButton;
    [SerializeField]
    private Inventory inventory;

    public List<Item> items = new List<Item>();

    private bool allCrew;
    private bool allParts;
    //private bool fixedShip;
    private bool didWin;
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
            partsText.text = "Parts: " + value;
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
            crewText.text = "Crew: " + value;
            crew = value;
        }
    }

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

    void Awake()
    {
        allCrew = false;
        allParts = false;
        //fixedShip = false;
        didWin = false;
        //anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        parts = 0;
        crew = 0;
        //shipText.text = "Status: Broken";
        partsText.text = "Parts: " + parts;
        crewText.text = "Crew: " + crew;
        currentFuel = maxFuel;
    }

    #region Functions

    public void CheckWin()
    {
        if(parts == 5 && crew == 5)
        {
            convoText.text = "You've collected all the ship parts and rescued all of your crew!";
            didWin = true;
            StartCoroutine(Wait());
            SceneManager.LoadScene("GameOver");
        }
    }
    public void TakeDamage(int damage)
    {
        currentFuel -= damage;
        fuelText.text = "Fuel: " + currentFuel;
        //anim.SetTrigger("Hurt");

        if(currentFuel <= 0)
        {
            convoText.text = "You're stranded!";
            PlayerFail();
        }
    }

    void PlayerFail()
    {
        Debug.Log("Player is out of fuel!");
        StartCoroutine(Wait());
        //anim.SetBool("isStranded",true);
        SceneManager.LoadScene("GameOver");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Grass")
        {
            grassSource.mute = false;
            grassSource.volume = 0.1f;
            GrassStep();
            grassSource.loop = true;
            convoText.text = "I think I'm hidden.";
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //print("Collided with " + other.gameObject.name);
            TakeDamage(25);
            //other.gameObject.GetComponent<AudioSource>().Play();
        }

        if(other.tag == "Friend")
        {
            //print("Collided with " + other.gameObject.name);
            //other.gameObject.GetComponent<AudioSource>().Play();
            convoText.text = "It's my friend!";
        }

        if(other.tag == "Item") // If we collide with an item that we can pick up
        {
            print("Collided with " + other.gameObject.name);
            Grab();
            inventory.AddItem(other.GetComponent<Item>());
            convoText.text = "I picked up my " + other.gameObject.name;
            parts++;
            partsText.text = "Parts: " + parts;
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Grass")
        {
            grassSource.loop = false;
            grassSource.mute = true;
            convoText.text = "";
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
        yield return new WaitForSeconds(3);
    }
    #endregion

}
