using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collusion : MonoBehaviour {
   
    
    Animator AnimatorPlayer;
    // Use this for initialization
    void Start () {
        AnimatorPlayer = GetComponent<Animator>();
        
    }

    // Update is called once per frame
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="AttackP1")
        {
            HealthBarP2.Health -= 10f;
        }
        if (collision.gameObject.tag == "AttackP2")
        {
            HealthBarP1.Health -= 10f;
        }

    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UpP1")
        {
            HealthBarP1.Health -= 10f;
          
        }
        if (collision.gameObject.tag == "DownP1")
        {
            HealthBarP1.Health -= 5f;
          
        }
        if (collision.gameObject.tag == "UpP2")
        {
            HealthBarP2.Health -= 10f;
           
        }
        if (collision.gameObject.tag == "DownP2")
        {
            HealthBarP2.Health -= 5f;
           
        }

    }
}

