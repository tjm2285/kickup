using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boundries : MonoBehaviour
{
    private Vector2 _screenBounds;
    private float _objectWidth;
    private float _objectHeight;
    [SerializeField]
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x + _objectWidth, _screenBounds.x * -1 - _objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y + _objectHeight, _screenBounds.y * -1 - _objectHeight);
        transform.position = viewPos;
    }
}
