using UnityEngine;
using UnityEngine.UI;

public class SearchingSprite : MonoBehaviour
{
    // class that enables or diables visibility of the Searching UI element.

    public Image image;

    public void DisplaySearching()
    {
        image.enabled = true;
    }

    public void DisableSearching()
    {
        image.enabled = false;
    }
}
