using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player_GroundedState : EntityState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }
    public override void Update()
    {
        base.Update();

        if(rb.linearVelocity.y< 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
        if (input.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
