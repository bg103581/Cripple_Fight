using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollusionP2 : MonoBehaviour
{

    PlayerControl myPlayerControl, playerControlEnemy;
    Animator AnimatorPlayerEnemy;
    RaycastHit2D circlecast2D;
    int layerMask;
    Vector3 direction;

    Transform[] visualEffectsTransforms;
    GameObject visualEffectsGameobject;
    Animator visualEffects;

    public string name;

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

        visualEffectsTransforms = GameObject.FindGameObjectWithTag("VisualEffects2").GetComponentsInChildren<Transform>();
        foreach (Transform child in visualEffectsTransforms) {
            if (child.tag == ("VisualEffect" + name)) {
                visualEffectsGameobject = child.gameObject;
            }
        }
        visualEffects = visualEffectsGameobject.GetComponent<Animator>();

        layerMask = 1 << 8;
    }

    void Update()
    {
        if (myPlayerControl.isLeft) {
            direction = transform.right;
        }
        else {
            direction = -transform.right;
        }
        circlecast2D = Physics2D.CircleCast(transform.position, 0.75f, direction, 0.5f, layerMask);
        Debug.DrawRay(transform.position, direction, Color.green);
    }

 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "UpP1") && ((myPlayerControl.attackName == "kick") || (myPlayerControl.attackName == "airdive") || (myPlayerControl.attackName == "Ulti")))
        {
            Debug.Log("checkUP1");
            if (circlecast2D.collider != null) {
                Debug.Log(circlecast2D.point);
                visualEffectsGameobject.transform.position = circlecast2D.point;
                visualEffects.enabled = true;
            } else {
                visualEffects.enabled = false;
            }
            if (!playerControlEnemy.blocklow && !playerControlEnemy.blockhigh)
            {
                if (myPlayerControl.attackName == "Ulti") {
                    HealthBarP1.Health -= 40f;
                    SuperBarP1.Super += 20f;
                } else {
                    HealthBarP1.Health -= 10f;
                    SuperBarP1.Super += 12.5f;
                }
                playerControlEnemy.hit = true;
                visualEffects.SetTrigger("hit_effect");
                if ((myPlayerControl.attackName == "kick") || (myPlayerControl.attackName == "Ulti")) {
                    myPlayerControl.startTimerHitLag = true;
                }
                //myPlayerControl.startTimerHitLag = true;
            }
            else if (playerControlEnemy.blocklow)
            {
                if (myPlayerControl.attackName == "airdive") {
                    HealthBarP1.Health -= 10f;
                    SuperBarP1.Super += 12.5f;
                    playerControlEnemy.hit = true;
                    visualEffects.SetTrigger("hit_effect");
                    myPlayerControl.startTimerHitLag = true;
                } else {
                    AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
                    //playerControlEnemy.hit = true;
                    myPlayerControl.startTimerHitLag = true;
                    visualEffects.SetTrigger("block_effect");
                }
            }
            else if (playerControlEnemy.blockhigh)
            {
                AnimatorPlayerEnemy.SetTrigger("isBlocking");
                //playerControlEnemy.hit = true;
                if ((myPlayerControl.attackName == "kick") || (myPlayerControl.attackName == "Ulti")) {
                    myPlayerControl.startTimerHitLag = true;
                }
                //myPlayerControl.startTimerHitLag = true;
                visualEffects.SetTrigger("block_effect");
            }

            if (playerControlEnemy.hitWallLeft || playerControlEnemy.hitWallRight)
            {
                myPlayerControl.hitEnemyWall = true;
                myPlayerControl.startTimerHitWall = true;
            }
        }


        if ((collision.gameObject.tag == "DownP1") && (myPlayerControl.attackName == "downkick"))
        {
            Debug.Log("checkDownP1");
            if (circlecast2D.collider != null) {
                Debug.Log(circlecast2D.point);
                visualEffectsGameobject.transform.position = circlecast2D.point;
                visualEffects.enabled = true;
            } else {
                visualEffects.enabled = false;
            }
            if (!playerControlEnemy.blocklow)
            {
                HealthBarP1.Health -= 10f;
                SuperBarP1.Super += 12.5f;
                playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
                visualEffects.SetTrigger("hit_effect");
            }
            else if (playerControlEnemy.blocklow)
            {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
                //playerControlEnemy.hit = true;
                myPlayerControl.startTimerHitLag = true;
                visualEffects.SetTrigger("block_effect");
            }

            if (playerControlEnemy.hitWallLeft || playerControlEnemy.hitWallRight)
            {
                myPlayerControl.hitEnemyWall = true;
                myPlayerControl.startTimerHitWall = true;
            }
        }

    }
}

