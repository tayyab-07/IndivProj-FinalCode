using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GuardPatrol : Node
{
    // This class handles the patrolling of the guards around their patrol paths

    private Transform _guardTransform;
    private Transform[] _patrolPoints;
    private NavMeshAgent _agent;
    private GuardBehaviourTree _guard;
    private AlertedSprite _alertedSprite;
    private SearchingSprite _searchingSprite;

    private int currentPatrolPoint;
    private float patrolStopDuration = 2.5f;
    private float patrolStopTimer = 0.0f;
    private bool stopped = false;

    public GuardPatrol(Transform guardTransform, Transform[] patrolPoints, NavMeshAgent agent, GuardBehaviourTree guard, AlertedSprite alertedSprite, SearchingSprite searchingSprite)
    { 
        _guardTransform = guardTransform;
        _patrolPoints = patrolPoints;
        _agent = agent;
        _guard = guard;
        _alertedSprite = alertedSprite;
        _searchingSprite = searchingSprite;
    }

    public override NodeState Evaluate()
    {
        // Method to set the patrol path of the guard

        // disable UI entirely if the guard is patrolling
        _alertedSprite.DisableAlerted();
        _searchingSprite.DisableSearching();

        Transform wp = _patrolPoints[currentPatrolPoint];

        // since organise attack returns false to allow for the rest of the tree to be run, in order to chase and actually attack the guard
        // the tree needs to make sure that the guard isnt patrolling either
        // since there is no class used to check whether a guard should be patrolling or not
        // there is some simple validation used which will return failure if the group are currently organising their attack
        if (_guard.organiseAttack == true)
        {
            state = NodeState.FAILURE;
            return state;
        }

        // if the guard is waiting along thier patrol path, start a timer and wait until they have waited the entire duration before moving on
        if (stopped == true)
        {
            patrolStopTimer = patrolStopTimer + Time.deltaTime;
            if (patrolStopTimer > patrolStopDuration)
            {
                stopped = false;
            }
        }

        else if (stopped == false)
        {
            // if guard reaches a waypoint set their target to the next waypoint
            if (Vector3.Distance(_guardTransform.position, wp.position) < (_agent.stoppingDistance + 0.2f))
            {
                patrolStopTimer = 0.0f;
                stopped = true;

                currentPatrolPoint = (currentPatrolPoint + 1) % _patrolPoints.Length;
            }
            else
            {
                _agent.SetDestination(wp.position);
            }
        }

        // return running constantly 
        // as its at the end of the tree, it can afford to do this as its not stopping anything else from running.
        // success or running here would work just the same
        state = NodeState.RUNNING;
        return state;
    }
}
