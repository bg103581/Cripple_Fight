using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDchangeIA : MonoBehaviour {

    public GameObject player, enemy;
    public GameObject Fedor1, Fedor2, Natalya1, Natalya2, Marcus1, Marcus2;

    // Use this for initialization
    void Start() {

    }

    void Update() {
        if (player == null) {
            updateHUD();
        }
    }

    void updateHUD() {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Ennemy");

        if (player.name == "FedorP1(Clone)" || player.name == "FedorP1skin(Clone)") {
            Fedor1.SetActive(true);
            Natalya1.SetActive(false);
            Marcus1.SetActive(false);
        } else if (player.name == "NataliaP1(Clone)" || player.name == "NataliaP1skin(Clone)") {
            Fedor1.SetActive(false);
            Natalya1.SetActive(true);
            Marcus1.SetActive(false);
        }

        if (enemy.name == "FedorEnemy(Clone)" || enemy.name == "FedorSkinEnemy(Clone)") {
            Fedor2.SetActive(true);
            Natalya2.SetActive(false);
            Marcus2.SetActive(false);
        } else if (enemy.name == "NataliaEnemy(Clone)" || enemy.name == "NataliaSkinEnemy(Clone)") {
            Fedor2.SetActive(false);
            Natalya2.SetActive(true);
            Marcus2.SetActive(false);
        }
    }
}
