﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject[] Players;
    public GameObject PosPlayer1, PosPlayer2;
    // Use this for initialization
    void Start () {
        Instantiate(Players[Menu.NumPlayer1], PosPlayer1.transform.position, PosPlayer1.transform.rotation);
        Instantiate(Players[Menu.NumPlayer2], PosPlayer2.transform.position, PosPlayer2.transform.rotation);
        Menu.checkPlayer1 = false;
        Menu.checkPlayer2 = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}