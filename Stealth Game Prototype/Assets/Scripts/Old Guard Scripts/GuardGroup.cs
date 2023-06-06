using UnityEngine;

public class GuardGroup : MonoBehaviour
{
    // This file is no longer used in the code
    // There is an almost identical class which controls the guards in roughly the same way called BTGuardGroup
    // BTGuardGriup does the same things as this class but wokrs with the behaviour tree architecture whereas this class does not. Making it obsolete.

    // There wa spotentila to simply rewire this class into the one currently used called BTGuardGroup
    // However at the tiem of making that class, it was not clear how similar they were going to be so annew class was created
    // Eventually however, they eneded up being very similar but with slightly differnt syntax

    Transform player;
    System.Random rnd = new System.Random();

    [Header("Guards")]
    public Guard[] guards;

    [Header("Search Locations")]
    public Vector3 loc0 = new Vector3(18, 0, 18);
    public Vector3 loc1 = new Vector3(0, 0, 22);
    public Vector3 loc2 = new Vector3(-22, 0, 22);
    public Vector3 loc3 = new Vector3(-22, 0, 0);
    public Vector3 loc4 = new Vector3(-22, 0, -22);
    public Vector3 loc5 = new Vector3(0, 0, -22);
    public Vector3 loc6 = new Vector3(22, 0, -22);
    public Vector3 loc7 = new Vector3(-22, 0, 0);
    public Vector3 loc8 = new Vector3(8, 0, 3.5f);
    public Vector3 loc9 = new Vector3(-8.5f, 0, 17);

    [Header("Search")]
    public float searchTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if any of the guards have seen the player
        // If they have, start the search, regardless if the others havent seen the player
        // It makes it look like the guards have communicated and are working together
        for (int i = 0; i < guards.Length; i++)
        {
            if (guards[i].zone == Guard.ZoneState.emptyZone && guards[i].playerSeen == true)
            {
                ConductSearch();
                break;
            }
        }

        CheckDetection();
    }

    void CheckDetection()
    {
        // Method to check if any of the guard are currently chasing the player
        // If they are, set the other guards to go to the players location so they can chase too

        for (int i = 0; i < guards.Length; i++)
        {
            if (guards[i].attackPlayer == true)
            {
                ResetSearch();

                for (int j = 0; j < guards.Length; j++)
                {
                    guards[j].agent.SetDestination(player.position);
                }
            }
        }
    }

    void ConductSearch()
    {
        // Method to organise the guards to go search

        // Search for 30 seconds then reset to start location
        searchTimer = searchTimer + Time.deltaTime;

        // Set all guards to search one place until 15sec and another until 30sec
        for (int i = 0; i < guards.Length; i++)
        {
            if (searchTimer > 2 && searchTimer < 15 && guards[i].search1 == false)
            {
                GuardSearch(guards[i]);
                guards[i].search1 = true;
            }

            if (searchTimer > 15 && searchTimer < 30 && guards[i].search2 == false)
            {
                GuardSearch(guards[i]);
                guards[i].search2 = true;
            }

            // After 30 sec, tell guards to go back to starting location
            if (searchTimer > 30)
            {
                for (int j = 0; j < guards.Length; j++)
                { 
                    guards[j].agent.SetDestination(guards[j].initialGuardLocation);
                    guards[j].playerSeen = false;
                    ResetSearch();
                }
            }
        } 
    }

    void GuardSearch(Guard guard)
    {
        // Method to randomly assign a guard to a search location

        float rand = rnd.Next(0, 10);

        switch (rand)
        {
            case 0:
                guard.agent.SetDestination(loc0);
                break;
            case 1:
                guard.agent.SetDestination(loc1);
                break;
            case 2:
                guard.agent.SetDestination(loc2);
                break;
            case 3:
                guard.agent.SetDestination(loc3);
                break;
            case 4:
                guard.agent.SetDestination(loc4);
                break;
            case 5:
                guard.agent.SetDestination(loc5);
                break;
            case 6:
                guard.agent.SetDestination(loc6);
                break;
            case 7:
                guard.agent.SetDestination(loc7);
                break;
            case 8:
                guard.agent.SetDestination(loc8);
                break;
            case 9:
                guard.agent.SetDestination(loc9);
                break;
        }
    }

    void ResetSearch()
    {
        // Method to reset the searchtimer and reset the guards search bools

        searchTimer = 0;
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].search1 = false;
            guards[i].search2 = false;
        }
    }

}