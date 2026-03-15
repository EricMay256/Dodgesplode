using System.Collections.Generic;
using UnityEngine;

namespace UBear.Inventory {
/// <summary>
/// Pure C# class representing an instance of an item in the game world or inventory.
/// Unlike ItemObject (which serves as a blueprint), this contains runtime data specific to each item instance.
/// </summary>
[System.Serializable]
public class Item
{
    /// <summary>
    /// Reference to the ItemObject blueprint for this instance
    /// </summary>
    [field: SerializeField]
    public ItemDefinition Blueprint { get; private set; }
    
    /// <summary>
    /// Current stack size for stackable items
    /// </summary>
    [field: SerializeField]
    public int StackCount { get; set; }
    
    /// <summary>
    /// Current durability of the item (for equipment)
    /// </summary>
    [field: SerializeField]
    public float CurrentDurability { get; set; }
    
    /// <summary>
    /// Time remaining for the item to be usable again (cooldown)
    /// </summary>
    [field: SerializeField]
    public float RemainingCooldown { get; set; }
    
    /// <summary>
    /// Is this item currently equipped
    /// </summary>
    [field: SerializeField]
    public bool IsEquipped { get; set; }
    
    /// <summary>
    /// List of modifiers applied to this item instance (e.g. enchantments, buffs). 
    /// This is a simple list of strings for demonstration, but could be expanded to a more complex system.
    /// </summary>
    [SerializeField]
    public List<string> Modifiers = new List<string>();

    /// <summary>
    /// Current durability percentage (0-1)
    /// </summary>
    public float DurabilityPercentage
    {
        get
        {
            if (Blueprint is EquipmentDefinition equipment && equipment.MaxDurability > 0)
            {
                return CurrentDurability / equipment.MaxDurability;
            }
            return 1f; // Non-equipment items have infinite durability
        }
    }
    
    /// <summary>
    /// Custom data for derived item types
    /// </summary>
    [field: SerializeField]
    private Dictionary<string, object> _customData = new Dictionary<string, object>();
    
    /// <summary>
    /// Constructor from an ItemObject blueprint
    /// </summary>
    public Item(ItemDefinition blueprint, int stackCount = 1)
    {
        Blueprint = blueprint;
        StackCount = Mathf.Min(stackCount, blueprint.MaxStackSize);
        RemainingCooldown = 0f;
        IsEquipped = false;
        
        // Initialize durability
        if (blueprint is EquipmentDefinition equipment && equipment.MaxDurability > 0)
        {
            CurrentDurability = equipment.MaxDurability;
        }
        else
        {
            CurrentDurability = -1f; // Infinite durability indicator
        }
    }
    
    /// <summary>
    /// Constructor from another ItemInstance (copy/clone)
    /// </summary>
    public Item(Item other)
    {
        Blueprint = other.Blueprint;
        StackCount = other.StackCount;
        CurrentDurability = other.CurrentDurability;
        RemainingCooldown = other.RemainingCooldown;
        IsEquipped = other.IsEquipped;
        _customData = new Dictionary<string, object>(other._customData);
    }
    
    /// <summary>
    /// Attempts to add items to the stack if this item is stackable
    /// </summary>
    public int AddToStack(int amount)
    {
        if (!Blueprint.IsStackable)
            return 0;
        
        int amountAdded = Mathf.Min(Blueprint.MaxStackSize - StackCount, amount);
        StackCount += amountAdded;
        return amountAdded;
    }
    
    /// <summary>
    /// Removes items from the stack
    /// </summary>
    public int RemoveFromStack(int amount)
    {
        int amountRemoved = Mathf.Min(StackCount, amount);
        StackCount -= amountRemoved;
        return amountRemoved;
    }
    
    /// <summary>
    /// Reduces durability by the specified amount
    /// </summary>
    public void ReduceDurability(float amount)
    {
        if (Blueprint is EquipmentDefinition equipment && equipment.MaxDurability > 0)
        {
            CurrentDurability = Mathf.Max(0, CurrentDurability - amount);
        }
    }
    
    /// <summary>
    /// Repairs durability
    /// </summary>
    public void RepairDurability(float amount)
    {
        if (Blueprint is EquipmentDefinition equipment && equipment.MaxDurability > 0)
        {
            CurrentDurability = Mathf.Min(equipment.MaxDurability, CurrentDurability + amount);
        }
    }
    
    /// <summary>
    /// Sets the cooldown for this item
    /// </summary>
    public void SetCooldown(float cooldown)
    {
        RemainingCooldown = Mathf.Max(0, cooldown);
    }
    
    /// <summary>
    /// Updates cooldown and effect time (call this in Update loops)
    /// </summary>
    public void UpdateTimers(float deltaTime)
    {
        if (RemainingCooldown > 0)
            RemainingCooldown -= deltaTime;
    }
    
    /// <summary>
    /// Checks if the item is on cooldown
    /// </summary>
    public bool IsOnCooldown => RemainingCooldown > 0;
    
    /// <summary>
    /// Checks if the item is broken (0 durability)
    /// </summary>
    public bool IsBroken => Blueprint is EquipmentDefinition equipment && CurrentDurability <= 0 && equipment.MaxDurability > 0;
    
    /// <summary>
    /// Store custom data for this item instance
    /// </summary>
    public void SetCustomData(string key, object value)
    {
        _customData[key] = value;
    }
    
    /// <summary>
    /// Retrieve custom data for this item instance
    /// </summary>
    public object GetCustomData(string key, object defaultValue = null)
    {
        return _customData.ContainsKey(key) ? _customData[key] : defaultValue;
    }
    
    /// <summary>
    /// Check if custom data exists
    /// </summary>
    public bool HasCustomData(string key)
    {
        return _customData.ContainsKey(key);
    }
    
    public override string ToString()
    {
        string baseInfo = $"{Blueprint.ItemName} x{StackCount}";
        
        if (Blueprint is EquipmentDefinition && IsBroken)
            return $"{baseInfo} [BROKEN]";
        
        if (IsOnCooldown)
            return $"{baseInfo} [COOLDOWN: {RemainingCooldown:F1}s]";
        
        return baseInfo;
    }
}}
