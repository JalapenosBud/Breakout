using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 6 brick types, 0-5
/*
 *  GREEN,
    YELLOW,
    ORANGE,
    RED,
    PURPLE,
    BLUE
*/


public class BrickBounds : MonoBehaviour {

    public List<GameObject> bricks = new List<GameObject>();

    //public int numberOfEachTypeBrick;
    public int bricksHorizontally;
    public int bricksVertically;

    public GameObject brickGo;
    private void Start()
    {
        GameLevelManager.Restart += GameLevelManager_Reset_Bricks;

        //Spawn bricks
        SpawnBricks();
        
    }

    private void SpawnBricks()
    {
        GameObject tempObj = new GameObject();

        for (int y = 0; y < bricksVertically; y++)
        {
            for (int x = 0; x < bricksHorizontally; x++)
            {
                tempObj = Instantiate(brickGo, new Vector2(transform.position.x + x * .75f, transform.position.y + y * .5f), transform.rotation);
                bricks.Add(tempObj);
                if (y == 0 || y == 1)
                {
                    brickGo.GetComponent<Brick>().SetBrick(tempObj, BrickTypes.GREEN);
                    //UtilityThings.ChooseColor(tempObj, BrickTypes.GREEN);
                }
                if (y == 2 || y == 3)
                {
                    brickGo.GetComponent<Brick>().SetBrick(tempObj, BrickTypes.YELLOW);
                }
                if (y == 4 || y == 5)
                {
                    brickGo.GetComponent<Brick>().SetBrick(tempObj, BrickTypes.ORANGE);
                }
                if (y == 6 || y == 7)
                {
                    brickGo.GetComponent<Brick>().SetBrick(tempObj, BrickTypes.RED);
                }
                if (y == 8 || y == 9)
                {
                    brickGo.GetComponent<Brick>().SetBrick(tempObj, BrickTypes.PURPLE);
                }
                if (y == 10 || y == 11)
                {
                    brickGo.GetComponent<Brick>().SetBrick(tempObj, BrickTypes.BLUE);
                }
            }
        }
        //print(bricks.Count.ToString());
    }

    void PutMissingBricksBack()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            if(!bricks[i].activeInHierarchy)
            {
                bricks[i].SetActive(true);
            }
        }
    }

    private void GameLevelManager_Reset_Bricks()
    {
        PutMissingBricksBack();
    }
}
