using UnityEngine;

namespace UBear.Inventory {
/// <summary>
/// Rarity levels for crafting materials
/// </summary>
public enum MaterialRarity
{
  Common,
  Uncommon,
  Rare,
  Epic,
  Legendary
}

[CreateAssetMenu(fileName = "New Material Item", menuName = "UBear/Inventory/Items/Material Item")]
/// <summary>
/// Blueprint for crafting and material items.
/// Defines rarity, crafting properties, and value.
/// </summary>
public class MaterialDefinition : ItemDefinition
{
  [Header("Material Properties")]
  public MaterialRarity Rarity = MaterialRarity.Common;
  
  [Tooltip("Value of this material (used in crafting calculations)")]
  public float MaterialValue = 1f;
  
  [Tooltip("Number of this material produced per crafting result")]
  public int YieldAmount = 1;
  
  [Tooltip("Can this material be used in crafting recipes")]
  public bool IsCraftingMaterial = true;
  
  [TextArea(3, 5)]
  public string MaterialDescription = "";
  
  [Tooltip("Category of material for crafting recipe matching")]
  public string MaterialCategory = "";

  void Awake()
  {
      ItemObjectType = ItemType.Material;
  }
}}
