using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScript : MonoBehaviour {
    public AudioClip musicClip;
    public AudioSource MusicSource;
	// Use this for initialization
	void Start () {
        MusicSource.clip= musicClip;

    }
	
	// Update is called once per frame
	void Update () {
        //MusicSource.Play();

    }
}
