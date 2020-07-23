using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Taller;
namespace Taller
{



public class CharacterSideScroller : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected CapsuleCollider2D capsuleCollider2D;

    public bool canMove = true;
    public float moveSpeed = 5f;
    public bool lockRotation = true;

    private bool bCanMove;

    private Vector2 moveDir;
    private Vector2 gravity;
    [Header("Jump Options")]
    public float jumpForce = 10f;
    public bool bCanJumpInAir = true;
    public int maxJumps = 1;
    public bool resetJumpsWhenIsNotInAir = true;
    public int jumpCount { get { return currentJumps; } }
    public int currentJumps;
    [Header("Gravity Options")]
    public LayerMask floorMask;
    bool bMustCheckHitTheFloor;

    public float gravityScale = 2f;

    public float extraFallGravity;
    public bool isInAir { get { return bIsInAir; } }
    private bool bIsInAir;
    public bool isFalling { get { return bIsfalling; } }
    private bool bIsfalling;
    Vector2 finalVelocity;


    protected virtual void Awake()
    {

        gravity = Physics2D.gravity;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        ResetJump();

        if (lockRotation)
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (capsuleCollider2D.IsTouchingLayers(floorMask))
        {
            bMustCheckHitTheFloor = true;
        }
    }

    public void ResetJump()
    {

        currentJumps = maxJumps;

    }
    public void Jump()
    {
        if (currentJumps <= 0) return;
        if (!bCanJumpInAir && bIsInAir)
            return;
        bIsInAir = true;
        bMustCheckHitTheFloor = false;
        currentJumps--;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
    public void AddMovementInput(Vector2 direction)
    {
        moveDir = direction;
    }
    

    void SetInFloor()
    {
        bMustCheckHitTheFloor = false;
        if (resetJumpsWhenIsNotInAir)
        {
            ResetJump();
        }
    }
    void CheckFloor()
    {
        if (Physics2D.CircleCast(transform.position, .25f, Vector2.down, Mathf.Abs(capsuleCollider2D.bounds.center.y), floorMask))
        {

            bIsInAir = false;
            if (bMustCheckHitTheFloor)
            {
                SetInFloor();
            }
        }
        else
        {
            bIsInAir = true;

        }


        if (bIsInAir)
        {

            bIsfalling = rb.velocity.y < 0;
        }

    }


    private void Move()
    {
        rb.gravityScale = gravityScale;
        if (!canMove) return;
        if (bIsInAir) return;
        finalVelocity = moveDir * moveSpeed;
        finalVelocity.y = rb.velocity.y;

        rb.velocity = finalVelocity;

    }

    private void AddExtraGravity()
    {
        if (!bIsInAir) return;

        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (rb.velocity.y < 0)
                rb.AddForce(Vector2.down * extraFallGravity);
        }
    }



    // Update is called once per frame
    protected virtual void Update()
    {
        CheckFloor();
        Move();
        AddExtraGravity();
    }
}
}