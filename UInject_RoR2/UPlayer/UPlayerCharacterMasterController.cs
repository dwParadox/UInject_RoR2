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
        public PlayerCharacterMasterController GetBase() => _component;

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
            foreach (var i in DropTable.AllItems)
                ItemDrop.AddItem(_component, i.ToString());
        }

        private EntityStates.NewtMonster.KickFromShop shopKick = new EntityStates.NewtMonster.KickFromShop();
        public void NoiseTest()
        {
            _component.master.GetBodyObject().GetComponent<EntityStateMachine>().mainStateType = new SerializableEntityStateType(typeof(EntityStates.NewtMonster.KickFromShop));
            _component.master.GetBodyObject().GetComponent<EntityStateMachine>().SetState(shopKick);
        }

        public string GetName() =>
            _component.networkUser.userName;
    }
}
