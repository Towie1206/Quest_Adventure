using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    public virtual void TakeDame(float damage, Transform damageDealer) // nhận damage và nhận đc transform của người đánh 
    {
        if(isDead)
            return;

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
