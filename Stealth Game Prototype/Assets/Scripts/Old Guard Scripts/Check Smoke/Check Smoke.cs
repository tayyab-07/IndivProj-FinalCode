using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class CheckSmoke : Node
{
    // This class was originally destined for the Guard Behaviour Tree
    // This class is currently NOT utilised in the Guard Behaviour Tree
    // This class was designed to check if a guard can see the smoke which would then return a SUCCESS
    // This feature is not part of the game and is currently broken. Hence the commented class


    private Transform _transform;
    private NavMeshAgent _agent;
    private Transform _smoke;
    private LayerMask _obstacleMask;
    private GuardBehaviourTree _guard;

    float angleC = 150;

    float inSmokeDist = 10.0f;
    float farViewingDist = 40.0f;

    System.Random rnd = new System.Random();

    /*
    
    public CheckSmoke(Transform transform, NavMeshAgent agent, Transform smoke, LayerMask obstacleMask, GuardBehaviourTree guard) 
    { 
        _transform = transform;
        _agent = agent;
        _smoke = smoke;
        _obstacleMask = obstacleMask;
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        Vector3 distToSmoke = (_smoke.position - _transform.position).normalized;
        float smokeGuardAngle = Vector3.Angle(_transform.forward, distToSmoke);

        // Checks if there is an obstacle between the guard and player
        if (!Physics.Linecast(_transform.position, _smoke.position, _obstacleMask))
        {

            if (Vector3.Distance(_transform.position, _smoke.position) < inSmokeDist)
            { 
                _agent.SetDestination(_transform.position);
                state = NodeState.SUCCESS; 
                return state;
            }

            // Checks to see the distance between the guard and player
            else if (Vector3.Distance(_transform.position, _smoke.position) < farViewingDist)
            {
                // Checks to see the angle between the guard and player
                if (smokeGuardAngle < angleC / 2f)
                {
                    _agent.SetDestination(_smoke.position + RandomOffset());
                    state = NodeState.SUCCESS;
                    return state;
                }
            }
        }

        _guard.smokeVisible = false;
        
        state = NodeState.FAILURE;
        return state;
    }

    private Vector3 RandomOffset()
    {
        // used to set a random offset to a smoke observation point on the map

        float rand = rnd.Next(0, 4);
        Vector3 location = new Vector3(0,0,0);

        switch (rand)
        {
            case 1:
                location = new Vector3(5,0,5);
                return location;
            case 2:
                location = new Vector3(5,0,-5);
                return location;
            case 3:
                location = new Vector3(-5,0,-5);
                return location;
            case 4: 
                location = new Vector3(-5,0,5);
                return location;
        }

        return location;

    }

    */

}
