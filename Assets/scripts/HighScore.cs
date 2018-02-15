using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore {

    private int score;
    private string name;
    private string time;

    public HighScore(int score, string name, string time)
    {
        this.score = score;
        this.name = name;
        this.time = time;
    }

    public override string ToString()
    {
        return name + "," + score + "," + time;
    }

}
