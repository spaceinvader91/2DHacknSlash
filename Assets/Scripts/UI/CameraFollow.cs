using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float dampTime = 0.15f, slowedDamp = 1.5f, snapTolerance = 2f, smoothSpeed = 1f;
    private float currentDampTime;
    public Vector3 offset = new Vector3(0.5f, 0.2f, 0);

    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        ReduceSnap();
        FollowPlayer();

    
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
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, currentDampTime);
            }

            else if (target.localScale == new Vector3(-1f, 1f, 1f))
            {
                destination = destination + -offset;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, currentDampTime);
            }
        }
    }

    void ReduceSnap()
    {

        if(Vector2.Distance(this.transform.position, target.position) > snapTolerance)
        {

            currentDampTime = Mathf.Lerp(currentDampTime, dampTime, smoothSpeed * Time.deltaTime);

        }

        else
        {
            currentDampTime = Mathf.Lerp(currentDampTime, slowedDamp, smoothSpeed * Time.deltaTime);
        }

    }
}


//change particle source ref from particle component to paticle controller component