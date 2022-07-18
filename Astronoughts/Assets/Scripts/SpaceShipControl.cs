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
    public GameObject solar3;
    public GameObject solar4;
    public GameObject engine;
    public GameObject engine2;
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
    private GameObject player;
    private GameObject UIController;
    private GameObject gm;
    private Player playerScript;
    private UIControllerScript UIScript;
    private GameManager gmScript;

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
        solar3.gameObject.SetActive(false);
        solar4.gameObject.SetActive(false);
        engine.gameObject.SetActive(false);
        engine2.gameObject.SetActive(false);
        cockpit.gameObject.SetActive(true);
        spaceshipWhole.gameObject.SetActive(false);
    }

    public void CheckingShip()
    {
        //First stage
        if(gmScript.spaceshipBrokenDownEnabled == true)
        {
            spaceshipBrokenDown.gameObject.SetActive(true);
        }

        //Cockpit is generally picked up first, but whatever is first collected disables the down body
        //cockpit enabled
        if(gmScript.shipcockpitEnabled == true)
        {
            gmScript.spaceshipBrokenDownEnabled = false;
            spaceshipBrokenDown.gameObject.SetActive(false);
            gmScript.spaceshipBrokenUpEnabled = true;
            spaceshipBrokenUp.gameObject.SetActive(true);
            shipCockpit.gameObject.SetActive(true);

            if(gmScript.shipPanelEnabled == true)
            {
                escapePod1.gameObject.SetActive(false);
                escapePod2.gameObject.SetActive(false);
                escapePod3.gameObject.SetActive(false);
                escapePod4.gameObject.SetActive(false);
                gmScript.spaceshipBrokenDownEnabled = false;
                spaceshipBrokenDown.gameObject.SetActive(false);
                gmScript.spaceshipBrokenUpEnabled = false;
                spaceshipBrokenUp.gameObject.SetActive(false);
                shipCockpit.gameObject.SetActive(false);
                gmScript.spaceshipWholeEnabled = true;
                spaceshipWhole.gameObject.SetActive(true);

                if(gmScript.shipengineEnabled == true)
                {
                    if(gmScript.solar1Enabled == true && gmScript.solar2Enabled == true)
                    {
                        // player has all the pieces, so the ship needs tp have the correct components
                        gmScript.spaceshipBrokenDownEnabled = false;
                        spaceshipBrokenDown.gameObject.SetActive(false);
                        gmScript.spaceshipBrokenUpEnabled = false;
                        spaceshipBrokenUp.gameObject.SetActive(false);
                        shipCockpit.gameObject.SetActive(false);
                        gmScript.spaceshipWholeEnabled = true;
                        spaceshipWhole.gameObject.SetActive(true);
                        solar3.gameObject.SetActive(true); // solar3 on the whole one
                        solar4.gameObject.SetActive(true); // solar4 on the whole one
                        engine2.gameObject.SetActive(true); // engine on the whole one
                        return;
                    }
                    else if(gmScript.solar1Enabled == true && gmScript.solar2Enabled == false)
                    {
                        //convoText.text = "I need to find the other solar panel.";
                        return;
                    }
                    else if(gmScript.solar1Enabled == false && gmScript.solar2Enabled == true)
                    {
                        //convoText.text = "I need to find the other solar panel.";
                        return;
                    }
                    else
                    {
                        engine2.gameObject.SetActive(true); // engine on the whole one
                        //convoText.text = "I'd better go find the solar panels.";
                    }
                    return;
                }

                if(gmScript.shipengineEnabled == false)
                {
                    if(gmScript.solar1Enabled == true && gmScript.solar2Enabled == true)
                    {
                        // player is only missing the engine
                        gmScript.spaceshipBrokenDownEnabled = false;
                        spaceshipBrokenDown.gameObject.SetActive(false);
                        gmScript.spaceshipBrokenUpEnabled = false;
                        spaceshipBrokenUp.gameObject.SetActive(false);
                        shipCockpit.gameObject.SetActive(false);
                        gmScript.spaceshipWholeEnabled = true;
                        spaceshipWhole.gameObject.SetActive(true);
                        solar3.gameObject.SetActive(true); // solar3 on the whole one
                        solar4.gameObject.SetActive(true); // solar4 on the whole one
                        //convoText.text = "I need to find the engine.";
                    }
                    else if(gmScript.solar1Enabled == true && gmScript.solar2Enabled == false)
                    {
                        //convoText.text = "I need to find the other solar panel and the engine.";
                        return;
                    }
                    else if(gmScript.solar1Enabled == false && gmScript.solar2Enabled == true)
                    {
                        //convoText.text = "I need to find the other solar panel and the engine.";
                        return;
                    }
                    else
                    {
                        //convoText.text = "I'm missing both solar panels and the engine.";
                    }
                    return;
                }
            }

            else if(gmScript.shipPanelEnabled == false)
            {
                gmScript.spaceshipBrokenDownEnabled = false;
                spaceshipBrokenDown.gameObject.SetActive(false);
                gmScript.spaceshipBrokenUpEnabled = true;
                spaceshipBrokenUp.gameObject.SetActive(true);
                shipCockpit.gameObject.SetActive(true);
                //convoText.text = "I've installed the cockpit, but I need the square, gold heat panels next.";
                return;
            }
        }
        else if(gmScript.shipcockpitEnabled == false)
        {
            //convoText.text = "I need to get the cockpit.";
            return;
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
