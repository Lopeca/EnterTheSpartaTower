using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/Player Data")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float hp;
    [SerializeField] private float stamina;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamageRatio;
    [SerializeField] private float attackSpeedRatio;

    public float Hp => hp;
    public float Stamina => stamina;
    public float MoveSpeed => moveSpeed;
    public float AttackDamageRatio => attackDamageRatio;
    public float AttackSpeedRatio => attackSpeedRatio;
}
