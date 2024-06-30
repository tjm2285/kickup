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
    public void SetText(string text)
    {
        _text.text = text;
    }

    public void RestartPressed()
    {
        RestartButtonPressed?.Invoke();
    }
}
