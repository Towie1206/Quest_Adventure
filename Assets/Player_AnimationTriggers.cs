using UnityEngine;

public class Player_AnimationTriggers : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    private void CurrentStateTrigger() // get accec to player and let current player's state know that we want to exit state 
    {
        player.CallAnimationTrigger();
    }

}
