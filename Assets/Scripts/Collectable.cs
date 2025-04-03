using UnityEngine;

public class Collectable : Collidable
{
    // logic
    protected bool collected;   // protected just means its private to everyone but your children

    protected override void OnCollide(Collider2D other)
    {
        //base.OnCollide(other); // this calls the OnCollide method from parent (Collidable)

        if (other.name == "Player")
            OnCollect();
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
