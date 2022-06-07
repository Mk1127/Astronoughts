using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAve : MonoBehaviour
{
    [SerializeField] GameObject caveRoof;
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
        caveRoof.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        caveRoof.SetActive(true);
    }
}
