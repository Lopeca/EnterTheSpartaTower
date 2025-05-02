using System;
using System.Collections;
using System.Collections.Generic;
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

    IDash dash;

    public bool moveControlLocked;
    void Awake()
    {
        playerStatHandler = GetComponent<PlayerStatHandler>();
        rb = GetComponent<Rigidbody2D>();
        playerState = PlayerState.Idle;

        dash = GetComponent<IDash>();
        dash.Init();

        moveControlLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveControlLocked)
        {
            Move();
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
}
