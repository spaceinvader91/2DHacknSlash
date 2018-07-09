using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float dampTime = 0.15f;
    public Vector3 offset = new Vector3(0.5f, 0.2f, 0);

    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = cam.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            
            Vector3 destination = transform.position + delta;

            if (target.localScale == new Vector3(1f, 1f, 1f))
            {
                destination = destination + offset;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }

            else if (target.localScale == new Vector3(-1f, 1f, 1f))
            {
                destination = destination + -offset;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
        }

    }
}