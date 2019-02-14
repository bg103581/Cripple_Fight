using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDchangeP1 : MonoBehaviour {
    public GameObject[] players;
    public GameObject Fedor1, Fedor2,  Natalya1, Natalya2, Marcus1, Marcus2;

	// Use this for initialization
	void Start () {
        
	}

    void Update() {
        if (players.Length == 0) {
            updateHUD();
        }
    }

    void updateHUD () {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players) {
            if (pl.layer == 8) {
                if (pl.name == "FedorP1(Clone)") {
                    Fedor1.SetActive(true);
                    Natalya1.SetActive(false);
                    Marcus1.SetActive(false);
                } else if (pl.name == "NataliaP1(Clone)") {
                    Fedor1.SetActive(false);
                    Natalya1.SetActive(true);
                    Marcus1.SetActive(false);
                } else {
                    Fedor1.SetActive(false);
                    Natalya1.SetActive(false);
                    Marcus1.SetActive(true);
                }
            } else {
                if (pl.name == "FedorP2(Clone)") {
                    Fedor2.SetActive(true);
                    Natalya2.SetActive(false);
                    Marcus2.SetActive(false);
                } else if (pl.name == "NataliaP2(Clone)") {
                    Fedor2.SetActive(false);
                    Natalya2.SetActive(true);
                    Marcus2.SetActive(false);
                } else if (pl.name == "MarcusP2(Clone)") {
                    Fedor2.SetActive(false);
                    Natalya2.SetActive(false);
                    Marcus2.SetActive(true);
                }
            }
        }
    }
}
