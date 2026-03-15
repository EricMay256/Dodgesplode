using UnityEngine;

namespace UBear.Inventory {
[CreateAssetMenu(fileName = "New Special Item", menuName = "UBear/Inventory/Items/Special Item")]
/// <summary>
/// Blueprint for special/quest items with unique properties.
/// Defines quest-related and special-use item properties.
/// </summary>
public class SpecialItemDefinition : ItemDefinition
{
    [Header("Special Item Properties")]
    [Tooltip("Is this item tied to a specific quest")]
    public bool IsQuestItem = false;
    
    [Tooltip("Quest ID this item is associated with")]
    public string AssociatedQuestID = "";
    
    [Tooltip("Should this item be hidden from normal inventory display")]
    public bool IsHidden = false;
    
    [Tooltip("Unique ID or key for special item interactions")]
    public string SpecialKey = "";
    
    [TextArea(5, 10)]
    public string SpecialDescription = "";
    
    [Tooltip("Can this item be sold or traded")]
    public bool IsUntradeable = false;
    
    [Tooltip("Can this item be dropped")]
    public bool IsUndropable = false;

    void Awake()
    {
        ItemObjectType = ItemType.Special;
    }
}}
