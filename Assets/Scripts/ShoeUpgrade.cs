using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeUpgrade : MonoBehaviour
{
    public int preReqLevel;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            other.GetComponent<Upgrades>().UpgradesShoes();
            Destroy(this.gameObject);
        } 
    }
}
