using System;
using UnityEngine;

namespace UBear.Inventory {
/// <summary>
/// Equipment slot enum for different equipment types
/// </summary>
public enum EquipmentCategory
{
  MainHand,
  OffHand,
  Head,
  Shoulder,
  Chest,
  Back,
  Wrist,
  Hands,
  Waist,
  Legs,
  Feet,
  Neck,
  Earring,
  Ring,
  Accessory,
}

[Serializable]
[CreateAssetMenu(fileName = "New Equipment Item", menuName = "UBear/Inventory/Items/Equipment Item")]
/// <summary>
/// Blueprint for equipment items that can be equipped and provide stat bonuses.
/// Defines armor, damage, and equipment slot properties.
/// </summary>
public class EquipmentDefinition : ItemDefinition
{
  [Header("Equipment Properties")]
  public EquipmentCategory EquipmentType = EquipmentCategory.MainHand;
  
  [Tooltip("Armor value provided by this equipment")]
  public float ArmorValue = 0f;
  
  [Tooltip("Damage bonus provided by this equipment")]
  public float DamageBonus = 0f;
  
  [Tooltip("Weight of the equipment (affects movement speed)")]
  public float Weight = 0f;
  
  [Tooltip("Minimum level required to equip this item")]
  public int RequiredLevel = 1;
  
  [Tooltip("Durability of the equipment. 0 = infinite")]
  public float MaxDurability = 0f;
  
  [TextArea(3, 5)]
  public string EquipmentDescription = "";

  void Awake()
  {
    ItemObjectType = ItemType.Equipment;
  }
}}
