using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour {

    [Range(0, 2)]
    public float volume = 1f;
    [Range(0, 2)]
    public float pitch = 1f;
    

    public AudioClip audioClip;

    public static BGMusic instance;

    private AudioSource source;

    //Always keep this gameObject there
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);

        source = this.gameObject.GetComponent<AudioSource>();

        //Debug.Log("Instance: " + instance);
        
        source.clip = audioClip;
        source.loop = true;
        source.volume = volume;
        source.pitch = pitch;

        source.Play();

    }

    private void Start()
    {
        instance = this;


    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        //SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        //SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        


    }
    



}
