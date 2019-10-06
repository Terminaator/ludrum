using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button start;
    public Button exit;

    // Start is called before the first frame update
    void Start()
    {
        start.GetComponent<Button>().onClick.AddListener(Game);
        exit.GetComponent<Button>().onClick.AddListener(Exit);

    }

    void Exit()
    {
        Debug.Log("pressed exit");
        /*if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }*/
        Application.Quit();
    }

    void Game()
    {
        Debug.Log("load scene");
        SceneManager.LoadScene(1);
    }
}
