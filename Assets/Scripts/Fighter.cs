using UnityEngine;

public class Fighter : MonoBehaviour
{
    // public fields
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    // immunity fields
    protected float immunteTime = 1.0f;
    protected float lastImmune;

    // push
    protected Vector3 pushDirection;

    // all fighters can receive damage (ReceiveDamage) and also die (Die)
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immunteTime)   // means u can receive damage rn
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 20, Color.red, transform.position, Vector3.zero, 0.5f);

            if (hitPoint < 0)
            {
                hitPoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
