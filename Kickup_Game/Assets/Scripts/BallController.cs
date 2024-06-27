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
    public GameObject _lowerBound;
    public GameObject _leftBound;
    public GameObject _rightBound;
    Vector2 lastVelocity;

    private Vector2 _screenBounds;
    private float _objectWidth;
    private float _objectHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;

        Debug.Log(_screenBounds);

    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    void FixedUpdate()
    {
       // lastVelocity = _rigidbody.velocity;


    }
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        //viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x + _objectWidth, _screenBounds.x * -1 - _objectWidth);
        //viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y + _objectHeight, _screenBounds.y * -1 - _objectHeight);
        transform.position = viewPos;

        if ((transform.position.x + _objectWidth) > _screenBounds.x)
        {            
            transform.position = new Vector3(_screenBounds.x + _objectWidth, transform.position.y);
            BounceX(_screenBounds.x);
        }
        else if ((transform.position.x - _objectWidth) < _screenBounds.x * -1)
        {         
            transform.position = new Vector3((_screenBounds.x * -1) - _objectWidth, transform.position.y);
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
        Debug.Log("mousebuttondown");
        var mouseVectorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       

        float turn = Input.GetAxis("Horizontal");
        Debug.Log(turn);
        Vector3 goalVector = ((mouseVectorPosition - transform.position).normalized) * -1;
        Debug.Log(goalVector);
       
        // _rigidbody.AddTorque(transform.up * torque * turn);
        _rigidbody.AddForce(new Vector2(goalVector.y, goalVector.z) * _thrust);
    }


    private void GameOver()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = 0;
        _rigidbody.simulated = false;
    }
}
