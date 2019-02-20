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
       
        if (collision.gameObject.tag == "UpP1")// && !playerControlEnemy.blocklow && !playerControlEnemy.blockhigh)
        {
            HealthBarP1.Health -= 10f;
            //SuperBarP1.Super += 20f;
           
        }
        if (collision.gameObject.tag == "DownP1") //&& !playerControlEnemy.blocklow && !playerControlEnemy.blockhigh)
        {
            HealthBarP1.Health -= 5f;
            //SuperBarP1.Super += 20f;
            
        }
    }
}
    

