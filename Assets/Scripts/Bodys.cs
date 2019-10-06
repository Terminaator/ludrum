using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodys : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blood0;
    public GameObject blood1;
    public GameObject blood2;
    public GameObject blood3;

    public Dictionary<Vector3, int> map = new Dictionary<Vector3, int>();
    public void addBody(Vector3 position)
    {
        if (!map.ContainsKey(position))
        {
            map.Add(position, Random.Range(0,3));
        }
    }

    public void spawn()
    {
        foreach (var pair in map)
        {
            int val = pair.Value;
            if(val == 0){
                blood0.transform.position = pair.Key;
                Instantiate(blood0);
            }
            else if(val == 1){
                blood1.transform.position = pair.Key;
                Instantiate(blood1);
            }
            else if(val == 2){
                blood2.transform.position = pair.Key;
                Instantiate(blood2);
            }
            else if(val == 3){
                blood3.transform.position = pair.Key;
                Instantiate(blood3);
            }
        }
    }
}
