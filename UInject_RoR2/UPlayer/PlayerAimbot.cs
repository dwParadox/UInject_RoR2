using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UInject.UMenu;
using UInject_RoR2.Drawing;
using UnityEngine;

namespace UInject_RoR2
{
    public class PlayerAimbot
    {
        public static List<CharacterMaster> Masters = new List<CharacterMaster>();
        public static Vector3 EndLocation = Vector3.zero;
        public static Vector3 StartLocation = Vector3.zero;

        private static bool CheckLoS(Vector3 start, Vector3 end)
        {
            Vector3 direction = end - start;
            RaycastHit raycastHit;
            return !Physics.Raycast(start, direction, out raycastHit, direction.magnitude, LayerIndex.world.mask, QueryTriggerInteraction.Ignore);
        }

        public static void DoAimbot(PlayerCharacterMasterController _component)
        {
            StartLocation = _component.master.GetBody().inputBank.aimOrigin;

            if (MenuManager.GetMenu("Aimbot").GetEnabled("Aimbot"))
            {
                var closestCharacter = Masters
                    .Where(c =>
                        CharacterIsValid(c) &&
                        CharacterIsMonster(c) &&
                        CharacterIsInRange(c) && 
                        CanSeeCharacter(c)
                    )

                    .OrderBy(c => Vector3.Distance(DrawingUtils.WorldToScreen(c.GetBody().coreTransform.position), new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)))
                    .FirstOrDefault();

                if (closestCharacter != null)
                {
                    EndLocation = closestCharacter.GetBody().coreTransform.position;

                    Vector3 closestDir = (EndLocation - StartLocation).normalized;

                    _component.master.GetBody().inputBank.aimDirection = closestDir;

                    if (!MenuManager.GetMenu("Aimbot").GetEnabled("Silent Aim"))   
                        _component.networkUser.cameraRigController.SetPitchYawFromLookVector(closestDir);

                    if (MenuManager.GetMenu("Aimbot").GetEnabled("Auto-Shoot"))
                        _component.master.GetBody().skillLocator.GetSkill(SkillSlot.Primary).ExecuteIfReady();
                }
            }
        }

        public static bool CharacterIsValid(CharacterMaster character) =>
            character.GetBody() != null && character.GetBody().coreTransform != null;

        public static bool CharacterIsMonster(CharacterMaster character) =>
            character.teamIndex == TeamIndex.Monster;

        public static bool CharacterIsInRange(CharacterMaster character) =>
            Vector3.Distance(character.GetBody().coreTransform.position, StartLocation) < 125f;

        public static bool CanSeeCharacter(CharacterMaster endCharacter) =>
            CheckLoS(StartLocation, endCharacter.GetBody().coreTransform.position);
    }
}
