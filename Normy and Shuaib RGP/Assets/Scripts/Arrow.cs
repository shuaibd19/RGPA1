using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script dictates the arrow behaviour
/// </summary>
public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private bool hitSomething = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void FixedUpdate()
    {
        if (!hitSomething)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "arrow")
        {
            hitSomething = true;
        }
    }
}
