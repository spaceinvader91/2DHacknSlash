using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float dampTime = 0.15f, slowedDamp = 1.5f, smoothSpeed = 1f, maxCamSpeed = 10f;
    private float currentDampTime;
    public Vector3 offset = new Vector3(0.5f, 0.2f, 0);

    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Camera cam;


    // Update is called once per frame

    private void Start()
    {
  
    }
    void Update()
    {
        //ReduceSnap();
        FollowPlayer();
       // CapCamSpeed();
    
    }

    void CapCamSpeed()
    {

        print(cam.velocity.x);
        float camSpeedX = cam.velocity.x;
        if(camSpeedX >= maxCamSpeed)
        {

        }


    }



    void FollowPlayer()
    {
        if (target)
        {
            Vector3 point = cam.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));

            Vector3 destination = transform.position + delta;

            if (target.localScale == new Vector3(1f, 1f, 1f))
            {
                destination = destination + offset;
               destination = new Vector3(destination.x, offset.y + destination.y, -10);
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }

            else if (target.localScale == new Vector3(-1f, 1f, 1f))
            {
                destination = destination + -offset;
                destination = new Vector3(destination.x, offset.y + destination.y, -10);
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
        }
    }

    void ReduceSnap()
    {

        //print(Vector2.Distance(transform.position, target.position));
        //print(cam.velocity.x);
        float camSpeedX = cam.velocity.x;

        if (camSpeedX >= maxCamSpeed || camSpeedX <= -maxCamSpeed)
        {
            print("true");
           // currentDampTime = Mathf.Lerp(currentDampTime, slowedDamp, smoothSpeed * Time.deltaTime);
            currentDampTime = slowedDamp;

        }




        else
        {
            //currentDampTime = Mathf.Lerp(currentDampTime, dampTime, smoothSpeed * Time.deltaTime);
            currentDampTime = dampTime;
        }

    }
}


//change particle source ref from particle component to paticle controller component