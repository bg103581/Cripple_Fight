using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveRyu : MonoBehaviour {

    public int speedX; // speed de déplacement de Ryu
    public int speedY; // speed de jump
    public Rigidbody2D rbRyu; // pour sauter
    private bool isJumping = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("right") && transform.position.x <= 10.45f) {
            transform.Translate(Vector2.right * speedX * Time.deltaTime); // faire avancer Ryu à droite
        }

        if (Input.GetKey("left") && transform.position.x >= -10.40f) {
            transform.Translate(Vector2.left * speedX * Time.deltaTime); // faire avancer Ryu à gauche
        }

        if (Input.GetKeyDown("space") && !isJumping) { // faire sauter Ryu
            rbRyu.velocity = Vector2.up * speedY;
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ground") {
            isJumping = false;
        }
    }
}