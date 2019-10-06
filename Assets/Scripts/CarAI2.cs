using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI2 : MonoBehaviour
{
    public float speedMult;
    public float rotationSpeedMult;

    private Rigidbody2D rb;
    private PIDController altitudePID1;
    private PIDController altitudePID2;
    private SpriteRenderer sr;

    private float startIgnorePIDTime;

    private bool isCrashed;
    private float crashTime;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        altitudePID1 = new PIDController();
        altitudePID2 = new PIDController();
        startIgnorePIDTime = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isCrashed) {
            PID();
        } else {
            goBack();
        }
        
    }
    private void PID(){
        int layerMask = 1<<8;
        // turning
        float rotationSpeed1 = 0;
        float rotationSpeed2 = 0;

        Vector3 eyeDir1 = Quaternion.AngleAxis(60, Vector3.forward)*transform.up;
        Vector3 eyeDir2 = Quaternion.AngleAxis(-60, Vector3.forward)*transform.up;
        
        // one side
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, eyeDir1, Mathf.Infinity, layerMask);
        Debug.DrawLine(transform.position, hit1.point, Color.red, 10);

        if (hit1.collider != null) {
            rotationSpeed1 = Mathf.Clamp01(altitudePID1.Update(hit1.distance*5));
        }

        // other side
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, eyeDir2, Mathf.Infinity, layerMask);
        Debug.DrawLine(transform.position, hit2.point, Color.blue, 10);

        if (hit2.collider != null) {
            rotationSpeed2 = Mathf.Clamp01(altitudePID2.Update(hit2.distance*5));
        }

        startIgnorePIDTime -= Time.deltaTime;

        if (rotationSpeed1 >= rotationSpeed2 && startIgnorePIDTime < 0) {
            transform.RotateAround(sr.bounds.center, Vector3.forward, rotationSpeedMult*Time.deltaTime);
        } else if (startIgnorePIDTime < 0){
            transform.RotateAround(sr.bounds.center, Vector3.forward, -rotationSpeedMult*Time.deltaTime);
        }

        // speed

        Vector3 eyeDir3 = transform.up;
        
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, eyeDir3, Mathf.Infinity, layerMask);
        //Debug.DrawLine(transform.position + eyeDir3, hit3.point, Color.blue, 10);
        if (hit3.collider != null) {
            //Debug.Log(transform.up * hit3.distance*rotationSpeedMult* Time.deltaTi    me);
            float speed = rb.velocity.magnitude;
            if (hit3.distance > 1) {
                speed += speedMult * Time.deltaTime;
            } else
            {
                speed -= 3*speedMult * Time.deltaTime;
            }
            
            float maxSpeed = Mathf.Pow(hit3.distance, 1/3)*5;
            if (speed > maxSpeed){
                speed = maxSpeed;
            }
            rb.velocity = transform.up*speed;

            
            //transform.position = transform.position + speed;
        }

        if (hit3.distance < 1f && hit2.distance < 1.3f || hit3.distance < 1f && hit1.distance < 1.3f) {
            isCrashed = true;
            crashTime = 0.5f;
        }
    }

    private void goBack() {
        transform.position = transform.position - transform.up*Time.deltaTime;
        transform.RotateAround(sr.bounds.center, Vector3.forward, -rotationSpeedMult*Time.deltaTime);
        crashTime -= Time.deltaTime;
        
        if (crashTime < 0) {
            isCrashed = false;
        }
    }
}
