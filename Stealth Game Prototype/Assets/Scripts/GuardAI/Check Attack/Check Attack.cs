using BehaviorTree;
public class CheckAttack : Node
{

    // This class check to see if the guard should be attacking, return SUCCESS if they should be

    private GuardBehaviourTree _guard;

    public CheckAttack(GuardBehaviourTree guard) 
    { 
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        // Returns success if group are attacking
        // feeds into Group Attack.cs

        // organise attack is set by the BTGuardGroup class 
        if (_guard.organiseAttack == true)
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
