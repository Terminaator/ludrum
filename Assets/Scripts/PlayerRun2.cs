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
        isRunning = false;
        Vector3 moveVec = new Vector3();
        if (Input.GetKey(KeyCode.W)) {
            moveVec.y = 1;
        } else if (Input.GetKey(KeyCode.S)) {
            moveVec.y = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            moveVec.x = 1;
        } else if (Input.GetKey(KeyCode.A)) {
            moveVec.x = -1;
        }
        transform.position += Vector3.Normalize(moveVec)*speed*Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(moveVec.y, moveVec.x)*Mathf.Rad2Deg - 90));
        animator.SetBool("Running", moveVec.magnitude != 0);

    }
}
