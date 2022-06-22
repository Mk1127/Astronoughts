
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

public class Player:MonoBehaviour
{
    #region Variables
    public static Player instance;

    public static Player Ps;
    private Animator anim;

    private int currentFuel;
    public int maxFuel = 100;

    //public GameObject endScreen;
    public GameObject player;

    // Shows the player what he needs to do
    public Text convoText;
    public Text[] allText;
    private int components;
    private int crew;
    //private int shipState;

    [SerializeField]
    private Text componentsText;
    [SerializeField]
    private Text crewText;
    [SerializeField]
    private Text fuelText;
    //[SerializeField]
    //private Text shipText;

    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private AudioSource prizeSource;

    public Button inventoryButton;
    public Inventory inventory;

    public Item[] items = new Item[3];

    private bool allCrew;
    private bool allParts;
    //private bool fixedShip;
    private bool didWin;
    public Rigidbody rb;
    #endregion

    #region Properties
    public int Components
    {
        get
        {
            return components;
        }
        set
        {
            componentsText.text = "Components: " + value;
            components = value;
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
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        components = 0;
        crew = 0;
        //shipText.text = "Status: Broken";
        componentsText.text = "Components: " + components;
        crewText.text = "Crew: " + crew;
        currentFuel = maxFuel;

        rb = GetComponent<Rigidbody>();
    }

    #region Functions

    public void CheckWin()
    {
        if(components == 5 && crew == 5)
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            print("Collided with " + other.gameObject.name);
            TakeDamage(25);
            //other.gameObject.GetComponent<AudioSource>().Play();
        }

        if(other.CompareTag("Friend"))
        {
            print("Collided with " + other.gameObject.name);
            //other.gameObject.GetComponent<AudioSource>().Play();
            convoText.text = "It's my friend!";
        }
        if(other.CompareTag("Item")) // If we collide with an item that we can pick up
        {
            print("Collided with " + other.gameObject.name);
            Grab();
            inventory.AddItem(other.GetComponent<Item>());
            convoText.text = "I picked up my " + other.gameObject.name;
            components++;
            componentsText.text = "Parts: " + components;
            other.gameObject.SetActive(false);
        }
    }

    public void Grab()
    {
        AudioClip clip = GetRandomClip();
        prizeSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0,clips.Length)];
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }
    #endregion

}
