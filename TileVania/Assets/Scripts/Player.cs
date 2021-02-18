using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 30f;
    [SerializeField] float climbSpeed = 6f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    Rigidbody2D rigidBody;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feet;
    float gravityScaleAtStart;
    bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidBody.gravityScale;  
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) {
            return;
        }
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Die();
    }

    private void Run() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1 to 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("running", playerHasHorizontalSpeed);
    }

    private void Jump() {
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die() {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) {
            animator.SetTrigger("die");
            rigidBody.velocity = deathKick;
            isAlive = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void ClimbLadder() {
        if(!feet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
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
