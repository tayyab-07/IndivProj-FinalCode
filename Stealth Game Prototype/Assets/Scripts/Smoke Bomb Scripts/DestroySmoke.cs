using UnityEngine;

// Class to destroy the smoke prefab after a set amount of time

public class DestroySmoke : MonoBehaviour
{
    float destroyTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        destroyTimer += Time.deltaTime;

        // Destroy timer limit specifically set to 12.5 because smoke lasts 11 ish seconds
        // a change in the the limit would cause the game object top be around for too long after the smoke completes
        // I.e the guards would not be able to see through wher the smoke was even though the smoke is no longer there
        // OR if the limit was much lower, the smoke would dissapear unnnaturally and the effect would not look proper
        if (destroyTimer > 12.5f)
        {
            Destroy(gameObject);
        }
    }
}
