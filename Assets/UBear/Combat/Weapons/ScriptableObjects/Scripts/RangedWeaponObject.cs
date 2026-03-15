using System.Collections.Generic;
using UnityEngine;
using UBear.Inventory;

namespace UBear.Combat
{
/// <summary>
/// Determines how the firing points on a weapon,
/// or the projectiles on a firing point, fire together
/// </summary>
public enum FireCoordinationPattern
{
  Sequential,
  AllTogether,
  Random,
  Combo
}

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "UBear/Combat/Weapons/Ranged Weapon")]
//Inherits from EquipmentItem, which inherits from Item, so that it can be handled by the inventory system, be equipped, and have stats
public class RangedWeaponObject : EquipmentDefinition
{
  public List<FiringPoint> FiringPoints;
  public FireCoordinationPattern FirepointPattern;
  public float ComboTime = 0.1f;
}

[System.Serializable]
public class FiringPoint
{
  public Vector3 Position;
  public List<ProjectileObject> Projectiles;
  public FireCoordinationPattern FirePattern;
  public int nextProjectileIndex = 0;
}
}