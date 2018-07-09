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
    }

    //Cache
    private RunJump _runJump;
    private PlayerAttacks _attacks;

    private void Start()
    {
        _runJump = GameObject.FindGameObjectWithTag("PlayerReferences").GetComponent<RunJump>();
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
    void Movement()
    {

        //Run Method (RunJump.cs)
        float dPadHorizontalAxis = Input.GetAxisRaw("DPadX");

        if (dPadHorizontalAxis > 0 || dPadHorizontalAxis < 0)
        {


            //Run Right
            if (dPadHorizontalAxis > 0)
            {
                _runJump.RunRight();
            }

            //Run Left
            if (dPadHorizontalAxis < 0)
            {
                _runJump.RunLeft();

            }

        }

        //Idle
        if (dPadHorizontalAxis == 0)
        {

    
            _runJump.RunStop();
        }


        if (Input.GetKeyDown(GameManager.GM.bButton))// && isGrounded)
        {
           
            isGrounded = false;
            _runJump.Jump();
        }


    }


    //Methods Run by outside scripts

    public void GroundCheckBool(bool _bool)
    {
        isGrounded = _bool;
        //Communicate with the attack script that the player has hit the ground
        _attacks.GroundDetection(_bool);

        //Reset Gravity upon touching the ground no matter what
        //gravity = 20f;

    }



    public void DisableMovement(bool value)
    {

        disableMovement = value;
    }



}
