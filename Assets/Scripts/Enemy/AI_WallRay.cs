using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   

public class AI_WallRay : MonoBehaviour {


    private RaycastHit2D hit, forwardHit;
    private Transform objectT;
    public float yOrigin, rayDistance, xOrigin = 1;
    public float xOffset = 0.5f;




    private void Update()
    {
        //PlatformRayCast();
        ForwardWallCast();
    }

    public bool ForwardWallCast()
    {
        var rayStart = new Vector2(transform.position.x - xOffset, transform.position.y);
        var rayDir =  Vector2.right;


        if (this.transform.localScale.x < 0)
        {
             rayStart = new Vector2(transform.position.x + xOffset, transform.position.y );
             rayDir =  Vector2.left;
        }

        forwardHit = Physics2D.Raycast(rayStart, rayDir, rayDistance);

        if (forwardHit.collider != null)
        {
            if (forwardHit.collider.gameObject.CompareTag("Ground"))
            {
                Debug.DrawRay(rayStart, rayDir, Color.blue);
                return true;
            }

            return false;
        }


        else
        {
            Debug.DrawRay(rayStart, rayDir, Color.black);

            return false;
        }


    }

    public bool PlatformRayCast()
    {
        var rayStart = new Vector2(transform.position.x - yOrigin, transform.position.y + yOrigin);
        var rayDir = Vector2.up + Vector2.left;

        if(this.transform.localScale.x == -1)
        {
            rayStart = new Vector2(transform.position.x + yOrigin, transform.position.y + yOrigin);
            rayDir = Vector2.up + Vector2.right;
        }

        hit = Physics2D.Raycast(rayStart, rayDir, rayDistance);


        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                Debug.DrawRay(rayStart, rayDir, Color.blue);
                return true;
            }

            return false;
        }


        else
        {
            Debug.DrawRay(rayStart, rayDir, Color.black);

            return false;
        }

        

    }

}
