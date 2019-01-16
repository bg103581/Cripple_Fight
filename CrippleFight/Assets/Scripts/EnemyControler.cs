using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    public Transform target;
    private GameObject wallleft;
    private GameObject wallRight;
    bool isleft;
    bool isright;
    public enum StateEnemy
    {
        walk, dashright, dashleft, jump, kick, crouch, Punch, KickCrouch, idle
    }
    public StateEnemy state;
    Animator playeranim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playeranim = target.gameObject.GetComponent<Animator>();
        //wallleft = GameObject.FindGameObjectWithTag ("wallF");
        //wallRight = GameObject.FindGameObjectWithTag ("wallR");

    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - target.transform.position.x;
        if (distance > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            checkleft();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            checkright();
        }
        if (onGround == false && state != StateEnemy.jump)
        {
            rb.velocity -= Vector2.up * 2;

        }
        transform.localScale = new Vector3(5, 5, 5);



        switchstate();

    }
    int indexkick;

    void checkleft()
    {

        Vector2 mytransform = transform.position + new Vector3(-1, 0, 0);
        RaycastHit2D ray = Physics2D.Raycast(mytransform, Vector2.left);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left), Color.yellow);
        if (ray.collider != null && ray.collider.transform.tag == "Player")
        {
            isleft = true;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            //print (distance);
            //changestate ();
            if (Mathf.Abs(distance) < 2)
            {


                changestate();

            }
            if (Mathf.Abs(distance) > 2 && Mathf.Abs(distance) <= 4)
            {

                WalkThink();

            }
            if (Mathf.Abs(distance) > 4 && Mathf.Abs(distance) <= 6)
            {

                Think();

            }
            if (Mathf.Abs(distance) > 6)
            {

                state = StateEnemy.walk;

            }
          
        }
        else
            isleft = false;
    }
    void checkright()
    {
        Vector2 mytransform = transform.position + new Vector3(1, 0, 0);
        RaycastHit2D ray = Physics2D.Raycast(mytransform, Vector2.right);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right), Color.red);
        if (ray.collider != null && ray.collider.transform.tag == "Player")
        {
            isright = true;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            print(distance);
            
            if (Mathf.Abs(distance) <= 2)
            {

                changestate();

            }
            if (Mathf.Abs(distance) > 2 &&  Mathf.Abs(distance)<=4)
            {

                WalkThink();

            }
            if (Mathf.Abs(distance) > 4 && Mathf.Abs(distance) <= 6)
            {

               Think();

            }
            if (Mathf.Abs(distance) > 6)
            {

                state = StateEnemy.walk;

            }
            
        }
        else
            isright = false;

    }
    float speed = 6;
    void switchstate()
    {
        switch (state)
        {
            case StateEnemy.dashright:

                rb.velocity = Vector2.right * speed * 6;




                anim.SetFloat("horizantal", rb.velocity.x);
                timethink = 0.0f;
                break;
            case StateEnemy.walk:
                if (isleft)
                {
                    rb.velocity = Vector2.left * speed;
                }
                if (isright)
                {
                    rb.velocity = Vector2.right * speed;
                }

                anim.SetFloat("horizantal", rb.velocity.x);

                break;
            case StateEnemy.jump:
                if (onGround)
                {
                    
                    rb.velocity = Vector2.up * 16;
                    anim.SetFloat("vertical", 10);

                }
                break;
            case StateEnemy.dashleft:
                rb.velocity = Vector2.left * speed * 6;
                anim.SetFloat("horizantal", rb.velocity.x);
                timethink = 0.0f;
                break;
            case StateEnemy.idle:
                
                anim.SetFloat("horizantal", 0);
                anim.SetFloat("vertical", 0);
                break;
            case StateEnemy.Punch:
                anim.SetTrigger("Punch");
                break;
            case StateEnemy.kick:
                anim.SetTrigger("kick");



                break;
            case StateEnemy.KickCrouch:
                anim.SetTrigger("KickCrouch");



                break;
            default:
                break;
        }
    }
    void changestate()
    {
        int a = cheackplayerstate();
        switch (a)
        {
            case 0:

                int kickstate = Random.Range(0, 3);
                if (kickstate == 0)
                {
                    state = StateEnemy.Punch;
                }
                else if (kickstate == 1)
                    state = StateEnemy.kick;
                else
                    state = StateEnemy.KickCrouch;

                break;
            case 1:

                state = StateEnemy.walk;
                break;
            case 2:
                if (isleft)
                {
                    state = StateEnemy.dashright;
                }
                if (isright)
                {
                    state = StateEnemy.dashleft;
                }


                break;
            case 3:
                state = StateEnemy.jump;

                break;
            case 4:
                state = StateEnemy.KickCrouch;
                break;
            default:
                break;
        }
    }
    public List<string> playersate;
    int index;
    int cheackplayerstate()
    {

        index = 0;
        foreach (var item in playersate)
        {
            if (playeranim.GetBool(item) == true)
            {
                index = playersate.IndexOf(item) + 1;

            }

        }

        return index;

    }

    void Punch()
    {

        anim.SetTrigger("Punch");


    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "ground")
        {
            onGround = true;

        }

    }


    bool onGround;
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "ground")
        {
            onGround = false;
        }
    }





    float timethink;
    void Think()
    {
        timethink += Time.deltaTime;

        if (timethink > 2)
        {

            if (isleft)
            {
                state = StateEnemy.dashleft;


            }
            if (isright)
            {
                state = StateEnemy.dashright;



            }


        }
        else
            state = StateEnemy.idle;

    }
    void WalkThink()
    {
        timethink += Time.deltaTime;

        if (timethink >1)
        {

           
                state = StateEnemy.walk;


           

        }
        else
            state = StateEnemy.idle;

    }

}
