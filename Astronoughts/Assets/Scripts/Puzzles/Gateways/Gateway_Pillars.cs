using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gateway_Pillars : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] List<GameObject> light = new List<GameObject>();
    [SerializeField] GameObject parent;

    [HideInInspector] public bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        SetParent();
        Toggle(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            triggered = true;
            Toggle(true);
        }
    }

    private void Toggle(bool toggle)
    {
        for (int i = 0; i < light.Count; i++)
        {
            light[i].SetActive(toggle);
        }

        UpdateGatewayCount();
    }

    private void UpdateGatewayCount()
    {
        if(parent != null)
        {
            parent.GetComponent<Gateway_Target>().UpdateGateWayCount();
        }
        else
        {
            Debug.LogError("Player Is Null!");
        }
    }

    private void SetParent()
    {
        if(parent == null)
        {
            if(transform.parent != null)
            {
                parent = transform.parent.gameObject;
            }
        }
    }
}
