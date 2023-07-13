using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    public float runSpeed = 10f;
    Rigidbody2D rb;
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
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
}
