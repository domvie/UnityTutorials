using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Vector3 launchVelocity;
    Rigidbody rigidBody;
    AudioSource audioSource;
    public bool inPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    public void Launch(Vector3 velocity)
    {
        inPlay = true;
        rigidBody.useGravity = true;
        rigidBody.velocity = velocity;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}
