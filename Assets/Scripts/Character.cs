using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;


public class Character : MonoBehaviour
{
    public Animator animator;
    public int attack = 1;
    public int defense = 1;
    public int alchemy = 1;
    public int specialSkill = 1;

    public int maxAttributeLevel = 10;
    public int health;
    public int maxHealth;

    public Slider healthSlider;
    public Text healthText;

    public event Action<int> OnHealthChanged;

    private AttributePoint.AttributeType? lastIncreasedAttribute = null;
    private HashSet<AttributePoint.AttributeType> increasedAttributes = new HashSet<AttributePoint.AttributeType>();

    void Start()
    {
        maxHealth = 9;
        UpdateHealth();

        healthSlider.maxValue = maxHealth;
        UpdateHealthUI(health);
        OnHealthChanged += UpdateHealthUI;

        OnHealthChanged?.Invoke(health);
    }   

    public bool IncreaseAttribute(AttributePoint.AttributeType attributeType, int amount, out string message)
    {
        message = string.Empty;

        if (GetAttributeLevel(attributeType) >= maxAttributeLevel)
        {
            message = $"<color=white>Max Level of {attributeType}!</color>";
            return false;
        }

        if (!CanIncreaseAttribute(attributeType))
        {
            message = "<color=white>You can't level up this attribute yet.</color>";
            return false;
        }

        bool success = false;
        switch (attributeType)
        {
            case AttributePoint.AttributeType.Attack:
                if (attack < maxAttributeLevel)
                {
                    attack += amount;
                    lastIncreasedAttribute = attributeType;
                    success = true;
                }
                break;
            case AttributePoint.AttributeType.Defense:
                if (defense < maxAttributeLevel)
                {
                    defense += amount;
                    lastIncreasedAttribute = attributeType;
                    UpdateHealth();
                    
                    success = true;
                }
                break;
            case AttributePoint.AttributeType.Alchemy:
                if (alchemy < maxAttributeLevel)
                {
                    alchemy += amount;
                    lastIncreasedAttribute = attributeType;
                    success = true;
                }
                break;
            case AttributePoint.AttributeType.SpecialSkill:
                if (specialSkill < maxAttributeLevel)
                {
                    specialSkill += amount;
                    lastIncreasedAttribute = attributeType;
                    success = true;
                }
                break;
        }

        if (success)
        {
            if (increasedAttributes.Contains(attributeType))
            {
                message = $"<color=white>{attributeType} Level up!</color>";
                increasedAttributes.Add(attributeType);
            }
            else
            {
                message = $"<color=white>{attributeType} Level up!</color>";
                increasedAttributes.Add(attributeType);
            }
        }

        return success;
    }

    public int GetAttributeLevel(AttributePoint.AttributeType attributeType)
    {
        switch (attributeType)
        {
            case AttributePoint.AttributeType.Attack:
                return attack;
            case AttributePoint.AttributeType.Defense:
                return defense;
            case AttributePoint.AttributeType.Alchemy:
                return alchemy;
            case AttributePoint.AttributeType.SpecialSkill:
                return specialSkill;
            default:
                return 1;
        }
    }

    public void AttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void SpecialSkillAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("SpecialSkill");
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

    private bool CanIncreaseAttribute(AttributePoint.AttributeType attributeType)
    {
        return lastIncreasedAttribute == null || lastIncreasedAttribute != attributeType;
    }

    private void UpdateHealth()
    {
        maxHealth = maxHealth + 1;
        health = Mathf.Min(9 + defense, maxHealth);
        

        OnHealthChanged?.Invoke(health);
    }

    private void UpdateHealthUI(int currentHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = $"HP: {currentHealth}/{maxHealth}";
    }
    private void OnDestroy()
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged -= UpdateHealthUI;
        }
    }
}
