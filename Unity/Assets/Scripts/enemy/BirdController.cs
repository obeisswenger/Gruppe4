using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    // The player game object
    public GameObject player;

    // The distance at which the bird will start following the player
    public float followDistance = 5.0f;

    // The distance above the player at which the bird will fly
    public float flyHeight = 2.0f;

    // The speed at which the bird will follow the player
    public float followSpeed = 1.0f;

    // The interval at which the attack() function will be called
    public float attackInterval = 1.0f;

    // The maximum vertical distance the bird will fly above and below the player
    public float maxVerticalDistance = 4.0f;

    // The speed at which the bird will fly up and down
    public float verticalSpeed = 1.0f;

    // The maximum horizontall distance the bird will fly above and below the player
    public float maxHorizontalDistance = 2.0f;

    // The speed at which the bird will fly horizontally
    public float horizontalSpeed = 1.0f;

    // The delay in seconds before the bird starts moving
    public float movementDelay = 1.0f;

    // Timer for calling the attack() function
    private float attackTimer = 0.0f;

    // Timer for the movement delay
    private float movementTimer = 0.0f;

    // Flag to track whether the bird is moving or not
    private bool isMoving = false;

    // The speed at which the bird will attack the player
    public float attackSpeed = 1f;

    // A reference to the enemy's Rigidbody2D component
    private Rigidbody2D rb;

    void Start()
    {
        // Get a reference to the enemy's Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update the movementTimer
        movementTimer += Time.deltaTime;

        // Check if the movementTimer has reached the movementDelay and the bird is not already moving
        if (movementTimer >= movementDelay && !isMoving)
        {
            // Set the isMoving flag to true
            isMoving = true;
        }

        // If the bird is moving
        if (isMoving)
        {
            // Calculate the distance between the bird and the player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // If the bird is within the follow distance of the player, start following
            if (distance <= followDistance)
            {
                // Calculate the position of the bird above the player
                Vector3 followPosition = new Vector3(player.transform.position.x + Mathf.Sin(Time.time * horizontalSpeed) * maxHorizontalDistance, player.transform.position.y + flyHeight + Mathf.Sin(Time.time * verticalSpeed) * maxVerticalDistance, 0.0f);

                // Smoothly move the bird towards the follow position
                transform.position = Vector3.Lerp(transform.position, followPosition, followSpeed * Time.deltaTime);

                // Update the attackTimer
                attackTimer += Time.deltaTime;

                // If the attackTimer has reached the attackInterval, call the attack() function and reset the timer
                if (attackTimer >= attackInterval)
                {
                    attack();
                    attackTimer = 0.0f;
                }
            }
        }
    }

    // This function will be called every attackInterval seconds while the bird is within the follow distance of the player
    void attack()
    {
        Vector2 direction = player.transform.position - transform.position;
        rb.AddForce(direction * attackSpeed, ForceMode2D.Impulse);
    }
}
