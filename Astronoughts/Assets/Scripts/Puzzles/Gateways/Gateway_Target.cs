using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gateway_Target : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] List<Gateway_Pillars> gateWays = new List<Gateway_Pillars>();
    [SerializeField] GameObject target;

    private int activeGates = 0;
    // Start is called before the first frame update
    void Start()
    {
        target.SetActive(false);
        FillGateways();
    }

    // Update is called once per frame
    public void UpdateGateWayCount()
    {
        activeGates = 0;
        for (int i = 0; i < gateWays.Count; i++)
        {
            if (gateWays[i].triggered)
            {
                activeGates++;
            }
        }

        ToggleTarget();
    }

    private void ToggleTarget()
    {
        if (activeGates == gateWays.Count)
        {
            target.SetActive(true);
        }
        else
        {
            target.SetActive(false);
        }
    }

    private void FillGateways()
    {
        if(transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                gateWays.Add(transform.GetChild(i).GetComponent<Gateway_Pillars>());
            }
        }

    }
}
