using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITextTypewriter : MonoBehaviour
{
    Text txt;
    TMP_Text textMeshTxt;
    string writtenTxt;
    public string[] writtenTxts;
    public Button instructions;

    //Serialize the settings
    //Imitate a screen cursor
    [SerializeField] string cursor = "";
    [SerializeField] float typingSpeed = 0.1f;
    [SerializeField] float delay = 0f;
    [SerializeField] bool delayCursor = false;
    [SerializeField] private string splashScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForEnd());
        //Get the text component of text field
        txt = GetComponent<Text>()!;
        textMeshTxt = GetComponent<TMP_Text>()!;

        //Check to make sure there a value
        if(txt != null)
        {
            //Fill variable
            writtenTxt = txt.text;
            txt.text = "";

            StartCoroutine("TextTypewriter");
        }
        if(textMeshTxt != null)
        {
            //Fill variable
            writtenTxt = textMeshTxt.text;
            textMeshTxt.text = "";

            StartCoroutine("TextMeshTypewriter");
        }
    }

    IEnumerator TextTypewriter()
    {
        //The text field is a Text -- Check for pause before starting
        Debug.Log(writtenTxt);
        txt.text = delayCursor ? cursor : "";
        yield return new WaitForSeconds(delay);

        foreach(char c in writtenTxt)
        {
            if(txt.text.Length > 0)
            {
                txt.text = txt.text.Substring(0,txt.text.Length - cursor.Length);
            }
            txt.text += c;
            txt.text += cursor;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator TextMeshTypewriter()
    {
        //The text field is a Text Mesh -- Check for pause before starting

        textMeshTxt.text = delayCursor ? cursor : "";
        yield return new WaitForSeconds(delay);

        foreach(char c in writtenTxt)
        {
            if(textMeshTxt.text.Length > 0)
            {
                textMeshTxt.text = textMeshTxt.text.Substring(0,textMeshTxt.text.Length - cursor.Length);
            }
            textMeshTxt.text += c;
            textMeshTxt.text += cursor;
            yield return new WaitForSeconds(typingSpeed);
        }
        //Check if a cursor is needed
        if(cursor != "")
        {
            textMeshTxt.text = textMeshTxt.text.Substring(0,textMeshTxt.text.Length - cursor.Length);
        }
    }
    IEnumerator WaitForEnd()
    {
        //yield on a new YieldInstruction that waits for 60 seconds.
        yield return new WaitForSeconds(60f);

        //After we have waited 60 seconds print the time again.
        SceneManager.LoadScene("Menu");
    }
}
