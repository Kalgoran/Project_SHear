using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth = 0;

    public int health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    private Action OnDeathEvent;
    public Action OnDeath
    {
        get { return OnDeathEvent; }
    }

    [SerializeField]
    private int _zkill = -999;
    public int zkill
    {
        get { return _zkill; }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        OnDeathEvent = Death;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            OnDeath.Invoke();
    }

    public void Death()
    {
        Debug.Log("Death");
    }
}