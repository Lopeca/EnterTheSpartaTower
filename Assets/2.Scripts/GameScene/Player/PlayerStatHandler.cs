using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStatData;
    [field: SerializeField] public float CurrentHP { get; private set; }
    [field: SerializeField] public float CurrentStamina { get; private set; }

    public bool isInvincible;

    public event Action<float> OnChangeHP;
    public event Action<float> OnChangeStamina;
    public bool HasEnoughStamina(float cost) => CurrentStamina >= cost;
    void Awake()
    {
        CurrentHP = playerStatData.MaxHp;
        CurrentStamina = playerStatData.MaxStamina;
        isInvincible = false;
    }

    private void Update()
    {
        AutoRecover();
    }

    private void AutoRecover()
    {
        CurrentStamina += playerStatData.StaminaRegenRage * Time.deltaTime;
        if(CurrentStamina > playerStatData.MaxStamina)
        {
            CurrentStamina = playerStatData.MaxStamina;
        }
        OnChangeStamina?.Invoke(CurrentStamina / playerStatData.MaxStamina);
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;
        if (CurrentHP < 0)
        {
            CurrentHP = 0;
        }
        OnChangeHP?.Invoke(CurrentHP / playerStatData.MaxHp);
    }

    public void ConsumeStamina(float cost)
    {
        CurrentStamina -= cost;
        OnChangeStamina?.Invoke(CurrentStamina / playerStatData.MaxStamina);
    }
    public float GetMoveSpeed()
    {
        return playerStatData.MoveSpeed;
    }

    public float GetAttackSpeedRatio()
    {
        return playerStatData.AttackSpeedRatio;
    }

    public float GetAttackDamageRatio()
    {
        return playerStatData.AttackDamageRatio;
    }
    
    public void LoadStatState(float hp, float stamina)
    {
        CurrentHP = hp;
        CurrentStamina = stamina;
    }
}
