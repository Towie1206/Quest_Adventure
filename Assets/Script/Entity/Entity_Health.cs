using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamgable   
{
    private Slider healthBar;
    private Entity_VFX entityVfx;
    private Entity entity;

    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7, 7);
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private float heavyKnockbackDuration = .5f;

    [Header("On Heavy Damage")]
    // percentage of health you should lose to consider damage as heavy
    [SerializeField] private float heavyDamageThreshold = .3f; //means : if attack will take 30% of HP from you that will be the heavydame

    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthBar = GetComponentInChildren<Slider>();

        currentHp = maxHp;
    }    


    //every time somebody takes damage the entity will know who dealt that damage.
    public virtual void TakeDamage(float damage, Transform damageDealer) 
    {
        if(isDead)
            return;

        entity.ReciveKnockback(CalulateKnockback(damage, damageDealer), CaculateDuration(damage));

        entityVfx?.PlayOnDamageVfx(); // mean :entityVfx != null entityVfx.PlayOnDamageVfx();
        ReduceHp(damage);
        UpdateHealthBar();

    }
    protected void ReduceHp(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar();

        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDead();
    }
    private void UpdateHealthBar()
    {
        if(healthBar == null)
            return;

        healthBar.value = currentHp / maxHp;
    }
    private Vector2 CalulateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;
        knockback.x *= direction;

        return knockback;
    }
    private float CaculateDuration(float damage) => IsHeavyDamage(damage) ? knockbackDuration : heavyKnockbackDuration;
    private bool IsHeavyDamage(float damage) => damage / maxHp > heavyDamageThreshold;
}
