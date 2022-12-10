using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 7;
    public Collider2D gravityorobs;
    private Rigidbody2D rb;
    public float jumphight = 15;
    private int jumpnumber = 2;
    private bool isgrounded = false;

    private Animator anim;
    private Vector3 rotation;

    // Gravity stuff
    public GameObject selectedObject; 
    public float gravity = 60;
    private bool gravitypull = false; //already moving
    private bool pulling = false; //not moving just initializing
    public float suckforce = 40;
    Vector3 mousePosition;
    private float maxmultiplier = 3;
    public float forcemultiplier = 0;
    
    // Slow motion stuff
    public TimeManager timeManager;
    public float slowdownFactor = 0.05f;
	public float slowdownLength = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotation = transform.eulerAngles;
    }

    // Force pulling player to orb
    private void gravityOrbfunction(Vector3 mousePosition)
    {
        rb.velocity = Vector3.zero; // vel to 0 so direction is orb
        rb.AddForce((mousePosition - transform.position).normalized * gravity * forcemultiplier, ForceMode2D.Impulse);
        gravitypull = true; // now we are moving
    }

    // Update is called once per frame
    void Update()
    {
        //Walk
        float richtung = Input.GetAxis("Horizontal");
        if(richtung != 0)
        {
            anim.SetBool("IsRunning",true);
        }
        else
        {
            anim.SetBool("IsRunning",false);
        }

        //Animation
        if(richtung < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0,180,0);
            transform.Translate(Vector2.right * speed * -richtung * Time.deltaTime);
        }
        if(richtung > 0)
        {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * speed * richtung * Time.deltaTime);
        }
        if(isgrounded == false)
        {
            anim.SetBool("IsJumping",true);
        }
        else
        {
            anim.SetBool("IsJumping",false);
        }

        anim.SetFloat("yVel",rb.velocity.y);

        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && jumpnumber > 0)
        {
            rb.velocity = new Vector2 (rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumphight, ForceMode2D.Impulse);
            jumpnumber = jumpnumber - 1;
            isgrounded = false;
        }

        //Slow motion Button
        if(Input.GetKeyDown(KeyCode.F))
        {
            timeManager.DoSlowmotion(slowdownFactor, slowdownLength);
        }

        // Mouse0 click event
        if(Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                if (selectedObject.tag == "gravityorb")
                {
                    rb.velocity = Vector3.zero;
                    pulling = true;
                    jumpnumber = 0;
                }
                if (selectedObject.tag == "collectable")
                {
                    selectedObject.GetComponent<Rigidbody2D>().AddForce((transform.position - selectedObject.transform.position).normalized * suckforce, ForceMode2D.Impulse);
                }
            }
        }
        else if(forcemultiplier > 0 && Input.GetMouseButtonUp(0))
        {
            selectedObject = null;
            gravityOrbfunction(mousePosition);
            forcemultiplier = 0;
            pulling = false;
        }

        // while pulled by the orb gravity is off
        if(gravitypull)
        {
            rb.gravityScale = 0;
            rb.drag = 2f; 
        }
        // If velocity to low turn on gravity again
        if(rb.velocity.x + rb.velocity.y < 2)
        {
            rb.gravityScale = 5;
            rb.drag = 0f; 
            gravitypull = false;
        }
    }
    
    void FixedUpdate ()
    {
        // Calculating forcemultiplier here so it is not influenced by hardware
        if(pulling)
        {
            if (forcemultiplier < maxmultiplier)
                {
                    forcemultiplier = forcemultiplier + 0.01f;
                }
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

    // Handeling collision with trigger objects
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "gravityorb")
        {
            gravitypull = false;
            rb.velocity = Vector3.zero;
            rb.drag = 0f; 
            rb.gravityScale = 5;
            rb.AddForce(Vector2.up * jumphight, ForceMode2D.Impulse);
            collider.gameObject.SetActive(false);
            timeManager.DoSlowmotion(slowdownFactor, slowdownLength);
            jumpnumber = 1;
        }    
        if(collider.gameObject.tag == "collectable")
        { 
            collider.gameObject.SetActive(false);
        }      
    }

}


