using UnityEngine;
using System.Collections.Generic;

namespace UBear.Inventory {
[CreateAssetMenu(fileName = "EquippedItemsData", menuName = "UBear/Inventory/EquippedItemsData", order = 1)]
public class EquippedItemsData : SavableObject
{
    public override string FilePath() => string.Concat(Application.persistentDataPath,"/Equipment/");
    public override string FileExtension() => ".eqp";
  public List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>()
  {
    new EquipmentSlot(EquipmentCategory.MainHand),
    new EquipmentSlot(EquipmentCategory.OffHand),
    new EquipmentSlot(EquipmentCategory.Head),
    new EquipmentSlot(EquipmentCategory.Shoulder),
    new EquipmentSlot(EquipmentCategory.Chest),
    new EquipmentSlot(EquipmentCategory.Back),
    new EquipmentSlot(EquipmentCategory.Wrist),
    new EquipmentSlot(EquipmentCategory.Hands),
    new EquipmentSlot(EquipmentCategory.Waist),
    new EquipmentSlot(EquipmentCategory.Legs),
    new EquipmentSlot(EquipmentCategory.Feet),
    new EquipmentSlot(EquipmentCategory.Neck),
    new EquipmentSlot(EquipmentCategory.Earring, "Earring Left"), 
    new EquipmentSlot(EquipmentCategory.Earring, "Earring Right"), 
    new EquipmentSlot(EquipmentCategory.Ring, "Ring Left"),
    new EquipmentSlot(EquipmentCategory.Ring, "Ring Right"),
    //Note: If you change this list, make sure to update the CategoryIndex enum
  };

  }
}
