using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollusionEnnemy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "UpEnemy")// && !playerControlEnemy.blocklow && !playerControlEnemy.blockhigh)
        {
            HealthBarP2.Health -= 10f;
            //SuperBarP1.Super += 20f;
           
        }
        if (collision.gameObject.tag == "DownEnemy") //&& !playerControlEnemy.blocklow && !playerControlEnemy.blockhigh)
        {
            HealthBarP2.Health -= 5f;
            //SuperBarP1.Super += 20f;
            
        }
    }
}
    

