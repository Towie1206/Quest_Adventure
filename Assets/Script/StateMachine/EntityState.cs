using UnityEngine;

public abstract class EntityState 
{
    protected StateMachine stateMachine;// trỏ đến state machine để có thể thay đổi state, vì tất cả các state đều sẽ kế thừa từ class này nên sẽ có thể truy cập được state machine
    protected string animBoolName; // tên trạng thái

    protected Animator anim;
    protected Rigidbody2D rb;
    protected Entity_Stats stats;

    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter() // everytime state will be changed, this method will be called
    {
        anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Update() // run logic of the state, this method will be called every frame
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();
    }
    public virtual void Exit() // this method will be called, everytime we are leaving the state and change a new one
    {
        anim.SetBool(animBoolName, false);
    }

    public void AnimationTrigger()
    {
        triggerCalled = true;
    }
    public virtual void UpdateAnimationParameters()
    {

    }

    public void SyncAttackSpeed()
    {
        float attackSpeed = stats.offense.attackSpeed.GetValue();
        anim.SetFloat("attackSpeedMultiplier", attackSpeed);
    }
}
