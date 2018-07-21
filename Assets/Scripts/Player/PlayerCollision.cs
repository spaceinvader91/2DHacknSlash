using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    //standarise these variable names
    private PlayerHitPhysics playerPhysicsRef;

    [SerializeField]
    private GameObject hitParticle, deathParticle, blockParticle;
    [SerializeField]
    private float playerHP = 100;

    //public for access outside this script
    public float bulletDmg = 3;


    private void Start()
    {

    }


    /// <summary>
    /// Removes HP by float dmg, Player Death if HP = 0
    /// </summary>
    /// <param name="dmg"></param>
    public void PlayerTakeDamage(float dmg)
    {

        playerHP -= dmg;

        if (playerHP <= 0)
        {

            Instantiate(deathParticle, transform.position, Quaternion.identity);
            playerHP = 100;
        }

        if (playerHP > 0)
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
        }



    }

    public void BlockCollision(Vector3 hitPos)
    {

        GameObject particleClone = Instantiate(blockParticle, hitPos, Quaternion.identity);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Enemy"))
        {
            

        }
    }



}






