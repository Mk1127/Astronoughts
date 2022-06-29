using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceShipController : MonoBehaviour
{
    //spaceship model
    public GameObject spaceshipBrokenDown;
    public GameObject spaceshipBrokenUp;
    public GameObject solar1;
    public GameObject solar2;
    public GameObject engine;
    public GameObject cockpit;
    public GameObject spaceshipWhole;

    //sources
    private GameObject player;
    private GameObject UIController;
    private GameObject GameManager;
    private Player playerScript;
    private UIControllerScript UIScript;
    private GameManager gmScript;

    // Start is called before the first frame update
    void Start()
    {
        GetReady();
        CheckShip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetReady()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UIController = GameObject.FindGameObjectWithTag("UIController");
        GameManager = GameObject.FindGameObjectWithTag("GameController");
        playerScript = player.GetComponent<Player>();
        UIScript = UIController.GetComponent<UIControllerScript>();
        gmScript = GameManager.GetComponent<GameManager>();
        spaceshipBrokenDown.gameObject.SetActive(true);
        spaceshipBrokenUp.gameObject.SetActive(false);
        solar1.gameObject.SetActive(false);
        solar2.gameObject.SetActive(false);
        engine.gameObject.SetActive(false);
        spaceshipWhole.gameObject.SetActive(false);
    }

    public void CheckShip()
{
        if(gmScript.spaceshipBrokenDownEnabled == true)
        {
            spaceshipBrokenDown.gameObject.SetActive(true);
        }
        if(gmScript.spaceshipBrokenDownEnabled == false)
        {
            spaceshipBrokenDown.gameObject.SetActive(true);
        }

        if(gmScript.spaceshipBrokenUpEnabled == true)
        {
            spaceshipBrokenUp.gameObject.SetActive(true);
        }
        if(gmScript.spaceshipBrokenUpEnabled == false)
        {
            spaceshipBrokenUp.gameObject.SetActive(true);
        }

        if(gmScript.shipSolar1Enabled == true)
        {
            solar1.gameObject.SetActive(true);
        }
        if(gmScript.solar1Enabled == false)
        {
            solar1.gameObject.SetActive(false);
        }

        if(gmScript.shipSolar2Enabled == true)
        {
            solar2.gameObject.SetActive(true);
        }
        if(gmScript.shipSolar2Enabled == false)
        {
            solar2.gameObject.SetActive(false);
        }

        if(gmScript.shipEngineEnabled == true)
        {
            engine.gameObject.SetActive(true);
        }
        if(gmScript.engineEnabled == false)
{
            engine.gameObject.SetActive(false);
        }

        if(gmScript.cockpitEnabled == true)
        {
            cockpit.gameObject.SetActive(true);
        }

        if(gmScript.cockpitEnabled == false)
        {
            cockpit.gameObject.SetActive(false);
        }
    }

}
