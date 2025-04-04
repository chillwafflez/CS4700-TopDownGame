using UnityEngine;

public class Player : Mover // Player <- Mover <- Fighter -< MonoBehavior
{
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");   // returns -1 if holding a, 0 if nothing, 1 if holding d
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }
}
