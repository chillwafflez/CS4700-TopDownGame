using UnityEngine;

public class Weapon : Collidable
{
    // damage structure | bottom values are used to transfer info to something the weapon collides with
    public int damagePoint = 1; 
    public float pushForce = 2.0f;

    // upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // swing
    private Animator animator;
    private float coolDown = 0.5f;  // swing cooldown
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();    // we're getting the component called SpriteRenderer to update the weapon's sprite
                                                            // so once we upgrade it we can update the weapon sprite
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > coolDown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D other)
    {
        if (other.tag == "Fighter")
        {
            if (other.name == "Player")
                return;

            // create a new Damage object to send to the 'fighter' we hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            other.SendMessage("ReceiveDamage", dmg);

            Debug.Log("Hit " + other.name);
        }
    }

    private void Swing()
    {
        animator.SetTrigger("Swing");

    }



}
