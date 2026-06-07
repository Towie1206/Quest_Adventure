using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;
    private float lastTimeAttacked;

    private bool comboAttackQueued;
    private int attackDir;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private const int FirstComboIndex = 1; // start combo index in number 1,this parametr is use in the animator   

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if(comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("Adjusted combo limit to match attack velocity array!");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        //đổi hướng khi đang tấn công
        //if (player.moveInput.x != 0 ?) yes -> value = ((int)player.moveInput.x), if (player.moveInput.x != 0 ?) no -> value = player.facingDir
        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;

        // if (player.moveInput.x != 0)
        // {
        //     attackDir = ((int)player.moveInput.x);
        // }
        // else 
        //  {
        //      attackDir = player.facingDir;
        // }


        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        //if deteced enemies make a combo

        if (input.Player.Attack.WasPressedThisFrame())
            QueuenextAttack(); 

        if (triggerCalled) 
            HandleStateExit();
    }
    public override void Exit()
    {
        base.Exit();

        comboIndex++;
        lastTimeAttacked = Time.time;        //remember time when we attacked
    }
    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);
    }
    private void QueuenextAttack()
    {
        if(comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }    
    }
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }
    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }
    private void ResetComboIndexIfNeeded()
    {
        //if time we attacked was long time ago, we reset combo index
        if(Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;

        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;
    }
}
