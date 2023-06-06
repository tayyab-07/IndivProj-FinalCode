using BehaviorTree;

public class GroupSearch : Node
{ 
    // This class allows the guard to start a coordinated search with the other guards

    private AlertedSprite _alertedSprite;
    private SearchingSprite _searchingSprite;

    public GroupSearch(AlertedSprite alertedSprite, SearchingSprite searchingSprite)
    {
        _alertedSprite = alertedSprite;
        _searchingSprite = searchingSprite;
    }

    public override NodeState Evaluate()
    {
        // very basic class to stop the guard from doing anything if they are searching
        // searching actions are determined in BTGuardGroup
        // in order to do this the guard cant be chasing or patrolling etc...
        // therefore this simple class is used to ensure the guard is not doing anything else while searching

        //disable the alerted UI element and enable the spotted UI element for Searching
        _alertedSprite.DisableAlerted();
        _searchingSprite.DisplaySearching();

        state = NodeState.SUCCESS; 
        return state;
    }
}
