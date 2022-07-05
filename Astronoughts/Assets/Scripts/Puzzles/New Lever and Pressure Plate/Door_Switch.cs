using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Switch : MonoBehaviour
{
    [SerializeField] List<Door_Door> targetDoors = new List<Door_Door>();
    [SerializeField] List<Material> materials = new List<Material>();
    [SerializeField] bool isLever;

    private bool toggled = false;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMaterial(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !isLever)
        {
            ToggleDoors();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && !isLever)
        {
            ToggleDoors();
        }
    }

    public void ToggleDoors()
    {
        for (int i = 0; i < targetDoors.Count; i++)
        {
            Debug.Log("Door " + i + " is toggled");
            targetDoors[i].ToggleDoor();
        }

        if (toggled)
        {
            ChangeMaterial(0);
            toggled = false;
        }
        else
        {
            ChangeMaterial(1);
            toggled = true;
        }
    }

    private void ChangeMaterial(int index)
    {
        if (materials.Count > 1)
        {
            GetComponent<MeshRenderer>().material = materials[index];
        }
    }
}
