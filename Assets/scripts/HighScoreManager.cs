using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class HighScoreManager{

    public List<HighScore> allHighScores = new List<HighScore>();

    //path to where to save the txt file
    private string path = "Assets/Resources/HighScore.txt";

    public void SaveHighScore(HighScore highScore)
    {
        //creating new streamwriter object

        //if (!File.Exists(path))
        //{
        //    File.CreateText(path);
        //}
        

        StreamWriter writer = new StreamWriter(path, true);
        //writing input
        writer.WriteLine(highScore);
        //closing the streamwriter
        writer.Close();

        //re-import file to update ref in editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load(path);

    }

    public void LoadHighScore()
    {
        //init object
        StreamReader reader = new StreamReader(path);

        string[] tempArr = new string[3];
        while(!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            line.Split(',');
            for (int i = 0; i < tempArr.Length; i++)
            {
                tempArr[i] = line;
            }
            Debug.Log(line);
        }


        //read data
        //reader.ReadLine

        //close reader
        reader.Close();
    }
}
