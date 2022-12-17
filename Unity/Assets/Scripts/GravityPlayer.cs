using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GravityPlayer : MonoBehaviour
{
    public float speed = 7;
    public Collider2D gravityorobs;
    private Rigidbody2D rb;
    public float jumphight = 15;


    public GameObject selectedObject; 
    public float gravity = 60;
    public float gravityE = 20;
    private bool gravitypull = false; //already moving
    private bool pulling = false; //not moving just initializing
    public float suckforce = 40;
    private float maxmultiplier = 3;
    public float forcemultiplier = 0;
    public Slider load;
    private Vector3 mousePos;
    private float maxDist = 7f;
    Vector2 direction;
    
    // Slow motion stuff
    public TimeManager timeManager;
    private float slowdownFactor = 0.05f;
	private float slowdownLength = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Force pulling player to orb
    private void gravityOrbPull()
    {
        rb.velocity = Vector3.zero; // vel to 0 so direction is orb
        direction = selectedObject.transform.position - transform.position;
        rb.AddForce(direction.normalized * gravity * forcemultiplier, ForceMode2D.Impulse);
        gravitypull = true; // now we are moving
        selectedObject = GameObject.FindGameObjectsWithTag("Empty")[0];
    }

    private void gravityOrbStart()
    {
        rb.velocity = Vector3.zero;
        pulling = true;
        load.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        //Slow motion Button
        if(Input.GetKeyDown(KeyCode.F))
        {
            timeManager.DoSlowmotionTimed(slowdownFactor, slowdownLength);
        }
        //Pull to next Orb
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            selectedObject = GetComponent<FindClosestObject>().nearestObjectWithTag("gravityorb");
            if(Math.Abs(direction.x) + Math.Abs(direction.y) < maxDist)
            {
            gravityOrbStart();
            }
        }
        else if(Input.GetKey(KeyCode.E))
        {
            selectedObject = GetComponent<FindClosestObject>().nearestObjectWithTag("gravityorb");
            direction = selectedObject.transform.position - transform.position;
            if(Math.Abs(direction.x) + Math.Abs(direction.y) < maxDist)
            {
                rb.AddForce(direction.normalized * gravityE , ForceMode2D.Force);
                gravityE = gravityE / 2;
            }
        }
        else
        {
            gravityE = 20;
        }
        // Mouse0 click event
        if(Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D targetObject = Physics2D.OverlapPoint(mousePos);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                if (selectedObject.tag == "gravityorb")
                {
                    gravityOrbStart();

                }
                if (selectedObject.tag == "collectable")
                {
                    selectedObject.GetComponent<Rigidbody2D>().AddForce((transform.position - selectedObject.transform.position).normalized * suckforce, ForceMode2D.Impulse);
                }
            }
        }
        else if(pulling)
        {
            load.value = forcemultiplier / 3;
            timeManager.DoSlowmotion(slowdownFactor, true);
            if (Input.GetKeyUp(KeyCode.Q))
            {
                gravityOrbPull();
                forcemultiplier = 0;
                pulling = false;
                load.gameObject.SetActive(false);
                timeManager.DoSlowmotion(1, false);
            }
            if (Input.GetMouseButtonUp(0))
            {
                gravityOrbPull();
                forcemultiplier = 0;
                pulling = false;
                load.gameObject.SetActive(false);
                timeManager.DoSlowmotion(1, false);
            }
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
                forcemultiplier = forcemultiplier + 0.05f;
            }
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
            GetComponent<Player>().jumpnumber = 1;
        }    
        if(collider.gameObject.tag == "collectable")
        { 
            collider.gameObject.SetActive(false);
        }      
    }

}


