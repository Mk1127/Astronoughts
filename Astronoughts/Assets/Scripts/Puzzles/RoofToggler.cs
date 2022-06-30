using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofToggler : MonoBehaviour
{
    [SerializeField] GameObject roof;
    [SerializeField] GameObject astronought;
    [SerializeField] bool exit;

    bool roofToggled = false;
    //private List<GameObject> listOfChildren;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(exit)
            {
                toggleAllChildren(roof);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!exit)
            {
                toggleAllChildren(roof);
            }
        }
    }

    private void toggleAllChildren(GameObject roof){
    
    foreach (Transform child in roof.transform){
        if (null == child)
            continue;
        if (child.gameObject.GetComponent<Renderer>()==null)
            continue;
        //child.gameobject contains the current child
        //listOfChildren.Add(child.gameObject);
        //component m_Renderer = child.gameObject.GetComponent<Renderer>();
        //turns renderer off on entry
            if (child.gameObject.GetComponent<Renderer>().enabled == true)
            {
                Debug.Log("Roof turns off");
                child.gameObject.GetComponent<Renderer>().enabled = false;
                astronought.SetActive(false);
            }
        //turns renderer back off on exit
            else 
            {
                Debug.Log("Roof turns on");
                child.gameObject.GetComponent<Renderer>().enabled = true;
                astronought.SetActive(true);
            }
        //toggleAllChildren(child.gameObject);
    }
}
}
