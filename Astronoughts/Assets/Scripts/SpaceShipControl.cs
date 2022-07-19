using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipControl : MonoBehaviour
{
    //spaceship model
    public GameObject spaceshipBrokenDown;
    public GameObject spaceshipBrokenUp;
    public GameObject spaceshipWholeUp;
    public GameObject solar1;
    public GameObject solar2;
    public GameObject engine;
    public GameObject cockpit; // part on ground
    public GameObject shipCockpit; // model on broken up ship
    public GameObject spaceshipWhole;

    //other Hub things
    public GameObject escapePod1;
    public GameObject escapePod2;
    public GameObject escapePod3;
    public GameObject escapePod4;

    public GameObject panelParts;
    public GameObject panelGrass;
    public GameObject panelView;
    public Text convoText;

    //sources
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject UIController;
    [SerializeField] private GameObject gm;
    [SerializeField] private Player playerScript;
    [SerializeField] private UIControllerScript UIScript;
    [SerializeField] private GameManager gmScript;

    // Start is called before the first frame update
    void Start()
    {
        Ready();
    }

    void Update()
    {
        CheckingShip();
        CheckingPanels();
    }

    private void Ready()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UIController = GameObject.FindGameObjectWithTag("UIController");
        gm = GameObject.FindGameObjectWithTag("GameController");
        playerScript = player.GetComponent<Player>();
        UIScript = UIController.GetComponent<UIControllerScript>();
        gmScript = gm.GetComponent<GameManager>();
        spaceshipBrokenDown.gameObject.SetActive(true);
        spaceshipBrokenUp.gameObject.SetActive(false);
        shipCockpit.gameObject.SetActive(false);
        solar1.gameObject.SetActive(false);
        solar2.gameObject.SetActive(false);
        engine.gameObject.SetActive(false);
        cockpit.gameObject.SetActive(true);
        spaceshipWhole.gameObject.SetActive(false);
    }

    public void CheckingShip()
    {
        //First stage
        if(gmScript.spaceshipBrokenDownEnabled == true)
        {
            spaceshipBrokenDown.gameObject.SetActive(true);
            spaceshipWhole.gameObject.SetActive(false);
        }

        //Cockpit is generally picked up first, but whatever is first collected disables the down body
        //cockpit enabled
        if(gmScript.shipcockpitEnabled == true)
        {
            //disable down spaceship and note it's disabled in gm
            gmScript.spaceshipBrokenDownEnabled = false;
            spaceshipBrokenDown.gameObject.SetActive(false);

            //enable up spaceship and note it's enabled in gm
            gmScript.spaceshipBrokenUpEnabled = true;
            spaceshipBrokenUp.gameObject.SetActive(true);
            shipCockpit.gameObject.SetActive(true);
        }
        if(gmScript.engineEnabled == true)
        {
            //disable down spaceship and note it's disabled in gm
            gmScript.spaceshipBrokenDownEnabled = false;
            spaceshipBrokenDown.gameObject.SetActive(false);

            //enable up spaceship and note it's enabled in gm
            gmScript.spaceshipBrokenUpEnabled = true;
            spaceshipBrokenUp.gameObject.SetActive(true);
            engine.gameObject.SetActive(true);
        }
        if(gmScript.solar1Enabled == true)
        {
            //disable down spaceship and note it's disabled in gm
            gmScript.spaceshipBrokenDownEnabled = false;
            spaceshipBrokenDown.gameObject.SetActive(false);

            //enable up spaceship and note it's enabled in gm
            gmScript.spaceshipBrokenUpEnabled = true;
            spaceshipBrokenUp.gameObject.SetActive(true);
            solar1.gameObject.SetActive(true); // solar1 on the broken one
        }
        if(gmScript.solar2Enabled == true)
        {
            //disable down spaceship and note it's disabled in gm
            gmScript.spaceshipBrokenDownEnabled = false;
            spaceshipBrokenDown.gameObject.SetActive(false);

            //enable up spaceship and note it's enabled in gm
            gmScript.spaceshipBrokenUpEnabled = true;
            spaceshipBrokenUp.gameObject.SetActive(true);
            solar2.gameObject.SetActive(true); // solar2 on the broken one
        }
        if(gmScript.cockpitEnabled == true && gmScript.solar1Enabled == true && gmScript.solar2Enabled == true && gmScript.engineEnabled == true)
        {
            if(gmScript.panelEnabled == true)
            {
                escapePod1.gameObject.SetActive(false);
                escapePod2.gameObject.SetActive(false);
                escapePod3.gameObject.SetActive(false);
                escapePod4.gameObject.SetActive(false);
                shipCockpit.gameObject.SetActive(false);
                gmScript.spaceshipBrokenDownEnabled = false;
                spaceshipBrokenDown.gameObject.SetActive(false);
                gmScript.spaceshipBrokenUpEnabled = false;
                spaceshipBrokenUp.gameObject.SetActive(false);
                gmScript.spaceshipWholeEnabled = true;
                spaceshipWhole.gameObject.SetActive(true);

            }
        }

    }

    public void CheckingPanels()
    {
        if(gmScript.grassCheck == true)
        {
            panelGrass.gameObject.SetActive(false);
        }
        if(gmScript.viewCheck == true)
        {
            panelView.gameObject.SetActive(false);
        }
        if(gmScript.gotCockpit == true)
        {
            panelParts.gameObject.SetActive(false);
        }
    }
}
