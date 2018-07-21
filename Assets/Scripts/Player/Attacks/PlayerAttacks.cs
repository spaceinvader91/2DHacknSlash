using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {


    private Animator parentAnim;


    //Ground Attacks
    public bool isLightAttacking, isHeavyAttacking, isLaunching,
                                  isHeavyAttacking2;
    //Aerial Attacks
    public bool isLightAerialAttacking, isHeavyAerialAttacking;

    private bool isGrounded;


    // Use this for initialization
    void Start()
    {

        parentAnim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isGrounded)
        {
            GroundAttacks();
            StandBlock();
        }

        if (!isGrounded)
        {
            AerialAttacks();
        }

    }


    //Ground Attacks
    //

    void GroundAttacks()
    {

        var horizontalAxis = Input.GetAxis("DPadX");
        var verticalAxis = Input.GetAxis("DPadY");


        //
        // Light Attacks
        //

        //Punch Attack -- A Button
        if (Input.GetKeyDown(GameManager.GM.xButton)
            && verticalAxis == 0
            && horizontalAxis == 0
            && !isLightAttacking
            && !isHeavyAttacking
            && !isLaunching)
        {
            parentAnim.SetBool("lightAttack1", true);
        }

        //
        // Heavy Attacks
        //

        // X Button
        if (Input.GetKeyDown(GameManager.GM.yButton)
            && !isLightAttacking
            && !isHeavyAttacking
            && !isLaunching)

        {
            parentAnim.SetBool("heavyAttacking", true);

        }


        // X 2nd press
        if (Input.GetKeyDown(GameManager.GM.yButton)
            && isHeavyAttacking)


        {
            parentAnim.SetBool("heavyAttacking2", true);

        }

        //
        // Launcher Attacks
        //


        // B Button
        if (Input.GetKeyDown(GameManager.GM.bButton))
        {
            parentAnim.SetBool("isLaunching", true);
        }

    }

    // Aerial Attacks
    //

    void AerialAttacks()
    {

        var horizontalAxis = Input.GetAxis("DPadX");
        var verticalAxis = Input.GetAxis("DPadY");




        //Punch Attack -- A Button
        if (Input.GetKeyDown(GameManager.GM.aButton))
           // && !isLightAerialAttacking)
        {

            parentAnim.SetBool("lightAerialAttack", true);
        }

        //Punch Attack -- X Button
        if (Input.GetKeyDown(GameManager.GM.xButton))
        //&& !isHeavyAerialAttacking)
        {
            parentAnim.SetBool("heavyAerialAttack", true);
        }

    }

    //
    // Block
    //

    void StandBlock()
    {
        var horizontalAxis = Input.GetAxis("DPadX");
        var verticalAxis = Input.GetAxis("DPadY");


        //LB and not crouching = stand block
        if (verticalAxis >= 0 && Input.GetKey(GameManager.GM.lbButton))
        {
            parentAnim.SetBool("isBlocking", true);
        }

        else
        {
            parentAnim.SetBool("isBlocking", false);
        }

    }



    //
    // Attack Bool Switches
    //




    // Light Attack 1
    //
    public void LightAttack(bool value)
    {

        isLightAttacking = value;

        if (!isLightAttacking)
        {
            parentAnim.SetBool("lightAttack1", false);
        }
    }


    // Heavy Attack 1
    //

    public void HeavyAttack(bool value)
    {
        isHeavyAttacking = value;

        if (!isHeavyAttacking)
        {
            parentAnim.SetBool("heavyAttacking", false);
        }
    }

    //Heavy Attack 2
    //

    public void HeavyAttack2(bool value)
    {
        isHeavyAttacking2 = value;

        if (!isHeavyAttacking2)
        {
            parentAnim.SetBool("heavyAttacking2", false);
        }

    }


    // Launcher Attack
    //

    public void LauncherAttack(bool value)
    {
        isLaunching = value;

        if (!isLaunching)
        {
            parentAnim.SetBool("isLaunching", false);
        }
    }

    // Light Aerial Attack 1
    //
    public void LightAerialAttack1(bool value)
    {
        isLightAerialAttacking = value;

        if (!isLightAerialAttacking)
        {
            parentAnim.SetBool("lightAerialAttack", false);
        }
    }

    // Heavy Aerial Attack 1
    //
    public void HeavyAerialAttack1(bool value)
    {
        isHeavyAerialAttacking = value;

        if (!isHeavyAerialAttacking)
        {
            parentAnim.SetBool("heavyAerialAttack", false);
        }
    }



    //
    // Ground Detection Bool
    //

    public void GroundDetection(bool value)
    {
        isGrounded = value;

    }



}
