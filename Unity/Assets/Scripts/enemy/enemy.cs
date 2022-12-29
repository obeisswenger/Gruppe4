using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // The speed at which the enemy moves
    public float speed = 1f;

    // The distance the enemy should stay away from the edge of the ground or a wall
    public float edgeDistance = 0.5f;

    // The layer mask for the ground and walls
    public LayerMask obstacleLayerMask;

    // The Rigidbody2D component attached to the enemy
    private Rigidbody2D rb;

    public bool atEdge;


    void Start()
    {
        // Get the Rigidbody2D and Collider2D components attached to the enemy
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Check if the enemy is at the edge of the ground or has hit a wall to the right
        atEdge = Physics2D.Raycast(transform.position, Vector2.right, edgeDistance, obstacleLayerMask);

        // Raycast from the top of the enemy to the right
        atEdge = atEdge || Physics2D.Raycast(transform.position + Vector3.up * 1f, Vector2.right, edgeDistance, obstacleLayerMask);

        // Check if the enemy is at the edge of the ground or has hit a wall to the left
        atEdge = atEdge || Physics2D.Raycast(transform.position, Vector2.left, edgeDistance, obstacleLayerMask);

        // Raycast from the top of the enemy to the left
        atEdge = atEdge || Physics2D.Raycast(transform.position + Vector3.up * 1f, Vector2.left, edgeDistance, obstacleLayerMask);


        // If the enemy is at the edge of the ground or has hit a wall, change direction
        if (atEdge)
        {
            transform.eulerAngles = transform.eulerAngles - new Vector3(0,180,0);
        }


        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
