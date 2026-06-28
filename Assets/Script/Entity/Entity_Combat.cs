using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats stats;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Status Effect details")]
    [SerializeField] private float defautDuration = 3;
    [SerializeField] private float chillSlowMultiplier = .2f;
    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack() // thực hiện 1 đòn đánh 
    {

        foreach (var target in GetDetectedColliders())
        {
            IDamgable damegable = target.GetComponent<IDamgable>();

            if (damegable == null)
                continue; // skip target, go to next target

            float elementalDamage = stats.GetElementalDamage(out ElementType element);
            float damage = stats.GetPhysicalDamage(out bool isCrit);
            // Có 2 chức năng vừa trả về bool vừa thực thi takedamage()
            bool targetGotHit = damegable.TakeDamage(damage, elementalDamage, element, transform); // transform của người thực hiện đòn đánh 

            if (element != ElementType.None)
                AppliedStatsEffect(target.transform, element);

            if (targetGotHit)
            {
                vfx.UpdateOnHitColor(element);
                vfx.CreateOnHitVFX(target.transform, isCrit);
            }
        }
    }

    public void AppliedStatsEffect(Transform target, ElementType element,float scaleFactor = 1)
    {
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();
        if (statusHandler == null)
            return;

        if (element == ElementType.Ice && statusHandler.CanBeApplied(ElementType.Ice))
            statusHandler.AppliedChillEffect(defautDuration, chillSlowMultiplier);

        if(element == ElementType.Fire && statusHandler.CanBeApplied(ElementType.Fire))
        {
            float fireDamage = stats.offense.fireDamage.GetValue() * scaleFactor;

            statusHandler.AppliedBurnEffect(defautDuration, fireDamage);
        }
    }
    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
