using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHeadP2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Head")
        {
            if (transform.position.x  <= collision.transform.position.x)
            {
                transform.Translate(new Vector2(-0.3f, 0));
              
            }
            else if (transform.position.x > collision.transform.position.x)
            {
                transform.Translate(new Vector2(0.3f, 0));
               
            }
        
        }
    }
}
