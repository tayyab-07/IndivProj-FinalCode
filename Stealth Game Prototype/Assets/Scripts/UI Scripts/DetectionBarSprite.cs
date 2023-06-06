using UnityEngine;
using UnityEngine.UI;

public class DetectionBarSprite : MonoBehaviour
{
    // Class for the guard`s Detection bar
    // class sets the current detection amount as well as enabling and disabling the bar

    public Slider slider;
    public Gradient gradient;

    public Image fill;
    public Image border;
    public Image binoculars;

    public void SetDetection(float detection)
    {
        // Sets the detection level of the bar. 
        // variable 'detection' comes from CheckGuardSpotted class
        slider.normalizedValue = detection;

        // Sets the colour of the bar based on the amount the player is spotted 
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void DisplayDetected()
    { 
        fill.enabled = true;
        border.enabled = true;
        binoculars.enabled = true;
    }

    public void DisableDetected()
    {
        fill.enabled = false;
        border.enabled = false;
        binoculars.enabled = false; 
    }

}
