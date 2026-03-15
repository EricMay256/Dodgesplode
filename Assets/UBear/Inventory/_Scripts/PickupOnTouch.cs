using UnityEngine;

namespace UBear.Inventory {
/// <summary>
/// Prototype component for items that can be picked up by the player on touch. 
/// Contains a reference to the item and amount contained in this pickup. 
/// Can be extended with additional functionality such as respawning, particle effects, etc.
/// </summary>
public class PickupOnTouch : MonoBehaviour
{
  [SerializeField]
  ItemDefinition containedItemObject;
  public int containedAmount = 1;
  public ItemDefinition ContainedItemObject => containedItemObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}}
