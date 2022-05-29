using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence_Totem : MonoBehaviour
{
    [SerializeField] Color totemColor;
    [SerializeField] private bool toggled = false;

    public void ToggleTotem()
    {
        if (!toggled)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = totemColor;
            GameObject parent = transform.parent.gameObject;
            parent.GetComponent<Sequence_Door>().addToSequence(this.name);
        }

        toggled = true;
    }

    public void ResetTotem()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        toggled = false;
    }
}
