using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject instructionsMenu;

    void Start()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Hub");
    }

    public void OnBackButtonClick()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
    }

    public void OnControlsButtonClick()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
        instructionsMenu.SetActive(false);
    }

    public void OnInstructionsButtonClick() 
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
