using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonController : MonoBehaviour
{
    public Animator returnAnimator;
    public bool isHidden;

    public Button returnButton;


    public void OpenSettings()
    {
        returnAnimator.SetBool("isHidden", true);
    }

    public void ReturnClicked()
    {
        // This button was clicked.
        Debug.Log("You clicked the return button");
        // run the button animation
        returnAnimator.SetBool("isHidden", true);
        SceneManager.LoadScene("MainMenu");
    }


}
