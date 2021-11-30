using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScreen;

    public Text scoreText;
    public int Score;

   public void UpdateLives(int currentLives)
    {
        Debug.Log("player lives: " + currentLives);
        
        livesImageDisplay.sprite = lives[currentLives];
    }
    
    public void UpdateScore()
    {
        Score += 10;
        
        scoreText.text = "Score: " + Score;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";
    }
}
