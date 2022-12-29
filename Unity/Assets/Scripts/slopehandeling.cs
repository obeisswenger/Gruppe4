using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slopehandeling : MonoBehaviour
{
    // The layer mask for the ground layer
    public LayerMask groundLayer;

    // The player's rigidbody component
    private Rigidbody2D rb;

    void Start()
    {
        // Get the player's rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Raycast down from the center of the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        // If the raycast hits something on the ground layer...
        if (hit.collider != null)
        {
            // Get the angle of the slope
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            // Determine the sign of the slope angle
            float slopeSign = Mathf.Sign(Vector3.Cross(hit.normal, Vector3.down).z);


            // If the slope is too steep...
            if (slopeAngle != 0 && Mathf.Abs(slopeAngle) < 50)
            {
                // Rotate the player to match the slope
                rb.rotation = slopeAngle * slopeSign;
            }
            else
            {
                // Otherwise, rotate the player back to upright
                rb.rotation = 0f;
            }
        }
    }
}