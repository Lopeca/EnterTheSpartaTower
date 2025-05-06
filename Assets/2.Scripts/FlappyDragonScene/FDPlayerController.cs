using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class FDPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;

    public float speed;
    public float jumpForce;
    public ParticleSystem deathEffect;
    public AudioClip deathSE;

    private bool isGrounded;
    bool isDead;
    private void Awake()
    {
        speed = 5f;
        jumpForce = 5f;
        isGrounded = false;
        isDead = false;
    }
    // Update is called once per frame
    void Update()
    {
        switch (FDGameManager.Instance.gameState){
            case FDGameState.Home:
                HandleHomeState();
                break;
            case FDGameState.Play:     
                HandlePlayState();
                break;
        }
        
    }

    private void HandleHomeState()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            FDGameManager.Instance.StartGame();
    }

    private void HandlePlayState()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (isGrounded)
            HandleJump();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (collision.CompareTag("Obstacle"))
        {
            Dead();
            FDGameManager.Instance.GameOver();
        }
        if (collision.CompareTag("Item"))
        {
            ICollectable collectable = collision.GetComponent<ICollectable>();
            collectable.Collect();
            
            if(collectable is FDCoin coin)
            {
                AudioManager.Instance.PlaySFX(coin.coinSE);
            }
        }
    }

    private void Dead()
    {
        rb.velocity = new Vector2(0, 0);
        sprite.color = new Color(0, 0, 0, 0);
        deathEffect.Play();
        isDead = true;
        AudioManager.Instance.PlaySFX(deathSE);
        Destroy(gameObject, 5f);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 directionalJumpForce = rb.gravityScale > 0 ? new Vector2(0, jumpForce) : new Vector2(0, -jumpForce);

            rb.AddForce(directionalJumpForce, ForceMode2D.Impulse);
            rb.gravityScale *= -1;


            sprite.flipY = !sprite.flipY;
            Vector3 spritePos = sprite.gameObject.transform.localPosition;
            spritePos.y *= -1;
            sprite.gameObject.transform.localPosition = spritePos;
        }
    }
}
