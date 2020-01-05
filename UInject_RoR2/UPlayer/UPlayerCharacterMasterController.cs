using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using UInject;
using RoR2;
using System.Runtime.InteropServices;
using UInject.UMenu;
using EntityStates;

namespace UInject_RoR2.UPlayer
{
    class UPlayerCharacterMaster : CustomComponent<PlayerCharacterMasterController, UPlayerCharacterMaster>
    {
        private PlayerAimbot _aimbot;
        private PlayerCheats _cheats;

        protected override void Start()
        {
            _aimbot = new PlayerAimbot(_component);
            _cheats = new PlayerCheats();
        }

        public static UPlayerCharacterMaster instance;

        protected override void Update()
        {
            if (_component.master == null)
                return;

            if (_component.master.GetBody() == null)
                return;

            LocalUser localUser = _component.networkUser.localUser;
            if (localUser == null)
                return;

            instance = this;

            _cheats.DoCheats(_component);
            _aimbot.DoAimbot();
        }

        protected override void OnGUI()
        {

        }

        public void FillInventory()
        {
            var availableItems = new List<PickupIndex>();
            availableItems.AddRange(Run.instance.availableTier1DropList);
            availableItems.AddRange(Run.instance.availableTier2DropList);
            availableItems.AddRange(Run.instance.availableTier3DropList);
            availableItems.AddRange(Run.instance.availableLunarDropList);

            _component.master.GetBodyObject().GetComponent<EntityStateMachine>().mainStateType = new SerializableEntityStateType(typeof(EntityStates.ScavMonster.GrantItem));
            foreach (var i in availableItems)
                _component.master.GetBodyObject().GetComponent<EntityStateMachine>().SetState(
                    new EntityStates.ScavMonster.GrantItem
                    {
                        dropPickup = i,
                        itemsToGrant = 1
                    });
        }

        private EntityStates.NewtMonster.KickFromShop shopKick = new EntityStates.NewtMonster.KickFromShop();
        public void FuckEar()
        {
            _component.master.GetBodyObject().GetComponent<EntityStateMachine>().mainStateType = new SerializableEntityStateType(typeof(EntityStates.NewtMonster.KickFromShop));
            _component.master.GetBodyObject().GetComponent<EntityStateMachine>().SetState(shopKick);
        }

        public string GetName() =>
            _component.networkUser.userName;
    }
}
