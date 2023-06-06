using UnityEngine;

public class PlayerZipline : MonoBehaviour
{
    // This class implemnets zipline functionality for the player

    [Header("Currently on Zipline?")]
    public bool zipping = false;

    [Header("Keyboard Control")]
    public KeyCode ziplineKey = KeyCode.E;

    [Header("Objects")]
    public Rigidbody rb;
    public PlayerMovement pm;

    private Vector3 startposition;
    private Vector3 endPosition;

    private Zipline currentZipline;

    private float yOffset = 1f;
    private float Radius = 2f;

    private float ZiplineTime = 10.0f;
    private float totalTime; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If the kley is pressed, ther will be a check of the surrounding area of the player to see if there ziplines there
        if (Input.GetKeyDown(ziplineKey))
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0, yOffset, 0), Radius, Vector3.up);

            for (int i = 0; i < hits.Length; i++)
            {
                // If there is a zipline near the player AND it has a valid landing zone, the player will latch on to the zipline
                if (hits[i].collider.tag == "Zipline")
                {
                    currentZipline = hits[i].collider.gameObject.GetComponent<Zipline>();

                    if (currentZipline.landingZone != null) 
                    { 
                        zipping = true;
                        startposition = transform.position;
                        endPosition = currentZipline.landingZone.zipHookPoint.position;

                        // Disable the player movement script, stop using gravity and turn the rigidbody to kinematic
                        // This is done as it helps prevent juddering when using the zipline
                        pm.enabled = false;
                        rb.useGravity = false;
                        rb.isKinematic = true;
                    }
                }
            }
        }

        if (zipping)
        {
            Zip();
        }
    }

    void Zip()
    {
        // Method uses linear interpolation between the taget point and starting point to send the player down the zipline

        totalTime += Time.deltaTime;
        float percentageCompleted = totalTime / ZiplineTime;
        transform.position = Vector3.Lerp(startposition, endPosition, percentageCompleted);

        if (percentageCompleted >= 0.99f)
        { 
            zipping = false;

            pm.enabled = true;
            rb.useGravity = true;
            rb.isKinematic = false;

            totalTime = 0;
            percentageCompleted = 0;
        }
    }
}
