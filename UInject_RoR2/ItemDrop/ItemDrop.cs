using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UInject_RoR2
{
    class ItemDrop
    {
        public static void AddItem(PlayerCharacterMasterController player, string item)
        {
            if (player == null)
                return;

            var pickupIndex = PickupCatalog.FindPickupIndex(item);

            if (pickupIndex == null)
                return;

            player.master.GetBodyObject().GetComponent<EntityStateMachine>().mainStateType = new SerializableEntityStateType(typeof(EntityStates.ScavMonster.GrantItem));
            player.master.GetBodyObject().GetComponent<EntityStateMachine>().SetState
                (
                   new EntityStates.ScavMonster.GrantItem
                   {
                       dropPickup = pickupIndex,
                       itemsToGrant = 1
                   }
                );

            player.master.GetBodyObject().GetComponent<EntityStateMachine>().mainStateType = new SerializableEntityStateType(typeof(EntityStates.GenericCharacterMain));
            player.master.GetBodyObject().GetComponent<EntityStateMachine>().SetState(new EntityStates.GenericCharacterMain());
        }


        public static void RollItems(PlayerCharacterMasterController player)
        {
            if (player == null)
                return;

            
            player.master.GetBodyObject().GetComponent<EntityStateMachine>().mainStateType = new SerializableEntityStateType(typeof(EntityStates.ScavBackpack.Opening));
            player.master.GetBodyObject().GetComponent<EntityStateMachine>().SetState(new EntityStates.ScavBackpack.Opening());
        }

        public static void RevertEntityState(PlayerCharacterMasterController player)
        {
            if (player == null)
                return;

            player.master.GetBodyObject().GetComponent<EntityStateMachine>().mainStateType = new SerializableEntityStateType(typeof(EntityStates.GenericCharacterMain));
            player.master.GetBodyObject().GetComponent<EntityStateMachine>().SetState(new EntityStates.GenericCharacterMain());
        }
    }
}
