using UnityEngine;

public abstract class EntityState 
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;

    public EntityState(Player player,StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        anim = player.anim;
        rb = player.rb;
    }


    public virtual void Enter() // everytime state will be changed, this method will be called
    {
        anim.SetBool(animBoolName, true);
    }
    public virtual void Update() // run logic of the state, this method will be called every frame
    {
        Debug.Log("Updating state: " + animBoolName);
    }
    public virtual void Exit() // this method will be called, everytime we are leaving the state and change a new one
    {
        anim.SetBool(animBoolName, false);
    }
}
