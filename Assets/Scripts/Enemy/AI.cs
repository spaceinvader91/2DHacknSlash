using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    private AI_Controller aiControlRef;
    private Rigidbody2D rb;


   
    public Transform playerChar;
    public float range, fov;

    private float distanceToPlayer, angle, angleLeft;
    private bool playerInRange;
    private bool moveRight;



    // Use this for initialization
    void Start()
    {
    
        aiControlRef = GetComponent<AI_Controller>();
        rb = GetComponent<Rigidbody2D>();
    }


    //
    //  Enemy AI
    //

    /// <summary>
    /// Face the enemy towards the selected target
    /// </summary>
    public void FacePlayer()
    {
        if (playerChar.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (playerChar.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


    }

    /// <summary>
    /// AI will move Left/Right between 2 vector3s
    /// </summary>
   public void PhysicsPatrol(Vector3 start, Vector3 des, float speed)
    {

        if (!playerInRange)
        {
            //Flip the sprite based on direction of travel

            if (transform.position.x <= start.x)
            {
                moveRight = true;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            if (transform.position.x >= des.x)
            {
                moveRight = false;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            if (moveRight)
            {
                rb.AddForce(Vector2.right * speed);


            }
            if (!moveRight)
            {
                rb.AddForce(Vector2.left * speed);

            }

            if (rb.velocity.x > 5)
            {
                rb.AddForce(Vector2.left * (speed));
            }

            if (rb.velocity.x < -5)
            {
                rb.AddForce(Vector2.right * (speed));
            }

        }
    }



    public bool FindPlayer()
    {
        //Straight Line distance
        distanceToPlayer = Vector3.Distance((Vector2)transform.position, (Vector2)playerChar.position);

        //The Angle the player is at
        angle = Vector2.Angle(Vector3.right, playerChar.position - transform.position);
        angleLeft = Vector2.Angle(Vector3.left, playerChar.position - transform.position);


        //If the player is within range, swing sword (sword range = 1)
        if (distanceToPlayer < range)  //range = known float
        {

            //if the player is within the FOV angle set, starts at Vector3.Right
            if (angle < fov * 0.5f || angleLeft < fov * 0.5f) //fov = known float
            {
                //Face Player
                FacePlayer();
                playerInRange = true;
            }

        }

        else
        {
            playerInRange = false;
        }
        return playerInRange;

    }

    void JumpCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1.5f);
        Debug.DrawRay(transform.position, Vector2.right, Color.green);

        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.right, Color.red);
        }
    }

    public GameObject customParticleSystem;

    private float shootTimer;
    public void FireAtPlayer(float _shootTimer)
    {

        
        

        if(shootTimer < _shootTimer)
        {
            print("Fire");
            var particleScript = customParticleSystem.GetComponentInChildren<CustomParticles>();
            particleScript.enabled = true;
            shootTimer += Time.deltaTime;
        }

        if(shootTimer >= _shootTimer)
        {
            print("stop firing");
            var particleScript = customParticleSystem.GetComponentInChildren<CustomParticles>();
            particleScript.enabled = false;
            shootTimer = 0;

        }


     
 


    }








    }






