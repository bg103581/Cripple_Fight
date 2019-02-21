using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject[] players;
    public GameObject player1, player2, wallLeft, wallRight;
    public float minLimitX, maxLimitX, center, centerY, Diff, dist, p1x, p2x, maxDist, maxLcol, maxRcol;
    public BoxCollider2D left, right;
    public Camera Cam;
    public GameObject ColliderCamR, ColliderCamL;
    public float CamL;
    public bool Camc;

    void Start() {

        maxDist = 22f;
        maxLcol = -16.5f;
        maxRcol = 16.5f;
    }

    void LateUpdate() {
        p1x = player1.transform.position.x;
        p2x = player2.transform.position.x;

        center = ((player1.transform.position.x + player2.transform.position.x) / 2f);
        centerY = ((player1.transform.position.y + player2.transform.position.y) / 2f) + 2.5f;
        Diff = (player1.transform.position.x - player2.transform.position.x);

        dist = Mathf.Abs(Vector3.Distance(player1.transform.position, player2.transform.position));

        LimitDistance();
        LookAtCenter();
    }

    void Update() {


        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1) {
            player1 = players[0];
            player2 = players[1];
        } else {
            player1 = players[0];
            player2 = GameObject.FindGameObjectWithTag("Ennemy");
        }



        CamL = Cam.transform.position.x - ColliderCamL.transform.position.x;
        ColliderCamL.transform.position = new Vector2(Cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, transform.position.y);
        ColliderCamR.transform.position = new Vector2(Cam.transform.position.x + CamL, transform.position.y);

    }

    void LookAtCenter() {

        if ((Camc == false)) {
            if ((p1x <= maxLcol) || (p2x <= maxLcol)) {
                transform.position = new Vector3(-9.65f, transform.position.y + 3, transform.position.z);
            } else if ((p1x >= maxRcol) || (p2x >= maxRcol)) {
                transform.position = new Vector3(8.5f, transform.position.y + 3, transform.position.z);
            } else {
                if (Mathf.Abs(p1x - p2x) > 5) {
                    if (player1.transform.position.x < player2.transform.position.x) {
                        Camera.main.orthographicSize = Mathf.Abs((6.5f - (Diff / 10)));

                    } else {
                        Camera.main.orthographicSize = Mathf.Abs((6.5f + (Diff / 10)));

                    }

                    if (center <= -9.65f) {
                        transform.position = new Vector3(-9.63f, transform.position.y + 3, transform.position.z);
                    } else if (center >= 8.5f) {
                        transform.position = new Vector3(8.3f, transform.position.y + 3, transform.position.z);
                    } else {
                        transform.position = new Vector3(center, transform.position.y + 3, transform.position.z);
                    }
                } else {
                    if (center <= -9.65f) {
                        transform.position = new Vector3(-9.63f, transform.position.y + 3, transform.position.z);
                    } else if (center >= 8.5f) {
                        transform.position = new Vector3(8.3f, transform.position.y + 3, transform.position.z);
                    } else {
                        transform.position = new Vector3(center, transform.position.y + 3, transform.position.z);
                    }
                }
            }
        }

        transform.position = new Vector3(transform.position.x, centerY, transform.position.z);

        if (Mathf.Abs(p1x - p2x) > 17) {
            ColliderCamL.SetActive(true);
            ColliderCamR.SetActive(true);
            Camc = true;

        }

        if (Mathf.Abs(p1x - p2x) <= 17) {

            ColliderCamL.SetActive(false);
            ColliderCamR.SetActive(false);
            Camc = false;
        }

    }

    void LimitDistance() {
        //si un joueur est au bout de bg à gauche, enable le collider gauche
        if ((p1x <= maxLcol) || (p2x <= maxLcol)) {
            left.offset = new Vector2(-23f, 0f);
            left.enabled = true;

            // si le distance max est atteint, enabled un collider droite
            if (dist >= maxDist) {
                if (!right.enabled) {
                    right.offset = new Vector2(Mathf.Max(p1x, p2x) + 6.2f, 0f);
                    right.enabled = true;
                }
            } else {
                right.enabled = false;
            }
        }

        // si un joueur est au bout de bg à droite, enable collider droite
        else if ((p1x >= maxRcol) || (p2x >= maxRcol)) {
            right.offset = new Vector2(23f, 0f);
            right.enabled = true;

            // si distance max , enable collider gauche
            if (dist >= maxDist) {
                if (!left.enabled) {
                    left.offset = new Vector2(Mathf.Min(p1x, p2x) - 6.2f, 0f);
                    left.enabled = true;
                }
            } else {
                left.enabled = false;
            }
        }

        // si aucun des joueurs ne sont au bout
        else {

            if (dist >= maxDist) {
                if (!right.enabled) {
                    right.offset = new Vector2(Mathf.Max(p1x, p2x) + 6.2f, 0f);
                    right.enabled = true;
                }

                if (!left.enabled) {
                    left.offset = new Vector2(Mathf.Min(p1x, p2x) - 6.2f, 0f);
                    left.enabled = true;
                }
            } else {
                left.enabled = false;
                right.enabled = false;
            }
        }

    }
}
