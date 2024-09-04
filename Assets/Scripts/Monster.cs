using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 50;
    public int health;
    public int attackPower = 5;

    public event Action<int> OnHealthChanged;

    void Start()
    {
        health = maxHealth;
        OnHealthChanged?.Invoke(health);
    }

    public void AttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void ReceiveDamageAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("ReceiveDamage");
        }
    }

    public void DeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        ReceiveDamageAnimation();

        OnHealthChanged?.Invoke(health);

        if (health <= 0)
        {
            Die();
        }
    }

     private void Die()
    {
         Destroy(gameObject);
    }
}
