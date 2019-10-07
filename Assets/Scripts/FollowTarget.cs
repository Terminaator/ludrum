using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        //FollowTarget[] others = FindObjectsOfType<FollowTarget>();
        //if (others.GetLength(0) > 1)
        //{
        //    Destroy(this.gameObject);
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
        //transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
