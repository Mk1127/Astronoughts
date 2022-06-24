using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser_System : MonoBehaviour
{
    [SerializeField] List<Geyser> geysers = new List<Geyser>();

    [HideInInspector] public int activeGeysers;
    // Start is called before the first frame update
    void Start()
    {
        activeGeysers = geysers.Count;
        UpdateGeysersForce();
    }

    public void UpdateGeysersForce()
    {
        for (int i = 0; i < geysers.Count; i++)
        {
            geysers[i].geyserForce = geysers[i].startGeyserForce / activeGeysers;
        }
    }
}
