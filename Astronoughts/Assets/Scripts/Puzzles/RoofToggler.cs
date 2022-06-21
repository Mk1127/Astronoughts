using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofToggler : MonoBehaviour
{
    [SerializeField] GameObject roof;
    //private List<GameObject> listOfChildren;

    private void OnTriggerEnter(Collider other)
    {
        //caveRoof.SetActive(false);
        toggleAllChildren(roof);
    }

    //private void OnTriggerExit(Collider other)
    //{
        //caveRoof.SetActive(true);
    //}
    
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
        if (child.gameObject.GetComponent<Renderer>().enabled==true)
        {
            Debug.Log("Roof turns off");
            child.gameObject.GetComponent<Renderer>().enabled=false;
        }
        //turns renderer back off on exit
        else 
        {
            Debug.Log("Roof turns on");
            child.gameObject.GetComponent<Renderer>().enabled=true;
        }
        //toggleAllChildren(child.gameObject);
    }
}
}
