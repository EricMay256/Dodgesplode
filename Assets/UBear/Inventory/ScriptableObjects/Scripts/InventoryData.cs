using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

namespace UBear.Inventory {
/// <summary>
/// Represents an inventory, which is a collection of item stacks (InventorySlots). 
/// Contains methods for adding items and saving/loading the inventory.
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "UBear/Inventory/Inventory Data")]
public class InventoryData : SavableObject
{
  public List<InventorySlot> Container = new List<InventorySlot>();
  public int MaximumSlots = 20;
  public override string FilePath() => string.Concat(Application.persistentDataPath,"/Inventories/");
  public override string FileExtension() => ".inv";
  /// <summary>
  /// Adds amount of item to the inventory, distributing it across stacks as necessary
  /// </summary>
  /// <param name="id">ID of item to add</param>
  /// <param name="amount">Number of items to add</param>
  /// <returns>Amount of items that were added to the inventory</returns>
  public int AddItem(int id, int amount)
  {
    if(id == 0)
    {
      Debug.LogWarning("Attempted to add item with ID 0 to inventory. Adding empty space does nothing and shouldn't be done.");
      return 0;
    }
    var item = ItemDatabaseSingleton.Instance.GetItemByID(id);
    return AddItem(item, amount);
  }

  /// <summary>
  /// Adds amount of item to the inventory, distributing it across stacks as necessary
  /// </summary>
  /// <param name="blueprint">ItemObject blueprint to add</param>
  /// <param name="amount">Amount of items to add</param>
  /// <returns>Amount of items that were added to the inventory</returns>
  public int AddItem(ItemDefinition blueprint, int amount)
  {
    if(blueprint == null)
    {
      Debug.LogWarning("Attempted to add null item blueprint to inventory.");
      return 0;
    }
    int initialAmount = amount;//Track the initial amount so we can return the amount added at the end
    int numStacks = 0;//Track the number of stacks we have added to so we can respect the unique stack limit for this item
    //Check for any possible existing stacks to increment before creating new stacks
    for (int i = 0; i < Container.Count; i++)
    {
      if (!Container[i].IsEmpty() && Container[i].Item.Blueprint.ID == blueprint.ID)
      {
        numStacks++;
        amount -= Container[i].AddAmount(amount);
        Assert.IsFalse(amount < 0, "AddAmount should never return a negative number");
        //If we have added all items, return the total amount added
        if (amount <= 0)
          return initialAmount;
      }
    }
    //There is leftover amount that couldn't fit in existing stacks, try to add it in new stacks until we run out of items or reach the unique stack limit for this item
    InventorySlot emptySlot;
    while (amount > 0 && (blueprint.UniqueStacks == 0 || numStacks < blueprint.UniqueStacks))
    {
      //If the inventory is full, we can't add any more items; return the amount we were able to add so far
      if(!GetEmptySlot(out emptySlot))
        return initialAmount - amount;

      int amountAdded = Mathf.Min(blueprint.MaxStackSize, amount);
      emptySlot.OverwriteItem(new Item(blueprint, amountAdded));
      amount -= amountAdded;
      numStacks++;
    }
    return initialAmount - amount;
  }

  /// <summary>
  /// Adds a full ItemInstance to the inventory
  /// </summary>
  public int AddItemInstance(Item itemInstance)
  {
    if(itemInstance?.Blueprint == null)
    {
      Debug.LogWarning("Attempted to add null item instance to inventory.");
      return 0;
    }
    if (Container.Count >= MaximumSlots)
    {
      Debug.LogWarning("Inventory is full.");
      return 0;
    }
    //Note: This would not preserve data like buffs or alterations to gear
    //To do so we need to change and/or replace the additem function
    return AddItem(itemInstance.Blueprint, itemInstance.StackCount);
  }

  public bool InventoryEmpty()
  {
    return Container.Count == 0 || Container.TrueForAll(slot => slot.IsEmpty());
  }

  public bool InventoryFull()
  {
    return Container.Count >= MaximumSlots && Container.TrueForAll(slot => !slot.IsEmpty());
  }

  /// <summary>
  /// Returns whether we were able to find or create an empty slot in out parameter emptySlot
  /// </summary>
  /// <param name="emptySlot">Contains the empty slot if true; otherwise unaltered</param>
  /// <returns>Whether an empty slot was able to be found or created</returns>
  public bool GetEmptySlot(out InventorySlot emptySlot)
  {
    emptySlot = Container.Find(slot => slot.IsEmpty());
    if(emptySlot == null && Container.Count < MaximumSlots)
    {
      emptySlot = new InventorySlot(null);
      Container.Add(emptySlot);
    }
    return emptySlot != null;
  }

  public void OnEnable()
  {
    Container = new List<InventorySlot>();
    Load(this);
  }
  public void OnDestroy()
  {
    Save(this);
    Container.Clear();
  }

  [ContextMenu("Fill remaining capacity with Empty Slots")]
  public void FillToCapacityWithEmptySlots()
  {
    while (Container.Count < MaximumSlots)
    {
      Container.Add(new InventorySlot(null));
    }
  }

  public int GetEmptySlotCount()
  {
    return Container.FindAll(slot => slot.IsEmpty()).Count;
  }

  public int GetFilledSlotCount()
  {
    return Container.FindAll(slot => !slot.IsEmpty()).Count;
  }

  public int GetItemCount(int itemID)
  {
    int count = 0;
    foreach (var slot in Container)
    {
      if (!slot.IsEmpty() && slot.Item.Blueprint.ID == itemID)
      {
        count += slot.Item.StackCount;
      }
    }
    return count;
  }

}}