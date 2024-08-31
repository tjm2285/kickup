using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    public delegate void RestartButtonHandler();
    public event RestartButtonHandler RestartButtonPressed;

    public delegate void ReturnButtonHandler();
    public event ReturnButtonHandler ReturnButtonPressed;
    public void SetText(string text)
    {
        _text.text = text;
    }

    public void RestartPressed()
    {
        RestartButtonPressed?.Invoke();
    }

    public void ReturnPressed()
    {
        ReturnButtonPressed?.Invoke();
    }
}
