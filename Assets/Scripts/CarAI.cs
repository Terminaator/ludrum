using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{   	
    public float acceleration;

    private Vector3 speedVec;
    public float turnSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Waypoint nextWP;

    private Vector3 movingTowards;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        movingTowards = nextWP.getRandomLocNearThis();
    }

    internal void GoToNext(Waypoint next)
    {
        nextWP = next;
        movingTowards = nextWP.getRandomLocNearThis();
        
    }

    // Update is called once per frame
    void Update()
    {   
        speedVec = rb.velocity; 
        if (2*Vector3.Magnitude(movingTowards - transform.position) > (speedVec.sqrMagnitude-4)/(acceleration)) {
            speedVec += 2*Vector3.Normalize(movingTowards - transform.position)*acceleration*Time.deltaTime;
        }
        else {
            speedVec -= 10*Vector3.Normalize(movingTowards - transform.position)*acceleration*Time.deltaTime;

        }
        speedVec.z = 0;
        rb.velocity = speedVec;
        //transform.position = transform.position + speedVec*Time.deltaTime;
        transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(speedVec.y, speedVec.x)*Mathf.Rad2Deg - 90);
        
    }   

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, movingTowards);
    }
}
