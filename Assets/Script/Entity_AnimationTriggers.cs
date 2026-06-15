using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{
    private Entity entity;
    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }
    private void CurrentStateTrigger() // get accec to player and let current player's state know that we want to exit state 
    {
        entity.CallAnimationTrigger();
    }

}
