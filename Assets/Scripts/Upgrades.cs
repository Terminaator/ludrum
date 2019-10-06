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
        } else if (currentShoeTier == 3) {
            shoes.color = Color.white;
            GetComponent<PlayerRun2>().animatorshoes.SetBool("Running", false);
            GetComponent<PlayerRun2>().animatorLegs.SetBool("Running", false);
        } else if (currentShoeTier == 4) {
            shoes.gameObject.SetActive(false);
            GetComponent<PlayerRun2>().bike.SetActive(true);
            GetComponent<PlayerRun2>().animatorshoes.SetBool("Roll", false);
            GetComponent<PlayerRun2>().animatorhands.SetBool("Running", false);
        } else if (currentShoeTier == 5) {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<PlayerRun2>().bike.SetActive(false);
            GetComponent<PlayerRun2>().animatorLegs.gameObject.SetActive(false);
            GetComponent<PlayerRun2>().animatorhands.gameObject.SetActive(false);

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<PlayerRun2>().buggy.gameObject.SetActive(true);
        }
    }

    public void Spawn(){
        float[] p = points[Random.Range(0,5)];
        if (currentShoeTier == 1)
        {
            shoesPrefab.transform.position= new Vector3(p[0],p[1],0);
            Instantiate(shoesPrefab);
        }
    }
}
