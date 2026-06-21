using UnityEngine;

public class Chest : MonoBehaviour , IDamgable
{
    private Rigidbody2D rb => GetComponentInChildren<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();
    private Entity_VFX entityVfx => GetComponent<Entity_VFX>();

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback;

    public void TakeDamage(float damage, Transform damegeDealer)
    {
        entityVfx.PlayOnDamageVfx();
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = knockback;

        rb.angularVelocity = Random.Range(-200, 200);

        //drop item
    }
    
}
