using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collusion : MonoBehaviour {
    public GameObject Col;
    
    Animator AnimatorPlayer;
    // Use this for initialization
    void Start () {
        AnimatorPlayer = GetComponent<Animator>();
        
    }

    // Update is called once per frame
     void Update()
    {
        StartCoroutine(ColliderActive());
    }

    public IEnumerator ColliderActive()
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
    }
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
}

