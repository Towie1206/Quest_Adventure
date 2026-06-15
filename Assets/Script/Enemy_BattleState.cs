using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player; // lấy vị trí player từ raycast
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (player == null)
        {
            player = enemy.PlayerDetection().transform;
        }
    }
    public override void Update()
    {
        base.Update();

        if (WithInAttackRange())
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
        }
    }

    private bool WithInAttackRange() => DistanceToPlayer() < enemy.attackDistance;
   
    private float DistanceToPlayer()
    {
        if (player == null)
            return float.MaxValue;

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }
    private int DirectionToPlayer()
    {
        if(player == null)
            return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
