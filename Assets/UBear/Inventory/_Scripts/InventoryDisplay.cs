using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UBear.Inventory {
//This class works under the assumption that all items are stored as an ordered list, 
//with all unused/empty slots at the end of the list
/// <summary>
/// Handles displaying the inventory on the UI. Listens for changes in the inventory and updates the display accordingly.
/// Makes use of the FlexibleGridLayout component to arrange item slots in a grid. 
/// Each item slot is represented by a prefab defined in the ItemDefinition blueprint.
/// </summary>
//This requirement is for developer convenience, and can be removed without side effect if desired.
//This script is currently written with the understanding that positioning and sizing of children objects is automatically handled.
[RequireComponent(typeof(UBear.UI.FlexibleGridLayout))]
public class InventoryDisplay : MonoBehaviour
{
  public InventoryData inventory;
//todo: set up some sort of listener/event system so that we don't have to update the display every frame
  List<GameObject> _displayedItems = new List<GameObject>();
  
  void Start()
  {
    CreateDisplay();
  }

  void Update()
  {
    UpdateDisplay();
  }

  void UpdateDisplay()
  {
    Image img;
    string amtText;
    TextMeshProUGUI textMesh;
    //If inventory size has grown, instantiate inventory slot
    while(_displayedItems.Count < inventory.Container.Count)
    {
      var slot = inventory.Container[_displayedItems.Count];
      if(slot?.Item?.Blueprint?.Prefab != null)
      {
        var obj = Instantiate(slot.Item.Blueprint.Prefab, Vector3.zero, Quaternion.identity, transform);
        _displayedItems.Add(obj);
      }
    }
    //If inventory size has shrunk, destroy excess inventory slots
    while(_displayedItems.Count > inventory.Container.Count)
    {
      Destroy(_displayedItems[_displayedItems.Count - 1]);
      _displayedItems.RemoveAt(_displayedItems.Count - 1);
    }
    for (int i = 0; i < inventory.Container.Count; i++)
    {
      var slot = inventory.Container[i];
      if(slot?.Item?.Blueprint == null) continue;
      
      img = _displayedItems[i].GetComponentInChildren<Image>();
      //if item is different, reload item
      if(slot.Item.Blueprint.ItemIcon != img.sprite)
      {
          img.sprite = slot.Item.Blueprint.ItemIcon;
      }
      //if amount is different, update amount text
      amtText = slot.Item.StackCount.ToString("n0");
      textMesh = _displayedItems[i].GetComponentInChildren<TextMeshProUGUI>();
      if(amtText != textMesh.text)
      {
          textMesh.text = amtText;
      }
    }
  }

  void CreateDisplay()
  {
    for (int i = 0; i < inventory.Container.Count; i++)
    {
      var slot = inventory.Container[i];
      if(slot?.Item?.Blueprint?.Prefab != null)
      {
        var obj = Instantiate(slot.Item.Blueprint.Prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.Item.StackCount.ToString("n0");
        _displayedItems.Add(obj);
      }
    }
  }
}}
