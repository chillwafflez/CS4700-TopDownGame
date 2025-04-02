using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {

        float x = Input.GetAxisRaw("Horizontal");   // returns -1 if holding a, 0 if nothing, 1 if holding d
        float y = Input.GetAxisRaw("Vertical");

        // Reset moveDelta 
        moveDelta = new Vector3(x, y, 0);

        // Swap sprite direction, whether ur going right or left
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one; // already declared sprite's direction
        } else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } // we did else if bcuz if we are at moveDelta.x == 0, then we are still and dont want to flip to a default direction automatically 

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

        //// Sprite Movement
        //transform.Translate(moveDelta * Time.deltaTime);

    }

}
