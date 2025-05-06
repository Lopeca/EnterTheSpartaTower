using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDash : MonoBehaviour, IDash
{
    public PlayerController Player { get; private set; }
    public PlayerStatHandler StatHandler { get; private set; }
    public AudioClip dashSE;

    private float duration;
    private float elapsedTime;
    private float speedMultiplier;
    private float staminaCost;

    private bool isActivated;

    public void Init()
    {
        Player = GetComponent<PlayerController>();
        StatHandler = GetComponent<PlayerStatHandler>();
        duration = 0.2f;
        elapsedTime = 0;
        speedMultiplier = 4f;
        staminaCost = 20;
        isActivated = false;
    }

    public void TriggerDash()
    {
        if (!Player.IsMoving) return;
        if (!StatHandler.HasEnoughStamina(staminaCost)) return;

        Player.moveControlLocked = true;
        Player.ChangeState(PlayerState.Dash);
        StatHandler.ConsumeStamina(staminaCost);
        AudioManager.Instance.PlaySFX(dashSE);

        Player.rb.velocity *= speedMultiplier;
        isActivated = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > duration)
            {
                Player.ChangeState(PlayerState.Idle);
                Player.moveControlLocked = false;
                elapsedTime = 0;
                isActivated = false;
            }
        }
    }
}
