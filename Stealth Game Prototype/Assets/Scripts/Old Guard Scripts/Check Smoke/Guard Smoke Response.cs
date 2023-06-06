using BehaviorTree;

public class GuardSmokeResponse : Node
{
    // This class was originally destined for the Guard Behaviour Tree
    // This class is currently NOT utilised in the Guard Behaviour Tree
    // This class was designed to set some booleans to true if the guard could currently see smoke which would have knock on effects for the other guards
    // This feature is not part of the game and is currently broken. Hence the commented class

    private GuardBehaviourTree _guard;

    /*
    
    public GuardSmokeResponse(GuardBehaviourTree guard) 
    { 
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        
        _guard.smokeSeen = true;
        _guard.smokeVisible = true;

        state = NodeState.SUCCESS; 
        return state;
    }


    */
}
