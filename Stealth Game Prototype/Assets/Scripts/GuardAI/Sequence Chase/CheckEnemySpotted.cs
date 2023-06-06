using BehaviorTree;
using UnityEngine;

public class CheckEnemySpotted : Node
{
    // This class checks how long the player has spent in any one of the enemy`s vision zones and reacts accordingly

    private Light _spotlight;
    private GuardBehaviourTree _guard;
    private AlertedSprite _alertedSprite;
    private SearchingSprite _searchingSprite;
    private DetectionBarSprite _detectionBarSprite;

    private float zone1Timer = 1.0f;
    private float zone2Timer = 1.5f;
    private float zone3Timer = 2.0f;
    private float zone4Timer = 3.0f;
    private float zone5Timer = 5.0f;

    public CheckEnemySpotted(Light spotlight, GuardBehaviourTree guard, AlertedSprite alertedSprite, SearchingSprite searchingSprite, DetectionBarSprite detectionBarSprite)
    {
        _spotlight = spotlight;
        _guard = guard;
        _alertedSprite = alertedSprite;
        _searchingSprite = searchingSprite;
        _detectionBarSprite = detectionBarSprite;
    }

    public override NodeState Evaluate()
    {
        // Method to chase player if they are caught in a vision zone for enough time
        // If player is within a zone, start a timer
        // If timer exceeds limit, change spotlight colour to indicate detection and set bools

        // timer doesnt exceed 0 or zone 5 timer
        _guard.timePlayerVisible = Mathf.Clamp(_guard.timePlayerVisible, 0, zone5Timer);

        if (_guard.zone == GuardBehaviourTree.ZoneState.zone1)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;

            // displaying the detction bar and setting its detection amount based on the current time
            _detectionBarSprite.DisplayDetected();
            _detectionBarSprite.SetDetection(_guard.timePlayerVisible/zone1Timer);

            if (_guard.timePlayerVisible >= zone1Timer)
            {
                UIonDetection();
                // The spotlight color is not set here ro zone 1, but is instead set in Guard Attack.
                // This is to give the dev some feedback on when that class is running 
                _guard.attackPlayer = true;
                _guard.playerSeen = true;
                _guard.playerVisible = true;
            }
        }

        else if (_guard.zone == GuardBehaviourTree.ZoneState.zone2)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;
            _detectionBarSprite.DisplayDetected();
            _detectionBarSprite.SetDetection(_guard.timePlayerVisible / zone2Timer);
            if (_guard.timePlayerVisible >= zone2Timer)
            {
                UIonDetection();
                _spotlight.color = Color.magenta;
                _guard.attackPlayer = false;
                _guard.playerSeen = true;
                _guard.playerVisible = true;
            }
        }

        else if (_guard.zone == GuardBehaviourTree.ZoneState.zone3)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;
            _detectionBarSprite.DisplayDetected();
            _detectionBarSprite.SetDetection(_guard.timePlayerVisible / zone3Timer);
            if (_guard.timePlayerVisible >= zone3Timer)
            {
                UIonDetection();
                _spotlight.color = Color.yellow;
                _guard.attackPlayer = false;
                _guard.playerSeen = true;
                _guard.playerVisible = true;
            }
        }

        else if (_guard.zone == GuardBehaviourTree.ZoneState.zone4)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;
            _detectionBarSprite.DisplayDetected();
            _detectionBarSprite.SetDetection(_guard.timePlayerVisible / zone4Timer);
            if (_guard.timePlayerVisible >= zone4Timer)
            {
                UIonDetection();
                _spotlight.color = Color.green;
                _guard.attackPlayer = false;
                _guard.playerSeen = true;
                _guard.playerVisible = true;
            }
        }

        else if (_guard.zone == GuardBehaviourTree.ZoneState.zone5)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;
            _detectionBarSprite.DisplayDetected();
            _detectionBarSprite.SetDetection(_guard.timePlayerVisible / zone5Timer);
            if (_guard.timePlayerVisible >= zone5Timer)
            {
                UIonDetection();
                _spotlight.color = Color.blue;
                _guard.attackPlayer = false;
                _guard.playerSeen = true;
                _guard.playerVisible = true;
            }
        }

        // if player is not in a zone, decrement the timer
        else if (_guard.zone == GuardBehaviourTree.ZoneState.emptyZone)
        {
            _alertedSprite.DisableAlerted();
            _detectionBarSprite.DisplayDetected();
            _detectionBarSprite.SetDetection(_guard.timePlayerVisible/zone5Timer);
            _spotlight.color = Color.white;
            _guard.attackPlayer = false;
            _guard.playerVisible = false;
            _guard.timePlayerVisible = _guard.timePlayerVisible - Time.deltaTime;

            // if the player is no longer visible and timer ticks down to 0, return false so that the guard doesnt chase or attack
            if (_guard.timePlayerVisible <= 0)
            {
                _detectionBarSprite.DisableDetected();
                _searchingSprite.DisableSearching();
                state = NodeState.FAILURE;
                return state;
            }
        }

        // return success if the player is detected in any zone from 1 through 5
        state = NodeState.SUCCESS;
        return state;
    }

    public void UIonDetection()
    {
        // method to make it quicker to set UI elements when the player has been fully spotted

        _detectionBarSprite.DisableDetected();
        _alertedSprite.DisplayAlerted();
        _searchingSprite.DisableSearching();
    }

}
