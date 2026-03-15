using UnityEngine;
using System.Collections.Generic;

namespace UBear.Inventory {
public class PlayerController : MonoBehaviour
{
  public InventoryData inventory;

  void OnTriggerEnter2D(Collider2D collision)
  {
    PickupOnTouch pickup = collision.GetComponent<PickupOnTouch>();
    if (pickup != null)
    {
      pickup.containedAmount -= inventory.AddItem(pickup.ContainedItemObject.ID, pickup.containedAmount);
      if (pickup.containedAmount <= 0)
        Destroy(collision.gameObject);
    }
  }

  void Onestroy()
  {
    inventory.Container = new List<InventorySlot>();
  }
}}
