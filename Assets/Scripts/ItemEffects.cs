using UnityEngine;

public class ItemEffects : MonoBehaviour
{
    private InventoryManager inventory;
    private Player player;

    void Start()
    {
        inventory = GetComponent<InventoryManager>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inventory != null && player != null)
            {
                int currentItem = inventory.GetCurrentItemIndex();

                // Assuming item 2 (index 1) is the healing item
                if (currentItem == 1)
                {
                    bool healed = player.TryHeal();
                    if (healed)
                        Debug.Log("Healed to full HP!");
                    else
                        Debug.Log("Heal is on cooldown.");
                }
                else if (currentItem == 2)
                {
                    bool boosted = player.TrySpeedBoost();
                    if (boosted)
                        Debug.Log("Speed boost activated!");
                    else
                        Debug.Log("Speed boost is on cooldown.");
                }
                else if (currentItem == 3)
                {
                    bool boosted = player.TryDamageBoost();
                    if (boosted)
                        Debug.Log("Damage boost activated!");
                    else
                        Debug.Log("Damage boost is on cooldown.");
                }

            }
        }
    }
}
