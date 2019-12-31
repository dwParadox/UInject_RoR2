using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UInject_RoR2.UCharacter
{
    class CharacterUtilities
    {
        private static bool CheckLoS(Vector3 start, Vector3 end)
        {
            Vector3 direction = end - start;
            RaycastHit raycastHit;
            return !Physics.Raycast(start, direction, out raycastHit, direction.magnitude, LayerIndex.world.mask, QueryTriggerInteraction.Ignore);
        }

        public static bool IsValid(CharacterMaster character) =>
            character.GetBody() != null && character.GetBody().coreTransform != null;

        public static bool IsMonster(CharacterMaster character) =>
            character.teamIndex == TeamIndex.Monster;

        public static bool IsInRange(Vector3 start, CharacterMaster character) =>
            Vector3.Distance(start, character.GetBody().coreTransform.position) < 125f;

        public static bool IsVisible(Vector3 start, CharacterMaster endCharacter) =>
            CheckLoS(start, endCharacter.GetBody().coreTransform.position);
    }
}
