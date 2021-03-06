﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

   
    public AudioSource Inputt;
   
    public static SoundManager soundManager;
    public AudioClip[] Inputs;

    void Awake()
    {

       soundManager = this;
        //Inputt.clip = Inputs[2];
        //Inputt.PlayDelayed(15f);

    }
    void Update()
    {
        if (Input.GetButtonDown("A1")==true || Input.GetButtonDown("A2")==true)
        { Inputt.PlayOneShot(Inputs[1]);  }
    
        if (Input.GetButtonDown("B1") == true || Input.GetButtonDown("B2") == true)
        {

            Inputt.PlayOneShot(Inputs[0]);
        }

        if (!Inputt.isPlaying) {
            Inputt.PlayOneShot(Inputs[2]);
        }
    }

    public void SetVolume(float vol) {
        AudioListener.volume = vol;
    }

}
