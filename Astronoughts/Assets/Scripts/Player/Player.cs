
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Player:MonoBehaviour
{
    #region Variables
    public static Player instance;

    public static Player Ps;
    private Animator anim;

    private int currentHealth;
    public int maxHealth = 100;

    //public GameObject endScreen;
    public GameObject player;

    // Shows the player what he needs to do
    public Text convoText;
    public Text[] allText;
    private int components;

    [SerializeField]
    private Text componentsText;
    public Text healthText;

    public Button inventoryButton;
    public Inventory inventory;

    public Item[] items = new Item[3];

    public bool didWin;
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
        didWin = false;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        components = 0;
        componentsText.text = "Components: " + components;
        //endScreen.gameObject.SetActive(false);
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
    }

    #region Functions

    public void CheckWin()
    {
        if(components == 5)
        {
            convoText.text = "You've collected all the ship parts!";
            //didWin = true;
            //StartCoroutine(Wait());
            //SceneManager.LoadScene("GameOver");
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.text = "Health: " + currentHealth;
        //anim.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            convoText.text = "You Died!";
            //PlayerDie();
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player died!");
        //StartCoroutine(Wait());
        //anim.SetBool("isDead",true);
        //SceneManager.LoadScene("GameOver");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            print("Collided with " + other.gameObject.name);
            TakeDamage(25);
            //other.gameObject.GetComponent<AudioSource>().Play();
        }

        if(other.gameObject.tag == "Friend")
        {
            print("Collided with " + other.gameObject.name);
            //other.gameObject.GetComponent<AudioSource>().Play();
            convoText.text = "It's my friend!";
        }
        if(other.tag == "Item") // If we collide with an item that we can pick up
        {
            print("Collided with " + other.gameObject.name);
            //other.gameObject.GetComponent<AudioSource>().Play();
            inventory.AddItem(other.GetComponent<Item>());
            convoText.text = "I picked up my " + other.gameObject.name;
            components++;
            componentsText.text = "Components: " + components;
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }
    #endregion

}
