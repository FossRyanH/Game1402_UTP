using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleBall : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float speed = 5.0f;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.isKinematic = true; //ObstacleBall is not affected by other objects
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        StartCoroutine(MoveBall());
    }

    IEnumerator MoveBall()
    {
        while (true)
        {
            _rb?.MovePosition(_rb.position + -Vector3.forward * speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
