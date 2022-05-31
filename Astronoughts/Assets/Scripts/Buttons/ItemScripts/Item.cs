using UnityEngine;
using System.Collections;
using System.Diagnostics;
public enum ItemType
{
    ARMOR, CLOTHING, CONSUMEABLE, POTION, WEAPON
};

public class Item : MonoBehaviour
{
    // The item's type
    public ItemType type;

    // The path to the neutral sprite
    public Sprite spriteNeutral;

    // The path to the highlighted sprite
    public Sprite spriteHighlighted;

    // The maximum amount of stacks
    public int maxSize;

    // This function uses the item
    public void Use()
    {
        switch(type)
        {
            case ItemType.ARMOR:
                UnityEngine.Debug.Log("I destroyed some armor.");
                break;
            case ItemType.CLOTHING:
                UnityEngine.Debug.Log("I destroyed some clothing.");
                break;
            case ItemType.CONSUMEABLE:
                UnityEngine.Debug.Log("I just consumed some food.");
                break;
            case ItemType.POTION:
                UnityEngine.Debug.Log("I just drank a potion.");
                break;
            case ItemType.WEAPON:
                UnityEngine.Debug.Log("I destroyed a weapon.");
                break;
        }
    }
}
