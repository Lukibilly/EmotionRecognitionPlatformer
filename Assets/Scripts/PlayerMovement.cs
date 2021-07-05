using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float runSpeed = 40f;
    [SerializeField] float jumpPower = 300;

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
    {   if(GameController.instance.gameStarted){
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if(Input.GetButton("Jump")){
                jump = true;
            }
        }
    }

    void FixedUpdate(){
        if(GameController.instance.gameStarted){
            isGrounded = GroundCheck();
            if(canMove) Move(horizontalMove);
            jump = false;
        }        
    }

    private bool GroundCheck(){
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }
    void Move(float dir){
        if(isGrounded && jump){
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x,jumpPower);                    
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
    public void setCanMove(bool can){
        if(can) canMove=true;
        else canMove=false;
    }
}
