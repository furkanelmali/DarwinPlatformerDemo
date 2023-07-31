using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;   
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

  
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed,0);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        FlipEnemyFacing();
        moveSpeed = -moveSpeed;
        
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)) , 1f);
    }
}
