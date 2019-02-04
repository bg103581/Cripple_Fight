using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject[] players;
    public GameObject player1, player2, wallLeft, wallRight;
    public float minLimitX, maxLimitX, center, dist, p1x, p2x;
    public BoxCollider2D left, right;

    void Start() {
        minLimitX = -9.6f;
        maxLimitX = 9.6f;

        wallLeft = GameObject.FindGameObjectWithTag("WallLeft");
        wallRight = GameObject.FindGameObjectWithTag("WallRight");
        left = wallLeft.GetComponent<BoxCollider2D>();
        right = wallRight.GetComponent<BoxCollider2D>();
    }

    void LateUpdate() {
        players = GameObject.FindGameObjectsWithTag("Player");
        player1 = players[0];
        player2 = players[1];

        p1x = player1.transform.position.x;
        p2x = player2.transform.position.x;

        center = (player1.transform.position.x + player2.transform.position.x) / 2f;
        dist = Mathf.Abs(Vector3.Distance(player1.transform.position, player2.transform.position));

        LimitDistance();
        LookAtCenter();

        
    }

    void LookAtCenter() {
        if ((center >= -20f ) && (center <= 20f)) {
            transform.position = new Vector3(center, transform.position.y, transform.position.z);
        }
    }

    void LimitDistance () {
        //si un joueur est au bout de bg à gauche, enable le collider gauche
        if ((p1x <= -31f) || (p2x <= -31f)) {
            left.offset = new Vector2(-48.5f, 0f);
            left.enabled = true;

            // si le distance max est atteint, enabled un collider droite
            if (dist >= 20f) {
                if (!right.enabled) {
                    right.offset = new Vector2(Mathf.Max(p1x, p2x) + 15.5f, 0f);
                    right.enabled = true;
                }
            } else {
                right.enabled = false;
            }
        }
        
        // si un joueur est au bout de bg à droite, enable collider droite
        else if ((p1x >= 31f) || (p2x >= 31f)) {
            right.offset = new Vector2(48.5f, 0f);
            right.enabled = true;

            // si distance max , enable collider gauche
            if (dist >= 20f) {
                if (!left.enabled) {
                    left.offset = new Vector2(Mathf.Min(p1x, p2x) - 15.5f, 0f);
                    left.enabled = true;
                }
            } else {
                left.enabled = false;
            }
        }
        
        // si aucun des joueurs ne sont au bout
        else {
            
            if (dist >= 20f) {
                if (!right.enabled) {
                    right.offset = new Vector2(Mathf.Max(p1x, p2x) + 15.5f, 0f);
                    right.enabled = true;
                }

                if (!left.enabled) {
                    left.offset = new Vector2(Mathf.Min(p1x, p2x) - 15.5f, 0f);
                    left.enabled = true;
                }
            } else {
                left.enabled = false;
                right.enabled = false;
            }
        }
        
    }
}
