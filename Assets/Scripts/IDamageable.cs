using System;

public interface IDamageable
{
    int health { get; set; }
    Action OnDeath { get; }
    int zkill { get; }

    void TakeDamage(int amount);
    void Death();
}