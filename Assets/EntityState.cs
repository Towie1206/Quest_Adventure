using UnityEngine;

public class EntityState 
{
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter() // everytime state will be changed, this method will be called
    {
        Debug.Log("Entering state: " + stateName);
    }
    public virtual void Update() // run logic of the state, this method will be called every frame
    {
        Debug.Log("Updating state: " + stateName);
    }
    public virtual void Exit() // this method will be called, everytime we are leaving the state and change a new one
    {
        Debug.Log("Exiting state: " + stateName);
    }
}
