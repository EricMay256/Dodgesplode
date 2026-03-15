using UnityEngine;

namespace UBear.Combat
{
public interface IDamaging
{
  int Damage { get; }
  //Any logic that should occur after inflicting damage can be placed here.
  void DamageApplied();
}
}