using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Be Sure To Include This
using DG.Tweening;

public class LockOnControls : MonoBehaviour
{
    Camera cam; //Main Camera
    LockOnFinder target; //Current Focused Enemy In List
    Image image;//Image Of Crosshair


    bool lockedOn;//Keeps Track Of Lock On Status    

    //Tracks Which Enemy In List Is Current Target
    int lockedEnemy;

    //List of nearby enemies
    public static List<LockOnFinder> nearByEnemies = new List<LockOnFinder>();

    public DashToTarget dashRef;
    public float crosshairSpeed = 2f;


    void Start()
    {
        cam = Camera.main;
        image = gameObject.GetComponent<Image>();
        dashRef = GameObject.FindGameObjectWithTag("PlayerReferences").GetComponent<DashToTarget>();

        lockedOn = false;
        lockedEnemy = 0;
        image.enabled = false;
    }


    

    void Update()
    {
        
        //Press RS  To Lock On
        if (Input.GetKeyDown(GameManager.GM.rsClick) && !lockedOn)
        {
            if (nearByEnemies.Count >= 1)
            {
                lockedOn = true;
                image.enabled = true;

                //Lock On To First Enemy In List By Default
                lockedEnemy = 0;
                target = nearByEnemies[lockedEnemy];
            }
        }
        //Turn Off Lock On When RS Is Pressed Or No More Enemies Are In The List
        else if ((Input.GetKeyDown(GameManager.GM.rsClick) && lockedOn) || nearByEnemies.Count == 0)
        {
            lockedOn = false;
            image.enabled = false;
            lockedEnemy = 0;
            target = null;
        }

        //Tap RB To Switch Targets
        if (Input.GetKey(GameManager.GM.rbButton))

        {
            Time.timeScale = 0.5f;

            if (Input.GetKeyDown(GameManager.GM.xButton))
            {

                if (lockedEnemy == nearByEnemies.Count - 1)
                {
                    //If End Of List Has Been Reached, Start Over
                    lockedEnemy = 0;
                    target = nearByEnemies[lockedEnemy];
                }
                else
                {
                    //Move To Next Enemy In List
                    lockedEnemy++;
                    target = nearByEnemies[lockedEnemy];
                }
            }
        }

        if (Input.GetKeyUp(GameManager.GM.rbButton))
        {
            Time.timeScale = 1;
        }

        if (lockedOn)
        {
            target = nearByEnemies[lockedEnemy];



            //Determine Crosshair Location Based On The Current Target
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.transform.position, crosshairSpeed * Time.deltaTime);

            

            //Rotate Crosshair
            gameObject.transform.Rotate(new Vector3(0, 0, -1));


            // use double tap attack to dash?
            //rb  = dash to target
            if (Input.GetKeyDown(GameManager.GM.aButton))
            {
               
                var targetPos = new Vector3(target.transform.position.x - 0.5f, target.transform.position.y);
                print("dash" + targetPos);
                dashRef.Dash(targetPos);
            }


        }
    }
}
