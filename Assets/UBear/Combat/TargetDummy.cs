using UnityEngine;

namespace UBear.Combat
{
  public class TargetDummy : IHarmable
  {
    public TargetDummy(bool invulnerable = true, float health = 1000000)
    {
      _health = health;
      _maxHealth = health;
      _isInvulnerable = invulnerable;
    }
    float _health;
    float _maxHealth;
    bool _isInvulnerable = true;
    public bool IsDead = false;
    public float CurHealth => _health;
    public float MaxHealth => _maxHealth;
    public float HealthRatio => _maxHealth > 0 ? (float)_health / _maxHealth : 0f;

    public void Die()
    {
      _health = 0;
      IsDead = true;
      Debug.Log("TargetDummy died.");
    }

    public void TakeDamage(float damage)
    {
      damage = Mathf.Max(0, damage); // Ensure damage is not negative
      Debug.Log($"TargetDummy took {damage} damage.");
      if (!_isInvulnerable)
      {
        _health -= damage;
        if (_health <= 0)
        {
          Die();
        }
      }
    }
  }
}
