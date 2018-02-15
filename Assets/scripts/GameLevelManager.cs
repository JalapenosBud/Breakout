using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelManager : MonoBehaviour {

    //text objects
    public Text livesLeft;
    public Text gameOverText;
    public Text gameTimerText;

    //buttons
    public Button gameOverButton;
    public Button saveHighScoreButton;

    //go's
    public GameObject inputfieldpanel;

    //data fields
    public int lives;
    public float gameTimer = 0;
    public bool isTimerRunning;
    public bool gameOver = false;

    //delegate and event for when reaching 0 lives
    public delegate void HittingZero();
    public static event HittingZero WeAreDone;

    //delegate and event for when "restarting" the game
    public delegate void RestartGame();
    public static event RestartGame Restart;

    //highscore manager class ref
    HighScoreManager scoreManager;

    private void Start()
    {
        livesLeft.text = "LIVES LEFT: " + lives;
        //buttons and text disabled when game starts
        gameOverText.gameObject.SetActive(false);
        gameOverButton.gameObject.SetActive(false);
        saveHighScoreButton.gameObject.SetActive(false);
        inputfieldpanel.gameObject.SetActive(false);
        //8. subscribe to the event in the ball class with a method
        Ball.DecreaseLife += Ball_DecreaseLife;
        isTimerRunning = true;
        gameOver = false;
        //subscribe to event that happens when resetting the ball
        Ball.ResettingTheBall += Ball_ResettingTheBall;
        WeAreDone += GameLevelManager_WeAreDone;
        //init scoremanager
        scoreManager = new HighScoreManager();
    }

    //this fires when lives are below 0
    private void GameLevelManager_WeAreDone()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverButton.gameObject.SetActive(true);
        saveHighScoreButton.gameObject.SetActive(true);
        isTimerRunning = false;
        gameOver = false;
    }

    private void Ball_ResettingTheBall()
    {
        lives--;
        livesLeft.text = "LIVES LEFT: " + lives;
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    gameOver = true;
        //}

        if(isTimerRunning)
        {
            gameTimer += Time.deltaTime;
            gameTimerText.text = string.Format("Time: {0:#.00}", gameTimer);
        }

        if(lives < 1 && gameOver)
        {
            print("printing now");
            HittingZero hittingZero = WeAreDone;
            if(hittingZero != null)
            {
                WeAreDone();
                //enable gameover text, button and save high score button
                GameLevelManager_WeAreDone();
            }
        }
    }

    /*private void OnDisable()
    {
        Ball.DecreaseLife -= Ball_DecreaseLife;
    }*/


    private void Ball_DecreaseLife()
    {
        //9. decrease life, print out new life
        lives--;
        livesLeft.text = "LIVES LEFT: " + lives;
        if(lives < 1)
        {
            gameOver = true;
        }
        Debug.Log("Hit game over");
    }
    /*
     * turn off timer
     * disable gameovertext
     * disable gameoverbutton
     * reset score to 0
     */
     //this method is on the RESTARTGAME button and restarts the game
    public void LetsRestartTheGameMethod()
    {
        RestartGame restartGame = Restart;
        if(restartGame != null)
        {
            //deactivate the gameover UI
            gameOverText.gameObject.SetActive(false);
            gameOverButton.gameObject.SetActive(false);
            saveHighScoreButton.gameObject.SetActive(false);
            inputfieldpanel.SetActive(false);
            //reset gametimer
            gameTimer = 0f;
            gameTimerText.text = "Time: ";
            //run the timer again
            isTimerRunning = true;
            //reset the score
            Score.instance.currentScore = 0;
            Score.instance.scoreText.text = "SCORE: ";
            //put lives back to 3 so the game can play
            lives = 3;
            livesLeft.text = "LIVES LEFT: " + lives;
            //fire the restart event
            Restart();
        }
    }

    public void ButtonProgressToInputField()
    {
        print("progress to save");
        gameOverButton.gameObject.SetActive(false);
        saveHighScoreButton.gameObject.SetActive(false);
        //input field panel
        inputfieldpanel.gameObject.SetActive(true);
        inputfieldpanel.GetComponentInChildren<InputField>().text = "";

    }

    /// <summary>
    /// button event to save highscore
    /// </summary>
    public void SaveHighScore()
    {
        //checks if gameobject is deactivated
        if(inputfieldpanel.GetComponentInChildren<InputField>() != null)
        {
            //if there is no input in the text field, return, ie dont do anything
            if(string.IsNullOrEmpty(inputfieldpanel.GetComponentInChildren<InputField>().text))
            {
                print("is empty");
                return;
            }
            else //else if there is input, set a temp name just in case, get highscore, add to list and print
            {
                print("works");
                var name = "blank";
                name = inputfieldpanel.GetComponentInChildren<InputField>().text;
                var currentHighScore = new HighScore(Score.instance.currentScore, name, string.Format("{0:#.00}", gameTimer));
                scoreManager.allHighScores.Add(currentHighScore);
                //still need to save the actual highscore to a document
                scoreManager.SaveHighScore(currentHighScore);
                print(currentHighScore);
                inputfieldpanel.SetActive(false);
                gameOverButton.gameObject.SetActive(true);
            }
            
        }
        
    }


}
