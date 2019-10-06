using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject backGround;
    public GameObject startButton;

    public GameObject exitButton;
    float speed = 0.25f;
    float RotAngleY = 14.17f;
    void Update()
    {
        float rY = Mathf.SmoothStep(0,RotAngleY,Mathf.PingPong(Time.time * speed,1));
        float y = Mathf.SmoothStep(0,RotAngleY/8,Mathf.PingPong(Time.time * speed*2,1));
        backGround.transform.rotation = Quaternion.Euler(0,0,rY);
        startButton.transform.rotation = Quaternion.Euler(0,0,y);
        exitButton.transform.rotation = Quaternion.Euler(0,0,-y);
        Debug.Log(rY/100);
        backGround.transform.localScale = new Vector3(1.5f-rY/100,1.5f-rY/100,1.5f-rY/100);
             
    }
}
