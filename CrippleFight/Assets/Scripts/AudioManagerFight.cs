using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerFight : MonoBehaviour {

    public AudioClip themeLoop;
    public AudioSource me;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!me.isPlaying) {
            me.PlayOneShot(themeLoop);
        }
	}
}
