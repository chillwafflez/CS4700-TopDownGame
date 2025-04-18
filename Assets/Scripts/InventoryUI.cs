using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image[] slotBackgrounds;    // Slot1, Slot2, etc.
    public Image[] itemIcons;          // The actual icons inside slots
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public Sprite[] itemSprites;       // The icons for Item1ÅE

    private int currentIndex = 0;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetSlotSelected(int index)
    {
        currentIndex = index;
        for (int i = 0; i < slotBackgrounds.Length; i++)
        {
            slotBackgrounds[i].sprite = (i == index) ? selectedSprite : notSelectedSprite;
        }
    }

    public void UnlockItem(int index)
    {
        if (index >= 0 && index < itemIcons.Length)
        {
            itemIcons[index].enabled = true;
            itemIcons[index].sprite = itemSprites[index];
        }
    }
}
