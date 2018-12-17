using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collusion : MonoBehaviour {

    PlayerControl playerControlEnemy;
    Animator AnimatorPlayerEnemy;

    void Start () {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players) {
            if (pl.transform.position.x != gameObject.GetComponentInParent<Transform>().position.x) {
                //Debug.Log(this.ToString() + " ; " + pl.ToString() + "; " + pl.transform.position);
                playerControlEnemy = pl.GetComponent<PlayerControl>();
                AnimatorPlayerEnemy = pl.GetComponent<Animator>();
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
        //Debug.Log("blockhighenemy : " + playerControlEnemy.blockhigh + "; blocklowenemy :" + playerControlEnemy.blocklow);
        //Debug.Log("hasblockhigh : " + playerControlEnemy.hasBlockhigh + "; hasblocklow :" + playerControlEnemy.hasBlocklow);
        if (collision.gameObject.tag == "UpP1") {
            if (!playerControlEnemy.blocklow && !playerControlEnemy.blockhigh) {
                HealthBarP1.Health -= 10f;
                SuperBarP1.Super += 20f;
                playerControlEnemy.hit = true;
            }
            else if (playerControlEnemy.blocklow) {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
            } else if (playerControlEnemy.blockhigh) {
                AnimatorPlayerEnemy.SetTrigger("isBlocking");
            }
        }
        
        if (collision.gameObject.tag == "UpP2") {
            if (!playerControlEnemy.blocklow && !playerControlEnemy.blockhigh) {
                HealthBarP2.Health -= 10f;
                SuperBarP2.Super += 20f;
                playerControlEnemy.hit = true;
            } 
            else if (playerControlEnemy.blocklow) {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
            } else if (playerControlEnemy.blockhigh) {
                AnimatorPlayerEnemy.SetTrigger("isBlocking");
            }
        }

        if (collision.gameObject.tag == "DownP1") {
            if (!playerControlEnemy.blocklow) {
                HealthBarP1.Health -= 5f;
                SuperBarP1.Super += 20f;
                playerControlEnemy.hit = true;
            } 
            else if (playerControlEnemy.blocklow) {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
            }
        }

        if (collision.gameObject.tag == "DownP2") {
            if (!playerControlEnemy.blocklow) {
                HealthBarP2.Health -= 5f;
                SuperBarP2.Super += 20f;
                playerControlEnemy.hit = true;
            } 
            else if (playerControlEnemy.blocklow) {
                AnimatorPlayerEnemy.SetTrigger("isCrouchBlocking");
            }
        }
    }
}

