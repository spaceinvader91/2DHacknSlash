using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    private Rigidbody2D playerRb;
    public GameObject hitParticle, deathParticle;
    [SerializeField]
    private float playerHP = 100;
    [SerializeField]
    private float bulletDmg = 3;
    //Reference to the current velocity of the player RB
    private Vector2 currentVelocity;

    private void Start()
    {

        playerRb = GetComponentInParent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        currentVelocity = playerRb.velocity;
    }

    /// <summary>
    /// Removes HP by float dmg, Player Death if HP = 0
    /// </summary>
    /// <param name="dmg"></param>
    public void PlayerTakeDamage(float dmg)
    {

        playerHP -= dmg;

        if(playerHP <= 0)
        {

            Instantiate(deathParticle, transform.position, Quaternion.identity);
            playerHP = 100;
        }

        if(playerHP > 0)
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
        }



    }







}



