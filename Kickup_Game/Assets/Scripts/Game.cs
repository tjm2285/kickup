using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Game : MonoBehaviour
{
    [SerializeField]
    private StartScreen _startScreen;
    [SerializeField]
    private GameObject _gamePlay;
    [SerializeField]
    private GameUI _gameUI;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private GameOverUI _gameOverScreen;

    [SerializeField]
    private int _streakLength;

    private GameObject _gameplayScene;
    private BallController _ball;

    private int _score = 0;
    // Start is called before the first frame update
    void Start()
    {
        _startScreen.StartPressedChanged += StartGame;
        _gameOverScreen.RestartButtonPressed += RestartGame;
        _gameOverScreen.ReturnButtonPressed += ResetGame;
    }

    private void ResetGame()
    {
        GameObject.Destroy(_gameplayScene);
        _startScreen.gameObject.SetActive(true);
        _gameOverScreen.gameObject.SetActive(false);
    }

    private void RestartGame()
    {       
        Debug.Log("RestartGame");
        _gameOverScreen.gameObject.SetActive(false);
        _score = 0;
        _gameUI.SetText(_score.ToString());
        _ball.StartGame(_camera);
        _ball.GameOverEvent += HandleGameOver;
    }

    private void StartGame(string levelName)
    {
        Addressables.LoadAssetAsync<GameObject>(levelName).Completed += OnLoadDone;        
    }
    private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        // In a production environment, you should add exception handling to catch scenarios such as a null result.
        _gameplayScene = GameObject.Instantiate( obj.Result);
        _gameplayScene.transform.SetParent(_gamePlay.transform);
        _startScreen.gameObject.SetActive(false);
        _score = 0;
        _gameUI.SetText(_score.ToString());

        _ball = _gameplayScene.GetComponentInChildren<BallController>();
        
        _ball.StartGame(_camera);
        _ball.BallHit += IncrementScore;
        _ball.GameOverEvent += HandleGameOver;
    }
    private void HandleGameOver()
    {
        Debug.Log("HandleGameOver");
        _gameOverScreen.gameObject.SetActive(true);
        _gameOverScreen.SetText(_score.ToString());
        _ball.GameOverEvent -= HandleGameOver;
    }

    private void IncrementScore()
    {
        _score++;
        _gameUI.SetText(_score.ToString());
        if(_score > _streakLength)
        {
            _ball.StartStreak();
        }
    }

    private void OnDisable()
    {
        _startScreen.StartPressedChanged -= StartGame;
    }
}
