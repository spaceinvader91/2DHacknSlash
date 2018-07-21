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

        print("cam velocity = " + mainCam.velocity.x);

        var cam_xVel = mainCam.velocity.x;
       

        for (int i = 0; i < backgroundElements.Count; i++)
        {
            var backgroundPosZ = backgroundElements[i].transform.position.z;
            var newBackgroundPos = (cam_xVel / 150) * (backgroundPosZ/10 );

            backgroundElements[i].transform.position = new Vector3(backgroundElements[i].transform.position.x - newBackgroundPos, backgroundElements[i].transform.position.y, backgroundElements[i].transform.position.z);


        }


    }


    
}
