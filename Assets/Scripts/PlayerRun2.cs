using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun2 : MonoBehaviour
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
        float rotation = -1;
        if (Input.GetKey(KeyCode.W))
        {
            moveVec = Vector3.up * speed * Time.deltaTime;
            rotation = 0;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {//If going diagonally, let's halve the speed, because it's being moved twice.
                moveVec = moveVec / 2;
            }
            transform.position = transform.position + moveVec;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVec = -Vector3.up * speed * Time.deltaTime;
            rotation = -180;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {//If going diagonally, let's halve the speed, because it's being moved twice.
                moveVec = moveVec / 2;
            }
            transform.position = transform.position + moveVec;
            transform.eulerAngles = moveVec;
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVec = Vector3.right * speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.W))
            {
                rotation = -45;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rotation = -135;
            }
            else
            {
                rotation = 90;
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {//If going diagonally, let's halve the speed, because it's being moved twice.
                moveVec = moveVec / 2;
            }
            transform.position = transform.position + moveVec;
            transform.eulerAngles = moveVec;
            //transform.RotateAround(sr.bounds.center, Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.W))
            {
                rotation = 45;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rotation = 135;
            }
            else
            {
                rotation = 90;
            }
            moveVec = Vector3.left * speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {//If going diagonally, let's halve the speed, because it's being moved twice.
                moveVec = moveVec / 2;
            }
            transform.position = transform.position + moveVec;
            //transform.RotateAround(sr.bounds.center, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if(rotation != -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }

        animator.SetBool("Running", isRunning);

    }
}
