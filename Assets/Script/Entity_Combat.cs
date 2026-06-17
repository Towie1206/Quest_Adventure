using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    public float damage = 10;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack() // thực hiện 1 đòn đánh 
    {

        foreach (var target in GetDetectedColliders())
        {
            Entity_Health targetHealth = target.GetComponent<Entity_Health>();

            targetHealth?.TakeDame(damage,transform); // if(targetHealth != null) targetHealth.TakeDamage(damage);
            // transform của người thực hiện đòn đánh 
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
