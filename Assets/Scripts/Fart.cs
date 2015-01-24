using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fart : MonoBehaviour {

	public List<AudioClip> Clips;
	public AudioSource Audio;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RandomPlay()
	{
		int dice = Random.Range(0, Clips.Count);
		AudioClip clip = Clips[dice];
		Audio.PlayOneShot(clip);
	}
}
