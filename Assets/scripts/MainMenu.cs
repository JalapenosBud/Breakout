using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button PlayButton;
    public Button HighScoreButton;

	public void ShowHighScore()
    {
        //deactivate buttons so we can show the highscore
        //PlayButton.gameObject.SetActive(false);
        //HighScoreButton.gameObject.SetActive(false);
        GetHighScoreShow();
    }

    void GetHighScoreShow()
    {
        HighScoreManager highScoreManager = new HighScoreManager();
        highScoreManager.LoadHighScore();
    }

}
