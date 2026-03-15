using UnityEngine;
using System.Collections.Generic;

namespace UBear.Inventory {
/// <summary>
/// Allows for easy lookup of items by ID. 
/// Should be attached to a game object in the first scene and populated with all items in the game.
/// </summary>
public class ItemDatabaseSingleton : MonoBehaviour
{
  public static ItemDatabaseSingleton Instance { get; private set; }
  public List<ItemDefinition> Items = new List<ItemDefinition>();
  private Dictionary<int, ItemDefinition> _dict;

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
      InitializeDictionary();
    }
  }


  [ ContextMenu("Force Initialize Dictionary")]
  public void InitializeDictionary()
  {
    _dict = new Dictionary<int, ItemDefinition>();
    for (int i = 0; i < Items.Count; i++)
    {
      if (Items[i] != null)
      {
        _dict.Add(Items[i].ID, Items[i]);
      }
    }
  }

  public bool ValidateID(int id)
  {
    return _dict.ContainsKey(id);
  }

  public ItemDefinition GetItemByID(int id)
  {
    if(!ValidateID(id))
    {
      Debug.LogWarning("Item ID " + id + " is not found in database.");
      return null;
    }
    if (_dict.GetValueOrDefault(id,null) == null)
    {
      Debug.LogWarning("Item ID " + id + " found in database but is null.");
      return null;
    }
    var item = _dict[id];
    return item;
  }
}}
