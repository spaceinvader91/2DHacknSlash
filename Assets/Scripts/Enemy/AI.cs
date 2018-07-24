using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public GameObject customParticleSystem;

    public float maxMoveSpeed;
    public  AI_WallRay aiWallRay;
    private AI_Controller aiControlRef;
    private CustomParticles particlControlRef;
    private TokenController tokenControlRef;
 
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
    public void GrabAIReferences(AI_Controller aiControl, Rigidbody2D rbRef, CustomParticles cusPart, AI_WallRay aiRay, TokenController token)
    {
        aiControlRef = aiControl;
        rb = rbRef;
        particlControlRef = cusPart;
        aiWallRay = aiRay;
        tokenControlRef = token;

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
    /// 
    public float scalingValue = 1;
    public void FacePlayer()
    {
        if (playerChar.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(scalingValue, scalingValue,1);
        }

        if (playerChar.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-scalingValue,scalingValue,1);
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
        if (rb.velocity.x > maxMoveSpeed)
        {
            rb.AddForce(Vector2.left * (rb.velocity.x));
        }

        if (rb.velocity.x < -maxMoveSpeed)
        {
            rb.AddForce(Vector2.right * (rb.velocity.x));
        }

    }


    /// <summary>
    /// Jump forward - takes forward force and upward force
    /// </summary>
    /// <param name="force"></param>
    public void JumpForward(float forwardForce, float upwardForce)
    {
        //Upwards Force
        rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);

        //Jump left
        if (transform.localScale.x < 0)
        {
            rb.AddForce(Vector2.left * forwardForce);
        }
        else
        {
            rb.AddForce(Vector2.right * forwardForce);
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
            transform.localScale = new Vector3(scalingValue,scalingValue, 1f);
        }

        if (transform.position.x >= des.x)
        {
            moveRight = false;
            transform.localScale = new Vector3(scalingValue,scalingValue, 1f);
        }

        if (moveRight)
        {
            rb.AddForce(Vector2.right * speed);


        }
        if (!moveRight)
        {
            rb.AddForce(Vector2.left * speed);

        }

    }



    public bool FindPlayer()
    {
        //Straight Line distance
        playerChar = GameObject.FindGameObjectWithTag("Player").transform;
        distanceToPlayer = Vector3.Distance((Vector2)transform.position, (Vector2)playerChar.position);

        //The Angle the player is at
        angle = Vector2.Angle(Vector3.right, playerChar.position - transform.position);
        angleLeft = Vector2.Angle(Vector3.left, playerChar.position - transform.position);


        //If the player is within range
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

    public bool lightAttackToken;

    public bool LightAttackToken()
    {


        if (!lightAttackToken)
        {
            if (tokenControlRef.RequestLightAttack())
            {
                lightAttackToken = true;
                return lightAttackToken;
            }

            return lightAttackToken;
        }

        return lightAttackToken;
    }

    /// <summary>
    /// Chases the player when they leave maxChaseRange
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="maxChaseRange"></param>

    public void ChasePlayer(float speed, float maxChaseRange)
    {
        var playerDir = playerChar.position.x - transform.position.x;

        if(distanceToPlayer > maxChaseRange)
        {
            if(playerDir > 0)
            {
                rb.AddForce(Vector2.right * speed);
                JumpCheck();

            }

            if (playerDir < 0)
            {
        
                rb.AddForce(Vector2.left * speed);
                JumpCheck();

            }

        }


    }


    public float jumpForce, forwardJumpForce, maxJumpSpeed;
    public void JumpCheck()
    {

        if (aiControlRef.GroundCheckBool())
        {

 
            if (aiWallRay.ForwardWallCast())
            {
                JumpForward(forwardJumpForce, jumpForce);
            }


            //put same limit on player char
            if (rb.velocity.y > maxJumpSpeed)
            {
                rb.AddForce(Vector2.down * rb.velocity.y);
            }

        }



    }


}






