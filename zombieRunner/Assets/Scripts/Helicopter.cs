using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    private bool called = false;
    private Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void OnDispatchHelicopter()
    {
        print("Heli called");
        called = true;
        rigidBody.velocity = new Vector3(0,0,50f);
    }
}
