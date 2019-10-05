using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
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
        randomBool = false;
        if (Random.Range(0,100) > 50){
            randomBool = false;
        }
        randomizer = Random.Range(0,0);
        speedRandomizer = Random.Range(800,1000)/1000f;
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

        Vector3 eyeDir1 = Quaternion.AngleAxis(30, Vector3.forward)*transform.up;
        Vector3 eyeDir2 = Quaternion.AngleAxis(-30, Vector3.forward)*transform.up;
        
        // one side
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, eyeDir1, Mathf.Infinity, layerMask);
        Debug.DrawLine(transform.position, hit1.point, Color.red, 10);

        if (hit1.collider != null) {
            if (randomBool) {
                rotationSpeed1 = Mathf.Clamp01(altitudePID1.Update(hit1.distance + randomizer));
            } else {
                rotationSpeed1 = Mathf.Clamp01(altitudePID1.Update(hit1.distance));
            }
            
        }

        // other side
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, eyeDir2, Mathf.Infinity, layerMask);
        Debug.DrawLine(transform.position, hit2.point, Color.blue, 10);

        if (hit2.collider != null) {
            if (randomBool) {
                rotationSpeed2 = Mathf.Clamp01(altitudePID2.Update(hit2.distance + randomizer));
            } else {
                rotationSpeed2 = Mathf.Clamp01(altitudePID2.Update(hit2.distance));
            }
        }
        Debug.Log(rotationSpeed1 - rotationSpeed2);
        if (rotationSpeed1 - rotationSpeed2 > 0) {
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
            Vector3 speed = transform.up * (Mathf.Sqrt(hit3.distance)/50) *speedMult* Time.deltaTime * speedRandomizer;
            if (speed.magnitude < 0.002) {
                isCrashed = true;
                crashTime = 1f;
                Debug.Log("crashed");
            }
            transform.position = transform.position + speed;
        }
    }

    private void goBack() {
        transform.position = transform.position - transform.up*Time.deltaTime;
        transform.RotateAround(sr.bounds.center, Vector3.forward, -2*Time.deltaTime);
        crashTime -= Time.deltaTime;
        
        if (crashTime < 0) {
            isCrashed = false;
        }
    }
}
