using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHeadP2 : MonoBehaviour
{

    public GameObject Player1, Player2, CheckPosR, CheckPosL;
    Vector2 Trans;
    Rigidbody2D RB1, RB2;
    public bool OnGround;
    public static bool CheckCollusion, CheckCollusionL;
    public static bool Ground;

    void Start()
    {
        OnGround = true;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {

            if (p.layer == 8 && p.CompareTag("Player"))
            {
                Player1 = p;
                RB1 = Player1.GetComponent<Rigidbody2D>();
            }
            if (p.layer == 9 && p.CompareTag("Player"))
            {
                Player2 = p;
                RB2 = Player2.GetComponent<Rigidbody2D>();
            }

            CheckPosR = GameObject.Find("CheckPosR");
            CheckPosL = GameObject.Find("CheckPosL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ground = OnGround;
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Trans = new Vector2(450 * Time.deltaTime, 0);

        if (collision.gameObject.tag == "ground")
        {
            OnGround = true;
            
        }

        else if ((collision.gameObject.tag == "DownP1" || collision.gameObject.tag == "HeadEnemy") && !OnGround && CheckHead.Ground)
        {

            if (Player2.transform.position.x <= Player1.transform.position.x && Player2.transform.position.x > CheckPosL.transform.position.x)
            {
               
                CheckCollusion = true;
                CheckCollusionL = false;
                CheckHead.CheckCollusion = false;
                CheckHead.CheckCollusionL = false;

            }
            else if (Player2.transform.position.x > Player1.transform.position.x && Player2.transform.position.x < CheckPosR.transform.position.x)
            {

                CheckCollusionL = true;
                CheckCollusion = false;

                CheckHead.CheckCollusion = false;
                CheckHead.CheckCollusionL = false;


            }
            else if ((Player2.transform.position.x > CheckPosR.transform.position.x))
            {
                CheckCollusionL = true;
                CheckCollusion = false;
           
                CheckHead.CheckCollusion = false;
                CheckHead.CheckCollusionL = false;

            }
            else if ((Player2.transform.position.x < CheckPosL.transform.position.x))
            {
                CheckCollusion = true;
                CheckCollusionL = false;
                CheckHead.CheckCollusion = false;
                CheckHead.CheckCollusionL = false;


            }
            else if (!OnGround && !CheckHead.Ground)
            {
                CheckCollusion = false;
                CheckCollusionL = false;
                CheckHead.CheckCollusion = false;
                CheckHead.CheckCollusionL = false;

            }

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
       


        if (collision.gameObject.tag == "ground")
        {
            OnGround = false;
        }


    }
}
