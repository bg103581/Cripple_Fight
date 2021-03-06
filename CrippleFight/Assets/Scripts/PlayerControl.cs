﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public int PlayerNumber ;
    private Transform enemy;
    private GameObject enemyGameobject;
    private GameObject effectsAnim;
    private GameObject ground;

    private Rigidbody2D rig2d;
    private Animator anim;
    public GameObject Player1, Player2;
    Rigidbody2D RB1, RB2;
    //public GameObject hadoken;

    private float horizontal, jhorizontal, shorizontal;
    private float vertical, jvertical, svertical;
    public float maxSpeed;
    private Vector2 movement, jmovement, smovement;
    public bool crouch, walk, isLeft;
    public bool blockhigh, blocklow, hit, knockback;
    private bool block;
    public bool hitWallLeft, hitWallRight, hitEnemyWall;

    //To manage knocback when hitting an enemy near the wall
    public bool startTimerHitWall;
    private bool countKnocbackTime;
    private float hitWallKnocbackTimeCounter;
    private float hitWallKnocbackTime = 0.3f;

    // To stop animation
    public bool stopMoving;

    // Variables needed for jumping
    public float jumpForce;
    public float jumpTime = 0.25f;
    private float jumpTimeCounter;
    private bool onGround;
    public  bool jump, isJumping;

    private bool punch;
    private bool kick;
    private bool shoryuken;
    private bool downKick;
    private bool Super;
    private bool airDive, isAirDiving;
    public string attackName;

    //Variables for dashing
    private bool isDashingLeft, isDashingRight = false;
    private bool hasRightPress, hasLeftPress;
    private bool startTimer, startDelay;
    private int rightPress, leftPress;
    private float delay, delayPress, timePassed, timePassedPress, dashTime, dashTimeCounter;

    //Variables for hitlag
    public bool startTimerHitLag;
    private bool countTimerHitLag;
    private float timerHitLagCounter;
    private float timerHitLagTime = 0.4f;

    //Variables for no jump after airdive
    public bool startTimerNoJump;
    private bool countTimerNoJump;
    private float timerNoJumpCounter;
    private float timerNoJumpTime = 0.4f;

    //Variables when frozen
    public bool isFrozen;
    private bool hasBlockHigh;
    private bool hasBlockLow;

    //Sounds
    public AudioClip[] sons;

    // Use this for initialization
    void Start() {
        GameObject[] playerss = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in playerss)
        {

            if (p.layer == 8 && p.CompareTag("Player"))
            {
                Player1 = p;
                RB1 = Player1.GetComponent<Rigidbody2D>();
            }
            if (p.layer == 9 && p.CompareTag("Player"))
            {
                Player2 = p;
                RB2 = Player2.GetComponent<Rigidbody2D>();
            }
            else
            {
                Player2 = p;
            }
        }
            rig2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        jumpTimeCounter = jumpTime;

        block = hit = knockback = false;
        rightPress = leftPress = 0;
        timePassed = timePassedPress = dashTimeCounter = 0;
        delayPress = 0.25f;
        delay = 0.25f;
        dashTime = 0.1f;
        hasRightPress = hasLeftPress = false;
        hitWallKnocbackTimeCounter = 0;
        timerHitLagCounter = 0;
        timerNoJumpCounter = 0;

        isAirDiving = false;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players) {
            if (pl.transform != this.transform) {
                enemy = pl.transform;
                enemyGameobject = pl;
            }
        }

        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Ennemy").transform;
            enemyGameobject = GameObject.FindGameObjectWithTag("Ennemy");
        }

        effectsAnim = GameObject.FindGameObjectWithTag("EffectsAnim" + PlayerNumber);
        ground = GameObject.FindGameObjectWithTag("ground");
        //StartCoroutine("debug");
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
        //stopMovement();

        //  get player distance

        if (!stopMoving) {
            // Tap jump and hold button jump
            if (jump) {
                rig2d.velocity = new Vector2(rig2d.velocity.x, jumpForce);
            }
            /*else if ((Input.GetButton("Jump" + PlayerNumber.ToString()) || Input.GetButton("A" + PlayerNumber.ToString()) /*|| Input.GetButton("StickCross" + PlayerNumber.ToString())) && isJumping) {  // si on reste appuyé, faire un long saut
                if (jumpTimeCounter > 0) {
                    rig2d.velocity = new Vector2(rig2d.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
            }*/
            
            // Movement is possible if the player is not crouching
            if (!crouch&& !UIManager.Checkpause) {
                if (onGround) { //au sol
                    if (!walk && !jump && !CheckHead.CheckCollusion && !CheckHeadP2.CheckCollusion && !CheckHead.CheckCollusionL && !CheckHeadP2.CheckCollusionL)
                    {
                        rig2d.velocity = new Vector2(0, rig2d.velocity.y);
                    }
                    else if( CheckHead.CheckCollusion && !CheckHeadP2.CheckCollusion)
                    {
                        Vector2 Trans= new Vector2(60, rig2d.velocity.y);
                        RB2.velocity = Trans;

                        CheckHead.CheckCollusion = false;
                    }
                    else if (!CheckHead.CheckCollusion && CheckHeadP2.CheckCollusion)
                    {
                        Vector2 Trans = new Vector2(60, rig2d.velocity.y);
                        RB1.velocity = Trans;
                        CheckHeadP2.CheckCollusion = false;
                    }
                    else if ( CheckHead.CheckCollusionL && !CheckHeadP2.CheckCollusionL)
                    {
                        Vector2 Trans = new Vector2(60, rig2d.velocity.y);
                        RB2.velocity = -Trans;
                        CheckHead.CheckCollusionL = false;
                    }
                    else if ( CheckHeadP2.CheckCollusionL && !CheckHead.CheckCollusionL)
                    {
                        Vector2 Trans = new Vector2(60, rig2d.velocity.y);
                        RB1.velocity = -Trans;
                        CheckHeadP2.CheckCollusionL = false;
                    }
                    if (walk) { // Joystick input is prioritised. If there is no joystick input, we check keyboard input

                        if (jump) { //direction en x du jump
                            if ((jhorizontal < 0f) || (shorizontal < 0f) || (horizontal < 0f)) {
                                rig2d.velocity = new Vector2(-maxSpeed * 0.70f, rig2d.velocity.y);
                            }
                            else if ((jhorizontal > 0f) || (shorizontal > 0f) || (horizontal > 0f)) {
                                rig2d.velocity = new Vector2(maxSpeed * 0.70f, rig2d.velocity.y);
                            }
                        }
                        
                        else if (shorizontal != 0) { //au sol reculer et avancer (joystick input)
                            if (block) {
                                rig2d.velocity = new Vector2(shorizontal * maxSpeed * 0.75f, rig2d.velocity.y);
                            } else {
                                rig2d.velocity = new Vector2(shorizontal * maxSpeed, rig2d.velocity.y);
                                //rig2d.AddForce(jmovement * (maxSpeed - horizontalVelocity.magnitude), ForceMode2D.Impulse); jgarde ça peut être utile
                            }
                        } 

                        else if (jhorizontal != 0) { //au sol reculer et avancer (joystick input)

                            if (block) {
                                rig2d.velocity = new Vector2(jhorizontal * maxSpeed * 0.75f, rig2d.velocity.y);
                            } else {
                                rig2d.velocity = new Vector2(jhorizontal * maxSpeed, rig2d.velocity.y);
                                //rig2d.AddForce(jmovement * (maxSpeed - horizontalVelocity.magnitude), ForceMode2D.Impulse); jgarde ça peut être utile
                            }
                        } else {
                            if (block) {
                                rig2d.velocity = new Vector2(horizontal * maxSpeed * 0.75f, rig2d.velocity.y);
                            } else {
                                rig2d.velocity = new Vector2(horizontal * maxSpeed, rig2d.velocity.y);
                            }
                        }
                    }
                }/*else {  //en l'air pour air control
                    if (walk) {
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
                }*/
            } else {
                rig2d.velocity = Vector2.zero;
                UIManager.Checkpause = false;
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
        }
        
        //knockback after getting hit or hitting an enemy near the wall
        if (knockback || hitEnemyWall) {
            if (isLeft) {
                rig2d.velocity = new Vector2(-maxSpeed * 0.25f, rig2d.velocity.y);
                //rig2d.AddForce(new Vector2(-maxSpeed, 0), ForceMode2D.Impulse);
            } else {
                rig2d.velocity = new Vector2(maxSpeed * 0.25f, rig2d.velocity.y);
                //rig2d.AddForce(new Vector2(maxSpeed * 4, 0), ForceMode2D.Impulse);
            }
        }

        if (airDive) {
            rig2d.velocity = Vector2.zero;
        }

        if (!onGround) {
            if (isAirDiving) {
                if (isLeft) {
                    rig2d.velocity = new Vector2(maxSpeed * 1.8f, rig2d.velocity.y);
                } else {
                    rig2d.velocity = new Vector2(-maxSpeed * 1.8f, rig2d.velocity.y);
                }
            }
        }

        if (hit) {
            hit = false;
        }
    }

    // Changes the speed to zero. Used for attack animations.
    /*void stopMovement() {
        float mySpeed = maxSpeed;
        if (stopMoving) {
            maxSpeed = 0;
        } else {
            maxSpeed = 10;
        }
    }*/
    

    // To update animations
    void UpdateAnimator() {
        anim.SetBool("isCrouching", crouch);
        anim.SetBool("isWalking", walk);
        anim.SetBool("isJumping", jump);
        //anim.SetBool("isPunching", punch);
        if (onGround) {
            anim.SetBool("isKicking", kick);
            anim.SetBool("isShoryuken", shoryuken);
            anim.SetBool("isDownKicking", downKick);
        }
        anim.SetBool("isHit", hit);
        anim.SetBool("isAirDiving", airDive);
    }

    // Allows player to fall faster
    void OnGroundCheck() {
        if (!onGround) {
            if (isAirDiving) {
                rig2d.gravityScale = 25f;
            } else {
                rig2d.gravityScale = 5f;
            }
        } else {
            isAirDiving = false;
            rig2d.gravityScale = 1;
        }
    }

    // Makes the players look at each other automatically
    void Scalecheck() {
        if (onGround) {
            isLeft = transform.position.x < enemy.position.x;
        }

        if (enemyGameobject.tag == "Player")
        {
            if (onGround && (enemyGameobject.GetComponent<PlayerControl>().onGround))
            {
                if (isLeft)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    if (horizontal < 0 || jhorizontal < 0 || shorizontal < 0)
                    {
                        block = true;
                    }
                    else
                    {
                        block = false;
                    }
                }
                else
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    if (horizontal > 0 || jhorizontal > 0 || shorizontal > 0)
                    {
                        block = true;
                    }
                    else
                    {
                        block = false;
                    }
                }

            }
        }
        else
        {
            if (isLeft)
            {
                if (onGround && (EnemyControler.Ground))
                {

             
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                }
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
        }

        if (!isFrozen) {
            blocklow = block && crouch;
            blockhigh = block && !crouch && onGround;
        }
        else {
            blocklow = hasBlockLow;
            blockhigh = hasBlockHigh;
        }
    }

    // Assigns keyboard and controller input
    void InputCheck() {

        // Get input from keyboard
        horizontal = Input.GetAxis("Horizontal" + PlayerNumber.ToString());
        vertical = Input.GetAxis("Vertical" + PlayerNumber.ToString());

        jhorizontal = Input.GetAxis("JHorizontal" + PlayerNumber.ToString());
        jvertical = Input.GetAxis("JVertical" + PlayerNumber.ToString());

        shorizontal = Input.GetAxis("StickHorizontal" + PlayerNumber.ToString());
        svertical = Input.GetAxis("StickVertical" + PlayerNumber.ToString());
        

        // Movement for keyboard input and controller input respectively
        movement = new Vector2(horizontal, 0);
        jmovement = new Vector2(jhorizontal, 0);
        smovement = new Vector2(shorizontal, 0);

        // Booleans to be used for animation
        crouch = ((vertical < 0f) || (jvertical < -0.3f) || (svertical < -0.3f)) && onGround && !isDashingLeft && !isDashingRight && !countTimerHitLag;
        walk = ((horizontal != 0) || (jhorizontal != 0) || (shorizontal != 0)) && !countTimerHitLag;
        jump = (/*Input.GetButtonDown("Jump" + PlayerNumber.ToString()) || */(vertical > 0f) || (jvertical > 0f) || (svertical > 0f)) && onGround && !stopMoving && (jumpTimeCounter == jumpTime) && !isDashingLeft && !isDashingRight && !crouch && !countTimerHitLag && !countTimerNoJump;
        /*Input.GetButtonDown("A" + PlayerNumber.ToString()) || Input.GetButton("StickCross" + PlayerNumber.ToString())*/
        punch = !isDashingLeft && !isDashingRight && !countTimerHitLag;/*(Input.GetButtonDown("Punch" + PlayerNumber.ToString()) || Input.GetButtonDown("X" + PlayerNumber.ToString()) || Input.GetButtonDown("StickSquare" + PlayerNumber.ToString())) && */
        kick = punch && (Input.GetButtonDown("X" + PlayerNumber.ToString()) || Input.GetButtonDown("Punch" + PlayerNumber.ToString()));
        shoryuken = punch && (Input.GetButtonDown("Y" + PlayerNumber.ToString()) || Input.GetButtonDown("UpAttack" + PlayerNumber.ToString())); /*((vertical > 0f) && punch) || ((jvertical > 0f) && punch)*/
        downKick = punch && (Input.GetButtonDown("A" + PlayerNumber.ToString()) || Input.GetButtonDown("LowAttack" + PlayerNumber.ToString()));/*crouch && punch*/
        Super = (Input.GetButtonDown("Super" + PlayerNumber.ToString()) || Input.GetButtonDown("B" + PlayerNumber.ToString()) /*|| Input.GetButtonDown("StickTriangle" + PlayerNumber.ToString())*/) && !isDashingRight && !isDashingLeft && onGround;
        airDive = (!onGround && !isAirDiving && (kick || shoryuken || downKick));

    
        if (Input.GetButtonUp("Jump" + PlayerNumber.ToString())/*Input.GetButtonUp("A" + PlayerNumber.ToString()) || Input.GetButton("StickCross" + PlayerNumber.ToString())*/) {  // empeche de reaugmenter le jump après stop jump
            isJumping = false;
        }
    }

    //check if dashing
    void DashCheck() {
        if (((horizontal > 0) || (jhorizontal > 0) || (shorizontal > 0)) && !hasRightPress) {
            rightPress++;
            hasRightPress = true;
            startTimer = true;
        }
        if (((horizontal < 0) || (jhorizontal < 0) || (shorizontal < 0)) && !hasLeftPress) {
            leftPress++;
            hasLeftPress = true;
            startTimer = true;
        }

        if ((horizontal == 0) && (jhorizontal == 0) && (shorizontal == 0)) {
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
                    if (isLeft) {
                        anim.SetTrigger("DashRight");
                    }
                    else {
                        anim.SetTrigger("DashLeft");
                    }
                    rightPress = 0;
                } else if (leftPress >= 2 && rightPress == 0 && onGround) {
                    isDashingLeft = true;
                    if (isLeft) {
                        anim.SetTrigger("DashLeft");
                    }
                    else {
                        anim.SetTrigger("DashRight");
                    }
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
            isJumping = true;
        }

        if (airDive) {
            isAirDiving = true;
        }

        //launch super after tapping super button
        if (Super) {
            if (PlayerNumber == 1 && SuperBarP1.Super >= 100f) {
                /*Instantiate(hadoken, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                hadoken.transform.Translate(new Vector2(this.transform.position.x + Time.deltaTime, this.transform.position.y));*/
                SuperBarP1.Super = 0f;
                anim.SetTrigger("Ulti");
            }
            if (PlayerNumber == 2 && SuperBarP2.Super >= 100f) {
                /*Instantiate(hadoken, new Vector3(this.transform.position.x - 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);*/
                SuperBarP2.Super = 0f;
                anim.SetTrigger("Ulti");
            }
        }

        //timer to count knocback when hitting an enemy near the wall
        if (startTimerHitWall) {
            countKnocbackTime = true;
            hitWallKnocbackTimeCounter = 0;
            startTimerHitWall = false;
        }

        if (countKnocbackTime) {
            if (hitWallKnocbackTimeCounter <= hitWallKnocbackTime) {
                hitWallKnocbackTimeCounter += Time.deltaTime;
            }
            else {
                hitWallKnocbackTimeCounter = 0;
                countKnocbackTime = false;
                hitEnemyWall = false;
            }
        }

        if(startTimerHitLag) {
            countTimerHitLag = true;
            timerHitLagCounter = 0;
            startTimerHitLag = false;
        }

        if (countTimerHitLag) {
            if(timerHitLagCounter <= timerHitLagTime) {
                timerHitLagCounter += Time.deltaTime;
            }
            else {
                timerHitLagCounter = 0;
                countTimerHitLag = false;
            }
        }

        if (startTimerNoJump) {
            countTimerNoJump = true;
            timerNoJumpCounter = 0;
            startTimerNoJump = false;
        }

        if (countTimerNoJump) {
            if (timerNoJumpCounter <= timerNoJumpTime) {
                timerNoJumpCounter += Time.deltaTime;
            } else {
                timerNoJumpCounter = 0;
                countTimerNoJump = false;
            }
        }
    }


    // To know if the player is on the ground or not
    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.tag == "ground") {
            onGround = true;
            isAirDiving = false;
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

        if (col.collider.tag == "WallLeft") {
            hitWallLeft = false;
        }

        if (col.collider.tag == "WallRight") {
            hitWallRight = false;
        }
    }

    private void OnCollisionStay2D(Collision2D col) {
        if (col.collider.tag == "WallLeft") {
            hitWallLeft = true;
        }

        if (col.collider.tag == "WallRight") {
            hitWallRight = true;
        }

        if (col.collider.tag == "ground") {
            onGround = true;
            isAirDiving = false;
        }
    }

    public void KickEvent() {
        rig2d.velocity = Vector2.zero;
    }

    public void launchSound(int num) {
        //AudioSource.PlayClipAtPoint(sons[num], gameObject.transform.position);
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>().PlayOneShot(sons[num], 2f);
    }

    public void setAttackName(string name) {
        attackName = name;
    }

    public void FreezeEnemy() {
        enemyGameobject.GetComponent<PlayerControl>().isFrozen = true;
        enemyGameobject.GetComponent<PlayerControl>().hasBlockHigh = enemyGameobject.GetComponent<PlayerControl>().blockhigh;
        enemyGameobject.GetComponent<PlayerControl>().hasBlockLow = enemyGameobject.GetComponent<PlayerControl>().blocklow;
        enemyGameobject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        enemyGameobject.GetComponent<Animator>().enabled = false;
        StartCoroutine(reBouger());
    }

    public void DeFreezeEnemy() {
        enemyGameobject.GetComponent<PlayerControl>().isFrozen = false;
        enemyGameobject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        enemyGameobject.GetComponent<Animator>().enabled = true;
    }

    public void AnimDustLeft() {
        effectsAnim.transform.position = transform.position - new Vector3(0, (transform.position.y - ground.transform.position.y),0);
        if (isLeft) {
            effectsAnim.GetComponent<Animator>().SetTrigger("dashLeft");
        }
        else {
            effectsAnim.GetComponent<Animator>().SetTrigger("dashRight");
        }
    }

    public void AnimDustRight() {
        effectsAnim.transform.position = transform.position - new Vector3(0, (transform.position.y - ground.transform.position.y), 0);
        if (isLeft) {
            effectsAnim.GetComponent<Animator>().SetTrigger("dashRight");
        }
        else {
            effectsAnim.GetComponent<Animator>().SetTrigger("dashLeft");
        }
    }

    public void StartTimerNoJump() {
        startTimerNoJump = true;
    }

    //à utiliser pour debug.log : startcoroutine dans le start()
    IEnumerator debug() {
        while (true) {
            Debug.Log("PlayerControl : player" + PlayerNumber + ": attackName = " + attackName);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator reBouger() {
        yield return new WaitForSeconds(1.6f);
        DeFreezeEnemy();
    }
    
}
