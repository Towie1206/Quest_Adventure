using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Update() // if player detecting the grond layer, if yes go to idle state
    {
        base.Update();
        if(player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }   
    }
}
