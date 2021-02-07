using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    public float standingThreshold = 0.1f;
    public float distanceToRaise = 40f;

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool IsStanding() {
        // Vector3 rotationInEuler = transform.rotation.eulerAngles;

        // float tiltInX = Mathf.Abs(rotationInEuler.x);
        // float tiltInZ = Mathf.Abs(rotationInEuler.z);

        // print(name + " " + tiltInX + " " + tiltInZ);

        // if (tiltInX < standingThreshold && tiltInZ < standingThreshold) {
        //     return true;
        // }
        // return false;
        if (Mathf.Abs(transform.up.y) < standingThreshold) {
            // print(name + " IS STANDING: " + Mathf.Abs(transform.up.y) + " < " + standingThreshold);
            return true;
        }
        return false;
    }

    public void RaiseIfStanding()
    {
        if (IsStanding())
        {
            rigidBody.useGravity = false;
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
        }
    }

    public void Lower()
    {
        transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
        rigidBody.useGravity = true;
    }
}
