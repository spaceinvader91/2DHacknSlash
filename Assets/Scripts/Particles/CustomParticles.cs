using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomParticles : MonoBehaviour {

    //User Variables
    //Velocity Settings
    public float startingVelocity, velocityOverLiftime;
    //X/Y Origin Offset
    public float yOffest, xOffset;
    //SpawnRate per second
    public float spawnDelay;
    //Max lifetime in seconds
    public float particleMaxLifeTime = 3f;
    //RigidBody Settings
    public float rbMass = 1, rbGravity = 0;
    public bool rbFreezeRoation = false;
    //Color
    public Color particleStartColor;
    //Size
    public float particleStartSize;
    //Collision
    public bool particleCollision;


    //Particle Variables
    //RigidBody to move each particle
    public List<Rigidbody2D> particleRBs;
    //List of current particle game objects
    public List<GameObject> aliveParticles;
    //Desired Particle
    public GameObject[] particleObjects;



    public void ParticleShoot()
    {
        //Set particle Spawn
        Vector2 particleSpawnPoint = new Vector3(transform.position.x + xOffset, transform.position.y + yOffest);

        //Create a clone of the desired object



        foreach (GameObject toSpawn in particleObjects)
        {
            GameObject particleClone = Instantiate(toSpawn, particleSpawnPoint, transform.rotation);

            //Set color of Sprite
            SpriteRenderer particleSprite = particleClone.GetComponentInChildren<SpriteRenderer>();
            particleSprite.color = particleStartColor;
            //Set Scale
            particleClone.transform.localScale = new Vector2(particleStartSize, particleStartSize);


            //Set parent for script reference
            particleClone.transform.SetParent(this.gameObject.transform);

            //Add spawned particles to the list
            aliveParticles.Add(particleClone);

            //Accquire RB component
            Rigidbody2D particleRB = particleClone.GetComponent<Rigidbody2D>();

            //Initial Velocity, relevant to local axis
            particleRB.AddForce(particleRB.transform.up * startingVelocity, ForceMode2D.Impulse);



            //Add each particle rigidbody to the list
            particleRBs.Add(particleRB);


        }
        


    }

    
    public void ParticleForceOverLifeTime()
    {
        foreach (Rigidbody2D particle in particleRBs)
        {

            particle.AddForce(particle.transform.up * velocityOverLiftime);

        }
    }

    public void ParticleRigidBodySettings()
    {

        foreach (Rigidbody2D particle in particleRBs)
        {

            particle.mass = rbMass;
            particle.gravityScale = rbGravity;
            particle.freezeRotation = rbFreezeRoation;

        }

    }


    public bool SetCollisions()
    {

        return particleCollision;

    }





    

    IEnumerator SpawnParticles()
    {
        yield return new WaitForSeconds(spawnDelay);
        ParticleShoot();
        yield return StartCoroutine(SpawnParticles());
    }





    //
    // Start and Update
    //

    private void Start()
    {
        StartCoroutine(SpawnParticles());

        //Reset Lists
        aliveParticles.Clear();
        particleRBs.Clear();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ParticleShoot();
        }

        //Constant Methods
        ParticleRigidBodySettings();
        ParticleForceOverLifeTime();
        //ParticleTrail();
        ParticleLifeTime();
    }


    private CustomParticleCollision particleScriptRef;

    public void ParticleLifeTime()
    {
        foreach (GameObject particle in aliveParticles)
        {

            particleScriptRef = particle.GetComponent<CustomParticleCollision>();
            //Run methods attached to each particle to adjust appearance.Minimise use of .GetComponent
            particleScriptRef.SetParticleLifeTime(particleMaxLifeTime);
            
        }


    }
    
    public void RemoveFromLists(GameObject particle, Rigidbody2D particleRB)
    {
       

        aliveParticles.Remove(particle);
        particleRBs.Remove(particleRB);



    }


}
