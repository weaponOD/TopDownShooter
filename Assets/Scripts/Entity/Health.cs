using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health {
    public int CurrentHealth { get { return currentHealth; } }

    protected int currentHealth;
    protected int maxHealth;

    protected HealthSettings healthSettings;
    protected Entity entity;

    private bool initialized;
    private bool hasDied;

    public Health(int maxHealth, Entity entity)
    {
        currentHealth = this.maxHealth = maxHealth;
        this.entity = entity;

        initialized = true;
    }

    public virtual void Damage(int amount)
    {
        AdjustHealth(-amount);
    }

    /// <summary>
    /// Health the entity by amount
    /// </summary>
    /// <param name="amount">Amount to heal</param>
    /// <returns>Returns false when not needed so health objects can stay in the world instead of overhealing</returns>
    public virtual bool Heal(int amount)
    {
        if (currentHealth == maxHealth)
            return false;

        AdjustHealth(amount);
        return true;
    }

    protected virtual void AdjustHealth(int amount)
    {
        if (initialized == false)
        {
            Debug.LogWarning("Tried to adjust a non initialized health script! Something has gone wrong!");
            return;
        }

        if (hasDied)
            return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        else if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        hasDied = true;
        entity.Died();
    }
}
