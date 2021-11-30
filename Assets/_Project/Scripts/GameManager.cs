using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true; 
    public GameObject Player;

    private UIManager _uiManager;

    public void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // if gameover is true --- if space key is pressed -> spawn the player ----- gameOver is false --- hide title screen

    private void Update()
    {
        if ( gameOver == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(Player, new Vector3(0,0,0), Quaternion.identity);
                gameOver = false;
                _uiManager.HideTitleScreen();
            }
        }
    }
}
