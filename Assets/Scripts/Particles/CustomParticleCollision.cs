using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticleCollision : MonoBehaviour {

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
        this.transform.SetParent(turretRef.transform);


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
            emitterScript.RemoveFromLists(particleRef, rbRef);
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject hitObject = collision.gameObject;

        if (hitObject.CompareTag("HeavyAttack"))
        {
            print("parried");
            var newDir = turretRef.transform.position - transform.position;
            rbRef.velocity = Vector2.zero;
            rbRef.AddForce(newDir.normalized*30f , ForceMode2D.Impulse);


            ChangeParticleColor();
            ChangeTrailColor();

        }

        if (hitObject.CompareTag("HeavyAttack"))
        {

            var newDir = turretRef.transform.position - transform.position;
            rbRef.AddForce(newDir.normalized / 150, ForceMode2D.Impulse);


        }

        if (hitObject.CompareTag("Player"))
        {
            print("hit player");
            emitterScript.RemoveFromLists(particleRef, rbRef);
            Destroy(gameObject);


        }

    }

    private TrailRenderer trailRef;
    private SpriteRenderer spriteRef;

    public Gradient trailGradient;

    public Color parryParticleColor;
    public Color parryTrailColor;

    void ChangeParticleColor()
    {
        spriteRef.color = parryParticleColor;


    }


    void ChangeTrailColor()
    {

        trailRef.colorGradient = trailGradient;
        
    }

}






