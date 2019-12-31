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

        protected override void Update()
        {
            if (_component.master == null)
                return;

            if (_component.master.GetBody() == null)
                return;

            LocalUser localUser = _component.networkUser.localUser;
            if (localUser == null)
                return;

            _cheats.DoCheats(_component);
            _aimbot.DoAimbot();
        }

        protected override void OnGUI()
        {

        }
    }
}
