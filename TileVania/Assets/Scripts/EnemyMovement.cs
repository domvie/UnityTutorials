using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight()) {
            rigidbody.velocity = new Vector2(moveSpeed, 0f);
        } else {
            rigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    bool IsFacingRight() {
        return transform.localScale.x > 0;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody.velocity.x)), -1f);
    }
}
