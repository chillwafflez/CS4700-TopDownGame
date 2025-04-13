using UnityEngine;

public class HealingFountain : Collidable
{
    public int healingAmount = 1;   // amount to heal
    private float healCooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D other)
    {
        if (other.name != "Player")
            return;

        if (Time.time - lastHeal > healCooldown)
        {
            lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }

}
