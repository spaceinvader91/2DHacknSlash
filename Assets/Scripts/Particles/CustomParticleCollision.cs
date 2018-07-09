using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticleCollision : MonoBehaviour {

    private PlayerCollision playerCollisionScript;
    private CustomParticles emitterScript;

    private float maxLifeTime, timer;
    private bool particlesCollide;


    private GameObject particleRef;
    public GameObject turretRef;
    private Rigidbody2D rbRef;

    private void Start()
    {
        particleRef = this.gameObject;
        rbRef = GetComponent<Rigidbody2D>();
        emitterScript = GetComponentInParent<CustomParticles>();
        playerCollisionScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollision>();
      
        //this.transform.SetParent(turretRef.transform);
        this.transform.SetParent(null);


        trailRef = GetComponentInChildren<TrailRenderer>();
        spriteRef = GetComponentInChildren<SpriteRenderer>();

    }

    public void SetParticleLifeTime(float lifeTime)
    {

        maxLifeTime = lifeTime;

    }

    private void DestroyAfterDelay()
    {
        timer += Time.fixedDeltaTime;
 
        if(timer >= maxLifeTime)
        {

            emitterScript.RemoveFromLists(particleRef, rbRef);

            Destroy(this.gameObject);
        }
    }

    private void ParticleCollisions()
    {
        particlesCollide = emitterScript.SetCollisions();
    }

    private void Update()
    {
        DestroyAfterDelay();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject hitObject = collision.collider.gameObject;

        if (hitObject.CompareTag("Ground"))
        {
           // emitterScript.RemoveFromLists(particleRef, rbRef);
            Destroy(this.gameObject);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject hitObject = collision.gameObject;

        if (hitObject.CompareTag("HeavyAttack"))
        {
            var newDir = turretRef.transform.position - transform.position;
            rbRef.velocity = Vector2.zero;
            rbRef.AddForce(newDir.normalized*20f , ForceMode2D.Impulse);

            ChangeParticleColor();
            ChangeTrailColor();

        }

        if (hitObject.CompareTag("Player"))
        {
            float bulletDmg = playerCollisionScript.bulletDmg;
            playerCollisionScript.PlayerTakeDamage(3);
            Destroy(this.gameObject);
        }

        if (hitObject.CompareTag("Enemy"))
        {

            print("Enemy");
            //Hurt enemy method goes here
             Destroy(this.gameObject);

        }

        }

    private TrailRenderer trailRef;
    private SpriteRenderer spriteRef;
    public Gradient trailGradient;

    public Color parryParticleColor;


    public void ChangeParticleColor()
    {

        
        spriteRef.color = parryParticleColor;


    }


    public void ChangeTrailColor()
    {

        trailRef.colorGradient = trailGradient;
        
    }

    private void OnDestroy()
    {
        emitterScript.RemoveFromLists(particleRef, rbRef);
    }

}






