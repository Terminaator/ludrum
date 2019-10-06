using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun2 : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private bool isRunning;

    private SpriteRenderer sr;
    public Animator animatorLegs;
    public Animator animatorhands;

    public Animator animatorshoes;

    private Upgrades upgrades;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        upgrades = GetComponent<Upgrades>();
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = false;
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

        animatorLegs.SetBool("Running", mag != 0);
        animatorhands.SetBool("Running", mag != 0);
        if (animatorshoes.gameObject.activeInHierarchy) {
            if (upgrades.currentShoeTier <= 2)
                animatorshoes.SetBool("Running", mag != 0);
            else if (upgrades.currentShoeTier == 3) {
                animatorshoes.SetBool("Roll",  mag != 0);
            } 
        }
    }
}
