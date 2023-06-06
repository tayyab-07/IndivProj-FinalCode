using BehaviorTree;

public class CheckSearch : Node
{
    // This class checks to see if the guard should be searching, return SUCCESS if they should be

    private GuardBehaviourTree _guard;

    public CheckSearch(GuardBehaviourTree guard)
    {
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        // The organiseSearch variable is handled by the BTGuardGroup class
        // return success if the group has organised a search
        if (_guard.organiseSearch == true)
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
