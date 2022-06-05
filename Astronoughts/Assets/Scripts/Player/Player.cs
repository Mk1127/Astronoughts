
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    #region Variables
    public static Player instance;

    public static Player Ps;
    private Animator anim;

    private int currentHealth;
    public int maxHealth = 100;

    public GameObject endScreen;
    public GameObject player;

    // Shows the player what he needs to do
    [SerializeField] private Text coinText;
    [SerializeField] private Text convoText;

    [SerializeField] private Text INTText;
    [SerializeField] private Text STRText;
    [SerializeField] private Text STAText;
    [SerializeField] private Text AGIText;
    [SerializeField] private Text healthValueText;
    [SerializeField] private Text healthText;

    [SerializeField] private Text[] statsText;

    public int baseIntellect;
    public int baseAgility;
    public int baseStrength;
    public int baseStamina;

    private int intellect;
    private int agility;
    private int strength;
    private int stamina;
    private int coins;

    public Button inventoryButton;
    public Inventory inventory;
    //public Inventory charPanel;

    // A reference to the chest (to be added)
    // private Inventory chest;

    public Item[] items = new Item[6];
 
    public Rigidbody rb;
    #endregion

    #region Properties
    public int Coins
    {
        get
        {
            return coins;
        }
        set
        {
            coinText.text = "Coins: " + value;
            coins = value;
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
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Coins = 0;
        //SetStats(0,0,0,0);
        //endScreen.gameObject.SetActive(false);
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.E))
        //{
        //    if(chest != null)
        //    {
        //        if(chest.canvasGroup.alpha == 0 || chest.canvasGroup.alpha == 1)
        //        {
        //            chest.Open();
        //       }
        //    }
        // }
        // if(Input.GetKeyDown(KeyCode.C))
        // {
        //     if(charPanel != null)
        //     {
        //         charPanel.Open();
        //     }
        // }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.text = "Health: " + currentHealth;
        anim.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            convoText.text = "You Died!";
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player died!");

        anim.SetBool("isDead",true);
        endScreen.gameObject.SetActive(true);
    }

    // Handles the player's collision with inventory items
    // <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            print("Collided with " + other.gameObject.name);
            other.gameObject.GetComponent<AudioSource>().Play();
        }

        if(other.gameObject.tag == "Friend")
        {
            print("Collided with " + other.gameObject.name);
            other.gameObject.GetComponent<AudioSource>().Play();
            convoText.text = "It's my friend!";
        }
        if(other.tag == "Item") //If we collide with an item that we can pick up
        {
            print("Collided with " + other.gameObject.name);
            //other.gameObject.GetComponent<AudioSource>().Play();
            convoText.text = "I picked up my " + other.gameObject.name;
            other.gameObject.SetActive(false);
        }
    }

}
