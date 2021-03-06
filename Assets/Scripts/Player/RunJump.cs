﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunJump : MonoBehaviour {

    //Cache
    [SerializeField]
    private Transform playerT;
    private Animator playerAnim;
    public Rigidbody2D playerRB;
    public float localScale = 1f;

    //cache
    private GroundDetection groundDetectionRef;

    //Run Variables
    private float
        dPadHorizontalAxis;

    public float
        runSpeed = 6f,
        maxRunSpeed,
        maxJumpSpeed;
    private float defaultRunspeed, reducedRunSpeed = 3f;

    //Jump Variables
    [SerializeField]
    private float jumpForce = 4f, forwardJumpForce = 2f;


    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        groundDetectionRef = GameObject.FindGameObjectWithTag("Player").GetComponent<GroundDetection>();

        defaultRunspeed = runSpeed;
    }

    private void Update()
    {
        ReducedJumpMovement();

        //Dash Cooldown

    }

    private void FixedUpdate()
    {
        LimitXMovementSpeed();
    }

    public void RunRight()
    {
        if (groundDetectionRef.RayCastDown())
        {
            playerAnim.SetBool("isRunning", true);
            playerT.localScale = new Vector3(localScale, localScale, 1f);
            playerRB.AddForce(Vector3.right * runSpeed);

            //if (playerRB.velocity.x >= maxRunSpeed)
            //{
            //    playerRB.AddForce(Vector2.left * runSpeed);
            //}

        }

        if (!groundDetectionRef.RayCastDown())
        {
            playerAnim.SetBool("isRunning", false);
            playerT.localScale = new Vector3(localScale, localScale, 1f);



        }
    }

    //restrict x velocity relative to direction of travel

    public void RunLeft()
    {
        if (groundDetectionRef.RayCastDown())
        {
            playerAnim.SetBool("isRunning", true);
            playerT.localScale = new Vector3(-localScale, localScale, 1f);
            playerRB.AddRelativeForce(Vector2.left * runSpeed);


        }

        if (!groundDetectionRef.RayCastDown())
        {
            playerAnim.SetBool("isRunning", false);
            playerT.localScale = new Vector3(-localScale, localScale, 1f);


        }
    }

    void LimitXMovementSpeed()
    {
        var currentVelocity = playerRB.velocity;
        print("current velocity x " + currentVelocity.x);

        if (playerRB.velocity.x > 0)
        {

            if (playerRB.velocity.x >= maxRunSpeed)
            {
                print("too fast");
                playerRB.AddForce(Vector2.left * (currentVelocity.x *10));
            }
        }

        if (playerRB.velocity.x < 0)
        {
            if (playerRB.velocity.x <= -maxRunSpeed)
            {
                playerRB.AddForce(Vector2.right * (-currentVelocity.x * 10));
            }
        }
    }
    

    public void RunStop()
    {
        playerAnim.SetBool("isRunning", false);
    }

    public void Jump()
    {
        //reduce movement
        //use axis for direction check

        playerAnim.SetTrigger("jump");
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);


    }


    public int jumpCount;
    public void DoubleJump(float jumpForce)
    {
        if (!groundDetectionRef.RayCastForward() && !groundDetectionRef.grounded && jumpCount == 0)
        {
            jumpCount++;
            playerAnim.SetTrigger("jump");
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    /// <summary>
    /// Move in the direction the player is facing by float (Impulse)
    /// </summary>
    /// <param name="dashSpeed"></param>
    public void Dash(float dashSpeed)
    {

            playerAnim.SetBool("dashing", true);

        playerAnim.SetBool("isLaunching", false);


            if (playerT.localScale == new Vector3(localScale, localScale, 1f))
            {
                playerRB.AddForce(Vector3.right * dashSpeed, ForceMode2D.Impulse);
            }
            if (playerT.localScale == new Vector3(-localScale, localScale, 1f))

            {
                playerRB.AddForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);
            }
        

      

    }


    public int wallJumpCount, maxWallJumps = 2;
    public void WallJump()
    {
        if (groundDetectionRef.RayCastForward() && !groundDetectionRef.grounded)
        {


                if (wallJumpCount <= maxWallJumps)
                {
                    wallJumpCount++;

                playerRB.AddForce(Vector2.up * jumpForce * 1.3f, ForceMode2D.Impulse);
                }

                

                else
                {
                    print("Max Wall Jumps");
                }

            }


        }
    




private void ReducedJumpMovement()
    {
        if (!groundDetectionRef.RayCastDown())
        {
            if (playerT.localScale.x == localScale)
            {
                playerAnim.SetBool("isRunning", false);
                playerRB.AddForce(Vector2.right * reducedRunSpeed);

                if (playerRB.velocity.x >= maxJumpSpeed)
                {
                    playerRB.AddForce(Vector2.left * playerRB.velocity.x);
                }


            }

            if (playerT.localScale.x == -localScale)
            {
                playerAnim.SetBool("isRunning", false);
                playerRB.AddForce(Vector2.left * reducedRunSpeed);

                if (playerRB.velocity.x <= -maxJumpSpeed)
                {
                    playerRB.AddForce(Vector2.right * -playerRB.velocity.x);
                }

            }
        }
    }

    private bool crouching;

    public void CrouchAnimCheck(bool _bool)
    {
        crouching = _bool;

        if (crouching)
        {
            playerAnim.SetBool("isCrouching", true);
        }
        else
        {
            playerAnim.SetBool("isCrouching", false);
        }

    }



    private bool dashing;

    public void DashAnimCheck(bool _bool)
    {
        dashing = _bool;

        if (!dashing)
        {
            playerAnim.SetBool("dashing", false);
        }

    }

}
