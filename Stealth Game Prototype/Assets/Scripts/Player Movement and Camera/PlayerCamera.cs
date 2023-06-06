using UnityEngine;

// tutorial from: https://www.youtube.com/watch?v=f473C43s8nE&t=379s&ab_channel=Dave%2FGameDevelopment
// This file is NOT written by me

public class PlayerCamera : MonoBehaviour
{
    // This class handles a basic FPS camera  

    // variable for mouse sensitivity
    public float sensX;
    public float sensY;

    // transforms "Orientation" object in "Player" holder
    public Transform orientation;

    // stores rotation amount
    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        // locks cursor within game window and makes it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // converts mouse x and y position into variables
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // rotate x and y by mouse input
        yRotation = yRotation + mouseX;
        xRotation = xRotation - mouseY;
        //clamp xRotation so player cant look to far up or down
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // rotate the player camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        // rotate the player
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
