using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float force = 50f;
    private Rigidbody2D rb2d;
    private CircleCollider2D coll;
    BrickBounds brickBounds;
    public GameObject attachpoint;
    public bool isBallInAir;

    //1. declare delegate loselife
    public delegate void LoseLife();
    //2. delcare static event from delegate
    public static event LoseLife DecreaseLife;

    //delegate and event for when resetting ball with space
    public delegate void ResetBall();
    public static event ResetBall ResettingTheBall;

    //timer
    public float startShootBallTimer;
    
    /// <summary>
    /// the method
    /// </summary>
    public void ResettingBall()
    {
        ResetBall resetBall = ResettingTheBall;
        if(resetBall != null)
        {
            ResettingTheBall();
        }
    }

    public void ChangeLife()
    {
        ResetBallAndTimer();
        //3. create temporary delegate and assign it to the event
        LoseLife loseLife = DecreaseLife;
        //4. if tmp not null
        if(loseLife != null)
        {
            //5. call the event
            DecreaseLife();
        }

    }

    private void Start()
    {
        brickBounds = FindObjectOfType<BrickBounds>();
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        //here balls attaches to point sitting on the pad
        AttachBallToPad();
        isBallInAir = false;

        //sub to events
        GameLevelManager.WeAreDone += Disable_movement_and_physics;
        GameLevelManager.Restart += GameLevelManager_Restart;
    }

    void ResetBallAndTimer()
    {
        isBallInAir = false;
        startShootBallTimer = 0f;
        AttachBallToPad();
    }

    private void GameLevelManager_Restart()
    {
        rb2d.gameObject.SetActive(true);
    }

    private void Disable_movement_and_physics()
    {
        rb2d.gameObject.SetActive(false);
    }

    /*private void OnDisable()
    {
        GameLevelManager.WeAreDone -= Disable_movement_and_physics;
        GameLevelManager.Restart -= GameLevelManager_Restart;
    }*/

    void AttachBallToPad()
    {
        transform.position = attachpoint.transform.position;
        transform.SetParent(attachpoint.transform);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isBallInAir)
        {
            isBallInAir = true;
            transform.SetParent(null);
            rb2d.AddForce(new Vector2(0, force));
            
            
        }
        else if(isBallInAir)
        {
            
            startShootBallTimer += Time.deltaTime;
            //BUG: sometimes you can press space double time and the ball just floats
            if(Input.GetKeyDown(KeyCode.Space) && startShootBallTimer > 1f)
            {
                ResetBallAndTimer();
                ResettingTheBall();
            }
        }

        //start THIS WORKS, DONT MESS AROUND WITH IT
        if (transform.parent == null)
        {
            return;
        }
        else if (transform.parent.name == "attachpoint" && transform.parent != null)
        {
            this.transform.position = attachpoint.transform.position;
        }
        //end THIS WORKS

    }

    bool CanStartShootAgain()
    {
        if(startShootBallTimer < 1f)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    /*
 * layers:
 * 8 pad
 * 9 ball
 * 10 brick
 * 11 bounds
 * 12 gameoverbounds
 */
    private void OnCollisionEnter2D(Collision2D col)
    {
        //pad
        if(col.gameObject.layer == 8)
        {
            rb2d.AddForce(new Vector2(0, force));

            //UtilityThings.GetCollisionAngle(col.transform, coll, col.contacts[0].point);
        }
        //brick
        if(col.gameObject.layer == 10)
        {
            var whatBrickType = col.gameObject.GetComponent<Brick>().brickTypes;

            Score.instance.CalculcateScore(whatBrickType);

            //if (col.gameObject.GetComponent<Brick>().brickTypes == BrickTypes.GREEN)
            //{
            //    Score.instance.CalculcateScore(BrickTypes.GREEN);
            //}

            //Destroy(col.gameObject);

            //instead of destroying we set it to inactive
            col.gameObject.SetActive(false);

            rb2d.AddForce(new Vector2(0, -force));

        }
        if(col.gameObject.layer == 11)
        {
            //right and flip to left
            if(rb2d.velocity.x > 0)
            {
                rb2d.AddForce(new Vector2(force * -1,force));
            }
            //left and flip to right
            else if (rb2d.velocity.x < 0)
            {
                rb2d.AddForce(new Vector2(force * -1, force));
            }
            //up and flip to down
            else if (rb2d.velocity.y > 0)
            {
                rb2d.AddForce(new Vector2(force, force * -1));
            }
            //down and flip to up for now
            else if (rb2d.velocity.y < 0)
            {
                rb2d.AddForce(new Vector2(force, force * -1));
            }
        }
        if(col.gameObject.layer == 12)
        {
            //6. method with events inside, that gets called when colliding with bottom layer
            //7. shit works!
            ChangeLife();
        }

    }



}
