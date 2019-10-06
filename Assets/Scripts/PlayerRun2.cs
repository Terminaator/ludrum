using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun2 : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    public float acceleration;

    public float maxSpeed;
    public float drag;

    private Vector3 currentSpeed;

    private SpriteRenderer sr;
    public Animator animatorLegs;
    public Animator animatorhands;

    public Animator animatorshoes;

    public Animator animatorHead;

    public GameObject bike;

    public GameObject buggy;
    private Upgrades upgrades;

    private bool moving;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animatorHead = GetComponent<Animator>();
        upgrades = GetComponent<Upgrades>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upgrades.currentShoeTier < 4) {
            Run();
        } else {
            Drive();
        }
        
        if(upgrades.currentShoeTier == 0){
            animatorhands.SetBool("Running", moving);
            animatorLegs.SetBool("Running", moving);
        }
        if (animatorshoes.gameObject.activeInHierarchy) {
            if (upgrades.currentShoeTier <= 2){
                animatorhands.SetBool("Running", moving);
                animatorLegs.SetBool("Running", moving);
                animatorshoes.SetBool("Running", moving);
            }else if (upgrades.currentShoeTier == 3) {
                animatorshoes.SetBool("Roll",  moving);
            }
        } else if (bike.gameObject.activeInHierarchy) {
            if (upgrades.currentShoeTier == 4) {
                animatorLegs.SetBool("Bike", moving);
                animatorHead.SetBool("Bike",  moving);
                animatorhands.SetBool("Bike",  moving);
            }
        } 
    }

    private void Run(){
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
        float mag = moveVec.magnitude;
        if (mag != 0) {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(moveVec.y, moveVec.x)*Mathf.Rad2Deg - 90));
        }
        moving = mag > 0; 
    }

    private void Drive() {

        moving = true;
        if (Input.GetKey(KeyCode.W)) {
            currentSpeed += transform.up*Time.deltaTime*acceleration;
            currentSpeed = Vector3.ClampMagnitude(currentSpeed, maxSpeed);
            
        } else if (Input.GetKey(KeyCode.S)) {
            currentSpeed -= transform.up*Time.deltaTime*acceleration;
            currentSpeed = Vector3.ClampMagnitude(currentSpeed, maxSpeed/2);
            
        } else {
            moving = false;
        }
        currentSpeed = currentSpeed*drag;
        transform.position = transform.position + currentSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D)){
            transform.RotateAround(sr.bounds.center, Vector3.forward, -rotationSpeed*Time.deltaTime);
        } else if (Input.GetKey(KeyCode.A)){
            transform.RotateAround(sr.bounds.center, Vector3.forward, rotationSpeed*Time.deltaTime);
        }
    }  
}
