using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    int triggerAttack = Animator.StringToHash("Attack");
    [SerializeField] AudioClip se;
    [SerializeField] Animator anim;
    [SerializeField] float damage;
    float attackTerm = 3f;
    float standbyTime = 1f;
    float lastAttackTime = float.MaxValue;

    private PlayerController player;

    private void Update()
    {
        if (lastAttackTime < attackTerm)
        {
            lastAttackTime += Time.deltaTime;
        }

        if(player != null)
        {
            ReactToPlayer();
        }
    }

    private void ReactToPlayer()
    {
        if (lastAttackTime > attackTerm)
        {
            TriggerAttack();
        }
        else if (lastAttackTime > standbyTime && lastAttackTime <= standbyTime + 1)
        {
            player.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
    }

    void TriggerAttack()
    {
        anim.SetTrigger(triggerAttack);
        lastAttackTime = 0f;
    }
}
