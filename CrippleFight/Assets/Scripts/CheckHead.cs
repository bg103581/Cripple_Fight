using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHead : MonoBehaviour
{

    public GameObject Player1, Player2, CheckPosR, CheckPosL;
    Vector2 Trans;
    Rigidbody2D RB1, RB2;
    public static bool CheckCollusion, CheckCollusionL;
    void Start()
    {
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

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Trans = new Vector2(450 * Time.deltaTime, 0);

        if (collision.gameObject.tag == "HeadP2" || collision.gameObject.tag == "HeadEnemy")
        {

            if (Player1.transform.position.x <= Player2.transform.position.x && Player1.transform.position.x > CheckPosL.transform.position.x)
            {
                //RB2.velocity = (-Trans);
                CheckCollusion = true;
                
                Debug.Log("aaa");
            }
            else if (Player1.transform.position.x > Player2.transform.position.x && Player1.transform.position.x < CheckPosR.transform.position.x)
            {

                //RB2.velocity = (Trans);
                CheckCollusionL = true;


            }
            else if ((Player1.transform.position.x > CheckPosR.transform.position.x))
            {

                //RB2.velocity = (-Trans);
            }
            else if ((Player1.transform.position.x < CheckPosL.transform.position.x))
            {

                //RB2.velocity = (Trans);
            }
            
        }
    }

}
