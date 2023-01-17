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

    public bool rotateNow;

    // A reference to the box collider 2D component
    public BoxCollider2D boxCollider;

    private int direction = 1;



    void Start()
    {
        // Get the Rigidbody2D and Collider2D components attached to the enemy
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the enemy is at the edge of the ground or has hit a wall to the right
        bool atEdge = Physics2D.Raycast(transform.position, Vector2.right, edgeDistance, obstacleLayerMask);

        // Raycast from the top of the enemy to the right
        atEdge = atEdge || Physics2D.Raycast(transform.position + Vector3.up * 1f, Vector2.right, edgeDistance, obstacleLayerMask);

        // Check if the enemy is at the edge of the ground or has hit a wall to the left
        atEdge = atEdge || Physics2D.Raycast(transform.position, Vector2.left, edgeDistance, obstacleLayerMask);

        // Raycast from the top of the enemy to the left
        atEdge = atEdge || Physics2D.Raycast(transform.position + Vector3.up * 1f, Vector2.left, edgeDistance, obstacleLayerMask);

        // Get the bounds of the box collider
        Bounds bounds = boxCollider.bounds;

        // Calculate the bottom left and right positions of the collider
        Vector2 bottomLeft = bounds.min;
        Vector2 bottomRight = bounds.min;
        bottomRight.x = bounds.max.x;

        // Cast rays from the bottom left and right positions
        RaycastHit2D leftHit = Physics2D.Raycast(bottomLeft, Vector2.down, Mathf.Infinity, obstacleLayerMask);
        RaycastHit2D rightHit = Physics2D.Raycast(bottomRight, Vector2.down, Mathf.Infinity, obstacleLayerMask);

        // Check if the enemy is close to the edge of the platform
        if ((!leftHit && direction == -1) || (!rightHit && direction == 1))
        {
            // The enemy is close to the edge of the platform, so change direction
            direction *= -1;
            transform.eulerAngles = transform.eulerAngles - new Vector3(0,180,0);
        }

        // Move the enemy
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
