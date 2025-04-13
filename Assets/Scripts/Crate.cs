using UnityEngine;

public class Crate : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
