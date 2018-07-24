using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCollision : MonoBehaviour {

    //standarise these variable names
    private PlayerHitPhysics playerPhysicsRef;
    private Image playerHealthBar;
    [SerializeField]
    private GameObject hitParticle, deathParticle, blockParticle;
    [SerializeField]
    private float playerHP = 100;

    public float bulletDmg = 10;


    private void Start()
    {
        playerHealthBar = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<Image>();
    }


    /// <summary>
    /// Removes HP by float dmg, Player Death if HP = 0
    /// </summary>
    /// <param name="dmg"></param>
    public void PlayerTakeDamage(float dmg)
    {

        playerHP -= dmg;

        if (playerHP <= 0)
        {

            Instantiate(deathParticle, transform.position, Quaternion.identity);
            playerHP = 100;
            UpdateHealthBar();
        }

        if (playerHP > 0)
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            UpdateHealthBar();
        }

    }
    public float TESTESTspeed = 2;
    private void UpdateHealthBar()
    {
        float normalizedHP = playerHP / 100;
  

        //if (playerHealthBar.fillAmount != normalizedHP)
        //{

        // playerHealthBar.fillAmount = Mathf.Lerp(playerHealthBar.fillAmount, normalizedHP, 0.5f * Time.deltaTime);
        playerHealthBar.DOFillAmount(normalizedHP, TESTESTspeed);
       // }
    }

    public void BlockCollision(Vector3 hitPos)
    {

        GameObject particleClone = Instantiate(blockParticle, hitPos, Quaternion.identity);

    }


}






