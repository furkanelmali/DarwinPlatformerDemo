using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] Vector2 deathKick;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    Rigidbody2D rb;
    Animator myAnim;

    bool isAlive;
  
    BoxCollider2D myFeetCollider;
    CapsuleCollider2D myBodyCollider;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive){return;}
        Run();
        FlipSprite();
        Climbing();
        isinAir();
        Die();
        
       
    }

    void OnMove(InputValue value)
    {
         if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
         if(!isAlive){return;}
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
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

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            rb.velocity = deathKick;
            isAlive = false;
        }
    }
}
