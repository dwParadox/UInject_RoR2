using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UInject_RoR2
{
#pragma warning disable CS0618 // Type or member is obsolete
    public static class DropTable
    {
        public static readonly List<PickupIndex> AllItems = new List<PickupIndex>();

        public static readonly List<PickupIndex> availableTier1DropList = new List<PickupIndex>();
        public static readonly List<PickupIndex> availableTier2DropList = new List<PickupIndex>();
        public static readonly List<PickupIndex> availableTier3DropList = new List<PickupIndex>();
        public static readonly List<PickupIndex> availableLunarDropList = new List<PickupIndex>();
        public static readonly List<PickupIndex> availableEquipmentDropList = new List<PickupIndex>();
        public static readonly List<PickupIndex> availableBossDropList = new List<PickupIndex>();

        private static ItemMask availableItems = ItemMask.all;
        private static EquipmentMask availableEquipment = EquipmentMask.all;

        public static void BuildDropTable()
        {
            availableTier1DropList.Clear();
            availableTier2DropList.Clear();
            availableTier3DropList.Clear();
            availableLunarDropList.Clear();
            availableEquipmentDropList.Clear();
            availableBossDropList.Clear();

            ItemIndex itemIndex = ItemIndex.Syringe;
            ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
            while (itemIndex < itemCount)
            {
                ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
                List<PickupIndex> list = null;
                switch (itemDef.tier)
                {
                    case ItemTier.Tier1:
                        list = availableTier1DropList;
                        break;
                    case ItemTier.Tier2:
                        list = availableTier2DropList;
                        break;
                    case ItemTier.Tier3:
                        list = availableTier3DropList;
                        break;
                    case ItemTier.Lunar:
                        list = availableLunarDropList;
                        break;
                    case ItemTier.Boss:
                        list = availableBossDropList;
                        break;
                }

                list?.Add(new PickupIndex(itemIndex));

                itemIndex++;
            }

            EquipmentIndex equipmentIndex = EquipmentIndex.CommandMissile;
            EquipmentIndex equipmentCount = (EquipmentIndex)EquipmentCatalog.equipmentCount;
            while (equipmentIndex < equipmentCount)
            {
                if (availableEquipment.HasEquipment(equipmentIndex))
                {
                    EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
                    if (equipmentDef.canDrop)
                    {
                        if (!equipmentDef.isLunar)
                        {
                            availableEquipmentDropList.Add(new PickupIndex(equipmentIndex));
                        }
                        else
                        {
                            availableLunarDropList.Add(new PickupIndex(equipmentIndex));
                        }
                    }
                }
                equipmentIndex++;
            }

            AllItems.AddRange(availableTier1DropList);
            AllItems.AddRange(availableTier2DropList);
            AllItems.AddRange(availableTier3DropList);
            AllItems.AddRange(availableLunarDropList);
            AllItems.AddRange(availableEquipmentDropList);
            AllItems.AddRange(availableBossDropList);
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}
