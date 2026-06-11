using Unity.VisualScripting;
using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter() // make obj go up, increase y velocity
    {
        base.Enter();
        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);

    }
    public override void Update() // if y velocity go down, change to fall state 
    {
        base.Update();
        //we need to be sure we are not in jump attack state when we transfer to fall state
        if(rb.linearVelocity.y<0 && stateMachine.currentState != player.jumpAttackState)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
