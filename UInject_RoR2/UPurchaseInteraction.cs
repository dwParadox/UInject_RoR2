using UnityEngine;

using UInject;

using UInject.UMenu;

using RoR2;

using UInject_RoR2.Drawing.Drawables;

namespace UInject_RoR2
{
    public class UPurchaseInteraction : CustomComponent<PurchaseInteraction, UPurchaseInteraction>
    {
        private Box renderableBox;

        protected override void Start()
        {
            renderableBox = new Box("ESP");
            renderableBox.AddDrawable("2D Boxes", new Box2D(false));
            renderableBox.AddDrawable("3D Boxes", new Box3D());
            renderableBox.AddDrawable("Display Names", new Text2D(GetDisplayName, TextPosition.Bottom));
        }

        protected override void Update()
        {
        }

        protected override void OnGUI()
        {
            if (!MenuManager.GetMenu("ESP").GetEnabled("Draw Loot"))
                return;

            if (!_component.available)
                return;

            float size = 0.5f;
            Vector3 center = _component.transform.position;
            Vector3 min = new Vector3(center.x - size, center.y - size, center.z - size);
            Vector3 max = new Vector3(center.x + size, center.y + size, center.z + size);

            GUI.color = Color.cyan;
            renderableBox.Draw(center, min, max);
        }

        private string GetDisplayName() =>
            _component.GetDisplayName();
    }
}
