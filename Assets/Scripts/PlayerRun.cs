using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private bool isRunning;

    private SpriteRenderer sr;
    private Animator animator;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = true;
        Vector3 moveVec;
        if (Input.GetKey(KeyCode.W)){
            moveVec = transform.up * speed * Time.deltaTime;
            transform.position = transform.position + moveVec;
        } else if (Input.GetKey(KeyCode.S)){
            moveVec = -transform.up * speed * Time.deltaTime;
            transform.position = transform.position + moveVec;
        } else {
            isRunning = false;
        }
        
        if (Input.GetKey(KeyCode.D)){
            transform.RotateAround(sr.bounds.center, Vector3.forward, -rotationSpeed*Time.deltaTime);
        } else if (Input.GetKey(KeyCode.A)){
            transform.RotateAround(sr.bounds.center, Vector3.forward, rotationSpeed*Time.deltaTime);
        }

        animator.SetBool("Running", isRunning);
        
    }
}
