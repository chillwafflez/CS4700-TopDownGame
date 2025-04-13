using UnityEngine;

public class Enemy : Mover
{
    // experience
    public int xpValue = 1;

    // logic
    public float triggerLength = 1; // distance to player to trigger enemy movement
    public float chaseLength = 5; // enemy will chase u for 5 meters, if u leave that area it'll go back to its OG spot
    private bool chasing; // to know whether enemy is chasing the player
    private bool collidingWithPlayer; // to know if we're currently colliding with the Player
    private Transform playerTransform;
    private Vector3 startingPosition;

    // hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform; // gets the transform component of the player
        startingPosition = transform.position;  // wherever u placed ur enemy on the map, thats finna be their starting position when u start the game
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>(); // gets the Hitbox collider, not the other collider on its head
    }

    private void FixedUpdate()
    {
        // is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength) 
        { 
            if (Vector3.Distance(playerTransform.position, startingPosition) <  triggerLength)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else // if not chasing go back to where we were
            {
                UpdateMotor(startingPosition - transform.position); 
            }
        }
        else // if player is out of range, go back to where we were
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        // check for overlaps
        collidingWithPlayer = false;
        // For all the collision work
        boxCollider.Overlap(filter, hits); // takes in our contact filter and an area of results for where to put it
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }  
      
            // the array isnt cleaned up, so we it ourselves
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        // 'kills' the enemy and removes them
        Destroy(gameObject); 

        // rewards Player for kill
        GameManager.instance.GrantXP(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}
