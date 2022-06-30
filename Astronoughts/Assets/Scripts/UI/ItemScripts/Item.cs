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

    private void Start()
    {
        GameManager gm = GameManager.gameManager;

        switch(type)
        {
            case ItemType.PART1:
                if(gm.panelEnabled)
                {
                    gameObject.SetActive(false);
                }
                break;
            case ItemType.PART2:
                if (gm.solar1Enabled)
                {
                    gameObject.SetActive(false);
                }
                break;
            case ItemType.PART3:
                if (gm.solar2Enabled)
                {
                    gameObject.SetActive(false);
                }
                break;
            case ItemType.PART4:
                if (gm.engineEnabled)
                {
                    gameObject.SetActive(false);
                }
                break;
            case ItemType.PART5:
                if (gm.cockpitEnabled)
                {
                    gameObject.SetActive(false);
                }
                break;
        }
    }

     public void Use()
     {
        switch(type)
        {
            case ItemType.PART1:
                UnityEngine.Debug.Log("I implemented the 1st Part.");
                break;
            case ItemType.PART2:
                UnityEngine.Debug.Log("I implemented the 2nd Part.");
                break;
            case ItemType.PART3:
                UnityEngine.Debug.Log("I implemented the 3rd Part.");
                break;
            case ItemType.PART4:
                UnityEngine.Debug.Log("I implemented the 4th Part.");
                break;
            case ItemType.PART5:
                UnityEngine.Debug.Log("I implemented the 5th Part.");
                break;
        }
     }
}
