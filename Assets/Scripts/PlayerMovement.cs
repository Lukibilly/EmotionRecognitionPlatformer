using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] float runSpeed = 40f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpPower = 300;

    float groundCheckRadius = 0.2f;
    float horizontalMove = 0f;
    bool jump = false;
    bool facingright = true;
    bool isGrounded = false;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButton("Jump")){
            jump = true;
        }
    }

    void FixedUpdate(){
        Move(horizontalMove);
        GroundCheck();
    }

    void GroundCheck(){
        isGrounded = false;
        Collider2D[] collidersdown = Physics2D.OverlapCircleAll(groundCheck.position,groundCheckRadius,groundLayer);
        if(collidersdown.Length>0)
            isGrounded = true;
        Collider2D[] collidersleft = Physics2D.OverlapCircleAll(groundCheckLeft.position,groundCheckRadius,groundLayer);
        if(collidersleft.Length>0)
            isGrounded = true;
        Collider2D[] collidersright = Physics2D.OverlapCircleAll(groundCheckRight.position,groundCheckRadius,groundLayer);
        if(collidersright.Length>0)
            isGrounded = true;
    }
    void Move(float dir){
        if(isGrounded && jump){
            isGrounded = false;
            jump = false;
            rb.AddForce(new Vector2(0f,jumpPower));            
        }
        #region MoveLeftRight
        float xvelocity = dir*runSpeed*Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xvelocity,rb.velocity.y);
        rb.velocity = targetVelocity;

        if(facingright && dir<0){
            transform.localScale = new Vector3(-1,1,1);
            facingright = false;

        }else if(!facingright && dir>0){
            transform.localScale = new Vector3(1,1,1);
            facingright = true;
        }
        animator.SetFloat("xVelocity",Mathf.Abs(rb.velocity.x));
        #endregion
    }
}
