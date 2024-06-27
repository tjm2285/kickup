using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    public float _thrust = 20f;
    public float torque;
    public GameObject _lowerBound;
    public GameObject _leftBound;
    public GameObject _rightBound;
    Vector2 lastVelocity;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        lastVelocity = _rigidbody.velocity;
    }


    private void OnMouseDown()
    {
        Debug.Log("mousebuttondown");
        var mouseVectorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /* float movementForce  = 10000f;

         Vector3 playerVectorPosition = transform.position;
         Vector2 ForceVector = (mouseVectorPosition - playerVectorPosition).normalized;
         _rigidbody.AddForce(ForceVector * movementForce * -1);*/

        float turn = Input.GetAxis("Horizontal");
        Debug.Log(turn);
        Vector3 goalVector = ((mouseVectorPosition - transform.position).normalized) * -1;
        Debug.Log(goalVector);
        //_rigidbody.velocity = Vector3.zero;
        // _rigidbody.AddTorque(transform.up * torque * turn);
        _rigidbody.AddForce(new Vector2(goalVector.y, goalVector.z) * _thrust);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _lowerBound)
        {
            Debug.Log("hit Ground");
        }
       /* else if (collision.gameObject == _leftBound)
        {
            Debug.Log("hit left");
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized,
                                            collision.contacts[0].normal);

            _rigidbody.velocity = direction * Mathf.Max(speed, 2f);
        }
        else if (collision.gameObject == _rightBound)
        {
            Debug.Log("hit right");
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized,
                                            collision.contacts[0].normal);

            _rigidbody.velocity = direction * Mathf.Max(speed, 2f);
        }*/

    }
}
