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
    [SerializeField]
    private GameObject disableScripts;
    public Slider load;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private Vector2 direction;
    
    public float speed = 7;
    public float jumphight = 15;
    public float gravity = 60;
    private float maxmultiplier = 3;
    public float forcemultiplier = 0;
    // The strength of the gravitational pull force applied to the affected objects
    public float pullForce = 10.0f;

    private bool pulling = false; 

    public float maxSelectDist = 10f;

    [SerializeField]
    private Camera cam;

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
    private void GravityPush()
    {
        rb.velocity = Vector3.zero; // vel to 0 so direction is orb
        direction = selectedObject.transform.position - transform.position;
        rb.AddForce(direction.normalized * gravity * forcemultiplier, ForceMode2D.Impulse);
        //rb.gravityScale = 0;
        rb.drag = 2f; 
        selectedObject = GameObject.FindGameObjectsWithTag("Empty")[0];
    }

    private void GravityPushStart()
    {
        rb.velocity = Vector3.zero;
        pulling = true;
        load.gameObject.SetActive(true);
    }

    private void Selection()
    {
        
        if(selectedObject.tag != "Empty")
        {
            selectedObject.layer = 8;
        }
        selectedObject = GetComponent<FindClosestObject>().nearestObjectWithLayer("Selectable");

        // Check if the Selectable Object is actual visible and also
        if(GetComponent<VisibilityCheck>().IsVisible(selectedObject))
        {
            Ray ray = new Ray(transform.position, selectedObject.transform.position - transform.position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Vector2.Distance(transform.position, selectedObject.transform.position));

            // Return true if the ray hit a game object with the "ground" tag
            if (hit.collider != null && hit.collider.tag != "ground")
            {
                selectionAnim.SetActive(true);
                selectionAnim.GetComponent<SelectedAnim>().selected = selectedObject;
            }
            else
            {
                print("3");
                Selection();
            }
        }
        else
        {
            GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>().Where(g => g.layer == LayerMask.NameToLayer("NotRight")).ToArray();
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.layer = 7;   
            }
            Selection();
        }
    }

    void EnableEnemyScript(bool enable)
    {
        MonoBehaviour script = selectedObject.GetComponent<BirdController>();
        if (script != null)
        {
            script.enabled = enable;
        }
        script = selectedObject.GetComponent<enemy>();
        if (script != null)
        {
            script.enabled = enable;
        }
    }

    public void GravityPull()
    {
        // Calculate the vector from the held object to the player
        Vector2 direction = transform.position - selectedObject.transform.position;

        // Normalize the vector and scale it by the force
        direction = direction.normalized * pullForce;
        
        EnableEnemyScript(false);
        // Apply the force to the held object
        selectedObject.GetComponent<Rigidbody2D>().AddForce(direction);
    }


    // Update is called once per frame
    void Update()
    {

        // Pulling button
        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(selectedObject.layer == 7)
            {
                GravityPull(); //Pull the object
                GetComponent<LaserController>().EnableLaser();
                GetComponent<LaserController>().UpdateLaser(selectedObject);
            }
        } 
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            EnableEnemyScript(true);
            GetComponent<LaserController>().DisableLaser();
        }

        // selection
        else if(Input.GetKeyDown(KeyCode.E))
        {
            Selection();
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            if(selectedObject.layer == 7)
            {
                GravityPushStart(); // Push to object
            }
            else
            {
                Selection();
            }
        }
        if(selectedObject.layer == 7 && !GetComponent<VisibilityCheck>().IsVisible(selectedObject))
        {
            selectedObject = GameObject.FindGameObjectsWithTag("Empty")[0]; // if object not seeable then not selected anymore
            selectionAnim.GetComponent<SelectedAnim>().selected = selectedObject;
            GetComponent<LaserController>().DisableLaser();
        }

        // Mouse0 click event
        if(Input.GetMouseButtonDown(0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Collider2D targetObject = Physics2D.OverlapPoint(mousePos);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                if (selectedObject.tag == "Selectable")
                {
                    GravityPushStart();
                }
            }
        }
        else if(pulling)
        {
            load.value = forcemultiplier / 3;
            timeManager.DoSlowmotion(slowdownFactor, true);
            if (Input.GetKeyUp(KeyCode.Q))
            {
                GravityPush();
                forcemultiplier = 0;
                pulling = false;
                load.gameObject.SetActive(false);
                timeManager.DoSlowmotion(1, false);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                GravityPush();
                forcemultiplier = 0;
                pulling = false;
                load.gameObject.SetActive(false);
                timeManager.DoSlowmotion(1, false);
            }
            if (forcemultiplier < maxmultiplier)
            {
                forcemultiplier = forcemultiplier + 0.05f;
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
        }
        if(rb.velocity.x > 200)
        {
            print(rb.velocity.x);
        }
    }
}


