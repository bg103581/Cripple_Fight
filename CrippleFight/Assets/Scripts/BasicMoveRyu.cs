using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveRyu : MonoBehaviour {

    public int speed; // speed de déplacement de Ryu
    public int jumpForce; // speed de jump
    public float jumpTime; //nb de sec max du jump
    public float jumpTimeCounter; //compteur du temps de jump
    public Rigidbody2D rbRyu; // pour sauter
    private bool isJumping = false;
    public Animator ryuAnimator;
    

	// Use this for initialization
	void Start () {
        jumpTimeCounter = jumpTime;
        ryuAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("right") && transform.position.x <= 10.45f) {
            transform.Translate(Vector2.right * speed * Time.deltaTime); // faire avancer Ryu à droite
            ryuAnimator.SetBool("isWalking", true);
            
        }

        else if (Input.GetKey("left") && transform.position.x >= -10.40f) {
            transform.Translate(Vector2.left * speed * Time.deltaTime); // faire avancer Ryu à gauche
           ryuAnimator.SetBool("isWalking", true);
        } else {
            ryuAnimator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown("space") && (jumpTimeCounter == jumpTime)) { // faire sauter Ryu
            rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);
            isJumping = true;
            ryuAnimator.SetBool("isJumping", true);
        }

        else if (Input.GetKey("space") && isJumping) { // augmente le jump quand rester appuyer
            if (jumpTimeCounter > 0) {
                rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                ryuAnimator.SetBool("isJumping", true);
            }
        } else {
            ryuAnimator.SetBool("isJumping", false);
        }

        if (Input.GetKeyUp("space")) { // empeche de reaugmenter le jump après stop jump
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ground") {
            isJumping = false;
            jumpTimeCounter = jumpTime;
        }
    }
}