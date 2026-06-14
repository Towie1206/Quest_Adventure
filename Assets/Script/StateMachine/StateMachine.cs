using UnityEngine;

public class StateMachine
{
    //có thể đọc được nhưng không thể gán giá trị từ bên ngoài class này
    public EntityState currentState { get; private set; }//chỉ class con có thể thay đổi giá trị của currentState

    public void Initialize(EntityState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }
    public void ChangeState(EntityState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public void UpdateActiveState()
    {
       currentState.Update();
    }
}
