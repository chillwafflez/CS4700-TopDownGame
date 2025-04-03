using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter; // filter to know what exactly you should collide with
    private BoxCollider2D boxCollider;  // private because its on the object
    private Collider2D[] hits = new Collider2D[10]; // array that contains data on what exactly did u hit during a frame

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); // whatever object the Collidable script is on, it requires a BoxCollider2D
    }

    protected virtual void Update()
    {
        // For all the collision work
        boxCollider.Overlap(filter, hits); // takes in our contact filter and an area of results for where to put it
        // basically it takes your box collider and looks for other colliders above/beneath it (so something in collision with
        // your box collider right now) and its gonna put it into the 'hits' array
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            // the array isnt cleaned up, so we it ourselves
            hits[i] = null; 
        }
    }

    protected virtual void OnCollide(Collider2D other)
    {
        Debug.Log(other.name);
    }
}
