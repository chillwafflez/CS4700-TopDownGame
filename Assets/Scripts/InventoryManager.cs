using System.ComponentModel;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventorySlot[] inventorySlots;
    public GameObject invetoryItemPrefab;
    public Player player;

    // Testing if items works
    
    public Item[] startItems;
    private void Start()
    {
        foreach (var item in startItems)
        {
            AddItem(item);
        }
    }

    // Allows InventroyManager to be accessed in every script and no extra instance created
    private void Awake()
    {
        instance = this;
    }

    // Add item from game into an empty slot
    public bool AddItem(Item item)
    {
        // Find an empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            if (slot.GetComponentInChildren<InventoryItem>() == null)
            {
                Debug.Log("Adding item");
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }


    // Create item in invetory
    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(invetoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponentInChildren<InventoryItem>();
        inventoryItem.InitializeItem(item);
        Debug.Log("Item added");
        ActivateEffect(item);
    }

    // Activate effect of items
    public void ActivateEffect(Item item)
    {
        if (item.itemIndex == 0)
        {
            player.IncreaseHealth();

        }
        else if (item.itemIndex == 1)
        {
            player.IncreaseDamage();
        }
        else if (item.itemIndex == 2)
        {
            player.IncreaseSpeed();
        }
    }

    // For win condition
    // If player has all 3 items then the they win
    public bool HasIngredients()
    {
        int num = 0;
        foreach (var slot in inventorySlots)
        {
            if (slot.GetComponentInChildren<InventoryItem>() != null)
            {
                num++;
            }
        }
        if (num == 3)
        {
            return true;
        }
        return false;
    }
}