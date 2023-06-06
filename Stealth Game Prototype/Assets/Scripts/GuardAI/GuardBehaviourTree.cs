using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This file implemnets the basic behaviour tree loop for the Guard AI in the game

public class GuardBehaviourTree : BehaviorTree.Tree
{
    [Header("Patrol Points")]
    public Transform[] patrolPoints;

    [Header("Objects")]
    public LayerMask obstacleMask;
    public Transform player;
    public NavMeshAgent agent;
    public Light spotlight;
    public GuardBehaviourTree guard;

    [Header("Smoke")]
    // Part of additional smoke implementation. Currently not working
    // Transform and LayerMask required as part of Check Smoke(Class not included in tree currently) and Check Enemy Zone respectively

    //public Transform smoke;
    //public LayerMask smokeMask;

    //public bool smokeVisible = false;
    //public bool smokeSeen = false;

    [Header("Sprites")]
    public AlertedSprite alertedSprite;
    public SearchingSprite searchingSprite;
    public DetectionBarSprite detectionBarSprite;

    [Header("Booleans")]
    public bool attackPlayer = false;
    public bool playerSeen = false;
    public bool playerVisible = false;

    public bool search1 = false;
    public bool search2 = false;

    [Header("Timer")]
    public float timePlayerVisible;

    [Header("Organise")]
    public bool organiseAttack;
    public bool organiseSearch;

    [Header("Zones")]
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

    protected override Node SetupTree()
    {
        // Structure for Guard behaviour tree

        Node root = new Selector(new List<Node>
        {
            /*
            // Classes for checking if the guard can see smoke and reacting accordingly
            // Currently not working
            
            new Sequence (new List<Node>
            { 
                new CheckSmoke(transform, agent, obstacleMask, guard),
                new GuardSmokeResponse(guard),
            }),
            */

            // First sequnce checks if the other guards are attacking, if they are it sends the current guard to go attack as well
            new Sequence (new List<Node>
            {
                new CheckAttack(guard),
                new GroupAttack(player, agent),
            }),

            // Second sequence handles detection of the PLayer and what to do once the player is detected based on if they are within attack range or not
            new Sequence (new List<Node>
            { 
                new CheckEnemyZone(transform, player, obstacleMask, guard),

                new CheckEnemySpotted(spotlight, guard, alertedSprite, searchingSprite, detectionBarSprite),

                new Selector(new List<Node>
                { 
                    new Sequence(new List<Node>
                    {
                        new CheckEnemyInAttackRange(guard),
                        new GuardAttack(spotlight),
                    }),

                    new GuardChase(player, agent),
                }),
            }),
            
            // Third sequence checks if other guards are currently searching, if they are, the current guard will join the search
            new Sequence (new List<Node>
            { 
                new CheckSearch(guard),
                new GroupSearch(alertedSprite, searchingSprite),
            }),

            // The last Node runs if nothing else returned a SUCCESS
            // This node just keeps the guard patrolling aroudn a set of points
            new GuardPatrol(transform, patrolPoints, agent, guard, alertedSprite, searchingSprite),

        });

        return root;
    }
}
