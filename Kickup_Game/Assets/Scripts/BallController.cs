using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Thrust = 20f;

    public GameObject _lowerBound;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
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
               
                m_Rigidbody.AddForce(transform.up * m_Thrust);
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
