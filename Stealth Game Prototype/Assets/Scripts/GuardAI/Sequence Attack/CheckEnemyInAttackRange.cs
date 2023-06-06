using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    // This class checks to see if the guard is close enough to attack the player, returns SUCCESS if they are

    private GuardBehaviourTree _guard;

    public CheckEnemyInAttackRange(GuardBehaviourTree guard)
    {
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        // The attackPlayer varibale is set in the CHeckEnemySpotted class
        // The attack is only set to tru when the player is within zone 1 of the enemy`s vsion cone

        if (_guard.attackPlayer == true)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }
}
