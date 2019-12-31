using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UInject.UMenu;
using UInject_RoR2.Drawing;
using UInject_RoR2.UCharacter;
using UnityEngine;

namespace UInject_RoR2
{
    public class PlayerAimbot
    {
        public static List<CharacterMaster> Masters = new List<CharacterMaster>();
        private PlayerCharacterMasterController _player;

        public PlayerAimbot(PlayerCharacterMasterController player)
        {
            this._player = player;
        }

        private Vector3 EndLocation = Vector3.zero;
        private Vector3 StartLocation = Vector3.zero;

        public void DoAimbot()
        {
            if (_player.master == null ||
                _player.master.GetBody() == null)
                return;

            StartLocation = _player.master.GetBody().inputBank.aimOrigin;

            if (MenuManager.GetMenu("Aimbot").GetEnabled("Aimbot"))
            {
                var closestCharacter = Masters
                    .Where(c =>
                        CharacterUtilities.IsValid(c) &&
                        CharacterUtilities.IsMonster(c) &&
                        CharacterUtilities.IsInRange(StartLocation, c) &&
                        CharacterUtilities.IsVisible(StartLocation, c)
                    )

                    .OrderBy(c => Vector3.Distance(DrawingUtils.WorldToScreen(c.GetBody().coreTransform.position), new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)))
                    .FirstOrDefault();

                if (closestCharacter != null)
                {
                    EndLocation = closestCharacter.GetBody().coreTransform.position;

                    Vector3 closestDir = (EndLocation - StartLocation).normalized;

                    _player.master.GetBody().inputBank.aimDirection = closestDir;

                    if (!MenuManager.GetMenu("Aimbot").GetEnabled("Silent Aim"))
                        _player.networkUser.cameraRigController.SetPitchYawFromLookVector(closestDir);

                    if (MenuManager.GetMenu("Aimbot").GetEnabled("Auto-Shoot"))
                        FireWeapon();
                }
            }
        }

        private void FireWeapon() =>
            _player.master.GetBody().skillLocator.GetSkill(SkillSlot.Primary).ExecuteIfReady();
    }
}
