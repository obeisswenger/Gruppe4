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


    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotation = transform.eulerAngles;
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


