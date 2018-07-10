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
    private bool playerInRange;
    private bool moveRight;
   


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
    /// Apply AI Variables(Player Transform, Range, FOV)
    /// </summary>
    /// <param name="player"></param>
    /// <param name="rng"></param>
    /// <param name="_fov"></param>
    public void SetAIVariables(Transform player, float rng, float _fov)
    {

        playerChar = player;
        range = rng;
        fov = _fov;
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

   

    private float shootTimer, shootDelay;
    public void FireAtPlayer(float _shootTimer)
    {
        customParticleSystem.transform.LookAt(playerChar.transform);

        if (shootDelay < _shootTimer)
        {

            shootDelay += Time.deltaTime;
            shootTimer += Time.deltaTime;
            if (shootTimer >= 0.5f)
            {
                shootTimer = 0;
                particlControlRef.ParticleShoot();
                
            }


        }

 


    }


}






