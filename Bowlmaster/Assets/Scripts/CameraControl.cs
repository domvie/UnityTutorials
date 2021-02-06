using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Ball ball;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= 1829f)
        {
            transform.position = ball.transform.position + offset;
        }
    }
}
