using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempTopTrigger : MonoBehaviour
{
    private bool toggled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            toggled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            toggled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
