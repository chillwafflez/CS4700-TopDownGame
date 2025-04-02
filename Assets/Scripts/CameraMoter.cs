using UnityEngine;

public class CameraMoter : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    // we use LateUpdate bcuz its called after Update and FixedUpdate. since we are moving our player in FixedUpdate
    // we have to make sure we move the camera AFTER the player moves bcuz if we dont there will be a slight desync
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // THIS IS TO CHECK IF WE'RE INSIDE THE BOUNDS ON THE X-AXIS
        // transform.position.x is the center of the camera / the current camera focus
        // we we're doing is we're getting the distance between that center and the player,
        // and if deltaX is bigger than boundX, that means we're outside of the bounds
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            // check if player is out of bounds on left or right (after if)
            if (transform.position.x < lookAt.position.x)   // is the center/camera focus smaller than the player's position
            {
                delta.x = deltaX - boundX;
            } else
            {
                delta.x = deltaX + boundX;
            }
        }

        // THIS IS TO CHECK IF WE'RE INSIDE THE BOUNDS ON THE Y-AXIS
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            // check if player is out of bounds on top or bottom (after if)
            if (transform.position.y < lookAt.position.y)   // is the center/camera focus smaller than the player's position
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
