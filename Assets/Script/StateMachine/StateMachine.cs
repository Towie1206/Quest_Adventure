using UnityEngine;

public class StateMachine
{
    //có thể đọc được nhưng không thể gán giá trị từ bên ngoài class này
    public EntityState currentState { get; private set; }//chỉ class con có thể thay đổi giá trị của currentState
    public bool canChangeState;

    public void Initialize(EntityState startingState)
    {
        canChangeState = true;
        currentState = startingState;
        currentState.Enter();
    }
    public void ChangeState(EntityState newState)
    {
        if (canChangeState == false)
            return;

        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public void UpdateActiveState()
    {
       currentState.Update();
    }
    public void SwitchOffStateMachine() => canChangeState = false;
}
