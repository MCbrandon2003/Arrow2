using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject t1;
    public GameObject t2;

    private int totalScore;
    private int previousScore;
    private Color scoreColor = Color.white;

    void Start()
    {
        totalScore = 0;
        previousScore = 0;
    }

    IEnumerator ChangeColorBack()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1);

        // Change the color back to white
        scoreColor = Color.white;
    }

    void Update()
    {
        int t1Score = t1.GetComponent<Target>().scores;
        int t2Score = t2.GetComponent<Target>().scores;

        previousScore = totalScore;
        totalScore = t1Score + t2Score;

        if (totalScore != previousScore)
        {
            // Change the score color to red when the score changes
            scoreColor = Color.red;

            // Call a coroutine to change the color back to white after 1 second
            StartCoroutine(ChangeColorBack());
        }
    }

    void OnGUI()
    {
        // Set the GUI color to scoreColor
        GUI.color = scoreColor;

        // Create a new GUIStyle
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 30; // Set the font size to 30

        // Draw the score label with the new GUIStyle
        GUI.Label(new Rect(10, 10, 200, 60), "Score: " + totalScore, style);
    }
}