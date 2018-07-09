using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour {

    private int comboCount;
    private Animator comboAnim;


 

    //UI declarations
    public GameObject CanvasGameObject;
    private Text countDisplay;
    private SpriteRenderer sRenderer;

  

    private void Start()
    {
        //switch off on start HERE

        comboAnim = CanvasGameObject.GetComponentInChildren<Animator>();

        countDisplay = CanvasGameObject.GetComponentInChildren<Text>();
        sRenderer = CanvasGameObject.GetComponentInChildren<SpriteRenderer>();

        //StartCoroutine(TextFade());
        CanvasGameObject.SetActive(false);
    }


    //use timer to reset combo counter

    
    public float comboTimeOut = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject other = collision.gameObject;

        if (other.CompareTag("Enemy"))
        {
            StopAllCoroutines();
            comboCount++;
            StartCoroutine(ComboTimeOut());
            

            if (comboCount >= 2)
            {
                CanvasGameObject.SetActive(true);
                //Turn the text opacity to 100%
                Color c = sRenderer.material.color;
                Color t = countDisplay.color;
                c.a = 1f;
                t.a = 1f;
                sRenderer.material.color = c;
                countDisplay.color = t;


                comboAnim.SetTrigger("comboHitShake");
                countDisplay.text = "x " + comboCount;


            }

        }
    }



   

    IEnumerator ComboTimeOut()
    {
        
        float currCountdownValue = comboTimeOut;

        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        
        }

        if (currCountdownValue <= 0)
        {
            comboCount = 0;
           StartCoroutine(TextFade());
            yield return null;
            
        }
    }



    IEnumerator TextFade()
    {

            comboCount = 0;

            Color c = sRenderer.material.color;
            Color t = countDisplay.color;

            //Fade the text opacity to 0%
            for (float f = c.a; f >= 0; f -= 0.2f)
            {
                t.a = f;
                c.a = f;

                if (c.a == 0)
                {
                    CanvasGameObject.SetActive(false);
                }

                countDisplay.color = t;
                sRenderer.material.color = c;


                yield return null;
            }
        
    }



    //public void IdleCheck(bool isIdle)
    //{

    //    if (isIdle)
    //    {
    //        print("idle trigger");
    //        StartCoroutine(DelayTextRoutine());
            
    //        comboCount = 0;
    //        print("Combo Ended");
    //        //CanvasGameObject.SetActive(false);
    //    }

    //}

}
