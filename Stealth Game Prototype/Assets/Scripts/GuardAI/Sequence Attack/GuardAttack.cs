using BehaviorTree;
using UnityEngine;

public class GuardAttack : Node
{
    // This class was going to hold the functionality for the guard to actually shoot at the player and be able to deal damage to the player
    // This class could never be implemented within the timeframe

    private Light _spotlight;

    public GuardAttack(Light spotlight) 
    { 
        _spotlight = spotlight;
    }

    public override NodeState Evaluate()
    {
        // placeholder class for now, sets the spotlight color to red when the guard should be attacking
        // This is where the actual attack perfropmed by a guard should be taking place

        _spotlight.color = Color.red;
        state = NodeState.SUCCESS;
        return state;
    }

}
