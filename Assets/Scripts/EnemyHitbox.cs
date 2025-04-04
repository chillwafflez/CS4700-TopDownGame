using UnityEngine;

public class EnemyHitbox : Collidable
{
    // damage
    public int damage = 1;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D other)
    {
        if (other.tag == "Fighter" && other.name == "Player")
        {
            // create a new damage object before sending it to the fighter we've hit (the Player)
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            other.SendMessage("ReceiveDamage", dmg);
        }
    }
}
