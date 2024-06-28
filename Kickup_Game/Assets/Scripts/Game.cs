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

    private int _score = 0;
    // Start is called before the first frame update
    void Start()
    {
        _startScreen.StartPressedChanged += StartGame;
    }

    private void StartGame()
    {
        _startScreen.transform.gameObject.SetActive(false);
        _ball.StartGame();
        _ball.BallHit += IncrementScore;
    }

    private void IncrementScore()
    {
        _score++;
        _gameUI.SetText(_score.ToString());
    }

    private void OnTest()
    {
        
    }

    private void OnDisable()
    {
        _startScreen.StartPressedChanged -= StartGame;
    }
}
