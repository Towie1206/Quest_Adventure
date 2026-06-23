using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private Player_Combat combat;
    private bool counterSomebody;
    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        combat = player.GetComponent<Player_Combat>(); 
    }

    public override void Enter()
    {
        base.Enter();

        counterSomebody = combat.CounterAttackPerform();
        stateTimer = combat.GetCounterRecoveryDuration();

        anim.SetBool("counterAttackPerformed", counterSomebody);

    }
    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, rb.linearVelocity.y);
        
        if(triggerCalled) // nếu mà thực hiện hết anim perform
            stateMachine.ChangeState(player.idleState);
        
        if (stateTimer < 0 && !counterSomebody) // nếu mà ko thực hiện perform thì chỉ đứng thế counter trong n s;
            stateMachine.ChangeState(player.idleState);
    }
}
