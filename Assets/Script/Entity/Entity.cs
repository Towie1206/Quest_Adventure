using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public event Action OnFlipped; //các class khác có thể đăng ký để lắng nghe sự kiện này.

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    protected StateMachine stateMachine;


    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;  // 1 = right, -1 = left

    [Header("Collision detection")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    //Condition variables biến điều kiện
    private bool isKnocked;
    private Coroutine knockbackCo;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void CurrentStateAnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public virtual void EntityDead()
    {

    }

    public void ReciveKnockback(Vector2 knockback, float knockbackDuration)
    {
        if (knockbackCo != null)
          StopCoroutine(knockbackCo);

        knockbackCo = StartCoroutine(KnockbackCo(knockback, knockbackDuration));
    }

    private IEnumerator KnockbackCo(Vector2 knockback, float knockbackDuration)
    {
        isKnocked = true;
        rb.linearVelocity = knockback;

        yield return new WaitForSeconds(knockbackDuration);

        rb.linearVelocity = Vector2.zero; //it's going to just slide all over the level if dont reset

        isKnocked = false;
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
            return; 


        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }
    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !facingRight)
        {
            Flip();
        }
        else if (xVelocity < 0 && facingRight)
        {
            Flip();
        }
    }
    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
        facingDir *= -1;

         // Gọi event OnFlipped nếu có object nào đang subscribe.
        OnFlipped?.Invoke(); // if(OnFlipped != null) OnFlipped.Invoke();
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        if (secondaryWallCheck != null)
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
                        && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
        }
        else
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));

        if (secondaryWallCheck != null)
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
    }
}
