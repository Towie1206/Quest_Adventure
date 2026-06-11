using System;
using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        
    }
    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.linearVelocity.y);
    }
    public override void Update()
    {
        base.Update();
        //if player try to move facing direction annd direction detected wall, so still in idle
        if (player.moveInput.x == player.facingDir && player.wallDetected)
            return;
        if (player.moveInput.x != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }

}
