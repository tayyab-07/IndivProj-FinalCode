using UnityEngine;
using UnityEngine.AI;

// [1] tutorial from: https://www.youtube.com/watch?v=TfhPBAe9Tt8&ab_channel=SebastianLague
// Github link for tutorial: https://github.com/SebLague/Intro-to-Gamedev/tree/master/Episode%2024

// Disclaimer: most of this file IS written by me.
// there are some bits still kept from the sources above.

// This file is no longer used in the code
// The classes in the new Guard Behaviour tree were largely based on the methods from this class. Making this class obsolete.

public class Guard : MonoBehaviour
{
    public Light spotlight;
    public LayerMask viewMask;
    public NavMeshAgent agent;
    Transform player;

    [Header("Zones")]
    public float timePlayerVisible;
    float angleA = 30;
    float angleB = 90;
    float angleC = 150;

    float zone1Timer = 1.0f;
    float zone2Timer = 1.5f;
    float zone3Timer = 2.0f;
    float zone4Timer = 3.0f;
    float zone5Timer = 5.0f;

    float farViewingDist = 40.0f;
    float mediumViewingDist = 25.0f;
    float nearViewingDist = 15.0f;

    // enum state to store different detection zones
    public ZoneState zone;
    public enum ZoneState
    {
        emptyZone,
        zone1,
        zone2,
        zone3,
        zone4,
        zone5
    }

    [Header("Search")]
    public bool search1 = false;
    public bool search2 = false;
    public bool playerSeen = false;
    public bool attackPlayer = false;
   
    Color initialSpotlightColour; 
    Color spottedColour;
    public Vector3 initialGuardLocation;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    private float patrolSpeed = 2.0f;
    private float patrolStopDuration = 2.5f;
    private float patrolStopTimer = 0.0f;
    private bool stopped = false;
    private float patrolRotationSpeed = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  

        initialSpotlightColour = spotlight.color; //[2]
        initialGuardLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // sets gurad patrol paths
        Patrol();

        // checks if player is within enemy vision zones
        DetermineVisionZone();

        // chases player if they stay in the vision zone too long
        ChasePlayer();
    }

    public void DetermineVisionZone()
    {
        // conditional statements to find out what zone a player is in
        // always set to empty zone if any condition isnt met

        Vector3 distToPlayer = (player.position - transform.position).normalized;  
        float playerGuardAngle = Vector3.Angle(transform.forward, distToPlayer);  

        // Checks if there is an obstacle between the guard and player
        if (!Physics.Linecast(transform.position, player.position, viewMask))
        {
            // Checks to see the distance between the guard and player
            if (Vector3.Distance(transform.position, player.position) < nearViewingDist)  
            {
                // Checks to see the angle between the guard and player
                if (playerGuardAngle < angleA / 2f)
                {
                    zone = ZoneState.zone1;
                }

                else if (playerGuardAngle < angleC / 2f)
                {
                    zone = ZoneState.zone2;
                }

                else
                {
                    zone = ZoneState.emptyZone;
                }
            }

            // Checks to see the distance between the guard and player
            else if (Vector3.Distance(transform.position, player.position) < mediumViewingDist)  
            {
                // Checks to see the angle between the guard and player
                if (playerGuardAngle < angleA / 2f)
                {
                    zone = ZoneState.zone2;
                }

                else if (playerGuardAngle < angleB / 2f)
                {
                    zone = ZoneState.zone3;
                }

                else if (playerGuardAngle < angleC / 2f)
                {
                    zone = ZoneState.zone4;
                }

                else
                {
                    zone = ZoneState.emptyZone;
                }
            }

            // Checks to see the distance between the guard and player
            else if (Vector3.Distance(transform.position, player.position) < farViewingDist)  
            {
                // Checks to see the angle between the guard and player
                if (playerGuardAngle < angleA / 2f)
                {
                    zone = ZoneState.zone3;
                }

                else if (playerGuardAngle < angleB / 2f)
                {
                    zone = ZoneState.zone4;
                }

                else if (playerGuardAngle < angleC / 2f)
                {
                    zone = ZoneState.zone5;
                }
                else
                {
                    zone = ZoneState.emptyZone;
                }
            }

            else
            {
                zone = ZoneState.emptyZone;
            }
        }
        else 
        { 
            zone = ZoneState.emptyZone; 
        }
    }

    private void Patrol()
    {
        // Method to set the patrol path of the guard

        Transform wp = patrolPoints[currentPatrolPoint];

        // if the guard is waiting along thier patrol path, start a timer and rotate the guard to face the next waypoint
        if (stopped == true)
        {
            patrolStopTimer = patrolStopTimer + Time.deltaTime;
            if (patrolStopTimer < patrolStopDuration)
            {
                Vector3 targetDirection = (wp.position - transform.position).normalized;  //[1]
                float angleToTarget = 90 - Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;  //[1]

                if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, angleToTarget)) > 0.05f)  //[1]
                {
                    float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, angleToTarget, patrolRotationSpeed * Time.deltaTime); //[1]
                    transform.eulerAngles = Vector3.up * angle; //[1]
                }

                return;
            }
            stopped = false;
        }

        // if guard reaches a waypoint set their target to the next waypoint
        if (Vector3.Distance(transform.position, wp.position) < 0.1f)
        {
            transform.position = wp.position;
            patrolStopTimer = 0.0f;
            stopped = true;

            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
        }
        // otherwise keep looking at the current waypoint
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, patrolSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, wp.position) > 2.0f)
            {
                transform.LookAt(wp.position);
            }
        }
    }

    private void ChasePlayer()
    {
        // Method to chase player if they are caught in a vision zone for enough time

        // If player is within a zone, start a timer
            // If timer exceeds limit, change spotlight colour to indicate detection, chase player and set bools to true

        if (zone == ZoneState.zone1)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   
            if (timePlayerVisible >= zone1Timer)
            {
                spottedColour = Color.red;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        else if (zone == ZoneState.zone2)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;  
            if (timePlayerVisible >= zone2Timer)
            {
                spottedColour = Color.magenta;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        else if (zone == ZoneState.zone3)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   
            if (timePlayerVisible >= zone3Timer)
            {
                spottedColour = Color.yellow;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        else if (zone == ZoneState.zone4)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   
            if (timePlayerVisible >= zone4Timer)
            {
                spottedColour = Color.green;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        else if (zone == ZoneState.zone5)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   
            if (timePlayerVisible >= zone5Timer)
            {
                spottedColour = Color.blue;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        // if player is not in a zone, decremment the timer
        else if (zone == ZoneState.emptyZone)
        {
            attackPlayer = false;
            spottedColour = initialSpotlightColour;
            timePlayerVisible = timePlayerVisible - Time.deltaTime;    
        }

        // timer doesnt exceed 0 or zone 5 timer
        timePlayerVisible = Mathf.Clamp(timePlayerVisible, 0, zone5Timer);   
        spotlight.color = spottedColour;
    }

    // draws gizmo to help visulise distance in scene view
    private void OnDrawGizmos()  
    {
        // Draw a red ray to show which way the guard is facing
        Gizmos.color = Color.red;  //[1]
        Gizmos.DrawRay(transform.position, transform.forward * 30);  //[1]

        // Draw a green line to show the patrol paths
        Gizmos.color = Color.green;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[(i+1) % patrolPoints.Length].position);
        }   
    }

}
