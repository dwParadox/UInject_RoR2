using UnityEngine;

using UInject;

using RoR2;

using UInject_RoR2.Drawing;
using UInject.UMenu;

namespace UInject_RoR2
{
    public class UTeleporterInteraction : CustomComponent<TeleporterInteraction, UTeleporterInteraction>
    {
        protected override void Start()
        {
        }

        protected override void Update()
        {
        }

        protected override void OnGUI()
        {
            if (MenuManager.GetMenu("ESP").GetEnabled("Draw Teleporter"))
                DrawingUtils.Draw3DBox(_component.gameObject.transform.position, 10, 10, Color.blue);
        }
    }
}
