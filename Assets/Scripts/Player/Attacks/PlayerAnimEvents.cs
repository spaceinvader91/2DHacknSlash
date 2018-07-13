using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour {

    private PlayerController controllerRef;
    private PlayerAttacks attackBoolRef;
    private RunJump controlsRef;

    private bool
        movement,
        lightAttack,
        heavyAttack;


	// Use this for initialization
	void Start () {

        controllerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        controlsRef = GameObject.FindGameObjectWithTag("PlayerReferences").GetComponent<RunJump>();
        attackBoolRef = GameObject.FindGameObjectWithTag("PlayerReferences").GetComponent<PlayerAttacks>();

    }



    //
    //
    // Movement Switches
    //
    //

    public void DisableMovement()
    {
        //when attack begins -- 
        movement = true;
        controllerRef.DisableMovement(movement);
    }

    public void EnableMovement()
    {

        //when attack ends
        movement = false;
        controllerRef.DisableMovement(movement);
    }



    //
    //
    // Light Attack Switches
    //
    //


    public void LightAttackPlaying()
    {
        lightAttack = true;
        attackBoolRef.LightAttack(lightAttack);
    }

    public void LightAttackEnded()
    {
        lightAttack = false;
        attackBoolRef.LightAttack(lightAttack);
    }



    //
    //
    // Heavy Attack Switches
    //
    //



    public void HeavyAttackPlaying()
    {
      
        heavyAttack = true;
        attackBoolRef.HeavyAttack(heavyAttack);

    }
    public void HeavyAttackEnded()
    {
    
        heavyAttack = false;
        attackBoolRef.HeavyAttack(heavyAttack);
    }

    public void HeavyAttackPlaying2()
    {

        var heavyAttack2 = true;
        attackBoolRef.HeavyAttack2(heavyAttack2);

    }
    public void HeavyAttackEnded2()
    {

        var heavyAttack2 = false;
        attackBoolRef.HeavyAttack2(heavyAttack2);
    }


    //
    //
    // Launcher Switches
    //
    //



    public void LauncherPlaying()
    {
     
        bool launcherAttack = true;
        attackBoolRef.LauncherAttack(launcherAttack);

    }
    public void LauncherEnding()
    {
   
        bool launcherAttack = false;
        attackBoolRef.LauncherAttack(launcherAttack);
    }



    //
    //
    // Aerial Attack Switches
    //
    //

    public void LightAerialAttackPlaying1()
    {

        bool value = true;
        attackBoolRef.LightAerialAttack1(value);

    }
    public void LightAerialAttackEnded2()
    {

        bool value = false;
        attackBoolRef.LightAerialAttack1(value);
    }

    public void HeavyAerialAttackPlaying1()
    {

        bool value = true;
        attackBoolRef.HeavyAerialAttack1(value);

    }
    public void HeavyAerialAttackEnded2()
    {

        bool value = false;
        attackBoolRef.HeavyAerialAttack1(value);
    }

    // Dash

    public void DashingStarted()
    {
        bool value = true;
        controlsRef.DashAnimCheck(value);
    }

    public void DashingEnded()
    {
        bool value = false;
        controlsRef.DashAnimCheck(value);
    }



    //
    // Track when player enters Idle - Reset Combo Count.
    //

    //public void IdleBegin()
    //{
    //    bool value = true;
    //    comboRef.IdleCheck(value);

    //}
}

