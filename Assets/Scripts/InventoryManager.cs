using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public InventoryUI inventoryUI; // Assign in Inspector
    public GameObject[] items;            // The actual item GameObjects (e.g., sprites under player)
    private bool[] itemUnlocked;          // Which items are unlocked
    private int currentItemIndex = 0;

    public int GetCurrentItemIndex()
    {
        return currentItemIndex;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        itemUnlocked = new bool[items.Length];
        itemUnlocked[0] = true; // Only item 1 is unlocked at the start
        EquipItem(currentItemIndex);

        if (inventoryUI != null)
        {
            inventoryUI.SetSlotSelected(0);
            inventoryUI.UnlockItem(0); // Unlock item 1 from the start
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) TrySelectItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) TrySelectItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) TrySelectItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) TrySelectItem(3);
    }

    void TrySelectItem(int index)
    {
        if (index < items.Length && itemUnlocked[index] && index != currentItemIndex)
        {
            UnequipItem(currentItemIndex);
            currentItemIndex = index;
            EquipItem(currentItemIndex);
            if (inventoryUI != null)
            {
                inventoryUI.SetSlotSelected(index);
            }
        }

        
    }

    void EquipItem(int index)
    {
        if (items[index] != null)
            items[index].SetActive(true);
    }

    void UnequipItem(int index)
    {
        if (items[index] != null)
            items[index].SetActive(false);
    }

    public void UnlockItem(int index)
    {
        if (index >= 0 && index < itemUnlocked.Length)
        {
            itemUnlocked[index] = true;
            if (inventoryUI != null)
            {
                inventoryUI.UnlockItem(index);
            }
            Debug.Log("Unlocked item " + (index + 1));
        }

        
    }
}