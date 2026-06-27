using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamgable
{
    private Slider healthBar;
    private Entity_VFX entityVfx;
    private Entity entity;
    private Entity_Stats stats;

    [SerializeField] protected float currentHp;
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
        stats = GetComponent<Entity_Stats>();
        healthBar = GetComponentInChildren<Slider>();

        currentHp = stats.GetMaxHealth();
        UpdateHealthBar();
    }


    //every time somebody takes damage the entity will know who dealt that damage.
    public virtual bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
    {
        if (isDead)
            return false;
        if (AttackEvaded())
            return false;

        Entity_Stats attackerStats = damageDealer.GetComponent<Entity_Stats>();
        float armorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0;

        float mitigation = stats.GetArmorMitigation(armorReduction);
        float physicalDamageTaken = damage * (1 - mitigation); // 150 damage // .6f = 60% // take 40% damage

        float resistance = stats.GetElementalResistance(element);
        float elementalDamageTaken = elementalDamage * (1 - resistance); // 150 damage // .6f = 60% // take 40% damage

        TakeKnockback(damageDealer, physicalDamageTaken);
        ReduceHp(physicalDamageTaken + elementalDamageTaken);


        return true;
    }
    private void TakeKnockback(Transform damageDealer, float finalDamage)
    {
        Vector2 knochback = CalulateKnockback(finalDamage, damageDealer);
        float durationh = CaculateDuration(finalDamage);

        entity?.ReciveKnockback(knochback, durationh);
    }

    private bool AttackEvaded() => Random.Range(0, 100) < stats.GetEvasion();
    protected void ReduceHp(float damage)
    {
        entityVfx?.PlayOnDamageVfx(); // mean :entityVfx != null entityVfx.PlayOnDamageVfx();
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
        if (healthBar == null)
            return;

        healthBar.value = currentHp / stats.GetMaxHealth();
    }
    private Vector2 CalulateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;
        knockback.x *= direction;

        return knockback;
    }
    private float CaculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    private bool IsHeavyDamage(float damage) => damage / stats.GetMaxHealth() > heavyDamageThreshold;
}
