using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveRyu : MonoBehaviour {

    public int speed; // speed de déplacement de Ryu
    public int jumpForce; // speed de jump
    public float jumpTime, jumpTimeCounter;
    public Rigidbody2D rbRyu; // pour sauter
    private bool isJumping = false;
    private bool stoppedJumping = true;

	// Use this for initialization
	void Start () {
        jumpTimeCounter = jumpTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("right") && transform.position.x <= 10.45f) {
            transform.Translate(Vector2.right * speed * Time.deltaTime); // faire avancer Ryu à droite
        }

        if (Input.GetKey("left") && transform.position.x >= -10.40f) {
            transform.Translate(Vector2.left * speed * Time.deltaTime); // faire avancer Ryu à gauche
        }

        if (Input.GetKeyDown("space") && !isJumping) { // faire sauter Ryu
            rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);
            isJumping = true;
            stoppedJumping = false;
        }

        if (Input.GetKey("space") && !stoppedJumping) { // augmente le jump quand rester appuyer
            if (jumpTimeCounter > 0) {
                rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp("space")) { // empeche de reaugmenter le jump après stop jump
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ground") {
            isJumping = false;
            jumpTimeCounter = jumpTime;
            stoppedJumping = true;
        }
    }
}