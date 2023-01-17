using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 7;
    private Rigidbody2D rb;
    public float jumphight = 15;
    public int jumpnumber = 2;
    private bool isgrounded = false;

    private Animator anim;
    private Vector3 rotation;

    // The layer mask for the ground and walls
    public LayerMask obstacleLayerMask;

    // The player's sprite renderer component
    private SpriteRenderer spriteRenderer;
    
    private float richtung;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //animation
        richtung = Input.GetAxis("Horizontal");
        if(richtung != 0)
        {
            anim.SetBool("IsRunning",true);
        }
        else
        {
            anim.SetBool("IsRunning",false);
        }
        if(isgrounded == false)
        {
            anim.SetBool("IsJumping",true);
        }
        else
        {
            anim.SetBool("IsJumping",false);
        }

        anim.SetFloat("yVal",rb.velocity.y);

        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && jumpnumber > 0)
        {
            rb.rotation = 0f;
            rb.velocity = new Vector2 (rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumphight, ForceMode2D.Impulse);
            jumpnumber = jumpnumber - 1;
            isgrounded = false;
        }

                //walking
        if(richtung < 0  && !Physics2D.Raycast(transform.position, Vector2.left, 0.4f, obstacleLayerMask))
        {
            spriteRenderer.flipX = true;
            transform.Translate(Vector2.left * speed * -richtung * Time.deltaTime);
        }
        else if(richtung > 0 && !Physics2D.Raycast(transform.position, Vector2.right, 0.4f, obstacleLayerMask))
        {
            spriteRenderer.flipX = false;
            transform.Translate(Vector2.right * speed * richtung * Time.deltaTime);
        }

    }

    // Handeling collision with non-trigger objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isgrounded = true;
            jumpnumber = 2;
        }
    }
}


