using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour
{
    public Vector3 Velocity;
    public AudioSource maudio;
    public float speed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Velocity * speed;

        if(Velocity != Vector3.zero && maudio.isPlaying)
        {
            maudio.Play();
        }
        else if (Velocity == Vector3.zero)
        {
            maudio.Stop();
        }
    }
}
