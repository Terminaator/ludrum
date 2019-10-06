using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public Waypoint nextWP;
    public float radius;


    void Start()
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.tag =="Enemy"){
            CarAI ai = other.GetComponent<CarAI>();
            if ( ai != null) {
                other.GetComponent<CarAI>().GoToNext(nextWP);
                }
        }
    }

    public Vector3 getRandomLocNearThis() {
        Vector2 rad = Random.insideUnitCircle*radius;
        return transform.position ;//+ new Vector3(rad.x, rad.y, 0);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawLine(transform.position, nextWP.transform.position);
    }

}
