using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    private PlayerController controllerRef;
    private Animator parentAnim;
    private Rigidbody2D parentRb;
    private GameObject playerObject;

    public bool grounded;

    // Use this for initialization
    void Start()
    {


        controllerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        parentAnim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        parentRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");



    }

    // Update is called once per frame
    void Update()
    {
       // RayCastForward();
        RayCastDown();
        GroundSwitch();
    }


    void GroundSwitch()
    {

        if (grounded)
        {
            //Land Animation
            parentAnim.SetBool("landed", true);
            //Ground Check True
            bool hitGround = true;
            controllerRef.GroundCheckBool(hitGround);
            //Set the ground as the parent
           // playerObject.transform.SetParent(hit.collider.transform.parent);
        }

        if (!grounded)
        { 
            parentAnim.SetBool("landed", false);
            bool hitGround = false;
            controllerRef.GroundCheckBool(hitGround);
            playerObject.transform.parent = null;
        }
    }

    public float rayDistance, rayBegin, xOffset;
    public RaycastHit2D hit;
    public RaycastHit2D hit2;
    public RaycastHit2D hit3;

    /// <summary>
    /// Raycasts below the player, returns null if no ground is detected (needs a layer mask)
    /// </summary>
    public bool RayCastDown()
    {
        var rayStart = new Vector3(transform.position.x, transform.position.y - rayBegin);

        hit = Physics2D.Raycast(rayStart, Vector2.down, rayDistance);

        var rayStart2 = new Vector3(transform.position.x + xOffset, transform.position.y - rayBegin);
        hit2 = Physics2D.Raycast(rayStart2, Vector2.down, rayDistance);

        var rayStart3 = new Vector3(transform.position.x - xOffset, transform.position.y - rayBegin);
        hit3 = Physics2D.Raycast(rayStart3, Vector2.down, rayDistance);

        if (hit.collider != null || hit2.collider != null || hit3.collider != null)
        {
            parentAnim.SetBool("landed", true);
            grounded = true;
            Debug.DrawRay(rayStart, Vector3.down, Color.blue);
            Debug.DrawRay(rayStart2, Vector3.down, Color.blue);
            Debug.DrawRay(rayStart3, Vector3.down, Color.blue);
        }

        if (hit.collider == null && hit2.collider == null && hit3.collider == null)
        {
            parentAnim.SetBool("landed", false);
            grounded = false;
            Debug.DrawRay(rayStart, Vector3.down, Color.red);
            Debug.DrawRay(rayStart2, Vector3.down, Color.red);
            Debug.DrawRay(rayStart3, Vector3.down, Color.red);
        }

        return grounded;

    }



    public float forwardRayStart = 1, forwardRayHeight;
    public bool RayCastForward()
    {

        var parentTransform = this.transform;
        var rayStart = new Vector3(transform.position.x + forwardRayStart, transform.position.y - forwardRayHeight);
        RaycastHit2D forwardHit = Physics2D.Raycast(rayStart, Vector2.right, rayDistance);

        if (parentTransform.localScale.x < 0)
        {
            rayStart = new Vector3(transform.position.x - forwardRayStart, transform.position.y - forwardRayHeight);
            forwardHit = Physics2D.Raycast(rayStart, Vector2.left, rayDistance);
        }

        if (forwardHit.collider != null)
        {
            if (forwardHit.collider.gameObject.CompareTag("Wall"))
            {
                Debug.DrawRay(rayStart, Vector3.down, Color.blue);
                return true;
            }

            return false;
        }

        if (forwardHit.collider == null)
        {
            Debug.DrawRay(rayStart, Vector3.down, Color.red);
            return false;
        }

        return false;



    }


}
