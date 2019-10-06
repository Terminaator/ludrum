using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    public int currentShoeTier = 0;
    public int currentClothesTier = 0;

    public SpriteRenderer shoes;


    public void UpgradesShoes() {
        currentShoeTier += 1;
        if (currentShoeTier == 1) {
            shoes.gameObject.SetActive(true);
            shoes.color = Color.yellow;
        } else if (currentShoeTier == 2) {
            shoes.color = Color.red;
        }
    }
}
