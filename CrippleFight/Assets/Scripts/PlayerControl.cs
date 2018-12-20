using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public int PlayerNumber ;
    private Transform enemy;

    private Rigidbody2D rig2d;
    private Animator anim;

    public GameObject hadoken;

    private float horizontal, jhorizontal;
    private float vertical, jvertical;
    public float maxSpeed = 10;
    private Vector2 movement, jmovement;
    private bool crouch, walk, isLeft;
    public bool blockhigh, blocklow, hit, knockback;
    private bool block;

    // To stop animation
    public bool stopMoving;

    // Variables needed for jumping
    public float jumpForce = 20;
    public float jumpTime = 0.25f;
    private float jumpTimeCounter;
    private bool onGround;
    private bool jump, isJumping;

    // Variables for player controls
    private bool punch;
    private bool kick;
    private bool shoryuken;
    private bool downKick;
    private bool Super;
    private bool diveAttack;
    private bool isDiving;

    //Variables for dashing
    private bool isDashingLeft, isDashingRight = false;
    private bool hasRightPress, hasLeftPress;
    private bool startTimer, startDelay;
    private int rightPress, leftPress;
    private float delay, delayPress, timePassed, timePassedPress, dashTime, dashTimeCounter;

    // Use this for initialization
    void Start() {
        block = hit = knockback = false;

        rig2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        jumpTimeCounter = jumpTime;

        rightPress = leftPress = 0;
        timePassed = timePassedPress = dashTimeCounter = 0;
        delayPress = 0.25f;
        delay = 0.25f;
        dashTime = 0.1f;
        hasRightPress = hasLeftPress = false;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players) {
            if (pl.transform != this.transform) {
                enemy = pl.transform;
            }
        }

        isDiving = false;
    }

    // Update each frame
    void Update() {
        InputCheck();
        ModifValues();
        Scalecheck();
        OnGroundCheck();
        DashCheck();
        UpdateAnimator();
    }

    // Used instead of Update because we're dealing with physics
    void FixedUpdate() {

        // Stops movement when attacking
        stopMovement();

        // Tap jump and hold button jump
        if (jump) {
            //Debug.Log("JUMP");
            rig2d.velocity = new Vector2(rig2d.velocity.x, jumpForce);
            //isJumping = true;
        } else if ((Input.GetButton("Jump" + PlayerNumber.ToString()) || Input.GetButton("A" + PlayerNumber.ToString())) && isJumping) {  // si on reste appuyé, faire un long saut
            if (jumpTimeCounter > 0) {
                rig2d.velocity = new Vector2(rig2d.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        // Movement is possible if the player is not crouching
        if (!crouch) {
            if (onGround) { //au sol
                if (!walk && !jump) {
                    rig2d.velocity = new Vector2(0, rig2d.velocity.y);
                }
                if (walk) { // Joystick input is prioritised. If there is no joystick input, we check keyboard input
                    //Debug.Log("block :" + block + "; blockhigh :" + blockhigh + "; blocklow :" + blocklow);
                    if (jhorizontal != 0) {
                        rig2d.velocity = new Vector2(jhorizontal * maxSpeed, rig2d.velocity.y);
                        //rig2d.AddForce(jmovement * (maxSpeed - horizontalVelocity.magnitude), ForceMode2D.Impulse); jgarde ça peut être utile
                    } else {
                        rig2d.velocity = new Vector2(horizontal * maxSpeed, rig2d.velocity.y);
                    }
                }
            } else {  //en l'air pour air control
                if (walk) {
                    Debug.Log("player" + PlayerNumber + ": velocity.x = " + rig2d.velocity.x);
                    if (jhorizontal != 0) {
                        if ((rig2d.velocity.x > 0 && jmovement.x < 0) || (rig2d.velocity.x < 0 && jmovement.x > 0) || (Mathf.Abs(rig2d.velocity.x) < maxSpeed)) {
                            rig2d.AddForce(jmovement * maxSpeed * 10);
                        }
                    } else {
                        if ((rig2d.velocity.x > 0 && movement.x < 0) || (rig2d.velocity.x < 0 && movement.x > 0) || (Mathf.Abs(rig2d.velocity.x) < maxSpeed)) {
                            rig2d.AddForce(movement * maxSpeed * 10);
                        }
                    }
                }
            }
        } else {
            //Debug.Log("Player " + PlayerNumber + ": crouch =" + crouch);
            rig2d.velocity = Vector2.zero;
        }

        //Dash
        if (isDashingRight) {
            if (dashTimeCounter < dashTime) {
                rig2d.velocity = new Vector2(maxSpeed * 3, rig2d.velocity.y);
                dashTimeCounter += Time.deltaTime;
            } else {
                dashTimeCounter = 0;
                isDashingRight = false;
            }
        } else if (isDashingLeft) {
            if (dashTimeCounter < dashTime) {
                rig2d.velocity = new Vector2(-maxSpeed * 3, rig2d.velocity.y);
                dashTimeCounter += Time.deltaTime;
            } else {
                dashTimeCounter = 0;
                isDashingLeft = false;
            }
        }

        //knockback after getting hit
        if (knockback) {
            if (isLeft) {
                rig2d.velocity = new Vector2(-maxSpeed * 0.5f, rig2d.velocity.y);
                //rig2d.AddForce(new Vector2(-maxSpeed, 0), ForceMode2D.Impulse);
            } else {
                rig2d.velocity = new Vector2(maxSpeed * 0.5f, rig2d.velocity.y);
                //rig2d.AddForce(new Vector2(maxSpeed * 4, 0), ForceMode2D.Impulse);
            }
        }

        if (hit) {
            //Debug.Log("player" + PlayerNumber + ": hit = " + hit);
            hit = false;
        }

        if (diveAttack) {
            isDiving = true;
            Debug.Log("DIVE KICK !!!");
        }
        
    }

    // Changes the speed to zero. Used for attack animations.
    void stopMovement() {
        float mySpeed = maxSpeed;
        if (stopMoving) {
            maxSpeed = 0;
        } else {
            maxSpeed = 10;
        }
    }
    
    // To update animations
    void UpdateAnimator() {
        anim.SetBool("isCrouching", crouch);
        anim.SetBool("isWalking", walk);
        anim.SetBool("isJumping", jump);
        anim.SetBool("isPunching", punch);
        anim.SetBool("isKicking", kick);
        anim.SetBool("isShoryuken", shoryuken);
        anim.SetBool("isDownKicking", downKick);
        anim.SetBool("isHit", hit);
    }

    // Allows player to fall faster
    void OnGroundCheck() {
        if (!onGround) {
            if (!isDiving) {
                rig2d.gravityScale = 5;
            } else {
                rig2d.gravityScale = 35;
            }
            
        } else {
            rig2d.gravityScale = 1;
        }
    }

    // Makes the players look at each other automatically and check guard
    void Scalecheck() {
        isLeft = transform.position.x < enemy.position.x;

        if (onGround) {
            if (isLeft) {
                transform.localScale = new Vector3(-4, 4, 4);
                if (horizontal < 0 || jhorizontal < 0) {
                    block = true;
                } else {
                    block = false;
                }
            } else {
                transform.localScale = new Vector3(4, 4, 4);
                if (horizontal > 0 || jhorizontal > 0) {
                    block = true;
                } else {
                    block = false;
                }
            }
        }

        blocklow = block && crouch;
        blockhigh = block && !crouch;
    }

    // Assigns keyboard and controller input
    void InputCheck() {
        // Get input from keyboard
        horizontal = Input.GetAxis("Horizontal" + PlayerNumber.ToString());
        vertical = Input.GetAxis("Vertical" + PlayerNumber.ToString());

        // Get input from controller
        jhorizontal = Input.GetAxis("JHorizontal" + PlayerNumber.ToString());
        jvertical = Input.GetAxis("JVertical" + PlayerNumber.ToString());

        // Movement for keyboard input and controller input respectively
        movement = new Vector2(horizontal, 0);
        jmovement = new Vector2(jhorizontal, 0);

        // Booleans to be used for animation
        crouch = ((vertical < 0f) || (jvertical < -0.3f)) && onGround;
        walk = ((horizontal != 0) || (jhorizontal != 0));
        jump = (Input.GetButtonDown("Jump" + PlayerNumber.ToString()) || Input.GetButtonDown("A" + PlayerNumber.ToString())) && onGround && !stopMoving && (jumpTimeCounter == jumpTime) && !isDashingLeft && !isDashingRight && !crouch;
        punch = Input.GetButtonDown("Punch" + PlayerNumber.ToString()) || Input.GetButtonDown("X" + PlayerNumber.ToString());
        kick = walk && punch;
        shoryuken = ((vertical > 0f) && punch) || ((jvertical > 0f) && punch);
        downKick = crouch && punch;
        Super = Input.GetButtonDown("Super" + PlayerNumber.ToString()) || Input.GetButtonDown("Y" + PlayerNumber.ToString());
        diveAttack = (!onGround && punch && !isDiving);


        if (Input.GetButtonUp("Jump" + PlayerNumber.ToString()) || Input.GetButtonUp("A" + PlayerNumber.ToString())) {  // empeche de reaugmenter le jump après stop jump
            isJumping = false;
        }
    }

    //check if dashing
    void DashCheck() {
        if (((horizontal > 0) || (jhorizontal > 0)) && !hasRightPress) {
            rightPress++;
            hasRightPress = true;
            startTimer = true;
        }
        if (((horizontal < 0) || (jhorizontal < 0)) && !hasLeftPress) {
            leftPress++;
            hasLeftPress = true;
            startTimer = true;
        }

        if ((horizontal == 0) && (jhorizontal == 0)) {
            hasRightPress = false;
            hasLeftPress = false;
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

        if (startDelay) {
            timePassed += Time.deltaTime;
            if (timePassed <= delay) {
                if (rightPress >= 2 && leftPress == 0 && onGround) {
                    isDashingRight = true;
                    rightPress = 0;
                } else if (leftPress >= 2 && rightPress == 0 && onGround) {
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
    }

    //to change values in update instead of fixedupdate
    void ModifValues() {
        
        if (jump) {
            Debug.Log("JUMP");
            isJumping = true;
        }

        //launch super after tapping super button
        if (Super) {
            if (PlayerNumber == 1 && SuperBarP1.Super == 100f) {
                /*Instantiate(hadoken, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                hadoken.transform.Translate(new Vector2(this.transform.position.x + Time.deltaTime, this.transform.position.y));*/
                SuperBarP1.Super = 0;
            }
            if (PlayerNumber == 2 && SuperBarP2.Super == 100f) {
                /*Instantiate(hadoken, new Vector3(this.transform.position.x - 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);*/
                SuperBarP2.Super = 0;
            }
        }
    }

    // To know if the player is on the ground or not
    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.tag == "ground") {
            if (PlayerNumber == 1) {
                Debug.Log("ATTERI");
            }
            onGround = true;
            isDiving = false;
            isJumping = false;
            jumpTimeCounter = jumpTime;
        }
        if(col.collider.tag == "Hadoken") {
            if(PlayerNumber == 1) {
                HealthBarP1.Health -= 50f;
            }
            if (PlayerNumber == 2) {
                HealthBarP2.Health -= 50f;
            }
        }
    }

    // To know if the player is jumping
    void OnCollisionExit2D(Collision2D col) {
        if (col.collider.tag == "ground") {
            onGround = false;
        }
    }

    public void EndHitEvent() {
        hit = false;
        Debug.Log("EndHitEvent() player" + PlayerNumber);
    }

    //à utiliser pour debug.log : startcoroutine dans le start()
    IEnumerator debug() {
        while (true) {
            Debug.Log("player" + PlayerNumber + ": velocity.x = " + rig2d.velocity.x);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
