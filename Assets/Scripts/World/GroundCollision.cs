using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform other = collision.transform;

        if (other.gameObject.CompareTag("Bullets"))
        {
            GroundHit(other);
            Destroy(other.gameObject);
        }

    }


    public GameObject groundDecal;

    void GroundHit(Transform hitTrans)
    {
        GameObject groundDecalClone = Instantiate(groundDecal, hitTrans.position, hitTrans.rotation);
    }

}
