using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("Counter Attack Details")]
    [SerializeField] private float counterRecovery = .1f;
    public bool CounterAttackPerform()
    {
        bool hasPerformedCounter = false;
        foreach(var target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();

            if(counterable == null) 
                continue; //skip this target, go to next target

            if (counterable.CanBeCountered) // canBeStunned của enemy skeleton true thì mới counter được
            {
                counterable.HandleCounter();
                hasPerformedCounter = true;
            }
        }
        
        return hasPerformedCounter;
    }
    public float GetCounterRecoveryDuration() =>  counterRecovery;
}
