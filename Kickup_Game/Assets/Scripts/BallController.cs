using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody _rigidbody;
    public float _thrust = 20f;
    public float torque;
    public GameObject _lowerBound;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.name);

                //when you hit to hemishpere it shoots dwon
                var mouseVectorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                /* float movementForce  = 10000f;

                 Vector3 playerVectorPosition = transform.position;
                 Vector2 ForceVector = (mouseVectorPosition - playerVectorPosition).normalized;
                 _rigidbody.AddForce(ForceVector * movementForce * -1);*/

                float turn = Input.GetAxis("Horizontal");
                Debug.Log(turn);
                Vector3 goalVector = ((mouseVectorPosition - transform.position).normalized)*-1;
                Debug.Log(goalVector);
                _rigidbody.AddTorque(transform.up * torque * turn);
                _rigidbody.AddForce(new Vector2(goalVector.y, goalVector.z) * _thrust);
                
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _lowerBound)
        {
            Debug.Log("hit Ground");
        }
    }
}
