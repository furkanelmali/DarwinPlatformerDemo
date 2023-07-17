using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    public float runSpeed = 10f;
    public float jumpSpeed = 5f;
    public float climbSpeed = 5f;
    Rigidbody2D rb;
    Animator myAnim;
  
    CapsuleCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Climbing();
        isinAir();
        
       
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;            
        }
        if(value.isPressed)
        {
           
            rb.velocity += new Vector2(0f,jumpSpeed);
             bool playerHasSpeedVertical = Mathf.Abs(rb.velocity.y) > 0;
            myAnim.SetBool("Jumping",playerHasSpeedVertical);
        }

        
    }

    
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed,rb.velocity.y);
        rb.velocity = playerVelocity;
        bool playerHasSpeedHorizontal = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("İsRunning",playerHasSpeedHorizontal);
    }

    void FlipSprite()
    {
        bool playerHasSpeedHorizontal = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if(playerHasSpeedHorizontal)
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x)*0.7f,0.7f);
    }

 

    void isinAir()
    {
        if(rb.velocity.y < 0)
        {
            myAnim.SetBool("Falling",true);
            myAnim.SetBool("Jumping",false);
        }
        else if(rb.velocity.y == 0)
        {
            myAnim.SetBool("Falling",false);

        }
    }

    void Climbing()
    {
        if(!myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = 8;
            myAnim.SetBool("isClimbing",false);
            return;
        }
        else
        {
            myAnim.SetBool("Jumping",false);
            rb.gravityScale = 0;
        }

        if(moveInput.y > 0 || moveInput.y < 0)
        {
            myAnim.SetBool("isClimbing",true);
        }
        else
        {
            myAnim.SetBool("isClimbing",false);
        }
         Vector2 climbVelocity = new Vector2(rb.velocity.x,moveInput.y * climbSpeed);
         rb.velocity = climbVelocity;
         
    }
}
