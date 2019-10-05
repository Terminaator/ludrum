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

    private float randomizer;
    private float speedRandomizer;

    private bool randomBool;

    private bool isCrashed;
    private float crashTime;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        altitudePID1 = new PIDController();
        altitudePID2 = new PIDController();
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
            rotationSpeed1 = Mathf.Clamp01(altitudePID1.Update(Mathf.Pow(hit1.distance,1.5f)));
        }

        // other side
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, eyeDir2, Mathf.Infinity, layerMask);
        Debug.DrawLine(transform.position, hit2.point, Color.blue, 10);

        if (hit2.collider != null) {
            rotationSpeed2 = Mathf.Clamp01(altitudePID2.Update(Mathf.Pow(hit2.distance,1.5f)));
        }
        Debug.Log(rotationSpeed1 - rotationSpeed2);
        if (rotationSpeed1 > rotationSpeed2) {
            transform.RotateAround(sr.bounds.center, Vector3.forward, rotationSpeedMult*rotationSpeed1*Time.deltaTime);
        } else {
            transform.RotateAround(sr.bounds.center, Vector3.forward, -rotationSpeedMult*rotationSpeed2*Time.deltaTime);
        }

        // speed

        Vector3 eyeDir3 = transform.up;
        
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, eyeDir3, Mathf.Infinity, layerMask);
        //Debug.DrawLine(transform.position + eyeDir3, hit3.point, Color.blue, 10);
        if (hit3.collider != null) {
            //Debug.Log(transform.up * hit3.distance*rotationSpeedMult* Time.deltaTime);

            rb.velocity = transform.up * Mathf.Pow(hit3.distance, 1/3) *speedMult;

            Debug.Log(rb.velocity.magnitude);
            
            //transform.position = transform.position + speed;
        }

        if (hit3.distance < 1f && hit2.distance < 1f && hit1.distance < 1f) {
            isCrashed = true;
            crashTime = 0.7f;
            Debug.Log("crashed");
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
