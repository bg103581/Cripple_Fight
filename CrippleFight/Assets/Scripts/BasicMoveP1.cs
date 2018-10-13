using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveP1 : MonoBehaviour {

    public int speed; // speed de déplacement de Ryu
    public int jumpForce; // speed de jump
    private int rightPress, leftPress; // nb de fois qu'on a appuyé sur left ou right
    public float jumpTime; //nb de sec max du jump
    public float jumpTimeCounter; // compteur du temps de jump
    private float delay, delayPress, timePassed, timePassedPress, dashTime, dashTimeCounter;
    public Rigidbody2D rbRyu; // pour sauter
    private bool isJumping = false, startTimer, startDelay, grounded, isRight, isLeft, isDashingRight, isDashingLeft;
    private bool startNeutralDelay, startDashDelay;
    private float neutralDelay, dashDelay;
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


        /*

        // si on appuie à droite, on commence le delai pour revenir en neutre
        if (Input.GetAxis("JHorizontal1") > 0.1f) {
            startNeutralDelay = true;
        }
        
        // on commence le delai pour le dash si au bout de 0.5seconds de neutral delai on réappuie à droite
        if (startNeutralDelay) {
            neutralDelay += Time.deltaTime;  // compteur de delai pour revenir en neutre
            if (neutralDelay <= 0.15f) {
                if (Input.GetAxis("JHorizontal1") == 0) {
                    startDashDelay = true;
                }
            } else {
                neutralDelay = 0;
                startNeutralDelay = false;
            }
        }

        // on dash si au bout de 0.5s de dash delay on a appuyé sur droite
        if (startDashDelay) {
            dashDelay += Time.deltaTime;  // compteur de delai pour dasher
            if (dashDelay <= 0.15f) {
                if (Input.GetAxis("JHorizontal1") > 0.1f)  {
                    isDashingRight = true;
                } else {
                    isDashingRight = false;
                }
            } else {
                dashDelay = 0;
                startDashDelay = false;
                startNeutralDelay = false;
            }
        }

    */
    

        //////////////////////////////////////////////
        /// CODE FOR KEYBOARD AND JOYSTICK CONTROL ///
        //////////////////////////////////////////////
        

        /*** CODE POUR LE DASH ***/

        if (Input.GetKeyDown("d")) {
            rightPress++;
            startTimer = true;
        } else if (Input.GetKeyDown("q")) {
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
                transform.Translate(Vector2.right * (speed * 3) * Time.deltaTime);
                dashTimeCounter += Time.deltaTime;
            } else {
                dashTimeCounter = 0;
                isDashingRight = false;
            }
        } else if (isDashingLeft) {
            if (dashTimeCounter <= dashTime) {
                transform.Translate(-Vector2.left * (speed * 3) * Time.deltaTime);
                dashTimeCounter += Time.deltaTime;
            } else {
                dashTimeCounter = 0;
                isDashingLeft = false;
            }
        }

        if (isRight && !grounded) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        } else if (isLeft && !grounded) {
            transform.Translate(-Vector2.left * speed * Time.deltaTime);
        }

        if ((!Input.GetKey("q") || !Input.GetKey("d")) && grounded) {
            isLeft = false;
            isRight = false;
        }


        /*** CODE POUR MARCHER ***/

        // Le joueur avance à droite en appuyant sur 'd' ou en bougant le joystick vers la droite,
        // Le joueur ne peut avancer ssi il n'est pas en train de se baisser et s'il est au sol

        if ((Input.GetKey("d") || (Input.GetAxis("JHorizontal1") > 0)) && ryuAnimator.GetBool("isCrouching") == false && grounded) {
            rbRyu.transform.rotation = new Quaternion(0, 0f, 0f, 0f);    // regarder à droite
            transform.Translate(Vector2.right * speed * Time.deltaTime); // faire avancer Ryu à droite
            Pos = transform.position;
            Pos.x = Mathf.Clamp(Pos.x, Max.x - Max.x / 10, -Max.x + Max.x / 10);
            transform.position = Pos;
            ryuAnimator.SetBool("isWalking", true);
            isRight = true;
            isLeft = false;

        }

        // Le joueur 1 avance à droite en appuyant sur 'd' ou en bougant le joystick vers la droite,
        // Le joueur 1 ne peut avancer ssi il n'est pas en train de se baisser et s'il est au sol

        else if ((Input.GetKey("q") || (Input.GetAxis("JHorizontal1") < 0)) && ryuAnimator.GetBool("isCrouching") == false && grounded) {
        rbRyu.transform.rotation = new Quaternion(0, 180f, 0f, 0f);
            transform.Translate(-Vector2.left * speed * Time.deltaTime); // faire avancer Ryu à gauche
            Pos = transform.position;
            Pos.x = Mathf.Clamp(Pos.x, Max.x - Max.x / 10, -Max.x + Max.x / 10);
            transform.position = Pos;
            ryuAnimator.SetBool("isWalking", true);
            isLeft = true;
            isRight = false;
        } else {
            ryuAnimator.SetBool("isWalking", false);
        }


        /*** CODE POUR SAUTER ***/


        // Le joueur 1 saute en appuyant sur 'z' ou en appuyant sur le button 'A' de la manette
        if ((Input.GetKeyDown("z") || Input.GetButtonDown("A1")) && (jumpTimeCounter == jumpTime)) {
            rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);                    // faire sauter Ryu
            isJumping = true;
            ryuAnimator.SetBool("isJumping", true);
            grounded = false;
        } else if ((Input.GetKey("z") || Input.GetButton("A1")) && isJumping) {  // si on reste appuyé, faire un long saut
            if (jumpTimeCounter > 0) {
                rbRyu.velocity = new Vector2(rbRyu.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                ryuAnimator.SetBool("isJumping", true);
            }
        } else {
            ryuAnimator.SetBool("isJumping", false);
        }

        if (Input.GetKeyUp("z") || (Input.GetButtonUp("A1"))) {                         // empeche de reaugmenter le jump après stop jump
            isJumping = false;
        }


        /*** CODE POUR LE KICK ***/

        if (Input.GetKey("a") || Input.GetButtonDown("Y1")) {
            ryuAnimator.SetBool("isKicking", true);

        } else {
            ryuAnimator.SetBool("isKicking", false);
        }

        /*** CODE POUR LE PUNCH ***/

        if (Input.GetKey("e") || Input.GetButtonDown("X1")) {
            ryuAnimator.SetBool("isPunching", true);
        } else {
            ryuAnimator.SetBool("isPunching", false);
        }

        /*** CODE POUR LE CROUCH ***/

        if (Input.GetKey("s") || Input.GetAxis("JVertical1") > 0) {
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