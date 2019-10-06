using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeUpgrade : MonoBehaviour
{
    public int preReqLevel;

    private bool taken = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !taken){
            other.GetComponent<Upgrades>().UpgradesShoes();
            GetComponent<Animator>().SetTrigger("Open");
            Destroy(this.gameObject, 3);
            taken = true;
        } 
    }
}
