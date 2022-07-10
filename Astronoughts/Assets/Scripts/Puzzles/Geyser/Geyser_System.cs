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
        float addZ = 0.45f;

        for (int i = 0; i < geysers.Count; i++)
        {
            geysers[i].geyserForce = geysers[i].startGeyserForce / activeGeysers;

            if (i == 0)
            {
                for (int z = 0; z < activeGeysers; z++)
                {
                    addZ -= 0.15f;
                }
            }


            Debug.Log(addZ);

            geysers[i].geyserVFX.transform.localScale = new Vector3(0.2f + addZ, 0.2f + addZ, 0.2f + addZ);
        }
    }
}
