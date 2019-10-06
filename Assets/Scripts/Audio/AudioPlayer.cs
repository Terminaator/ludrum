using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    public AudioClipGroup screamAudioGroup;
    public AudioClipGroup stepAudioGroup;

    public static AudioPlayer instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
