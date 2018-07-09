using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    private PlayerController playerScript;
    private Animator parentAnim;
    private Rigidbody2D parentRb;
    private GameObject playerObject;

    public bool grounded;

    // Use this for initialization
    void Start()
    {


        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        parentAnim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        parentRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");



    }

    // Update is called once per frame
    void Update()
    {
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
            playerScript.GroundCheckBool(hitGround);
            //Set the ground as the parent
            playerObject.transform.SetParent(hit.collider.transform.parent);
        }

        if (!grounded)
        { 
            parentAnim.SetBool("landed", false);
            bool hitGround = false;
            playerScript.GroundCheckBool(hitGround);
            playerObject.transform.parent = null;
        }
    }

    public float rayDistance, rayBegin;
    public RaycastHit2D hit;

    /// <summary>
    /// Raycasts below the player, returns null if no ground is detected (needs a layer mask)
    /// </summary>
    public bool RayCastDown()
    {
        var rayStart = new Vector3(transform.position.x, transform.position.y - rayBegin);

        hit = Physics2D.Raycast(rayStart, Vector2.down, rayDistance);

        if (hit.collider != null)
        {
            parentAnim.SetBool("landed", true);
            grounded = true;
            Debug.DrawRay(rayStart, Vector3.down, Color.blue);
        }

        if (hit.collider == null)
        {
            parentAnim.SetBool("landed", false);
            grounded = false;
            Debug.DrawRay(rayStart, Vector3.down, Color.red);
        }

        return grounded;

    }


}
