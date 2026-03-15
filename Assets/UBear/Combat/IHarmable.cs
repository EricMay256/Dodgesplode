using UnityEngine;
using Unity;
using NUnit.Framework;
using System;

namespace UBear.Combat
{
public interface IHarmable
{
  void TakeDamage(float damage);
  void Die();
  float CurHealth {get;}
  float MaxHealth {get;}
  float HealthRatio {get;}
}
}