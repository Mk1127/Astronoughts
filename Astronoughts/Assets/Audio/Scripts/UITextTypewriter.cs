//////////////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Julian Davis
//Section: (2022SU.SGD.289)
//Instructor: Amber Johnson
//////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UITextTypewriter : MonoBehaviour
{
    public string text;
    private string tempText = "";
    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TypeWriter");
    }


    IEnumerator TypeWriter()
    {
        for(int i = 0;i <= text.Length;i++)
        {
            tempText = text.Substring(0,i);
            this.GetComponent<Text>().text = tempText;
            yield return new WaitForSeconds(speed);
        }
        StartCoroutine("ResetText");
    }

    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(2);
        this.GetComponent<Text>().text = "";
        Debug.Log(tempText);
    }

}
