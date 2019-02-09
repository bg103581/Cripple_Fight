﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollusionP2 : MonoBehaviour
{

    PlayerControl myPlayerControl, playerControlEnemy;
    Animator AnimatorPlayerEnemy;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players)
        {

            if (pl.layer == 8 && pl.CompareTag("Player"))
            {
                playerControlEnemy = pl.GetComponent<PlayerControl>();
                AnimatorPlayerEnemy = pl.GetComponent<Animator>();
            }

            if (pl.layer == 9)
            {
                myPlayerControl = pl.GetComponent<PlayerControl>();
            }

        }
    }

    void Update()
    {
        
    }

 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UpP1")
        {
            Debug.Log("checkUP1");
            if (!playerControlEnemy.blocklow && !playerControlEnemy.blockhigh)
            {
                HealthBarP1.Health -= 10f;
                SuperBarP1.Super += 20f;
                playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
            }
            else if (playerControlEnemy.blocklow)
            {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
                //playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
            }
            else if (playerControlEnemy.blockhigh)
            {
                AnimatorPlayerEnemy.SetTrigger("isBlocking");
                //playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
            }

            if (playerControlEnemy.hitWallLeft || playerControlEnemy.hitWallRight)
            {
                myPlayerControl.hitEnemyWall = true;
                myPlayerControl.startTimerHitWall = true;
            }
        }


        if (collision.gameObject.tag == "DownP1")
        {
            Debug.Log("checkDownP1");
            if (!playerControlEnemy.blocklow)
            {
                HealthBarP1.Health -= 5f;
                SuperBarP1.Super += 20f;
                playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
            }
            else if (playerControlEnemy.blocklow)
            {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
                //playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
            }

            if (playerControlEnemy.hitWallLeft || playerControlEnemy.hitWallRight)
            {
                myPlayerControl.hitEnemyWall = true;
                myPlayerControl.startTimerHitWall = true;
            }
        }

    }
}
