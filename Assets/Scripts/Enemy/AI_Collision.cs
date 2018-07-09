using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Collision : MonoBehaviour {

    //AI Cache
    private AI aiRef;
    private AI_Controller aiControlRef;
    private HitPhysics aiHitRef;

    //Player Cache
    private HitPhysics playerHitRef;



    private void Start()
    {
        //AI References
        aiRef = GetComponent<AI>();
        aiControlRef = GetComponent<AI_Controller>();
        aiHitRef = GetComponent<HitPhysics>();

        //Player References
        playerHitRef = GameObject.FindGameObjectWithTag("Player").GetComponent<HitPhysics>();


        //Initialise the AI
        aiHitRef.GravitySetting(defaultGravity);

        //Initialise the Player
        playerHitRef.GravitySetting(defaultGravity);
    }

    private void Update()
    {
        if (inAir)
        {
            AerialHitLimit();
        }
 
        AerialCheck();
    }

    //
    //Collision Goes Under Here
    //
    [SerializeField]
    private float aerialTimer, aerialHitCounter;

    /// <summary>
    /// Number of Hits before being knocked to the ground
    /// </summary>
    public float aerialHitLimit = 3f, defaultGravity = 3, juggleGravity = 1;
    [SerializeField]
    public float knockDownForce, launcherForce, knockBackForce,airHitForce, freezeTimer, hangTime;


    public float hitForce = 2;
    public float
        swordHit = 1,
        launcher = 1;

    private bool inAir;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        var other = collision.gameObject.tag;


        if (other == "LauncherAttack" && !inAir)
        {
            //Adjust enemy settings
            aiHitRef.GravitySetting(juggleGravity);
            aiHitRef.KnockUp(launcherForce);
           // aiHitRef.FreezeTimeOnHit(freezeTimer);
            aiControlRef.HitStunned(true);

            //Adjust player settings
            playerHitRef.GravitySetting(juggleGravity);
            playerHitRef.KnockUp(launcherForce);

            aerialTimer = 0;

        }

        if (other == "LightAttack" && !inAir)
        {
            //Adjust enemy settings
            aiHitRef.KnockBack(knockBackForce);
            aiControlRef.HitStunned(true);
            aiControlRef.TakeDamage(launcher);

            //Adjust Player settings
            playerHitRef.KnockBack(knockBackForce);

        }

        if (other == "HeavyAttack" && !inAir)
        {
            //adjust enemy settings
            aiHitRef.KnockBack(knockBackForce);
            aiControlRef.HitStunned(true);
            aiControlRef.TakeDamage(swordHit);
            //Adjust Player settings
            playerHitRef.KnockBack(knockBackForce);
        }

        //When in the Air

        if (other == "LightAttack" && inAir)
        {
            aerialTimer = 0;
            ++aerialHitCounter;

            //Move the enemy
            aiHitRef.KnockUp(airHitForce);
            //Apply Hitstun
            aiControlRef.HitStunned(true);

            //Move the player
            playerHitRef.KnockUp(airHitForce);


        }

        if (other == "HeavyAttack" && inAir)
        {
            aerialTimer = 0;
            ++aerialHitCounter;

            //Move the enemy
            aiHitRef.KnockUp(airHitForce);
            //Apply hitstun
            aiControlRef.HitStunned(true);

            //Move the player
            playerHitRef.KnockUp(airHitForce);

        }


    }




    void AerialHitLimit()
    {

        if (aerialHitCounter == aerialHitLimit)
        {
            //Reset Enemy gravity and knockdown
            aiHitRef.GravitySetting(defaultGravity);
            aiHitRef.KnockDown(knockDownForce);
       

            //Reset Player gravity and knockdown
            playerHitRef.GravitySetting(defaultGravity);
            playerHitRef.KnockDown(knockDownForce);

            //Freeze Time
            aiHitRef.FreezeTimeOnHit(freezeTimer);

            aerialHitCounter = 0;
            inAir = false;

        }

        aerialTimer += Time.deltaTime;

        if (aerialTimer >= hangTime)
        {
            aiHitRef.GravitySetting(defaultGravity);
            aerialTimer = 0;
            inAir = false;
        }

        
    }

    void AerialCheck()
    {
 
        if (!aiControlRef.GroundCheckBool())
        {

            inAir = true;
        }

        if (aiControlRef.GroundCheckBool())
        {
            inAir = false;
           aiHitRef.GravitySetting(defaultGravity);
            playerHitRef.GravitySetting(defaultGravity);
        }
    }


}
