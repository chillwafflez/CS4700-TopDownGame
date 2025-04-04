using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset moveDelta 
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // Swap sprite direction, whether ur going right or left
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one; // already declared sprite's direction
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        // we did else if bcuz if we are at moveDelta.x == 0, then we are still and dont want to flip to a default direction automatically 

        // add push vector, if any
        moveDelta += pushDirection;

        // reduce the push force every frame, based off the recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // make sure we can move in this direction by casting a box there first | this hit only tests on the y-axis
        // if the box returns null, we are free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)   // this means we can move bcuz since we havent hit anything, collider is null, we didnt have any result on our boxcast
        {
            // Sprite Movement
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)   // this means we can move bcuz since we havent hit anything, collider is null, we didnt have any result on our boxcast
        {
            // Sprite Movement
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
