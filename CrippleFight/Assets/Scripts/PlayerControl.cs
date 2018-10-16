using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public int PlayerNumber = 1;
    private Transform enemy;

    private Rigidbody2D rig2d;
    private Animator anim;

    private float horizontal, jhorizontal;
    private float vertical, jvertical;
    public float maxSpeed = 25;
    private Vector2 movement, jmovement;
    private bool crouch;

    // Variables needed for jumping
    public float jumpForce = 20;
    public float jumpDuration = .1f;
    private float _jmpDuration;
    private float _jmpForce;
    private bool jumpKey;
    private bool onGround;
    private bool walk;
    private bool jump;
    private bool punch;
    private bool kick;


    // Use this for initialization
    void Start() {
        rig2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        _jmpForce = jumpForce;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players) {
            if (pl.transform != this.transform) {
                enemy = pl.transform;
            }
        }

    }

    // Update each frame
    void Update() {
        Scalecheck();
        OnGroundCheck();
        UpdateAnimator();
    }

    // Used instead of Update because we're dealing with physics
    void FixedUpdate() {
        // Get input from keyboard
        horizontal = Input.GetAxis("Horizontal" + PlayerNumber.ToString());
        vertical = Input.GetAxis("Vertical" + PlayerNumber.ToString());

        // Get input from controlle
        jhorizontal = Input.GetAxis("JHorizontal" + PlayerNumber.ToString());
        jvertical = Input.GetAxis("JVertical" + PlayerNumber.ToString());

        // Movement for keyboard input and controller input respectively
        Vector2 movement = new Vector2(horizontal, 0);
        Vector2 jmovement = new Vector2(jhorizontal, 0);

        // Booleans to be used for animation
        crouch = ((vertical < -0.1f) || (jvertical < -0.1f)) && onGround;
        walk = (horizontal != 0) || (jhorizontal != 0);
        jump = (vertical > 0.1f) || (jvertical > 0.1f);
        punch = Input.GetButtonDown("Punch" + PlayerNumber.ToString()) || Input.GetButtonDown("X" + PlayerNumber.ToString());
        kick = Input.GetButtonDown("Kick" + PlayerNumber.ToString()) || Input.GetButtonDown("Y" + PlayerNumber.ToString());


        if (vertical > 0.1f) {
            if (!jumpKey) {
                _jmpDuration += Time.deltaTime;
                _jmpForce += Time.deltaTime;

                if (_jmpDuration < jumpDuration) {
                    rig2d.velocity = new Vector2(rig2d.velocity.x, _jmpForce);
                } else {
                    jumpKey = true;
                }
            }
        }

        // Movement is possible if the player is not crouching
        if (!crouch) {

            // Joystick input is prioritised. If there is no joystick input, we check keyboard input
            if (jhorizontal != 0)  {
                rig2d.velocity = new Vector2(jhorizontal * maxSpeed, rig2d.velocity.y);
            } else {
                rig2d.velocity = new Vector2(horizontal * maxSpeed, rig2d.velocity.y);
            }

        } else {
            rig2d.velocity = Vector2.zero;
        }
    }

    
    // To update animations
    void UpdateAnimator() {
        anim.SetBool("isCrouching", crouch);
        anim.SetBool("isWalking", walk);
        anim.SetBool("isJumping", jump);
        anim.SetBool("isPunching", punch);
        anim.SetBool("isKicking", kick);
    }

    // Allows player to fall faster
    void OnGroundCheck() {
        if (!onGround) {
            rig2d.gravityScale = 5;
        } else {
            rig2d.gravityScale = 1;
        }
    }

    // Makes the players look at each other automatically
    void Scalecheck() {
        if (transform.position.x < enemy.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = Vector3.one;
    }

    // To know if the player is on the ground or not
    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.tag == "ground") {
            onGround = true;
            jumpKey = false;
            _jmpDuration = 0;
            _jmpForce = jumpForce;
        }
    }

    // To know if the player is jumping
    void OnCollisionExit2D(Collision2D col) {
        if (col.collider.tag == "ground") {
            onGround = false;
        }
    }
}
