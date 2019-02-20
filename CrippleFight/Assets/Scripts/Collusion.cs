﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collusion : MonoBehaviour
{

    PlayerControl myPlayerControl, playerControlEnemy;
    Animator AnimatorPlayerEnemy;
    
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players) {

            if (pl.layer == 9 && pl.CompareTag("Player"))
             {
                playerControlEnemy = pl.GetComponent<PlayerControl>();
                AnimatorPlayerEnemy = pl.GetComponent<Animator>();
            }
            if (pl.layer == 8)
            {
                myPlayerControl = pl.GetComponent<PlayerControl>();
            }
        }
    }

    void Update()
    {
        //StartCoroutine(ColliderActive());
    }

    /*public IEnumerator ColliderActive()
{
    yield return new WaitForSeconds(0.3f);

    if (AnimatorPlayer.GetBool("isKicking") == true || AnimatorPlayer.GetBool("isPunching") == true)
        {
            Col.SetActive(true);
        }
        else
        {
            Col.SetActive(false);
        }
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*   if (collision.gameObject.tag == "UpP1") {
               Debug.Log("check1");
               if (!playerControlEnemy.blocklow && !playerControlEnemy.blockhigh) {
                   HealthBarP1.Health -= 10f;
                   SuperBarP1.Super += 20f;
                   playerControlEnemy.hit = true;
               }
               else if (playerControlEnemy.blocklow) {
                   AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
                   playerControlEnemy.hit = true;
               } else if (playerControlEnemy.blockhigh) {
                   AnimatorPlayerEnemy.SetTrigger("isBlocking");
                   playerControlEnemy.hit = true;
               }

               if(playerControlEnemy.hitWallLeft || playerControlEnemy.hitWallRight) {
                   myPlayerControl.hitEnemyWall = true;
                   myPlayerControl.startTimerHitWall = true;
               }
           }*/

        if ((collision.gameObject.tag == "UpP2") && ((myPlayerControl.attackName == "kick") || (myPlayerControl.attackName == "airdive") || (myPlayerControl.attackName == "Ulti"))) {
            Debug.Log("checkUpP2");
            if (!playerControlEnemy.blocklow && !playerControlEnemy.blockhigh) {
                if (myPlayerControl.attackName == "Ulti") {
                    HealthBarP2.Health -= 40f;
                    SuperBarP2.Super += 20f;
                } else {
                    HealthBarP2.Health -= 10f;
                    SuperBarP2.Super += 12.5f;
                }
                playerControlEnemy.hit = true;
                if ((myPlayerControl.attackName == "kick") || (myPlayerControl.attackName == "Ulti")) {
                    myPlayerControl.startTimerHitLag = true;
                }
            } else if (playerControlEnemy.blocklow) {
                if (myPlayerControl.attackName == "airdive") {
                    HealthBarP2.Health -= 10f;
                    SuperBarP2.Super += 12.5f;
                    playerControlEnemy.hit = true;
                } else {
                    AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
                    //playerControlEnemy.hit = true;
                    myPlayerControl.startTimerHitLag = true;
                }
            } else if (playerControlEnemy.blockhigh) {
                AnimatorPlayerEnemy.SetTrigger("isBlocking");
                //playerControlEnemy.hit = true;
                if ((myPlayerControl.attackName == "kick") || (myPlayerControl.attackName == "Ulti")) {
                    myPlayerControl.startTimerHitLag = true;
                }
            }

            if (playerControlEnemy.hitWallLeft || playerControlEnemy.hitWallRight) {
                myPlayerControl.hitEnemyWall = true;
                myPlayerControl.startTimerHitWall = true;
            }
        }




        if (collision.gameObject.tag == "UpEnemy")
        {
            if (myPlayerControl.attackName == "Ulti")
            {
                HealthBarP2.Health -= 40f;
                SuperBarP2.Super += 20f;
            }
            else
            {
                HealthBarP2.Health -= 10f;
                SuperBarP2.Super += 12.5f;
            }
            if ((myPlayerControl.attackName == "kick") || (myPlayerControl.attackName == "Ulti"))
            {
                myPlayerControl.startTimerHitLag = true;
            }
            if (myPlayerControl.attackName == "airdive")
            {
                HealthBarP2.Health -= 10f;
                SuperBarP2.Super += 12.5f;
                playerControlEnemy.hit = true;
            }
            else
            {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
                //playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
            }

        
    
}

      

       /* if (collision.gameObject.tag == "DownP1") {
            if (!playerControlEnemy.blocklow) {
                HealthBarP1.Health -= 5f;
                SuperBarP1.Super += 20f;
                playerControlEnemy.hit = true;
            } 
            else if (playerControlEnemy.blocklow) {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
                playerControlEnemy.hit = true;
            }

            if (playerControlEnemy.hitWallLeft || playerControlEnemy.hitWallRight) {
                myPlayerControl.hitEnemyWall = true;
                myPlayerControl.startTimerHitWall = true;
            }
        }*/

        if ((myPlayerControl.attackName == "downkick")) {
            Debug.Log("checkDownP2");
            if (!playerControlEnemy.blocklow) {
                HealthBarP2.Health -= 10f;
                SuperBarP2.Super += 12.5f;
                playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
            } else if (playerControlEnemy.blocklow) {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
                //playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
            }

            if (playerControlEnemy.hitWallLeft || playerControlEnemy.hitWallRight) {
                myPlayerControl.hitEnemyWall = true;
                myPlayerControl.startTimerHitWall = true;
            }
        }
        if( collision.gameObject.tag == "DownEnemy")
        {
            HealthBarP2.Health -= 10f;
            SuperBarP2.Super += 12.5f;
            playerControlEnemy.hit = true;
            myPlayerControl.startTimerHitLag = true;
        }
    }
}

