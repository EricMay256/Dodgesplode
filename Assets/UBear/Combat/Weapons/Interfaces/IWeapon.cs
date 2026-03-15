using UnityEngine;

namespace UBear.Combat
{
public interface IWeapon
{
  void Attack()
  {
    Debug.Log("Attacking with default weapon behavior.");
  }
  void Attack(Vector3 direction)
  {
    Debug.Log($"Attacking in direction: {direction} with default weapon behavior.");
  }
}
}