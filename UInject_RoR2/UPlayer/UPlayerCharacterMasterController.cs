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

        private static float _attackSpeedBackup = 0f;
        private void AttackSpeed()
        {
            if (_attackSpeedBackup == 0f)
            {
                _attackSpeedBackup = _component.master.GetBody().baseAttackSpeed;
                MenuManager.GetMenu("Main").SetValue("Attack Speed", _attackSpeedBackup);
            }

            _component.master.GetBody().baseAttackSpeed = MenuManager.GetMenu("Main").GetValue("Attack Speed");
        }

        private static float _runSpeedBackup = 0f;
        private void RunSpeed()
        {
            if (_runSpeedBackup == 0f)
            {
                _runSpeedBackup = _component.master.GetBody().baseMoveSpeed;
                MenuManager.GetMenu("Main").SetValue("Run Speed", _runSpeedBackup);
            }

            _component.master.GetBody().baseMoveSpeed = MenuManager.GetMenu("Main").GetValue("Run Speed");
        }

        private static float _jumpHeightBackup = 0f;
        private void JumpHeight()
        {
            if (_jumpHeightBackup == 0f)
            {
                _jumpHeightBackup = _component.master.GetBody().baseJumpPower;
                MenuManager.GetMenu("Main").SetValue("Jump Height", _jumpHeightBackup);
            }

            _component.master.GetBody().baseJumpPower = MenuManager.GetMenu("Main").GetValue("Jump Height");
        }

        private void NoSpread()
        {
            if (MenuManager.GetMenu("Aimbot").GetEnabled("No Spread"))
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
            if (_component.master == null)
                return;

            if (_component.master.GetBody() == null)
                return;

            LocalUser localUser = _component.networkUser.localUser;
            if (localUser == null)
                return;

            OneShot();
            AttackSpeed();
            RunSpeed();
            JumpHeight();

            NoSpread();
            NoCooldowns();

            PlayerAimbot.DoAimbot(_component);
        }

        protected override void OnGUI()
        {

        }
    }
}
