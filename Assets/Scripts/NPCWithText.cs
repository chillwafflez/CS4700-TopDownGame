using System;
using UnityEngine;

public class NewMonoBehaviourScript : Collidable
{
    public string message;
    public string endMessage;
    private float cooldown = 4.0f;
    private float lastShout;

    protected override void Start()
    {
        base.Start();
        lastShout = -cooldown;
    }
    protected override void OnCollide(Collider2D other)
    {
        if (InventoryManager.instance.HasIngredients())
        {
            DisplayMessage();
        }
        else
        {
            DisplayMessage();
        } 
    }

    public void DisplayMessage()
    {
        if (Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
        }
    }
}
