using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum GameState { GS_PAUSEMENU, GS_GAME, GS_LEVELCOMPLETED, GS_GAME_OVER }
public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.GS_PAUSEMENU;
    public static GameManager instance;
    public Canvas inGameCanvas;

    public TMP_Text scoreText;
    private int score = 0;

    public Image[] keysTab;
    private int keysFound = 0;

    public Image[] livesTab;
    private int lives = 3;
    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
    public void AddKeys(Color color)
    {
        keysTab[keysFound].color = color;
        keysFound++;
    }
    public void AddLive()
    {
        livesTab[lives].enabled = true;
        lives++;
    }
    public void RemoveLive()
    {
        livesTab[lives - 1].enabled = false;
        lives--;
    }
    public int GetLives()
    {
        return lives;
    }
    private void Awake()
    {
        instance = this;
        scoreText.text = score.ToString();
        for (int i = 0; i < keysTab.Length; i++)
        {
            keysTab[i].color = Color.grey;
        }
/*        for (int i = 0; i < livesTab.Length; i++)
        {
            livesTab[i].enabled = false;
        }
        for (int i = 0; i < lives; i++)
        {
            livesTab[i].enabled = true;
        }*/
    }

    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        if (currentGameState == GameState.GS_GAME)
        {
            inGameCanvas.enabled = true;
        }
        else
        {
            inGameCanvas.enabled = false;
        }
    }
    public void PauseMenu()
    {
        currentGameState = GameState.GS_PAUSEMENU;
    }
    public void InGame()
    {
        currentGameState = GameState.GS_GAME;
    }
    public void LevelCompleted()
    {
        currentGameState = GameState.GS_LEVELCOMPLETED;
    }
    public void GameOver()
    {
        currentGameState = GameState.GS_GAME_OVER;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentGameState)
            {
                case GameState.GS_PAUSEMENU:
                    {
                        InGame();
                        break;
                    }
                case GameState.GS_GAME:
                    {
                        PauseMenu();
                        break;

                    }
                default:
            break;
            }
        }
    }
}