using UnityEngine;
using System.Collections;
public enum ItemType
{
    PART1, PART2, PART3, PART4, PART5
};

public class Item : MonoBehaviour
{
    public ItemType type;

    // Various Sprite Images to be used
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    // The maximum amount of stacks
    public int maxSize;

     public void Use()
    {
        switch(type)
        {
            case ItemType.PART1:
                UnityEngine.Debug.Log("I collected the 1st Component.");
                break;
            case ItemType.PART2:
                UnityEngine.Debug.Log("I collected the 2nd Component.");
                break;
            case ItemType.PART3:
                UnityEngine.Debug.Log("I collected 3rd Component.");
                break;
            case ItemType.PART4:
                UnityEngine.Debug.Log("I collected 4th Component.");
                break;
            case ItemType.PART5:
                UnityEngine.Debug.Log("I collected 5th Component.");
                break;
        }
    }
}
