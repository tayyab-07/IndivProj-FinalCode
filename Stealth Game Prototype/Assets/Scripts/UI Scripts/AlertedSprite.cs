using UnityEngine;
using UnityEngine.UI;

public class AlertedSprite : MonoBehaviour
{
    // class that enables or diables visibility of the Alerted UI element.

    public Image image;

    public void DisplayAlerted()
    {
        image.enabled = true;
    }

    public void DisableAlerted() 
    { 
        image.enabled = false;
    }

}
