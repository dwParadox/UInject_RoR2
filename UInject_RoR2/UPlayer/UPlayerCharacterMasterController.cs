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
        protected override void Start()
        {

        }

        public static float GetNormalDamage()
        {
            return _damageBackup;
        }

        private static float _damageBackup = 0f;
        private void OneShot()
        {
            if (_damageBackup == 0f)
            {
                _damageBackup = _component.master.GetBody().baseDamage;
                MenuManager.GetMenu("Main").SetValue("Damage Multiplier", _damageBackup);
            }

            _component.master.GetBody().baseDamage = MenuManager.GetMenu("Main").GetValue("Damage Multiplier");
        }

        private void NoSpread()
        {
            if (MenuManager.GetMenu("Main").GetEnabled("No Spread"))
                _component.master.GetBody().SetSpreadBloom(0f, false);
        }

        private void RechargeSkill(SkillSlot slot)
        {
            _component.master.GetBody().skillLocator.GetSkill(slot).rechargeStopwatch = 0f;
            _component.master.GetBody().skillLocator.GetSkill(slot).stock = 3;
        }

        private void NoCooldowns()
        {
            if (MenuManager.GetMenu("Main").GetEnabled("No Cooldowns"))
            {
                RechargeSkill(SkillSlot.Primary);
                RechargeSkill(SkillSlot.Secondary);
                RechargeSkill(SkillSlot.Special);
                RechargeSkill(SkillSlot.Utility);
            }
        }

        protected override void Update()
        {
            OneShot();

            NoSpread();

            NoCooldowns();

            PlayerAimbot.DoAimbot(_component);
        }

        protected override void OnGUI()
        {

        }
    }
}
