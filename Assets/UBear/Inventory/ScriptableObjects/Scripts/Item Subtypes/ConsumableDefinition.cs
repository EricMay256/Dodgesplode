using UnityEngine;

namespace UBear.Inventory {
[CreateAssetMenu(fileName = "New Consumable Item", menuName = "UBear/Inventory/Items/Consumable Item")]
/// <summary>
/// Blueprint for consumable items that can be used and consumed.
/// Defines effects, duration, and cooldown properties.
/// </summary>
public class ConsumableDefinition : ItemDefinition
{
  [Header("Consumable Properties")]
  [Tooltip("Duration of the consumable effect in seconds")]
  public float EffectDuration = 0f;
  
  [Tooltip("Cooldown before this item can be used again")]
  public float UseCooldown = 0f;
  
  [TextArea(5, 10)]
  [Tooltip("Description of the effect this consumable provides")]
  public string EffectDescription = "";
  
  [Tooltip("If true, removes the item after use")]
  public bool DestroyOnUse = true;

  void Awake()
  {
      ItemObjectType = ItemType.Consumable;
  }
}}
