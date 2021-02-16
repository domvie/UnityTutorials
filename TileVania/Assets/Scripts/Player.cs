using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 30f;
    [SerializeField] float climbSpeed = 6f;

    Rigidbody2D rigidBody;
    Animator animator;
    Collider2D collider;
    float gravityScaleAtStart;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        gravityScaleAtStart = rigidBody.gravityScale;  
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
    }

    private void Run() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1 to 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("running", playerHasHorizontalSpeed);
    }

    private void Jump() {
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder() {
        if(!collider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            animator.SetBool("climbing", false);
            rigidBody.gravityScale = gravityScaleAtStart;
            return;
        }
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, controlThrow * climbSpeed);
        rigidBody.velocity = climbVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("climbing", playerHasHorizontalSpeed);
        rigidBody.gravityScale = 0;
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f);
        }
    }
}
