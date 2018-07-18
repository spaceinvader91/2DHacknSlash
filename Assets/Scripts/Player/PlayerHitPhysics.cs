using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerHitPhysics : MonoBehaviour
{



    //Cache

    private Rigidbody2D objectRB;
    private GroundDetection groundDetectionRef;

    public float defaultGravity = 5;


    // Use this for initialization
    void Start()
    {

        objectRB = GetComponent<Rigidbody2D>();
        groundDetectionRef = GetComponent<GroundDetection>();

    }

    private void FixedUpdate()
    {
        GravitySimulation();
    }


    private float gravity = 10;
    /// <summary>
    /// Pushes the RB down by float 
    /// </summary>
    /// <param name="grav"></param>
    public void GravitySetting(float grav)
    {
        gravity = grav;

    }


    /// <summary>
    /// Knocks the RB right by float (Use AI.FacePlayer() to orientate before applying force)
    /// </summary>
    /// <param name="hitForce"></param>
    public void KnockBack(float hitForce)
    {

        if (objectRB.transform.localScale.x == -1)
        {

            objectRB.AddForce(Vector2.left * hitForce, ForceMode2D.Impulse);
        }

        if (objectRB.transform.localScale.x == 1)
        {

            objectRB.AddForce(Vector2.right * hitForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Knocks the RB UPwards by float
    /// </summary>
    /// <param name="hitForce"></param>
    public void KnockUp(float hitForce)
    {


        objectRB.AddRelativeForce(Vector2.up * hitForce, ForceMode2D.Impulse);


    }

    /// <summary>
    /// Knocks the RB DOWNwards by float
    /// </summary>
    /// <param name="hitForce"></param>
    public void KnockDown(float hitForce)
    {
        objectRB.AddForce(Vector2.down * hitForce, ForceMode2D.Impulse);
    }



    public IEnumerator FreezeTimeOnHit(float freezeTimer)
    {

        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(freezeTimer);
        Time.timeScale = 1;

    }

    private void GravitySimulation()
    {
        objectRB.AddForce(Vector3.down * gravity);


        if (groundDetectionRef.RayCastDown())
        {

            gravity = defaultGravity;
        }
    }

   // private void 
   
}





