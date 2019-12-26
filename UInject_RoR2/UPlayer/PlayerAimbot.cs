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

        public static bool CanSeeCharacter(CharacterMaster endCharacter)
        {
            return CheckLoS(StartLocation, endCharacter.GetBody().transform.position);
        }
        
        public static void DoAimbot(PlayerCharacterMasterController _component)
        {
            StartLocation = _component.master.GetBody().inputBank.aimOrigin;

            if (MenuManager.GetMenu("Aimbot").GetEnabled("Aimbot"))
            {
                var closestCharacter = Masters
                    .Where(c => c.GetBody() != null && c.GetBody().coreTransform != null && CanSeeCharacter(c))
                    .OrderBy(c => Vector3.Distance(DrawingUtils.WorldToScreen(c.GetBody().coreTransform.position), new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)))
                    .FirstOrDefault();

                if (closestCharacter != null)
                {
                    EndLocation = closestCharacter.GetBody().coreTransform.position;

                    Vector3 closestDir = (EndLocation - StartLocation).normalized;
                    _component.master.GetBody().inputBank.aimDirection = closestDir;

                    if (MenuManager.GetMenu("Aimbot").GetEnabled("Auto-Shoot"))
                        _component.master.GetBody().skillLocator.GetSkill(SkillSlot.Primary).ExecuteIfReady();
                }
            }
        }
    }
}
