using UnityEngine;
using static UnityEditor.Progress;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fighter"))
        {
            InventoryManager.instance.AddItem(item);
            Destroy(gameObject); // Remove pickup from the scene
            
        }
    }
}
