using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camfollow : MonoBehaviour
{
    public float speed = 2f;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x,target.position.y,-10);
        transform.position = Vector3.Slerp(transform.position,newPos,speed*Time.deltaTime);
    }
}
