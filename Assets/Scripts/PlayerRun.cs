using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec = new Vector3();
        float deltaSpeed = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W)){
            moveVec.x += (Mathf.Cos(transform.rotation.z)*deltaSpeed);
            moveVec.y += (Mathf.Sin(transform.rotation.z)*deltaSpeed);
        } else if (Input.GetKey(KeyCode.S)){
            moveVec.x += (Mathf.Cos(transform.rotation.z)*-deltaSpeed);
            moveVec.y += (Mathf.Sin(transform.rotation.z)*-deltaSpeed);
        }

        if (Input.GetKey(KeyCode.D)){
            transform.Rotate(new Vector3(0,0,1), rotationSpeed*Time.deltaTime);
        } else if (Input.GetKey(KeyCode.A)){
            transform.Rotate(new Vector3(0,0,1), rotationSpeed*Time.deltaTime);
        }
    }
}
