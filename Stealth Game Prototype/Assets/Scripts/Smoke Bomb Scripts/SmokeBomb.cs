using UnityEngine;

// This class was NOT written by me.
// Tutorial from: https://www.youtube.com/watch?v=BYL6JtUdEY0&ab_channel=Brackeys

public class SmokeBomb : MonoBehaviour
{
    // This class handles the release of the effect, in this implemnation this is the smoke effect afetr the smok bomb cannister is destroyed

    public GameObject explosionEffect;

    public float delay = 1f;

    float countdown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown = countdown - Time.deltaTime;

        if (countdown <= 0 && hasExploded == false)
        { 
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }

}
