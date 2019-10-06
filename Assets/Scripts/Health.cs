using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public GameObject text;
    public AudioSource scream;

    private int lives = 3;
    private int[] checks = new int[]{0,0,0,0};

    private float duration = 2f;

    private float timer = 0f;

    private bool wait = false;
    private void Start() {
        text.GetComponent<UnityEngine.UI.Text>().text =  lives + "";
    }
    private void Update() {
        if (wait)
        {
            timer += Time.deltaTime;
            if (timer >= duration) {
                wait = false;
                scream.Pause();
                timer = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy" && !wait && timer == 0f)
        {
            if(lives == 1){
                SceneManager.LoadScene(0); 
            }
            lives--;
            wait = true;
            text.GetComponent<UnityEngine.UI.Text>().text = lives + "";
            Blink();
            scream.Play(0);
        }
        Debug.Log("lives: " + lives);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Vector3 pos = other.transform.position - transform.position;
        if (other.gameObject.tag == "Finish" && pos.x < 0)
        {
            if (checks[3] == 1)
            {
                checks = new int[]{0,0,0,0};
                lives = 3;
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
