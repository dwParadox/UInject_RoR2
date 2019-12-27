using RoR2;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using UInject.UMenu;
using UInject_RoR2.Drawing;
using UInject_RoR2.Drawing.Drawables;


namespace UInject_RoR2.UCharacter
{
    public class CharacterESP
    {
        private CharacterMaster _characterMaster;
        private Box boxRenderer;

        public CharacterESP(CharacterMaster characterMaster)
        {
            this._characterMaster = characterMaster;

            boxRenderer = new Box("ESP");
            boxRenderer.AddDrawable("2D Boxes", new Box2D());
            boxRenderer.AddDrawable("3D Boxes", new Box3D());
            boxRenderer.AddDrawable("Snaplines", new Snapline());
            boxRenderer.AddDrawable("Display Names", new Text2D(GetDisplayName, TextPosition.Top));
        }

        private static Color GetColor(CharacterMaster characterMaster)
        {
            if (characterMaster.teamIndex == TeamIndex.Player)
                return Color.green;

            if (characterMaster.teamIndex == TeamIndex.Neutral)
                return Color.gray;

            return PlayerAimbot.CanSeeCharacter(characterMaster) ? Color.yellow : Color.red;
        }

        public void DrawCharacter(CharacterMaster characterMaster)
        {
            if (!((MenuManager.GetMenu("ESP").GetEnabled("Draw Monsters") && characterMaster.teamIndex == TeamIndex.Monster) ||
                 (MenuManager.GetMenu("ESP").GetEnabled("Draw Allies") && characterMaster.teamIndex == TeamIndex.Player)))
                return;

            var characterBody = characterMaster.GetBody();

            if (characterBody == null)
                return;

            float worldWidth = characterBody.characterMotor.capsuleRadius + 0.85f;
            float worldHeight = characterBody.characterMotor.capsuleHeight + 0.5f;

            Vector3 world = characterBody.transform.position;
            Vector3 minWorld = new Vector3(world.x - worldWidth / 2f, world.y - worldHeight / 2f, world.z - worldWidth / 2f);
            Vector3 maxWorld = new Vector3(world.x + worldWidth / 2f, world.y + worldHeight / 2f, world.z + worldWidth / 2f);

            GUI.color = GetColor(characterMaster);
            boxRenderer.Draw(world, minWorld, maxWorld);
        }

        private string GetDisplayName() =>
            _characterMaster.GetBody().GetDisplayName();
    }
}
