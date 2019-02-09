using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject[] players;
    public GameObject player1, player2, wallLeft, wallRight;
    public float center, centerY, Diff, dist, p1x, p2x, maxDist;
    public BoxCollider2D left, right;
    public Camera Cam;
    public GameObject ColliderCamR, ColliderCamL;
    public float CamL;
    public bool Camc;

    void Start() {
        wallLeft = GameObject.FindGameObjectWithTag("LeftLimit");
        wallRight = GameObject.FindGameObjectWithTag("RightLimit");
        left = wallLeft.GetComponent<BoxCollider2D>();
        right = wallRight.GetComponent<BoxCollider2D>();
        maxDist = 10f;
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

   
        CamL = Cam.transform.position.x - ColliderCamL.transform.position.x;
        ColliderCamL.transform.position = new Vector2(Cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + 1, transform.position.y);
        ColliderCamR.transform.position = new Vector2(Cam.transform.position.x+CamL, transform.position.y);




    }

    void LookAtCenter() {

        if ((Camc==false) ) {
            if (Mathf.Abs(p1x - p2x) <= 7)
            {
                transform.position = new Vector3(center, transform.position.y, transform.position.z);
            }
            else
            {
                if (player1.transform.position.x < player2.transform.position.x)
                {
                    Camera.main.orthographicSize = Mathf.Abs((3f - (Diff / 10)));

                }
                else
                {
                    Camera.main.orthographicSize = Mathf.Abs((3f + (Diff / 10)));

                }
                transform.position = new Vector3(center, transform.position.y, transform.position.z);
            }
            
            
            
        }
        transform.position = new Vector3(transform.position.x, centerY, transform.position.z);
       
        if (Mathf.Abs(p1x - p2x) > 17)
        {
            Debug.Log("tr");
            ColliderCamL.SetActive(true);
            ColliderCamR.SetActive(true);
            Camc = true;

        }
        if (Mathf.Abs(p1x - p2x)<= 17)
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
                    right.offset = new Vector2(Mathf.Max(p1x, p2x) + 5.5f, 0f);
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
                    left.offset = new Vector2(Mathf.Min(p1x, p2x) - 5.5f, 0f);
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
                    right.offset = new Vector2(Mathf.Max(p1x, p2x) + 5.5f, 0f);
                    right.enabled = true;
                }

                if (!left.enabled) {
                    left.offset = new Vector2(Mathf.Min(p1x, p2x) - 5.5f, 0f);
                    left.enabled = true;
                }
            } else {
                left.enabled = false;
                right.enabled = false;
            }
        }
        
    }
}
