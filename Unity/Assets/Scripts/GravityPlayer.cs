using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GravityPlayer : MonoBehaviour
{
    public GameObject selectedObject; 
    [SerializeField]
    private GameObject selectionAnim;
    public Slider load;
    private Vector3 mousePos;
    public Collider2D gravityorobs;
    private Rigidbody2D rb;
    private Vector2 direction;
    
    public float speed = 7;
    public float jumphight = 15;
    public float gravity = 60;
    private float maxmultiplier = 3;
    public float forcemultiplier = 0;

    private bool pulling = false; 

    public float maxSelectDist = 10f;

    
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
        rb.gravityScale = 0;
        rb.drag = 2f; 
        selectedObject = GameObject.FindGameObjectsWithTag("Empty")[0];
    }

    private void gravityOrbStart()
    {
        rb.velocity = Vector3.zero;
        pulling = true;
        load.gameObject.SetActive(true);
    }

    private void selection()
    {
        if(selectedObject.tag == "gravityorb")
            {
                selectedObject.tag = "not right";
            }
            selectedObject = GetComponent<FindClosestObject>().nearestObjectWithTag("gravityorb");
            if(GetComponent<VisibilityCheck>().IsVisible(selectedObject))
            {
                selectionAnim.SetActive(true);
                selectionAnim.GetComponent<SelectedAnim>().selected = selectedObject;
            }
            else
            {
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("not right");
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.tag = "gravityorb";   
                }
                selectedObject = GetComponent<FindClosestObject>().nearestObjectWithTag("gravityorb");
                if(GetComponent<VisibilityCheck>().IsVisible(selectedObject))
                {
                    selectionAnim.SetActive(true);
                    selectionAnim.GetComponent<SelectedAnim>().selected = selectedObject;
                }
            }
    }

    // Update is called once per frame
    void Update()
    {

        //Slow motion Button
        if(Input.GetKeyDown(KeyCode.F))
        {
            timeManager.DoSlowmotionTimed(slowdownFactor, slowdownLength);
        } 
        // selection
        else if(Input.GetKeyDown(KeyCode.E))
        {
            selection();
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            if(selectedObject.tag == "gravityorb")
            {
                gravityOrbStart(); //Pull to next Orb
            }
            else
            {
                selection();
            }
        }
        if(selectedObject.tag == "gravityorb" && !GetComponent<VisibilityCheck>().IsVisible(selectedObject))
        {
            selectedObject = GameObject.FindGameObjectsWithTag("Empty")[0]; // if object not seeable then not selected anymore
            selectionAnim.GetComponent<SelectedAnim>().selected = selectedObject;
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

        // If velocity to low turn on gravity again
        if(rb.velocity.x + rb.velocity.y < 2)
        {
            rb.gravityScale = 5;
            rb.drag = 0f; 
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


