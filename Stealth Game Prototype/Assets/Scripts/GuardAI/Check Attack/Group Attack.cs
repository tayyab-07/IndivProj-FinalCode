using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class GroupAttack : Node
{
    // Class Tell thge player to manuever for an attack (Go to player)
    // The actual attacking portion from the guards will be dealt with in the Guard Attack class

    private Transform _player;
    private NavMeshAgent _agent;

    public GroupAttack(Transform player, NavMeshAgent agent)
    {
        _player = player;
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        // Sets the guard to go to the player position
        // return failure to allow for the rest of the tree to run
        // doing it this way allows the guard to still travell to the player but also use the following branch to check their zone and either chase or actually attack the player

        _agent.SetDestination(_player.position);
        state = NodeState.FAILURE; 
        return state;
    }
}