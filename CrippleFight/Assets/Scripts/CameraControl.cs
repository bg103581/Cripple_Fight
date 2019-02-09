using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject[] players;
    public GameObject player1, player2, wallLeft, wallRight;
    public float minLimitX, maxLimitX, center, centerY, Diff, dist, p1x, p2x, maxDist;
    public BoxCollider2D left, right;
    public Camera Cam;
    public GameObject ColliderCamR, ColliderCamL;
    public float CamL;
    public bool Camc;
    void Start() {
        wallLeft = GameObject.FindGameObjectWithTag("WallLeft");
        wallRight = GameObject.FindGameObjectWithTag("WallRight");
        left = wallLeft.GetComponent<BoxCollider2D>();
        right = wallRight.GetComponent<BoxCollider2D>();
        maxDist = 24.5f;
    }

    void LateUpdate() {
        
        players = GameObject.FindGameObjectsWithTag("Player");
        player1 = players[0];
        player2 = players[1];
        

        p1x = player1.transform.position.x;
        p2x = player2.transform.position.x;

        center = ((player1.transform.position.x + player2.transform.position.x)/2f);
        centerY = (player1.transform.position.y + player2.transform.position.y) / 2f;
        Diff = (player1.transform.position.x - player2.transform.position.x) ;
        
        dist = Mathf.Abs(Vector3.Distance(player1.transform.position, player2.transform.position));



        LimitDistance();
        LookAtCenter();

        
    }
     void Update()
    {

   
       CamL=Cam.transform.position.x- ColliderCamL.transform.position.x;
        ColliderCamL.transform.position = new Vector2(Cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x , transform.position.y);
        ColliderCamR.transform.position = new Vector2(Cam.transform.position.x+CamL, transform.position.y);




    }

    void LookAtCenter() {

        //Debug.Log(Camc);

        if ((Camc==false) ) {
            transform.position = new Vector3(center, centerY, transform.position.z );

            
            if (player1.transform.position.x< player2.transform.position.x)
            {
                Camera.main.orthographicSize = Mathf.Abs((4f - (Diff / 6)));
                
            }
            else 
            {
                Camera.main.orthographicSize = Mathf.Abs((4f + (Diff / 6)));
                
            }
            if (Camera.main.orthographicSize > 5f)
            {
                ColliderCamL.SetActive(true);
                ColliderCamR.SetActive(true);
                Camc = true;

            }

        }

        if (Mathf.Abs(Diff) <= 10)
        {
            ColliderCamL.SetActive(false);
            ColliderCamR.SetActive(false);
            Camc = false;
        }

        //player1.transform.position = new Vector2(Mathf.Clamp(player1.transform.position.x,-10,10),transform.position.y);

        //player1.transform.position= new Vector2 (Mathf.Clamp(player1.transform.position.x, Mathf.Abs(Cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x), Cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x), player1.transform.position.y);



    }

    void LimitDistance () {
        //si un joueur est au bout de bg à gauche, enable le collider gauche
        if ((p1x <= -31.5f) || (p2x <= -31.5f)) {
            left.offset = new Vector2(-48.5f, 0f);
            left.enabled = true;

            // si le distance max est atteint, enabled un collider droite
            if (dist >= maxDist) {
                if (!right.enabled) {
                    right.offset = new Vector2(Mathf.Max(p1x, p2x) + 16.2f, 0f);
                    right.enabled = true;
                }
            } else {
                right.enabled = false;
            }
        }
        
        // si un joueur est au bout de bg à droite, enable collider droite
        else if ((p1x >= 31.5f) || (p2x >= 31.5f)) {
            right.offset = new Vector2(48.5f, 0f);
            right.enabled = true;

            // si distance max , enable collider gauche
            if (dist >= maxDist) {
                if (!left.enabled) {
                    left.offset = new Vector2(Mathf.Min(p1x, p2x) - 16.2f, 0f);
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
                    right.offset = new Vector2(Mathf.Max(p1x, p2x) + 16.2f, 0f);
                    right.enabled = true;
                }

                if (!left.enabled) {
                    left.offset = new Vector2(Mathf.Min(p1x, p2x) - 16.2f, 0f);
                    left.enabled = true;
                }
            } else {
                left.enabled = false;
                right.enabled = false;
            }
        }
        
    }
}
