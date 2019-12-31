using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UInject.UMenu;

namespace UInject_RoR2.UPlayer
{
    class PlayerCheats
    {
        public void DoCheats(PlayerCharacterMasterController player)
        {
            OneShot(player);
            AttackSpeed(player);
            RunSpeed(player);
            JumpHeight(player);

            NoSpread(player);
            NoCooldowns(player);
        }

        private static float _damageBackup = 0f;
        private void OneShot(PlayerCharacterMasterController player)
        {
            if (_damageBackup == 0f)
            {
                _damageBackup = player.master.GetBody().baseDamage;
                MenuManager.GetMenu("Main").SetValue("Damage Multiplier", _damageBackup);
            }

            player.master.GetBody().baseDamage = MenuManager.GetMenu("Main").GetValue("Damage Multiplier");
        }

        private static float _attackSpeedBackup = 0f;
        private void AttackSpeed(PlayerCharacterMasterController player)
        {
            if (_attackSpeedBackup == 0f)
            {
                _attackSpeedBackup = player.master.GetBody().baseAttackSpeed;
                MenuManager.GetMenu("Main").SetValue("Attack Speed", _attackSpeedBackup);
            }

            player.master.GetBody().baseAttackSpeed = MenuManager.GetMenu("Main").GetValue("Attack Speed");
        }

        private static float _runSpeedBackup = 0f;
        private void RunSpeed(PlayerCharacterMasterController player)
        {
            if (_runSpeedBackup == 0f)
            {
                _runSpeedBackup = player.master.GetBody().baseMoveSpeed;
                MenuManager.GetMenu("Main").SetValue("Run Speed", _runSpeedBackup);
            }

            player.master.GetBody().baseMoveSpeed = MenuManager.GetMenu("Main").GetValue("Run Speed");
        }

        private static float _jumpHeightBackup = 0f;
        private void JumpHeight(PlayerCharacterMasterController player)
        {
            if (_jumpHeightBackup == 0f)
            {
                _jumpHeightBackup = player.master.GetBody().baseJumpPower;
                MenuManager.GetMenu("Main").SetValue("Jump Height", _jumpHeightBackup);
            }

            player.master.GetBody().baseJumpPower = MenuManager.GetMenu("Main").GetValue("Jump Height");
        }

        private void NoSpread(PlayerCharacterMasterController player)
        {
            if (MenuManager.GetMenu("Aimbot").GetEnabled("No Spread"))
                player.master.GetBody().SetSpreadBloom(0f, false);
        }

        private void RechargeSkill(PlayerCharacterMasterController player, SkillSlot slot)
        {
            player.master.GetBody().skillLocator.GetSkill(slot).rechargeStopwatch = 0f;
            player.master.GetBody().skillLocator.GetSkill(slot).stock = 3;
        }

        private void NoCooldowns(PlayerCharacterMasterController player)
        {
            if (MenuManager.GetMenu("Main").GetEnabled("No Cooldowns"))
            {
                RechargeSkill(player, SkillSlot.Primary);
                RechargeSkill(player, SkillSlot.Secondary);
                RechargeSkill(player, SkillSlot.Special);
                RechargeSkill(player, SkillSlot.Utility);
            }
        }
    }
}
