using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Idle = 1,
    Move = 1 << 1,
    Dash = 1 << 2,
    Knockback = 1 << 3
}
public class PlayerController : MonoBehaviour
{
    private static readonly int isMoving = Animator.StringToHash("IsMove");

    private PlayerStatHandler playerStatHandler;
    public PlayerStatHandler PlayerStatHandler {  get { return playerStatHandler; } }

    private Vector2 moveDirection;
    public bool IsMoving => moveDirection != Vector2.zero;

    [SerializeField] private Vector2 lookDirection;
    [SerializeField] private PlayerState playerState;

    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public Animator animator;
    public AudioClip damagedSE;
    public ParticleSystem deathEffect;

    IDash dash;
    public InteractHandler interactHandler;

    public bool moveControlLocked;
    bool isInvincible;
    bool isDead;
    float remainedInvincibleTime;
    void Awake()
    {
        playerStatHandler = GetComponent<PlayerStatHandler>();
        rb = GetComponent<Rigidbody2D>();
        playerState = PlayerState.Idle;

        dash = GetComponent<IDash>();
        dash.Init();

        moveControlLocked = false;
        isInvincible = false;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        
        // 위치에 따른 렌더링 순서 조정
        sprite.sortingOrder = GameUtility.SortByPos(transform.position.y);

        HandleInvincible();
        
        if (!moveControlLocked)
        {
            Move();
        }
    }

    private void HandleInvincible()
    {
        if (!isInvincible) return;

        if(remainedInvincibleTime <= 0)
        {
            Color color = sprite.color;
            color.a = 1f;
            sprite.color = color;
            isInvincible = false;
        }
        else
        {
            float invincibleAlpha = Mathf.PingPong(remainedInvincibleTime, 0.5f);
            invincibleAlpha += 0.3f;
            Color color = sprite.color;
            color.a = invincibleAlpha;
            sprite.color = color;
            remainedInvincibleTime -= Time.deltaTime;
        }
    }

    private void Move()
    {
        rb.velocity = moveDirection * playerStatHandler.playerStatData.MoveSpeed;

        if (rb.velocity != Vector2.zero)
        {
            playerState = PlayerState.Move;
            animator.SetBool(isMoving, true);
        }
        else
        {
            playerState = PlayerState.Idle;
            animator.SetBool(isMoving, false);
        }
    }

    public void ChangeState(PlayerState state)
    {
        this.playerState = state;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        if (isInvincible) return;
        playerStatHandler.TakeDamage(damage);
       
        AudioManager.Instance.PlaySFX(damagedSE);
        if (playerStatHandler.CurrentHP == 0)
        {
            Die();
        }
        else
        {
            ApplyKnockback();
        }
    }

    private void Die()
    {
        deathEffect.gameObject.SetActive(true);
        deathEffect.Play();
        isDead = true;
        Color c = sprite.color;
        c.a = 0;
        sprite.color = c;
        GameManager.Instance.GameOver();

    }

    private void ApplyKnockback()
    {
        remainedInvincibleTime = 1f;
        isInvincible = true;
        rb.velocity = -lookDirection * 5;
        moveControlLocked = true;
        StartCoroutine(UnlockMoveControl(0.1f));
    }

   IEnumerator UnlockMoveControl(float duration)
    {
        yield return new WaitForSeconds(duration);
        moveControlLocked = false;
    }

    private void OnMove(InputValue inputValue)
    {
        moveDirection = inputValue.Get<Vector2>().normalized;   
    }

    void OnLook(InputValue inputValue)
    {
        Vector2 mousePosOnScreen = inputValue.Get<Vector2>();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mousePosOnScreen);

        lookDirection = mousePos - (Vector2)transform.position;

        bool isLeft = lookDirection.x < 0;
        if (lookDirection.magnitude < .5f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
        if (!moveControlLocked)
        {
            sprite.flipX = isLeft;
        }
    }

    void OnDash()
    {
        if (!moveControlLocked)
        {
            dash.TriggerDash();
        }
    }

    void OnInteract()
    {
        if (!moveControlLocked)
        {
            interactHandler.Interact();
        }
    }
}
