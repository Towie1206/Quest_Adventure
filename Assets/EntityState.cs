using UnityEngine;

public abstract class EntityState 
{
    protected Player player; // trỏ đến player để có thể truy cập được các thuộc tính và phương thức của player, vì tất cả các state đều sẽ kế thừa từ class này nên sẽ có thể truy cập được player
    protected StateMachine stateMachine;// trỏ đến state machine để có thể thay đổi state, vì tất cả các state đều sẽ kế thừa từ class này nên sẽ có thể truy cập được state machine
    protected string animBoolName; // tên trạng thái

    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input; // để có thể truy cập được input của player, vì tất cả các state đều sẽ kế thừa từ class này nên sẽ có thể truy cập được input

    protected float stateTimer;
    public EntityState(Player player,StateMachine stateMachine, string animBoolName) // constructor, để khởi tạo các thuộc tính của class, khi tạo một state mới sẽ phải truyền vào player, state machine và tên trạng thái
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }


    public virtual void Enter() // everytime state will be changed, this method will be called
    {
        anim.SetBool(animBoolName, true);
    }
    public virtual void Update() // run logic of the state, this method will be called every frame
    {
        stateTimer -= Time.deltaTime;
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if(input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }
    public virtual void Exit() // this method will be called, everytime we are leaving the state and change a new one
    {
        anim.SetBool(animBoolName, false);
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
