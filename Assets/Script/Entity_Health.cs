using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entityVfx;
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
    }    


    //every time somebody takes damage the entity will know who dealt that damage.
    public virtual void TakeDame(float damage, Transform damageDealer) 
    {
        if(isDead)
            return;

        entityVfx?.PlayOnDamageVfx(); // mean :entityVfx != null entityVfx.PlayOnDamageVfx();
        ReduceHp(damage);

    }
    protected void ReduceHp(float damage)
    {
        maxHp -= damage;

        if (maxHp <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
    }
}
