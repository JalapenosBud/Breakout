using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour 
{
    public GameObject padResetPoint;
	public float boxSize = 2f;
	public LayerMask WhatToHit;
	Rigidbody2D rb2d;
	public float moveSpeed = 5f;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
        //sub to events
        Ball.DecreaseLife += Ball_DecreaseLife;
        GameLevelManager.WeAreDone += Disable_movement_and_physics;
        GameLevelManager.Restart += GameLevelManager_Restart;

	}

    private void GameLevelManager_Restart()
    {
        gameObject.SetActive(true);
        rb2d.gameObject.SetActive(true);
    }

    private void Disable_movement_and_physics()
    {
        rb2d.gameObject.SetActive(false);
    }

    private void Ball_DecreaseLife()
    {
        this.transform.position = padResetPoint.transform.position;
    }

    void Update()
	{
		MovePad ();

	}

    /*private void OnDisable()
    {
        Ball.DecreaseLife -= Ball_DecreaseLife;
        GameLevelManager.WeAreDone -= Disable_movement_and_physics;
        GameLevelManager.Restart -= GameLevelManager_Restart;
    }*/

    void MovePad()
	{
		var move = Input.GetKey (KeyCode.LeftShift) ? moveSpeed = 10f : moveSpeed = 5f;

		rb2d.velocity = new Vector2 (Input.GetAxis("Horizontal") * moveSpeed, 0);
	}

    #region wastecodeatm
    /*
     *
	 *make box or circlecast above pad collision
	 *check if ball is in either side of box
	 *check for what side its coming from and flip velocity 
	 
    //TODO:
    //half the boxes
    void OnDrawGizmos()
	{
		//this aligns to the left side of the pad
		Gizmos.DrawWireCube ((transform.position + Vector3.up * 0.75f) + ((Vector3.left * 2f )/ 2f), new Vector3 (2f,1f,0));
		//this alins to the right side of the pad
		Gizmos.DrawWireCube((transform.position + Vector3.up * 0.75f) + ((Vector3.right * 2f) / 2f), new Vector3(2f, 1f, 0));

    var leftBoxToCheck = Physics2D.BoxCast ((transform.position + Vector3.up * 0.75f) + ((Vector3.left * 2f) / 2f), new Vector3(2f, 1f, 0), 0, Vector2.up, 1f, WhatToHit);
		var rightBoxToCheck = Physics2D.BoxCast ((transform.position + Vector3.up * 0.75f) + ((Vector3.right * 2f) / 2f), new Vector3(2f, 1f, 0), 0, Vector2.up, 1f, WhatToHit);
		if (leftBoxToCheck) 
		{
			//print ("hit left box");
            hitInsideBox = true;

        }
        else
        {
            hitInsideBox = false;
        }

		if (rightBoxToCheck) 
		{
			//print ("hit right box");
            hitInsideBox = true;
        }
        else
        {
            hitInsideBox = false;
        }




	}*/
    #endregion
}
