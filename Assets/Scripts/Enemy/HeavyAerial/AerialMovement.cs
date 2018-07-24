using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AerialMovement : MonoBehaviour {

    public Rigidbody2D rb;
    public float  moveForce = 1f;

	// Use this for initialization
	void Start () {


        playerChar = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {


    
	}

    private void FixedUpdate()
    {
  
    }

    

    /// <summary>
    /// Call to Hover to desired height
    /// </summary>
    public void Hover(float maxHeight, float upwardsForce)
    {

        if (transform.position.y < maxHeight)
        {


            rb.AddRelativeForce(Vector3.up * (rb.mass * upwardsForce));
        }

        if (transform.position.y > maxHeight + 0.2f )
        {
            print("too high");
            rb.AddRelativeForce(Vector3.down * (-rb.velocity.y /2));
        }

        if(transform.position.y < maxHeight - 0.2f)
        {
            print("too low");
            rb.AddRelativeForce(Vector3.up * (-rb.velocity.y*3));
        }





    }

    void MoveLeftRight()
    {
        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector2.left * moveForce);
        }        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector2.right * moveForce);
        }
    }



   public void MoveRight(float moveSpeed)
    {
 
        transform.localScale = new Vector3(1f, 1f, 1f);
        rb.AddForce(Vector2.right * moveSpeed);
    }

    public void MoveLeft(float moveSpeed)
    {

        transform.localScale = new Vector3(-1f, 1f, 1f);
        rb.AddForce(Vector2.left * moveSpeed);
    }

    public void MoveUp()
    {
        
    }


    public void LimitMoveSpeed(float maxMoveSpeedY, float maxMoveSpeedX)
    {
        if(rb.velocity.y > maxMoveSpeedY)
        {
            rb.AddRelativeForce(Vector2.up * -rb.velocity.y);
        }

        if (rb.velocity.x > maxMoveSpeedX)
        {
            rb.AddRelativeForce(Vector2.right * -rb.velocity.x);
        }
    }

    private float angle, angleLeft, distanceToPlayer;

    public Transform playerChar;
    private bool playerInRange, playerFound;

    /// <summary>
    /// Returns true if player is in range and fov
    /// </summary>
    /// <returns></returns>
    public bool FindPlayer(float range, float fov)
    {
        //Straight Line distance
        
        distanceToPlayer = Vector3.Distance((Vector2)transform.position, (Vector2)playerChar.position);

        //The Angle the player is at
        angle = Vector2.Angle(Vector3.right, playerChar.position - transform.position);
        angleLeft = Vector2.Angle(Vector3.left, playerChar.position - transform.position);


        //If the player is within range
        if (distanceToPlayer < range)  //range = known float
        {

            //if the player is within the FOV angle set, starts at Vector3.Right
            if (angle < fov * 0.5f || angleLeft < fov * 0.5f) //fov = known float
            {
                //Face Player
                FacePlayer();
                playerInRange = true;

                playerFound = true;

            }

        }

        else
        {
            playerInRange = false;

        }
        return playerInRange;

    }


    //Search below the enemy to find the player and attack

    public float downRayOffset = 0.5f;
    public bool FindPlayerBelow()
    {
        var downRay = new Vector2(transform.position.x, transform.position.y - downRayOffset);
        var downHit = Physics2D.Raycast(downRay, Vector2.down, Mathf.Infinity);


        if (downHit.collider != null)
        {
            if (downHit.collider.gameObject.CompareTag("Player"))
            {


                Debug.DrawRay(downRay, Vector3.down, Color.green);
                return true;
            }
            Debug.DrawRay(downRay, Vector3.down, Color.red);
            return false;
        }

        else
        {
            Debug.DrawRay(downRay, Vector3.down, Color.red);
            return false;
        }

    }

    public float scalingValue = 1;


    public void FacePlayer()
    {
        if (playerChar.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(scalingValue, scalingValue, 1);
        }

        if (playerChar.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-scalingValue, scalingValue, 1);
        }


    }

}
