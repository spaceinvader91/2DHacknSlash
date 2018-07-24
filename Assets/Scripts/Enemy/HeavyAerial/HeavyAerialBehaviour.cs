using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAerialBehaviour : MonoBehaviour {

    private AerialMovement movementRef;
    public Transform playerTransform;
	// Use this for initialization
	void Start () {

        movementRef = GetComponent<AerialMovement>();
        playerTransform = movementRef.playerChar;
	}
	
	// Update is called once per frame
	void Update () {

        PauseGame();
		
	}

    public float maxHeight, upwardsForce, maxMoveSpeedY, maxMoveSpeedX;
    public float range, fov;
    private void FixedUpdate()
    {
        movementRef.Hover(maxHeight, upwardsForce);
        movementRef.LimitMoveSpeed(maxMoveSpeedY, maxMoveSpeedX);

        

        if(!movementRef.FindPlayer(range, fov))
        {
            Patrol();
        }

        else
        {
            Chase();
        }
    }


    public float start, des, moveSpeed;

    void Patrol()
    {
        if (transform.position.x <= start)
        {
            movementRef.MoveRight(moveSpeed);
        }
        if (transform.position.x >= des)
        {
            movementRef.MoveLeft(moveSpeed);
        }

    }

    void Chase()
    {

        //check player direction.
        //move in front of player,
        //drop block
        var playerLocalScaleX = movementRef.playerChar.localScale.x;
        var playerPos = movementRef.playerChar.position;
       


        //Player facing right
        if (playerLocalScaleX > 0)
        {
            //dest = + units in front of player relative to height from player?

            var newPos = new Vector2(playerPos.x + 0.5f, maxHeight);
            MoveToPosition(newPos);

        }
        if (playerLocalScaleX < 0)
        {
            //dest = + units in front of player relative to height from player?

            var newPos = new Vector2(playerPos.x - 0.5f, maxHeight);
            MoveToPosition(newPos);

        }


    }

    public float repositionSpeed = 5f;
    void MoveToPosition(Vector2 newPos)
    {
        var currentPos = transform.position;
        var playerPos = movementRef.playerChar.position;
        var distToPlayer = playerPos.x - transform.position.x;
        var xAxis = newPos.x;
        var yAxis = newPos.y;
        var moveRight = false;


        if(xAxis > currentPos.x)
        {
            moveRight = true;
        }


        //Reposition X Axis
        if (moveRight)
        {
           
            print("Reposition Right");
            movementRef.MoveRight(repositionSpeed);

            if (currentPos.x >= xAxis + 0.5f)
            {
                movementRef.MoveLeft(movementRef.rb.velocity.x * 4);
            }

        }

        if (!moveRight)
        {
            print("Reposition Left");
            movementRef.MoveLeft(repositionSpeed);

            if (currentPos.x <= xAxis - 0.5f)
            {
                movementRef.MoveRight(movementRef.rb.velocity.x * 4);
            }
        }
        //Reposition Y Axis

        maxHeight = yAxis;



    }


    public GameObject iceblock;
    void IceBlockAttack()
    {

        if (movementRef.FindPlayerBelow())
        {

            if (spawnedBlocks.Count == 0)
            {

                Invoke("SpawnBomb", 0);
            }

           

        }


    }

    public List<GameObject> spawnedBlocks;

    void SpawnBomb()
    {
        GameObject iceBlockClone = Instantiate(iceblock, transform.position, Quaternion.identity);
        spawnedBlocks.Add(iceBlockClone);
        StartCoroutine(ResetBombList());

        var iceScript = iceBlockClone.GetComponent<IceBlock>();
        iceScript.TEST_SetActive();
        Debug.Log("ice block attack");

    }

    IEnumerator ResetBombList()
    {
       


        for (int i = 0; i < 4; i++)
        {
            GameObject iceBlockClone = Instantiate(iceblock, transform.position, Quaternion.identity);
            spawnedBlocks.Add(iceBlockClone);
            var iceScript = iceBlockClone.GetComponent<IceBlock>();
            iceScript.TEST_SetActive();

            yield return new WaitForSeconds(0.7f);
        }

        yield return new WaitForSeconds(2);
        spawnedBlocks.Clear();

        yield return null;
    }



    public GameObject explosionParticle;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            var particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


    void PauseGame()
    {
        if (Input.GetKeyDown(GameManager.GM.startButton) && Time.timeScale == 1)
        {
            Time.timeScale = 0;
            print("pause");
        }
        else if (Input.GetKeyDown(GameManager.GM.startButton) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            print("unpause");
        }
    }
}
