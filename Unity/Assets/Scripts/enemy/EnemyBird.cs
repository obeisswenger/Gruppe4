using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{
// The player game object
    public GameObject player;

    // The speed at which the enemy moves
    public float speed = 10f;

    public float attackSpeed = 7f;
    // The distance at which the enemy will start following the player
    public float followDistance = 20f;

    // The distance at which the enemy will stop following the player
    public float stopFollowDistance = 10f;

    // The distance at which the enemy will attack the player
    public float attackDistance = 10f;

    // The amount of time that the enemy will pause after attacking the player
    public float attackPauseTime = 5f;

    // The amount of damage that the enemy will do to the player
    public int damage = 1;

    // The direction that the enemy is moving
    public Vector2 direction;

    // The time that the enemy started pausing after attacking the player
    private float pauseStartTime;

    // A reference to the enemy's Rigidbody2D component
    private Rigidbody2D rb;

    private float distance;

    private int x = 1;
    void Start()
    {
        // Get a reference to the enemy's Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calculate the distance between the enemy and the player
        distance = Vector2.Distance(transform.position, player.transform.position);

        // If the enemy is within the follow distance
        if (distance < followDistance)
        {
            // If the enemy is within the attack distance
            if (distance < attackDistance)
            {
                // If the enemy is not currently paused
                if (Time.time > pauseStartTime + attackPauseTime)
                {
                    // Attack the player
                    Attack();
                }
            }
            // If the enemy is outside of the attack distance
            else
            {
                // If the enemy is outside of the stop follow distance
                if (distance > stopFollowDistance)
                {
                    // Follow the player
                    Follow();
                }
                // If the enemy is within the stop follow distance
                else
                {
                    Hover();
                }
            }
        }
        // If the enemy is outside of the follow distance
        else
        {
            // Don't follow the player
            direction = Vector2.zero;
            print("stay");
        }

        // Set the velocity of the enemy's Rigidbody2D component based on the direction
        //rb.velocity = direction * speed;
        transform.Translate(direction*speed*Time.deltaTime);
    }

    void Hover()
    {
        print("hover");
        if(distance > 10)
        {
            x = x*(-1);
        }
        direction = new Vector2(x,  player.transform.position.y + 5f - transform.position.y);
        transform.Translate(direction*speed*Time.deltaTime);
    }

    void Follow()
    {
        print("follow");
        // Set the direction to face the player
        direction = (player.transform.position - transform.position).normalized;
    }

    void Attack()
    {
        print("attack");
        // Set the direction to face down towards the player
        direction =  player.transform.position - transform.position;

        // Bird attacks
        rb.AddForce(direction * attackSpeed, ForceMode2D.Impulse);

        // Set the time that the enemy started pausing after attacking the player
        pauseStartTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            // Change direction
            direction = -direction;
        }
    }
}