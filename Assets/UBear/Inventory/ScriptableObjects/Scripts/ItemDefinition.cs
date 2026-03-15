using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

namespace UBear.Inventory {
/// <summary>
/// Item type classification
/// </summary>
public enum ItemType
{
    Consumable,
    Equipment,
    Material,
    Special
}

/// <summary>
/// Blueprint/template for items. Serves as a ScriptableObject that defines the properties of an item type.
/// Use ItemInstance to represent an actual instance of an item with runtime data.
/// 
/// ItemObject = Blueprint/Template (one per item type)
/// ItemInstance = Runtime instance (multiple can exist from one blueprint)
/// </summary>
[System.Serializable]
public abstract class ItemDefinition : ScriptableObject, ISellable
{
    [Header("Identity")]
    public string ItemName;
    public ItemType ItemObjectType;
    
    [TextArea(15,20)] 
    public string Description;
    
    [Header("Database")]
    /// <summary>
    /// Unique ID for serialization and database lookup
    /// </summary>
    [Tooltip("Unique ID for serialization")]
    public int ID;
    
    [Header("Visuals & Prefabs")]
    public GameObject Prefab; ///Todo: reevaluate
    public Sprite ItemIcon;
    
    [Header("Inventory")]
    [Tooltip("Maximum stack size for this item")]
    public int MaxStackSize = 1;
    
    [Tooltip("If greater than 0, this item can only have this number of unique stacks in the inventory")]
    public int UniqueStacks = 0;

    [Tooltip("Sell price of the item. Set to -1 for unsellable items.")]
    [SerializeField]
    private int _sellPrice = 1;


    [Tooltip("Tags for categorization and identification")]
    [SerializeField]
    List<string> Tags = new List<string>();
    
    /// <summary>
    /// MaxStackSize > 1
    /// </summary>
    public bool IsStackable => MaxStackSize > 1;

    /// <summary>
    /// Creates a new ItemInstance from this blueprint
    /// </summary>
    public Item CreateInstance(int stackCount = 1)
    {
        Debug.Assert(stackCount > 0, "Stack count must be greater than 0 when creating an item instance.");
        return new Item(this, stackCount);
    }
    
    /// <summary>
    /// Checks if this item belongs to a specific category via tags
    /// </summary>
    public bool HasTag(string tag)
    {
      Debug.Assert(!string.IsNullOrEmpty(tag), "Tag cannot be null or empty.");
        return Tags.Contains(tag);
    }

    public int GetSellPrice() => _sellPrice;

    public bool IsSellable() => _sellPrice >= 0;
  }
}
