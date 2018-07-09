using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunJump : MonoBehaviour {

    //Cache
    [SerializeField]
    private Transform playerT;
    private Animator playerAnim;
    public Rigidbody2D playerRB;

    //Run Variables
    private float
        dPadHorizontalAxis;

    public float
        runSpeed = 6f;

    //Jump Variables
    [SerializeField]
    private float jumpForce = 4f;


    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }


    public void RunRight()
    {
        playerAnim.SetBool("isRunning", true);
        playerT.localScale = new Vector3(1f, 1f, 1f);
        playerT.Translate(Vector2.right * runSpeed * Time.deltaTime);

    }

    public void RunLeft()
    {
        playerAnim.SetBool("isRunning", true);

        playerT.localScale = new Vector3(-1f, 1f, 1f);
        playerT.Translate(Vector2.left * runSpeed * Time.deltaTime);
    }

    public void RunStop()
    {
        playerAnim.SetBool("isRunning", false);
    }

    public void Jump()
    {
        playerAnim.SetTrigger("jump");
       // playerRB.gravityScale = 1;
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
}
