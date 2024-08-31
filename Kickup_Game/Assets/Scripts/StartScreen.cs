using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{

    public delegate void StartScreenButtonHandler(string levelName);
    public event StartScreenButtonHandler StartPressedChanged;    
    // Start is called before the first frame update

    public void Level1Pressed()
    {
        StartPressedChanged?.Invoke("FieldScene");
    }
    public void Level2Pressed()
    {
        StartPressedChanged?.Invoke("BeachScene");
    }
}
