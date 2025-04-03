using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest; // after collected, update the chest's sprite
            Debug.Log("Grant " + pesosAmount + " pesos!");
        }

        //base.OnCollect();
        //Debug.Log("Grant pesos");
    }
}
