using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    Rigidbody2D _rigidbody;
    public float _thrust = 20f;
    public float torque;

    public Vector3 startPosition;

    private Vector2 _screenBounds;
    private float _objectWidth;
    private float _objectHeight;

    private Vector3 originalPositon;
    
    public delegate void BallHitHandler();
    public event BallHitHandler BallHit;

    public delegate void GameOverHandler();
    public event GameOverHandler GameOverEvent;

    // Start is called before the first frame update
    void Start()
    {        
        Debug.Log(startPosition);
        originalPositon = startPosition;
        _screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    public void StartGame()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        gameObject.SetActive(true);
        _rigidbody.simulated = true;
        transform.position = startPosition;        
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;        
        transform.position = viewPos;

        if ((transform.position.x + _objectWidth) > _screenBounds.x)
        {            
            transform.position = new Vector3(_screenBounds.x + _objectWidth, transform.position.y, transform.position.z);
            BounceX(_screenBounds.x);
        }
        else if ((transform.position.x - _objectWidth) < _screenBounds.x * -1)
        {         
            transform.position = new Vector3((_screenBounds.x * -1) - _objectWidth, transform.position.y, transform.position.z);
            BounceX(_screenBounds.x * -1);
        }
        else if (transform.position.y - _objectHeight < (_screenBounds.y * -1))
        {            
            GameOver();
        }
    }
   

    public void BounceX(float boundary)
    {        
        Vector3 position = transform.position;
        Vector3 velocity = _rigidbody.velocity;
        float durationAfterBounce = (position.x - boundary) / velocity.x;
        position.x = 2f * boundary - position.x;
        velocity.x = -velocity.x;
        
        transform.position = position;
        _rigidbody.velocity = velocity;
        /*EmitBounceParticles(
            boundary,
            position.y - velocity.y * durationAfterBounce,
            boundary < 0f ? 90f : 270f
        );*/
    }

    private void OnMouseDown()
    {
        var mouseVectorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        float turn = Input.GetAxis("Horizontal");
        
        Vector3 goalVector = ((mouseVectorPosition - transform.position).normalized) * -1;
        _rigidbody.AddTorque( torque );
        _rigidbody.AddForce(new Vector2(goalVector.y, goalVector.z) * _thrust);
        BallHit?.Invoke();
    }


    private void GameOver()
    {
        GameOverEvent?.Invoke();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = 0;
        _rigidbody.simulated = false;
    }
}
