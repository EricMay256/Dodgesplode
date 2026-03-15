using UnityEngine;

namespace UBear.Combat
{
public interface IDamaging
{
  int GetDamage()
  {
    return 10; // Default damage value, can be overridden by implementing classes
  }
  void DamageApplied()
  {
    // Optional method to be called when damage is applied, can be used for effects or cooldowns
  }
}
}