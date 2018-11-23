using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collusion : MonoBehaviour {

    PlayerControl playerControlEnemy;
    Animator AnimatorPlayer;

    void Start () {
        AnimatorPlayer = GetComponent<Animator>();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players) {
            if (pl.transform.position.x != gameObject.GetComponentInParent<Transform>().position.x) {
                //Debug.Log(this.ToString() + " ; " + pl.ToString() + "; " + pl.transform.position);
                playerControlEnemy = pl.GetComponent<PlayerControl>();
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
        Debug.Log("blockhighenemy : " + playerControlEnemy.blockhigh + "; blocklowenemy :" + playerControlEnemy.blocklow);
        if (collision.gameObject.tag == "UpP1" && !playerControlEnemy.blocklow && !playerControlEnemy.blockhigh) {
                HealthBarP1.Health -= 10f;
                SuperBarP1.Super += 20f;
                playerControlEnemy.hit = true;
        }
        if (collision.gameObject.tag == "UpP2" && !playerControlEnemy.blocklow  && !playerControlEnemy.blockhigh) {
                HealthBarP2.Health -= 10f;
                SuperBarP2.Super += 20f;
                playerControlEnemy.hit = true;
        }
        if (collision.gameObject.tag == "DownP1" && !playerControlEnemy.blocklow) {
                HealthBarP1.Health -= 5f;
                SuperBarP1.Super += 20f;
                playerControlEnemy.hit = true;
        }
        if (collision.gameObject.tag == "DownP2" && !playerControlEnemy.blocklow) {
                HealthBarP2.Health -= 5f;
                SuperBarP2.Super += 20f;
                playerControlEnemy.hit = true;
        }
    }
}

