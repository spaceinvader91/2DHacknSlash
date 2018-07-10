using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public GameObject customParticleSystem;

    private AI_Controller aiControlRef;
    private CustomParticles particlControlRef;
    private Rigidbody2D rb;
    private Transform playerChar;
    private float range, fov;
    private float distanceToPlayer, angle, angleLeft;
    private float fireWindow, fireRate, timeBetweenShots;
    private bool playerInRange;
    private bool moveRight;

    public bool playerFound;
   


    /// <summary>
    /// Send References to the  AI script
    /// </summary>
    /// <param name="aiControl"></param>
    /// <param name="rbRef"></param>
    /// <param name="cusPart"></param>
    public void GrabAIReferences(AI_Controller aiControl, Rigidbody2D rbRef, CustomParticles cusPart)
    {
        aiControlRef = aiControl;
        rb = rbRef;
        particlControlRef = cusPart;
    }

    /// <summary>
    /// Apply AI Variables(Player Transform, Range, FOV, Fire Rate)
    /// </summary>
    /// <param name="player"></param>
    /// <param name="rng"></param>
    /// <param name="_fov"></param>
    public void SetAIVariables(Transform player, float rng, float _fov,float _fireWindow, float _fireRate, float _timeBetweenShots)
    {

        playerChar = player;
        range = rng;
        fov = _fov;
        fireRate = _fireRate;
        fireWindow = _fireWindow;
        timeBetweenShots = _timeBetweenShots;
    }

    private void Update()
    {
        LimitMoveSpeed();
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

    private float _speed;

    void MoveRight()
    {
        moveRight = true;
        transform.localScale = new Vector3(1f, 1f, 1f);
        rb.AddForce(Vector2.right * _speed);
    }

    void MoveLeft()
    {
        moveRight = false;
        transform.localScale = new Vector3(-1f, 1f, 1f);
        rb.AddForce(Vector2.left * _speed);
    }

    void LimitMoveSpeed()
    {
        if (rb.velocity.x > 5)
        {
            rb.AddForce(Vector2.left * (_speed));
        }

        if (rb.velocity.x < -5)
        {
            rb.AddForce(Vector2.right * (_speed));
        }

    }

    /// <summary>
    /// AI will move Left/Right between 2 vector3s
    /// </summary>
    /// 
    //move to new script for advanced control of ai
    public void PhysicsPatrol(Vector3 start, Vector3 des, float speed)
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

                playerFound = true;
            }

        }

        else
        {
            playerInRange = false;
        }
        return playerInRange;

    }

   

    private float windowTimer, rateTimer, resetWinTimer;

    public void FireAtPlayer()
    {
        customParticleSystem.transform.LookAt(playerChar.transform);

        if ( windowTimer < fireWindow)
        {

            windowTimer += Time.deltaTime;
            rateTimer += Time.deltaTime;
            if (rateTimer >= fireRate)
            {
                rateTimer = 0;
                particlControlRef.ParticleShoot();
                
            }


        }

        if(windowTimer >= fireWindow)
        {
            resetWinTimer += Time.deltaTime;

            if(resetWinTimer >= timeBetweenShots)
            {
                windowTimer = 0;
                resetWinTimer = 0;
            }


        }


    }

    public float maxChaseRange;

    void ChasePlayer()
    {

        if(distanceToPlayer > maxChaseRange)
        {

            var playerDir = playerChar.position.x - transform.position.x;
            

            if(playerDir > 0)
            {


            }


            if (playerDir < 0)
            {


            }

        }


    }


}






