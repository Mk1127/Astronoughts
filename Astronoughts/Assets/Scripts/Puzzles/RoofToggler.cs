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
            if(exit == true)
            {
                toggleAllChildren(roof, true);
                other.GetComponent<Player_Movement>().canHover = true;
            }
            else
            {
                toggleAllChildren(roof, false);
                other.GetComponent<Player_Movement>().canHover = false;
            }
        }
    }

    private void toggleAllChildren(GameObject roof, bool toggleRoof)
    {
    
        foreach (Transform child in roof.transform)
        {
            if (null == child)
                continue;
            if (child.gameObject.GetComponent<Renderer>()==null)
                continue;

               Debug.Log("Roof turns off");
                child.gameObject.GetComponent<Renderer>().enabled = toggleRoof;

            if(child.transform.childCount > 0)
            {
                foreach(Transform grandChild in child.transform)
                {
                    grandChild.GetComponent<Renderer>().enabled = toggleRoof;
                }
            }

                if(astronought != null)
                {
                    astronought.SetActive(toggleRoof);
                }
                
        }
    }
}
