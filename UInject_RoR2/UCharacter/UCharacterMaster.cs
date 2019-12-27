
using UnityEngine;

using UInject;
using UInject_RoR2.Drawing;
using RoR2;

using UInject_RoR2.UCharacter;
using UnityEngine.Events;
using RoR2.CharacterAI;
using UInject.UMenu;

namespace UInject_RoR2
{
    public class UCharacterMaster : CustomComponent<CharacterMaster, UCharacterMaster>
    {
        private CharacterESP _esp;

        private void PlayerDeath() =>
            PlayerAimbot.Masters.Remove(_component);

        protected override void Start()
        {
            _esp = new CharacterESP(_component);

            if (_component.gameObject.GetComponent<AISkillDriver>() != null)
            {                
                PlayerAimbot.Masters.Add(_component);
                _component.onBodyDeath.AddListener(PlayerDeath);
            }
        }

        protected override void Update()
        {

        }

        protected override void OnGUI()
        {
            _esp.DrawCharacter(_component);
        }
    }
}
