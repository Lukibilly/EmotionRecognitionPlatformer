using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform groundCheckSide;
    public float runSpeed = 40f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpPower = 300;

    float groundCheckRadius = 0.2f;
    float horizontalMove = 0f;
    bool jump = false;
    bool facingright = true;
    bool isGrounded = false;
    bool canMove = true;

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
        /* LOSE LIFE SAMPLE ------------------------------
        if(Input.GetKeyDown(KeyCode.Return)){            
            gameObject.GetComponent<LifeCount>().LoseLife();
        }
        --------------------------------------------------*/
    }

    void FixedUpdate(){
        GroundCheck();
        if(canMove) Move(horizontalMove);
        jump = false;
    }

    void GroundCheck(){
        Collider2D[] collidersDown = Physics2D.OverlapCircleAll(groundCheck.position,groundCheckRadius,groundLayer);
        Collider2D[] collidersSide = Physics2D.OverlapCircleAll(groundCheckSide.position,groundCheckRadius,groundLayer);
        if(collidersDown.Length>0 || collidersSide.Length>0){
            isGrounded = true;
        }else{
            isGrounded = false;
        }
        
            
    }
    void Move(float dir){
        if(isGrounded && jump){
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x,jumpPower);
            //rb.AddForce(new Vector2(0f,jumpPower));                        
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
