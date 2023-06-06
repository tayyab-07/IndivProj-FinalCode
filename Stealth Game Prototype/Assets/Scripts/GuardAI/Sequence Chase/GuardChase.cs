using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class GuardChase : Node
{
    // This class implemnets very basic functionality to chase after the player if they have been spotted
    // Due to the order that this class appears in the behaviour tree, it is unneccessary to chek whether the guard should be chasing or not
    // This is because we have already determined that the guard can see the player AND we have determined that the guard is not clopse enough to attack
    // Therefore the guard will chase 

    private Transform _player;
    private NavMeshAgent _agent;

    public GuardChase(Transform player, NavMeshAgent agent)
    {
        _player = player;
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        // chases after the player by setting the nav,esh agent to the players location
        _agent.SetDestination(_player.position);
        state = NodeState.SUCCESS; 
        return state;
    }

}
