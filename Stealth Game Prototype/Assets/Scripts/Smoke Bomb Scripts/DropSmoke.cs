using UnityEngine;
using TMPro;

// This file was largely NOT written by me
// I only wrote the parts related to the cooldown, and limiting the smoke bombs uses

// To make it clear, the parts i wrote will have //[ME] at the end of the line

// Main Tutorial from: https://www.youtube.com/watch?v=BYL6JtUdEY0&ab_channel=Brackeys
// Tutorial fro TextMeshPro/TMP from: https://www.youtube.com/watch?v=Xw506Rfd9Q4&ab_channel=GameDevTrauminEnglish

// Class to throw a smoke bomb

public class DropSmoke : MonoBehaviour
{
    [Header("Variables")]
    public float throwForce;
    public float throwCooldown; //[ME]
    public int bombCount; //[ME]
    bool readyToThrow = true; //[ME]
    Vector3 offset = new Vector3();

    [Header("Objects")]
    public GameObject grenadePrefab;
    public Transform Orienatation;
    public TMP_Text bombCountText;

    [Header("Grenade Key")]
    public KeyCode grenadekey = KeyCode.G;


    void Start()
    {
        // Set current smoke bomb count in HUD
        bombCountText.text = bombCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // only throw the grenade if ready to throw and the player has grenades to throw. Once thrown only reset the throw after 'throwCooldown' time
        if (Input.GetKey(grenadekey) && readyToThrow == true && bombCount > 0) 
        {
            readyToThrow = false; //[ME]

            ThrowGrenade();

            bombCount = bombCount - 1; //[ME]

            // Set new smoke bomb count in HUD
            bombCountText.text = bombCount.ToString();

            Invoke(nameof(ResetThrow), throwCooldown); //[ME]
        }
    }

    // throw grenade in direction of player orientation object. Found under Player in hierachy
    void ThrowGrenade()
    { 
        offset = (Orienatation.forward * 1.5f);
        GameObject grenade = Instantiate(grenadePrefab, transform.position + offset, Orienatation.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(Orienatation.forward * throwForce, ForceMode.VelocityChange);
    }

    void ResetThrow()
    {
        readyToThrow = true; //[ME]
    }
}
