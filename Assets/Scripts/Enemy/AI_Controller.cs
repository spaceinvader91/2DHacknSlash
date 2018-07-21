using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour {


    public Transform playerChar;
    public float range, fov;
    //AI Movement Variables
    public float patrolDistance, patrolSpeed, chaseSpeed;
    public float maxChaseRange;
    public float destDist;
    public float fireWindow = 2f, fireRate = 0.5f, timeBetweenShots = 1.5f;
    private Vector3 start, des;



    //AI Script
    private AI aiRef;
    AI_Controller aiControlRef;
    private AI_WallRay wallScriptRef;

    //Particle Controls
    CustomParticles customPartRef;

    //Token Controls
    private TokenController tokenControl;

    //RigidBody
    Rigidbody2D rbRef;

    //Track token return
    private bool haveToken;


    // Use this for initialization
    void Start () {

        //Set patrol path
         start = new Vector3(transform.position.x - destDist, transform.position.y);
         des = new Vector3(transform.position.x + destDist, transform.position.y);

        //Grab AI References
        aiRef = GetComponent<AI>();
        aiControlRef = GetComponent<AI_Controller>();
        customPartRef = GetComponentInChildren<CustomParticles>();
        rbRef = GetComponent<Rigidbody2D>();
        wallScriptRef = GetComponentInChildren<AI_WallRay>();
        playerChar = GameObject.FindGameObjectWithTag("Player").transform;
        tokenControl = GameObject.FindGameObjectWithTag("PlayerReferences").GetComponent<TokenController>();

        ApplyAIReferences();
	}


	// Update is called once per frame
	void Update () {

        if(isGrounded && !hitStunned && !aiRef.FindPlayer())
        {
            aiRef.PhysicsPatrol(start, des, patrolSpeed);
    
        }

        if(isGrounded && !hitStunned && aiRef.FindPlayer())
        {
            aiRef.ChasePlayer(chaseSpeed, maxChaseRange);



            //Request Token
            if (aiRef.LightAttackToken())
            {
                haveToken = true;
                aiRef.FireAtPlayer();
            }
          
        }

        if (!aiRef.FindPlayer() && haveToken)
        {

            tokenControl.ReturnTokens(0);
            haveToken = false;
        }



            HitStunTimer();
        GroundCheckBool();

        ApplyAISettings();
		
	}

    //
    // Variables to other scripts
    //

    void ApplyAISettings()
    {
        aiRef.SetAIVariables(playerChar, range, fov, fireWindow, fireRate, timeBetweenShots);
    }

    void ApplyAIReferences()
    {
        aiRef.GrabAIReferences(aiControlRef, rbRef, customPartRef, wallScriptRef, tokenControl);

    }

    //
    // Damage Variables & Methods
    //
    public GameObject hitParticle, deathParticle;
    public float enemyHP = 4;


    //Take Damage
    /// <summary>
    /// Remove float dmg from the enemies health, death if 0
    /// </summary>
    /// <param name="dmg"></param>
    public void TakeDamage(float dmg)
    {
        enemyHP -= dmg;

        //Take Damage
        if (enemyHP > 0)
        {
            Instantiate(hitParticle, transform.position, transform.rotation);
        }

        //Death
        if (enemyHP <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            enemyHP = 4;
            // Destroy(gameObject);
        }
    }

    //
    // Ground Check Variables & Method
    //

    RaycastHit2D hit;
    [SerializeField]
    private float rayOrigin, rayDistance;
    private bool isGrounded;
    public bool hitStunned;


    /// <summary>
    /// Returns false is the enemy is not touching the ground (Add Layer Mask?)
    /// </summary>
    public bool GroundCheckBool()
    {
        var rayStart = new Vector2(transform.position.x, transform.position.y - rayOrigin);



        hit = Physics2D.Raycast(rayStart, Vector2.down, rayDistance);

        if (hit.collider != null)
        {
            isGrounded = true;
            

            Debug.DrawRay(rayStart, Vector2.down, Color.blue);
        }



        if (hit.collider == null)
        {
            isGrounded = false;

            Debug.DrawRay(rayStart, Vector2.down, Color.red);
        }

        return isGrounded;
    }

    /// <summary>
    /// Enemy will enter hitstun state until false
    /// </summary>
    /// <param name="_bool"></param>
    /// <returns></returns>
    public bool HitStunned(bool _bool)
    {
        //Reset hit stun timer on hit
        hitStunTimer = 0;
        
        hitStunned = _bool;
        return hitStunned;
    }


    /// <summary>
    /// Resets Hit Stun after a time
    /// </summary>
    [SerializeField]
    private float hitStunMaxTime = 3;
    private float hitStunTimer;
    void HitStunTimer()
    {

        if (hitStunned)
        {

            hitStunTimer += Time.deltaTime;

            if (hitStunTimer >= hitStunMaxTime)
            {
                hitStunTimer = 0;
                HitStunned(false);
            }

        }


    }

}
