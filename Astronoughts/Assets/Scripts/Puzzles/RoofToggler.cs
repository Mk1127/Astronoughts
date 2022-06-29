using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofToggler : MonoBehaviour
{
    [SerializeField] GameObject roof;
    [SerializeField] bool exit;

    bool roofToggled = false;
    //private List<GameObject> listOfChildren;

    private void OnTriggerStay(Collider other)
    {
        //caveRoof.SetActive(false);
        if(other.tag == "Player")
        {
            if(!exit)
            {
                if (!roofToggled)
                {
                    toggleAllChildren(roof, true);
                    roofToggled = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(exit)
        {
            roofToggled = false;
            toggleAllChildren(roof, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!exit)
            {
                roofToggled = false;
                toggleAllChildren(roof, false);
            }
        }
    }
    
private void toggleAllChildren(GameObject roof, bool toggle){
    
    foreach (Transform child in roof.transform){
        if (null == child)
            continue;
        if (child.gameObject.GetComponent<Renderer>()==null)
            continue;
        //child.gameobject contains the current child
        //listOfChildren.Add(child.gameObject);
        //component m_Renderer = child.gameObject.GetComponent<Renderer>();
        //turns renderer off on entry
        if (child.gameObject.GetComponent<Renderer>().enabled == toggle)
        {
            Debug.Log("Roof turns off");
            child.gameObject.GetComponent<Renderer>().enabled = !toggle;
        }
        //turns renderer back off on exit
        else 
        {
            Debug.Log("Roof turns on");
            child.gameObject.GetComponent<Renderer>().enabled = toggle;
        }
        //toggleAllChildren(child.gameObject);
    }
}
}
