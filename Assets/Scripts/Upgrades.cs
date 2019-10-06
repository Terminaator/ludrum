using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    public int currentShoeTier = 0;
    public int currentClothesTier = 0;

    public SpriteRenderer shoes;
    public GameObject shoesPrefab;

    private float[][] points = new float[][]{new float[]{-8.01f,0f},new float[]{-7.53f,7.56f},new float[]{7.23f,7.56f},new float[]{7.23f,-3.95f}, new float[]{0.67f,-8.4f}};

    public void UpgradesShoes() {
        currentShoeTier += 1;
        if (currentShoeTier == 1) {
            shoes.gameObject.SetActive(true);
            shoes.color = Color.yellow;
        } else if (currentShoeTier == 2) {
            shoes.color = Color.red;
        }
    }

    public void Spawn(){
        currentShoeTier += 1;
        float[] p = points[Random.Range(0,5)];
        if (currentShoeTier == 1)
        {
            shoesPrefab.transform.position= new Vector3(p[0],p[1],0);
            Instantiate(shoesPrefab);
        }
    }
}
