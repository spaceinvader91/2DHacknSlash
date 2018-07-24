using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class DashToTarget : MonoBehaviour {


    public Transform dashTarget;
    public Transform playerTransform;



    private GroundDetection groundRef;
    private Animator anim;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashMeterMax = 100;
    [SerializeField]
    private float currentDashMeter;
    [SerializeField]
    private float dashCost = 20;

    private Image dashBar;

    private void Start()
    {
        groundRef = playerTransform.GetComponent<GroundDetection>();
        anim = playerTransform.GetComponentInChildren<Animator>();
        dashBar = GameObject.FindGameObjectWithTag("DashUI").GetComponent<Image>();

        currentDashMeter = dashMeterMax;
    }

    private void Update()
    {
        //Dash();
        RefillMeter();
      
    }

   // hold RB to dash
   /// <summary>
   /// Dash from player position to target
   /// </summary>
   /// <param name="target"></param>
    public void Dash(Vector3 target)
    {

            if (currentDashMeter >= dashCost)
            {

                //calculate angle between player and dash point
                var angle = Vector3.Angle(playerTransform.position, target);
                Vector3 cross = Vector3.Cross(playerTransform.position, target);

                //  Vector3 dir = Quaternion.Euler(target.x, target.y, target.z) ;

   

                anim.SetBool("landed", false);


                DashMeter(-dashCost);
           }
        



     
    }


    /// <summary>
    /// Add _float to the current dash meter 
    /// </summary>
    /// <param name="_float"></param>
    /// 
    public void DashMeter(float _float)
    {

        currentDashMeter += _float;

       // _float = Mathf.Lerp(currentDashMeter, currentDashMeter + _float, 0.5f * Time.deltaTime);

        dashBar.fillAmount = currentDashMeter / 100;


    }

    void RefillMeter()
    {

        if (Input.GetKey(GameManager.GM.yButton))
        {
            currentDashMeter += 2f;
            dashBar.fillAmount = currentDashMeter / 100;
        }
    }

    //dash on button held
    //Time the button press
    private float timer = 0f, buttonHold = 0.2f;
    private bool longPress;


    /// <summary>
    /// Returns TRUE if RB is held for longer than 0.2s
    /// </summary>
    /// <returns></returns>
    /// 
    public bool RB_LongPress()
    {



        timer += Time.deltaTime;


        if (Input.GetKeyUp(GameManager.GM.rbButton) && timer <= buttonHold)
        {
            timer = 0;
            longPress = false;
        }

        if (Input.GetKeyUp(GameManager.GM.rbButton) && timer > buttonHold)
        {
            timer = 0;
            longPress = true;
        }

        return longPress;

    }





}
