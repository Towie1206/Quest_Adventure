using UnityEngine;

public class SkillObject_Base : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float checkRadius = 1;


    protected void DamageEnemiesInRadius(Transform t, float radius)
    {
        foreach (var target in EnemiesAround(t, radius))
        {
            IDamgable damgable = target.GetComponent<IDamgable>();

            if (damgable == null)
                continue;

            damgable.TakeDamage(1, 1, ElementType.None, transform);
        }
    }

    protected Transform FindClosestTarget()
    {
        Transform target = null;
        float colosestDistance = Mathf.Infinity; // ban dau = vo cuc, lan kiem tra dau tien chac chan se cap nhat

        foreach (var enemy in EnemiesAround(transform, 10))
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if(distance < colosestDistance)
            {
                target = enemy.transform;
                colosestDistance = distance;
            }
        }

        return target;
    }

    protected Collider2D[] EnemiesAround(Transform t, float radius)
    {
        return Physics2D.OverlapCircleAll(t.position, radius, whatIsEnemy);
    }

    protected virtual void OnDrawGizmos()
    {

        if (targetCheck == null)
            targetCheck = transform;

        Gizmos.DrawWireSphere(targetCheck.position, checkRadius);
    }
}
