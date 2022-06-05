using UnityEngine;
using System.Collections;
using System.Diagnostics;
public enum ItemType
{
    JETPACK, ENGINE, SOLAR, POTION, WEAPON
};

public class Item : MonoBehaviour
{
    // Type of item
    public ItemType type;

    // The unclicked sprite
    public Sprite spriteNeutral;

    // The clicked sprite
    public Sprite spriteHighlighted;

    // The maximum amount of stacks
    public int maxSize;

    // This function uses the item
    public void Use()
    {
        switch(type)
        {
            case ItemType.JETPACK:
                UnityEngine.Debug.Log("I collected my jetpack.");
                break;
            case ItemType.ENGINE:
                UnityEngine.Debug.Log("I collected the engine.");
                break;
            case ItemType.SOLAR:
                UnityEngine.Debug.Log("I collected the solar panels.");
                break;
            case ItemType.POTION:
                UnityEngine.Debug.Log("I just drank a potion.");
                break;
            case ItemType.WEAPON:
                UnityEngine.Debug.Log("I collected a weapon.");
                break;
        }
    }
}
