using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {


    public List<GameObject> backgroundElements;
    public Camera mainCam;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CamDirection();
	}

    void CamDirection()
    {

        var cam_xVel = mainCam.velocity.x;
        var cam_yVel = mainCam.velocity.y;
       

        for (int i = 0; i < backgroundElements.Count; i++)
        {
            var backgroundPosZ = backgroundElements[i].transform.position.z;
            var newBackgroundPosX = (cam_xVel / 150) * (backgroundPosZ / 10);
            var newBackgroundPosY = (cam_yVel / 150) * (backgroundPosZ / 10);

            backgroundElements[i].transform.position = new Vector3(backgroundElements[i].transform.position.x - newBackgroundPosX, backgroundElements[i].transform.position.y - newBackgroundPosY, backgroundElements[i].transform.position.z);



        }


    }


    
}
