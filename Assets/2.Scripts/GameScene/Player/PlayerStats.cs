using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/Player Data")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float maxHp;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaRegenRate;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamageRatio;
    [SerializeField] private float attackSpeedRatio;

    public float MaxHp => maxHp;
    public float MaxStamina => maxStamina;
    public float StaminaRegenRage => staminaRegenRate;
    public float MoveSpeed => moveSpeed;
    public float AttackDamageRatio => attackDamageRatio;
    public float AttackSpeedRatio => attackSpeedRatio;
}
