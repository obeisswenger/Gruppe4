using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


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
    private void SelectablePull()
    {
        rb.velocity = Vector3.zero; // vel to 0 so direction is orb
        direction = selectedObject.transform.position - transform.position;
        rb.AddForce(direction.normalized * gravity * forcemultiplier, ForceMode2D.Impulse);
        rb.gravityScale = 0;
        rb.drag = 2f; 
        selectedObject = GameObject.FindGameObjectsWithTag("Empty")[0];
    }

    private void SelectableStart()
    {
        rb.velocity = Vector3.zero;
        pulling = true;
        load.gameObject.SetActive(true);
    }

    private void selection()
    {
        if(selectedObject.tag != "Empty")
        {
            selectedObject.layer = 8;
        }
        selectedObject = GetComponent<FindClosestObject>().nearestObjectWithLayer("Selectable");
        if(GetComponent<VisibilityCheck>().IsVisible(selectedObject))
        {
            selectionAnim.SetActive(true);
            selectionAnim.GetComponent<SelectedAnim>().selected = selectedObject;
        }
        else
        {
            GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>().Where(g => g.layer == LayerMask.NameToLayer("NotRight")).ToArray();
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.layer = 7;   
            }
            selectedObject = GetComponent<FindClosestObject>().nearestObjectWithLayer("Selectable");
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
            if(selectedObject.layer == 7)
            {
                SelectableStart(); //Pull to next Orb
            }
            else
            {
                selection();
            }
        }
        if(selectedObject.layer == 7 && !GetComponent<VisibilityCheck>().IsVisible(selectedObject))
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
                if (selectedObject.tag == "Selectable")
                {
                    SelectableStart();
                }
            }
        }
        else if(pulling)
        {
            load.value = forcemultiplier / 3;
            timeManager.DoSlowmotion(slowdownFactor, true);
            if (Input.GetKeyUp(KeyCode.Q))
            {
                SelectablePull();
                forcemultiplier = 0;
                pulling = false;
                load.gameObject.SetActive(false);
                timeManager.DoSlowmotion(1, false);
            }
            if (Input.GetMouseButtonUp(0))
            {
                SelectablePull();
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
        if(rb.velocity.y > 200)
        {
            print(rb.velocity.y);
            //rb.velocity = new Vector2 (rb.velocity.x, 15);
        }
        if(rb.velocity.x > 200)
        {
            print(rb.velocity.x);
            //rb.velocity = new Vector2 (15, rb.velocity.y);
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

    
    /*
    // Handeling collision with trigger objects
    private void OnTriggerEnter2D(Collider2D collider)
    { 
        if(collider.gameObject.tag == "Selectable")
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
    */

}


