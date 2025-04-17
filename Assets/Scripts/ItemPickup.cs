using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemIndex; // Set this in Inspector: 1 for Item2, 2 for Item3, etc.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fighter"))
        {
            InventoryManager im = FindObjectOfType<InventoryManager>();
            if (im != null)
            {
                im.UnlockItem(itemIndex);
            }

            Destroy(gameObject); // Remove pickup from the scene
        }
    }
}
