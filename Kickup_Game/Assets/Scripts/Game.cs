using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private StartScreen _startScreen;
    [SerializeField]
    private BallController _ball;
    [SerializeField]
    private GameUI _gameUI;

    [SerializeField]
    private GameOverUI _gameOverScreen;

    private int _score = 0;
    // Start is called before the first frame update
    void Start()
    {
        _startScreen.StartPressedChanged += StartGame;
        _gameOverScreen.RestartButtonPressed += RestartGame;
    }

    private void RestartGame()
    {
        _gameOverScreen.gameObject.SetActive(false);
        _score = 0;
        _gameUI.SetText(_score.ToString());
        _ball.StartGame();

    }

    private void StartGame()
    {
        _startScreen.gameObject.SetActive(false);
        _score = 0;
        _gameUI.SetText(_score.ToString());
        _ball.StartGame();
        _ball.BallHit += IncrementScore;
        _ball.GameOverEvent += HandleGameOver; 
    }

    private void HandleGameOver()
    {
        _gameOverScreen.gameObject.SetActive(true);
        _gameOverScreen.SetText(_score.ToString());
    }

    private void IncrementScore()
    {
        _score++;
        _gameUI.SetText(_score.ToString());
    }

    private void OnDisable()
    {
        _startScreen.StartPressedChanged -= StartGame;
    }
}
