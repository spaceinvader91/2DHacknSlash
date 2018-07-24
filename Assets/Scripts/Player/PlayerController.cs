using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]



public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool isGrounded, disableMovement;


    private void Update()
    {
        PlayerControlRestrictions();

        if (isGrounded)
        {
            DoubleTapDash();
            CrouchControls();
        }

        if (!isGrounded)
        {
            AerialJumpCheck();

        }


    }

    //Cache
    private RunJump runJumpRef;
    private PlayerAttacks _attacks;

    private void Start()
    {
        runJumpRef = GameObject.FindGameObjectWithTag("PlayerReferences").GetComponent<RunJump>();
        _attacks = GameObject.FindGameObjectWithTag("PlayerReferences").GetComponent<PlayerAttacks>();
    }


    //Movement Restrictions
    //
    void PlayerControlRestrictions()
    {

        if (!disableMovement)
        {
            Movement();
        }

    }


    //Move Left/Right
    //
    private float jumpCount;
    void Movement()
    {
        //Run Method (RunJump.cs)
        float dPadHorizontalAxis = Input.GetAxisRaw("DPadX");

        if(dPadHorizontalAxis > 0 || dPadHorizontalAxis < 0) 
        {

            //Run Right
            if (dPadHorizontalAxis > 0)
            {
                runJumpRef.RunRight();
            }

            //Run Left
            if (dPadHorizontalAxis < 0)
            {
                runJumpRef.RunLeft();
            }

        }

        //Idle
        if (dPadHorizontalAxis == 0)
        {

    
            runJumpRef.RunStop();
  
        }


        if (Input.GetKeyDown(GameManager.GM.aButton) && isGrounded)
        {

            isGrounded = false;
            runJumpRef.Jump();

        }
    }

    

    void CrouchControls()
    {
        float dPadVerticalAxis = Input.GetAxisRaw("DPadY");


        if (dPadVerticalAxis < 0)
        {
            runJumpRef.CrouchAnimCheck(true);
        }

        else
        {
            runJumpRef.CrouchAnimCheck(false);
        }
    }

    void AerialJumpCheck()
    {
        if (Input.GetKeyDown(GameManager.GM.aButton))
        {
            runJumpRef.WallJump();
            runJumpRef.DoubleJump(doubleJumpForce);
        }


    }


    public float doubleJumpForce = 3f;
    public float dashSpeed;
    public float dblTapFwdTime = 0.5f;  // Tap twice within this time and you will be double-tapping.
    public float lungeTime = 1.0f;

    private float lastTapFwdTime = 0;  // the time of the last tap that occurred
    private bool dblTapFwdReady = false;  // whether you you will execute a double-tap upon the next tap
    private bool walkingFwd = false;
    private bool lunging = false;


    void DoubleTapDash()
    {

        float Horizontal = Input.GetAxisRaw("DPadX");
        // disable double-tapping after a short time limit	
        if (Time.time > lastTapFwdTime + dblTapFwdTime)
        {
            dblTapFwdReady = false;
        }

        if (lunging == false)
        {
            if (Horizontal > 0 || Horizontal < 0)
            {
                if (!walkingFwd)
                {
                    walkingFwd = true;
                    lastTapFwdTime = Time.time;

                    if (dblTapFwdReady)
                    {
                        // Stop the other animations if necessary.
                        StartCoroutine(Dash());
                    }
                    else
                    {
                        dblTapFwdReady = true;
                    }
                }
            }

            if (Horizontal == 0)
            {
                walkingFwd = false;
                // ^^ Idle animation Here
                print("relaxing");
            }

            if (walkingFwd)
            {
                // ^^ walk animation Here
                print("walking");
            }
        }

        else
        {
            print("lunging!");
        }
    }

    IEnumerator Dash()
    {
        lunging = true;
        runJumpRef.Dash(dashSpeed);
        yield return new WaitForSeconds(lungeTime);
        lunging = false;
    }



//Methods Run by outside scripts


public void GroundCheckBool(bool _bool)
    {
        isGrounded = _bool;
        //Communicate with the attack script that the player has hit the ground
        _attacks.GroundDetection(_bool);

        //Reset Wall Jump Count
        if (isGrounded)
        {
            runJumpRef.wallJumpCount = 0;
            runJumpRef.jumpCount = 0;

        }
    

    }



    public void DisableMovement(bool value)
    {

        disableMovement = value;
    }



}
