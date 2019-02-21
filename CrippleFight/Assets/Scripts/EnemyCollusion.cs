using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollusion : MonoBehaviour {
   public Rigidbody2D RB1,RB2;
   public GameObject Player;
    // Use this for initialization

    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        RB1 = Player.GetComponent<Rigidbody2D>();
        RB2 = GetComponent<Rigidbody2D>();
     
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (CheckHead.CheckCollusion && !CheckHeadP2.CheckCollusion)
        {
            Vector2 Trans = new Vector2(60, RB2.velocity.y);
            RB2.velocity = Trans;

            CheckHead.CheckCollusion = false;
        }
        else if (!CheckHead.CheckCollusion && CheckHeadP2.CheckCollusion)
        {
            Vector2 Trans = new Vector2(60, RB1.velocity.y);
            RB1.velocity = Trans;
            CheckHeadP2.CheckCollusion = false;
        }
        else if (CheckHead.CheckCollusionL && !CheckHeadP2.CheckCollusionL)
        {
            Vector2 Trans = new Vector2(60, RB2.velocity.y);
            RB2.velocity = -Trans;
            CheckHead.CheckCollusionL = false;
        }
        else if (CheckHeadP2.CheckCollusionL && !CheckHead.CheckCollusionL)
        {
            Vector2 Trans = new Vector2(60, RB1.velocity.y);
            RB1.velocity = -Trans;
            CheckHeadP2.CheckCollusionL = false;
        }
    }
}
