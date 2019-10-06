using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject backGround;
    public GameObject startButton;

    public GameObject exitButton;
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;
    float speed = 0.25f;
    float RotAngleY = 14.17f;
    void Update()
    {
        float rY = Mathf.SmoothStep(0,RotAngleY,Mathf.PingPong(Time.time * speed,1));
        float y = Mathf.SmoothStep(0,RotAngleY/8,Mathf.PingPong(Time.time * speed*2,1));
        float y2 = Mathf.SmoothStep(0, RotAngleY / 8, Mathf.PingPong((Time.time + 1) * speed * 3, 1));
        backGround.transform.rotation = Quaternion.Euler(0,0,rY);
        startButton.transform.rotation = Quaternion.Euler(0,0,y);
        exitButton.transform.rotation = Quaternion.Euler(0,0,-y);
        tutorial1.transform.rotation = Quaternion.Euler(0, 0, y);
        tutorial2.transform.rotation = Quaternion.Euler(0, 0, -y);
        tutorial3.transform.rotation = Quaternion.Euler(0, 0, y2);
        tutorial4.transform.rotation = Quaternion.Euler(0, 0, -y2);
        backGround.transform.localScale = new Vector3(1.5f-rY/100,1.5f-rY/100,1.5f-rY/100);
             
    }
}
