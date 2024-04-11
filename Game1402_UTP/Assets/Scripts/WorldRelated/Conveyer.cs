using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Conveyer : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = rb.position;
        rb.position += Vector3.back * speed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }
}
