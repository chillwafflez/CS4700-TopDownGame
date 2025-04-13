using UnityEngine;

public class Weapon : Collidable
{
    // damage structure | bottom values are used to transfer info to something the weapon collides with
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 }; 
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 2.0f, 3.6f, 4f };

    // upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    // swing
    private Animator animator;
    private float coolDown = 0.5f;  // swing cooldown
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        //spriteRenderer = GetComponent<SpriteRenderer>();    // we're getting the component called SpriteRenderer to update the weapon's sprite
        //                                                    // so once we upgrade it we can update the weapon sprite
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
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            other.SendMessage("ReceiveDamage", dmg);

            Debug.Log("Hit " + other.name);
        }
    }

    private void Swing()
    {
        animator.SetTrigger("Swing");

    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // change weapon stats
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
