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


    //Particle Variables
    //RigidBody to move each particle
    public List<Rigidbody2D> particleRBs;
    //List of current particle game objects
    public List<GameObject> aliveParticles;
    //Desired Particle
    public GameObject particleObject;
    //Parent Object
    public GameObject parentObject;

    


    public void ParticleShoot()
    {
        Vector2 particleSpawnPoint = new Vector2(0, 0);

        if (parentObject.transform.localScale.x == 1)
        {
            //Set particle Spawn
             particleSpawnPoint = new Vector3(transform.position.x + xOffset, transform.position.y + yOffest);
        }
        if (parentObject.transform.localScale.x == -1)
        {
            //Set particle Spawn
            particleSpawnPoint = new Vector3(transform.position.x - xOffset, transform.position.y - yOffest);
        }

        //Create a clone of the desired object




          
            GameObject particleClone = Instantiate(particleObject, particleSpawnPoint, transform.rotation);

            //Set color of Sprite
            SpriteRenderer particleSprite = particleClone.GetComponentInChildren<SpriteRenderer>();
            particleSprite.color = particleStartColor;
            //Set Scale
            particleClone.transform.localScale = new Vector2(particleStartSize, particleStartSize);


            //Set parent for script reference
            //particleClone.transform.SetParent(this.gameObject.transform);
            particleClone.transform.SetParent(parentObject.transform);

            //Add spawned particles to the list
            aliveParticles.Add(particleClone);

            //Accquire RB component
            Rigidbody2D particleRB = particleClone.GetComponent<Rigidbody2D>();

            //Initial Velocity, relevant to local axis
            particleRB.AddForce(particleRB.transform.forward * startingVelocity, ForceMode2D.Impulse);
            var tempTrans = particleClone.gameObject.transform;
            tempTrans.eulerAngles = new Vector3(0, 0, 0);
            particleClone.gameObject.transform.eulerAngles = tempTrans.eulerAngles;


            //Add each particle rigidbody to the list
            particleRBs.Add(particleRB);


        
        


    }

    
    public void ParticleForceOverLifeTime()
    {
        foreach (Rigidbody2D particle in particleRBs)
        {
            if (particle != null)
            {
                particle.AddForce(particle.transform.up * velocityOverLiftime);
            }

            else
            {
                return;
            }
        }
    }

    public void ParticleRigidBodySettings()
    {

        foreach (Rigidbody2D particle in particleRBs)
        {
            if (particle != null)
            {

                particle.mass = rbMass;
                particle.gravityScale = rbGravity;
                particle.freezeRotation = rbFreezeRoation;
            }

            else
            {
                return;
            }

        }

    }



    private bool isFiring;
    public void FireSwitch(bool _bool)
    {
        isFiring = _bool;
    }

    IEnumerator SpawnParticles()
    {

        if (isFiring)
        {
            yield return new WaitForSeconds(spawnDelay);
            ParticleShoot();
            yield return StartCoroutine(SpawnParticles());
        }
    }




    //
    // Start and Update
    //

    private void Start()
    {
        //StartCoroutine(SpawnParticles());

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
        //StartCoroutine(SpawnParticles());
    }


    private CustomParticleCollision particleScriptRef;

    public void ParticleLifeTime()
    {
        foreach (GameObject particle in aliveParticles)
        {

            if (particle != null)
            {
                particleScriptRef = particle.GetComponent<CustomParticleCollision>();
                //Run methods attached to each particle to adjust appearance.Minimise use of .GetComponent
                particleScriptRef.SetParticleLifeTime(particleMaxLifeTime);
            }

            else
            {
                return;
            }


        }


    }
    
    public void RemoveFromLists(GameObject particle, Rigidbody2D particleRB)
    {


        aliveParticles.Remove(particle);
        particleRBs.Remove(particleRB);



    }

    /// <summary>
    /// Resets lists when called
    /// </summary>
    public void OnSetActive()
    {
        aliveParticles.Clear();
        particleRBs.Clear();
    }

}
