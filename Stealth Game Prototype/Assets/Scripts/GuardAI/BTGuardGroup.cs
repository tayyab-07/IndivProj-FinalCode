using UnityEngine;

// This class handles a group of guards
// It controls when they cooridnate attacks
// It also completely controls when, where and how long each guard searches for 

public class BTGuardGroup : MonoBehaviour
{
    [Header("Random")]
    System.Random rnd = new System.Random();

    [Header("Guards")]
    public GuardBehaviourTree[] guards;

    [Header("Search Locations")]
    public Transform loc0;
    public Transform loc1;
    public Transform loc2;
    public Transform loc3;
    public Transform loc4;
    public Transform loc5;
    public Transform loc6;
    public Transform loc7;
    public Transform loc8;
    public Transform loc9;

    [Header("Search Timer")]
    public float searchTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Continuos loop running in order to chekc if any of the guards have seen the player but have since lost sight of him
        for (int i = 0; i < guards.Length; i++)
        {
            // If a guard has seen the player, loop through all guards and tell all of the them to organise a search 
            if (guards[i].playerSeen == true && guards[i].playerVisible == false)
            {
                for (int j = 0; j < guards.Length; j++)
                {
                    guards[j].organiseSearch = true;
                }
                ConductSearch();
                break;
            }
        }

        ConductAttack();
    }

    void ConductSearch()
    {
        // Method conducts the timer for the guards search pattern and resets the serach if the player hsnt been found in teh allotted time

        // Search for 'serachTimer' seconds then reset to start location
        searchTimer = searchTimer + Time.deltaTime;

        // Set all guards to search one place until 'x' sec and another until 'y' sec
        for (int i = 0; i < guards.Length; i++)
        {
            if (searchTimer > 5 && searchTimer < 25 && guards[i].search1 == false)
            {
                GuardSearch(guards[i]);
                guards[i].search1 = true;
            }

            if (searchTimer > 25 && searchTimer < 40 && guards[i].search2 == false)
            {
                GuardSearch(guards[i]);
                guards[i].search2 = true;
            }

            // After 40 sec, tell guards to go back to starting location
            if (searchTimer > 40)
            {
                ResetSearch();
            }
        }
    }

    void ConductAttack()
    {
        // Method mainly organises attack
        // Method also has functionlaity for disbanding the attack if none of the guards can see the player

        int guardsNotSeeingPlayer = 0;

        // Loops through all guards to see if any are currently looking at the player
        for (int i = 0; i < guards.Length; i++)
        {
            // If a guard is looking at the player, reset the guards Search variables and organise an attack
            if (guards[i].playerVisible == true)
            {
                searchTimer = 0;
                for (int j = 0; j < guards.Length; j++)
                {
                    guards[j].search1 = false;
                    guards[j].search2 = false;
                    guards[j].organiseAttack = true;
                }
            }

            // if no gurad is looking at the player, increment a variable
            // This variable can be used to compare againts the total number of guards in a group
            else
            {
                guardsNotSeeingPlayer = guardsNotSeeingPlayer + 1;
            }

            // If all of the guards cannot see the player, stop attacking 
            if (guardsNotSeeingPlayer == guards.Length)
            {
                for (int j = 0; j < guards.Length; j++)
                {
                    guards[j].organiseAttack = false;
                }
                guardsNotSeeingPlayer = 0;
            }
        }
    }

    void GuardSearch(GuardBehaviourTree guard)
    {
        // Method to randomly assign a guard to a search location
        // Search location are set by empty gameObjects`s transforms in Unity

        // Random variable to assign a guard with a random search location
        float rand = rnd.Next(0, 10);

        switch (rand)
        {
            case 0:
                guard.agent.SetDestination(loc0.position);
                break;
            case 1:
                guard.agent.SetDestination(loc1.position);
                break;
            case 2:
                guard.agent.SetDestination(loc2.position);
                break;
            case 3:
                guard.agent.SetDestination(loc3.position);
                break;
            case 4:
                guard.agent.SetDestination(loc4.position);
                break;
            case 5:
                guard.agent.SetDestination(loc5.position);
                break;
            case 6:
                guard.agent.SetDestination(loc6.position);
                break;
            case 7:
                guard.agent.SetDestination(loc7.position);
                break;
            case 8:
                guard.agent.SetDestination(loc8.position);
                break;
            case 9:
                guard.agent.SetDestination(loc9.position);
                break;
        }
    }

    void ResetSearch()
    {
        // Method to reset ALL of the search variables, should the player not be found 
        searchTimer = 0;
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].organiseSearch = false;
            guards[i].playerSeen = false;
            guards[i].search1 = false;
            guards[i].search2 = false;
        }
    }

}