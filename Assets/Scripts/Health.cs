using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public GameObject text;
    public GameObject bodysPrefab;

    private int lives = 3   ;
    private int[] checks = new int[]{0,0,0,0};

    private float duration = 2f;

    private float timer = 0f;

    private bool wait = false;
    private void Start() {
        GameObject bodys = GameObject.FindGameObjectWithTag("Bodys");
        if (bodys == null)
        {
            Instantiate(bodysPrefab);
        }
        else
        {
            bodys.GetComponent<Bodys>().spawn();
        }
        text.GetComponent<UnityEngine.UI.Text>().text =  "LIVES: "+ lives + "";
    }
    private void Update() {
        if (wait)
        {
            timer += Time.deltaTime;
            if (timer >= duration) {
                wait = false;
                timer = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy" && !wait && timer == 0f)
        {
            if(lives == 1){
                GameObject.FindGameObjectWithTag("Bodys").GetComponent<Bodys>().addBody(this.transform.position);
                DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Bodys"));
                SceneManager.LoadScene(0); 
            }
            lives--;
            wait = true;
            text.GetComponent<UnityEngine.UI.Text>().text = "LIVES: "+ lives + "";
            Blink();
            AudioPlayer.instance.screamAudioGroup.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Vector3 pos = other.transform.position - transform.position;
        if (other.gameObject.tag == "Finish" && pos.x < 0)
        {
            if (checks[3] == 1)
            {
                Debug.Log("ginne");
                checks = new int[]{1,0,0,0};
                if(lives < 3){
                    lives+=1;
                }
                
                GameObject.FindGameObjectWithTag("Player").GetComponent<Upgrades>().Spawn();
                text.GetComponent<UnityEngine.UI.Text>().text =  lives + "";
            }
            else
            {
                checks[0] = 1;
            }
        }
        else if (other.gameObject.tag == "firstCheck" && pos.y < 0 && checks[0] == 1)
        {
            checks[1] = 1;
        }
        else if (other.gameObject.tag == "secondCheck" && pos.y < 0 && checks[1] == 1)
        {
            checks[2] = 1;
        }
        else if(other.gameObject.tag == "finalCheck" && pos.y > 0  && checks[2] == 1)
        {
            checks[3] = 1;
        }
        Debug.Log(string.Join(",", checks));
    }

    private void Blink(){
          StartCoroutine(Blinks());   
    }
    private IEnumerator Blinks()
    {
        Renderer renderer = GetComponent<Renderer>();
        while(wait)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        
        renderer.enabled = true;
    }
}
