﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveP2 : MonoBehaviour {

    public int speed; // speed de déplacement de Ryu
    public int jumpForce; // speed de jump
    private int rightPress, leftPress; // nb de fois qu'on a appué sur left ou right
    public float jumpTime; //nb de sec max du jump
    public float jumpTimeCounter; //compteur du temps de jump
    private float delay, delayPress, timePassed, timePassedPress, dashTime, dashTimeCounter;
    public Rigidbody2D rbRyu; // pour sauter
    private bool isJumping = false, startTimer, startDelay, grounded, isRight, isLeft, isDashingRight, isDashingLeft;
    public Animator ryuAnimator;
    Vector2 Pos, Max;

    // Use this for initialization
    void Start() {
        jumpTimeCounter = jumpTime;
        ryuAnimator = GetComponent<Animator>();
        rightPress = leftPress = 0;
        timePassed = timePassedPress = dashTimeCounter = 0;
        delayPress = 0.25f;
        delay = 0.25f;
        dashTime = 0.1f;
        isDashingLeft = isDashingRight = isRight = isLeft = false;
        Vector3 Cam = Camera.main.transform.position;
        Max = Camera.main.ScreenToWorldPoint(Cam);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("right")) {
            rightPress++;
            startTimer = true;
        } else if (Input.GetKeyDown("left")) {
            leftPress++;
            startTimer = true;
        }

        if (startTimer) {
            timePassedPress += Time.deltaTime;
            if (timePassedPress >= delayPress) {
                startTimer = false;
                rightPress = 0;
                leftPress = 0;
                timePassedPress = 0;
            }
        }

        if (leftPress >= 2 || rightPress >= 2) {
            startDelay = true;
        }

        if (Input.GetKey("right") && transform.position.x <= 10.45f && ryuAnimator.GetBool("isCrouching") == false && grounded) {
            rbRyu.transform.rotation = new Quaternion(0, 180f, 0f, 0f);
            transform.Translate(-Vector2.right * speed * Time.deltaTime); // faire avancer Ryu à droite
            Pos = transform.position;
            Pos.x = Mathf.Clamp(Pos.x, Max.x - Max.x / 10, -Max.x + Max.x / 10);
            transform.position = Pos;
            ryuAnimator.SetBool("isWalking", true);
            isRight = true;
            isLeft = false;

        } else if (Input.GetKey("left") && transform.position.x >= -10.40f && ryuAnimator.GetBool("isCrouching") == false && grounded) {
            rbRyu.transform.rotation = new Quaternion(0, 0f, 0f, 0f);
            transform.Translate(Vector2.left * speed * Time.deltaTime); // faire avancer Ryu à gauche
            Pos = transform.position;
            Pos.x = Mathf.Clamp(Pos.x, Max.x - Max.x / 10, -Max.x + Max.x / 10);
            transform.position = Pos;
            ryuAnimator.SetBool("isWalking", true);
            isLeft = true;
            isRight = false;
        } else {
            ryuAnimator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown("up") && (jumpTimeCounter == jumpTime)) { // faire sauter Ryu
            rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);
            isJumping = true;
            ryuAnimator.SetBool("isJumping", true);
            grounded = false;
        } else if (Input.GetKey("up") && isJumping) { // augmente le jump quand rester appuyer
            if (jumpTimeCounter > 0) {
                rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                ryuAnimator.SetBool("isJumping", true);
            }
        } else {
            ryuAnimator.SetBool("isJumping", false);
        }

        if (startDelay) {
            timePassed += Time.deltaTime;
            if (timePassed <= delay) {
                if (rightPress >= 2 && leftPress == 0 && grounded) {
                    isDashingRight = true;
                    rightPress = 0;
                } else if (leftPress >= 2 && rightPress == 0 && grounded) {
                    isDashingLeft = true;
                    leftPress = 0;
                }
            } else {
                timePassed = 0;
                startDelay = false;
                rightPress = 0;
                leftPress = 0;
            }
        }

        if (isDashingRight) {
            if (dashTimeCounter <= dashTime) {
                transform.Translate(-Vector2.right * (speed * 3) * Time.deltaTime);
                dashTimeCounter += Time.deltaTime;
            } else {
                dashTimeCounter = 0;
                isDashingRight = false;
            }
        } else if (isDashingLeft) {
            if (dashTimeCounter <= dashTime) {
                transform.Translate(Vector2.left * (speed * 3) * Time.deltaTime);
                dashTimeCounter += Time.deltaTime;
            } else {
                dashTimeCounter = 0;
                isDashingLeft = false;
            }
        }

        if (isRight && !grounded) {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        } else if (isLeft && !grounded) {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if ((!Input.GetKey("right") || !Input.GetKey("left")) && grounded) {
            isLeft = false;
            isRight = false;
        }

        if (Input.GetKeyUp("up")) { // empeche de reaugmenter le jump après stop jump
            isJumping = false;
        }
        if (Input.GetKey("l")) {
            ryuAnimator.SetBool("isKicking", true);

        } else {
            ryuAnimator.SetBool("isKicking", false);
        }
        if (Input.GetKey("m")) {
            ryuAnimator.SetBool("isPunching", true);
        } else {
            ryuAnimator.SetBool("isPunching", false);
        }
        if (Input.GetKey("down")) {
            ryuAnimator.SetBool("isCrouching", true);

        } else {
            ryuAnimator.SetBool("isCrouching", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ground") {
            isJumping = false;
            jumpTimeCounter = jumpTime;
            grounded = true;
        }
    }
}