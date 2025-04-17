using UnityEngine;

public class Player : Mover // Player <- Mover <- Fighter -< MonoBehavior
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    private float healCooldown = 15f;
    private float lastHealTime = -Mathf.Infinity;
    private bool isSpeedBoosted = false;
    private float boostMultiplier = 1.5f;
    private float boostDuration = 5f;
    private float boostCooldown = 15f;
    private float lastBoostTime = -Mathf.Infinity;
    private float boostEndTime = 0f;
    private bool isDamageBoosted = false;
    private float damageMultiplier = 5f;
    private float damageBoostDuration = 5f;
    private float damageBoostCooldown = 15f;
    private float lastDamageBoostTime = -Mathf.Infinity;
    private float damageBoostEndTime = 0f;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //DontDestroyOnLoad(gameObject);
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitPointChange();
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Check if boost should wear off
        if (isSpeedBoosted && Time.time >= boostEndTime)
        {
            xSpeed /= boostMultiplier;
            ySpeed /= boostMultiplier;
            isSpeedBoosted = false;
            Debug.Log("Speed boost ended.");
        }

        if (isAlive)
            UpdateMotor(new Vector3(x, y, 0));

        if (isDamageBoosted && Time.time >= damageBoostEndTime)
        {
            isDamageBoosted = false;
            Debug.Log("Damage boost ended.");
        }
    }

    public void SwapSprite(int skinID)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinID];
    }

    public void OnLevelUp()
    {
        maxHitPoint++;  // increase max hp
        hitPoint = maxHitPoint; // refill hp

    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healingAmount)
    {
        if (hitPoint == maxHitPoint)
            return;


        hitPoint += healingAmount;
        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.05f);
        GameManager.instance.OnHitPointChange();

    }

    public void Respawn()
    {
        Heal(maxHitPoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

    public bool TryHeal()
    {
        if (Time.time - lastHealTime >= healCooldown)
        {
            Heal(maxHitPoint); // fully heal
            lastHealTime = Time.time;
            return true;
        }

        return false; // still on cooldown
    }

    public bool TrySpeedBoost()
    {
        if (Time.time - lastBoostTime >= boostCooldown && !isSpeedBoosted)
        {
            lastBoostTime = Time.time;
            boostEndTime = Time.time + boostDuration;
            isSpeedBoosted = true;

            xSpeed *= boostMultiplier;
            ySpeed *= boostMultiplier;

            Debug.Log("Speed boost activated!");
            return true;
        }

        Debug.Log("Speed boost is on cooldown.");
        return false;
    }

    public bool TryDamageBoost()
    {
        if (Time.time - lastDamageBoostTime >= damageBoostCooldown && !isDamageBoosted)
        {
            isDamageBoosted = true;
            lastDamageBoostTime = Time.time;
            damageBoostEndTime = Time.time + damageBoostDuration;
            Debug.Log("Damage boost activated!");
            return true;
        }

        Debug.Log("Damage boost is on cooldown.");
        return false;
    }


    public float GetDamageMultiplier()
    {
        return isDamageBoosted ? damageMultiplier : 1f;
    }


}
