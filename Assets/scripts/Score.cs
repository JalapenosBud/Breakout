using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static Score instance;
    public int currentScore;

    public Text scoreText;

    private void Awake()
    {
        instance = this;
    }

    public void CalculcateScore(BrickTypes brickTypes)
    {
        if(brickTypes == BrickTypes.GREEN)
        {
            AddScore(100);
        }
        if(brickTypes == BrickTypes.YELLOW)
        {
            AddScore(250);
        }

        if (brickTypes == BrickTypes.ORANGE)
        {
            AddScore(500);
        }
        if (brickTypes == BrickTypes.RED)
        {
            AddScore(500);
        }
        if (brickTypes == BrickTypes.PURPLE)
        {
            AddScore(1000);
        }
        if (brickTypes == BrickTypes.BLUE)
        {
            AddScore(1500);
        }


    }

    private void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = "SCORE: " + currentScore;
    }

}
