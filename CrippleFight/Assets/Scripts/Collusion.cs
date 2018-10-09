using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collusion : MonoBehaviour {
    public GameObject Col;
    
    Animator AnimatorPlayer;
    // Use this for initialization
    void Start () {
        AnimatorPlayer = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (AnimatorPlayer.GetBool("isKicking") == true)
        {
            Col.SetActive(true);
        }
        else {
            Col.SetActive( false);
        }
	}
}
