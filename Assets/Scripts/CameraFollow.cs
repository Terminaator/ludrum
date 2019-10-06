using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    public Vector3 offset;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {   
        Vector3 newPos = player.position + offset;
        if (newPos.x > 3.82) 
            newPos.x = 3.82f;
        else if (newPos.x < -3.5)
            newPos.x = -3.5f;

        if (newPos.y > 7.78) 
            newPos.y = 7.78f;
        else if (newPos.y < -7.62)
            newPos.y = -7.62f;
        transform.position = newPos;
    }
}
