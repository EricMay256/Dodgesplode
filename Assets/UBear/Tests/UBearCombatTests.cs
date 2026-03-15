using NUnit.Framework;
using UnityEngine;
using UBear.Combat;

namespace UBear.Tests
{
public class UBearCombatTests
{
 const float Tolerance = 0.01f;
  [Test]
  public void TargetDummy_TakeDamage_HealthReduces()
  {
    var dummy = new TargetDummy(invulnerable: false, health: 100);
    dummy.TakeDamage(30);
    Assert.AreEqual(70, dummy.CurHealth, Tolerance);
    Assert.AreEqual(0.7f, dummy.HealthRatio, Tolerance);
    dummy.TakeDamage(69);
    Assert.AreEqual(1, dummy.CurHealth, Tolerance);
    Assert.AreEqual(0.01f, dummy.HealthRatio, Tolerance);
    Assert.IsFalse(dummy.IsDead, "TargetDummy should not be dead yet.");
    dummy.TakeDamage(2);
    Assert.AreEqual(0, dummy.CurHealth, Tolerance);
    Assert.IsTrue(dummy.IsDead, "TargetDummy should be dead now.");
  } 
  [Test]
  public void TargetDummy_TakeDamage_Invulnerable_NoHealthChange()
  {
    var dummy = new TargetDummy(invulnerable: true, health: 100);
    dummy.TakeDamage(30);
    Assert.AreEqual(100, dummy.CurHealth, Tolerance);
    Assert.AreEqual(1f, dummy.HealthRatio, Tolerance);
  }
}
}
