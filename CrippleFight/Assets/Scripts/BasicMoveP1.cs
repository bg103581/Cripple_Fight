using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveP1 : MonoBehaviour {

    public int speed; // speed de déplacement de Ryu
    public int jumpForce; // speed de jump
    public float jumpTime; //nb de sec max du jump
    public float jumpTimeCounter; //compteur du temps de jump
    public Rigidbody2D rbRyu; // pour sauter
    private bool isJumping = false;
    public Animator ryuAnimator;


    // Use this for initialization
    void Start() {
        jumpTimeCounter = jumpTime;
        ryuAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey("d") && transform.position.x <= 10.45f && ryuAnimator.GetBool("isCrouching") == false) {
            rbRyu.transform.rotation = new Quaternion(0, 0f, 0f, 0f);
            transform.Translate(Vector2.right * speed * Time.deltaTime); // faire avancer Ryu à droite
            ryuAnimator.SetBool("isWalking", true);

        } else if (Input.GetKey("q") && transform.position.x >= -10.40f && ryuAnimator.GetBool("isCrouching") == false) {
            rbRyu.transform.rotation = new Quaternion(0, 180f, 0f, 0f);
            transform.Translate(-Vector2.left * speed * Time.deltaTime); // faire avancer Ryu à gauche
            ryuAnimator.SetBool("isWalking", true);
        } else {
            ryuAnimator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown("z") && (jumpTimeCounter == jumpTime)) { // faire sauter Ryu
            rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);
            isJumping = true;
            ryuAnimator.SetBool("isJumping", true);
        } else if (Input.GetKey("z") && isJumping) { // augmente le jump quand rester appuyer
            if (jumpTimeCounter > 0) {
                rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                ryuAnimator.SetBool("isJumping", true);
            }
        } else {
            ryuAnimator.SetBool("isJumping", false);
        }

        if (Input.GetKeyUp("z")) { // empeche de reaugmenter le jump après stop jump
            isJumping = false;
        }
        if (Input.GetKey("a")) {
            ryuAnimator.SetBool("isKicking", true);

        } else {
            ryuAnimator.SetBool("isKicking", false);
        }
        if (Input.GetKey("e")) {
            ryuAnimator.SetBool("isPunching", true);
        } else {
            ryuAnimator.SetBool("isPunching", false);
        }
        if (Input.GetKey("s")) {
            ryuAnimator.SetBool("isCrouching", true);

        } else {
            ryuAnimator.SetBool("isCrouching", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ground") {
            isJumping = false;
            jumpTimeCounter = jumpTime;
        }
    }
}