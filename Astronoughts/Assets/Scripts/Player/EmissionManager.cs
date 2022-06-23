using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionManager : MonoBehaviour
{
    public bool sparksEnabled = true;
    public float duration = 5f;
    public AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        sparksEnabled = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            source.GetComponent<AudioSource>().Play();
        }
    }

    public void ToggleSparks()
    {
        StartCoroutine(ToggleEffect());
    }

    IEnumerator ToggleEffect()
    {
        sparksEnabled = false;
        Debug.Log("Sparks are off");

        yield return new WaitForSeconds(duration);

        sparksEnabled = true;
        Debug.Log("Sparks are on");
    }
}
