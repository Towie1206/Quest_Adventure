using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player; // trỏ đến player để có thể truy cập được các thuộc tính và phương thức của player, vì tất cả các state đều sẽ kế thừa từ class này nên sẽ có thể truy cập được player
    protected PlayerInputSet input; // để có thể truy cập được input của player, vì tất cả các state đều sẽ kế thừa từ class này nên sẽ có thể truy cập được input
    
    public PlayerState(Player player,StateMachine stateMachine, string animBoolName) : base(stateMachine,animBoolName)// constructor, để khởi tạo các thuộc tính của class, khi tạo một state mới sẽ phải truyền vào player, state machine và tên trạng thái
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
        stats = player.stats;
    }
    public override void Update()
    {
        base.Update();
        

        if(input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    private bool CanDash()
    {
        if (player.wallDetected)
            return false;

        if(stateMachine.currentState == player.dashState)
            return false;

        return true;
    }


}
