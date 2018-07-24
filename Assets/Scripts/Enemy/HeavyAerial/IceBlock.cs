using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceBlock : MonoBehaviour {

    private Transform iceBlock;
    public float meltDelay = 2, meltTime = 5f;

	// Use this for initialization
	void Start () {

        iceBlock = transform;
    }
	
	// Update is called once per frame
	void Update () {

        if (iceBlock.localScale.y == 0.1f)
        {
            Destroy(gameObject);
        }

        if(iceBlock.localScale.y <= 0.8f)
        {
            iceBlock.DOScaleX(0, meltTime);
        }
    }



    IEnumerator MeltIceBlock()
    {
        yield return new WaitForSecondsRealtime(meltDelay);

        iceBlock.DOScaleY(0.1f, meltTime);


    }


    public GameObject iceShatterParticle;
    private bool grounded;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.CompareTag("Ground"))
        {
            grounded = true;
            StartCoroutine(MeltIceBlock());
        }

        if (hitObject.CompareTag("Player"))
        {

            if (!grounded)
            {
                var shatterParticle = Instantiate(iceShatterParticle, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
       
    }


    public void TEST_SetActive()
    {
        this.gameObject.SetActive(true);
    }

}
