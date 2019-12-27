using System.Collections.Generic;
using UInject.UMenu;
using UInject_RoR2.Drawing;
using UnityEngine;

namespace UInject_RoR2.Drawing.Drawables
{
    public class Box
    {
        private string _menuName;
        private Dictionary<string, IDrawable> _drawables;
        public Box(string menuName)
        {
            this._menuName = menuName;
            this._drawables = new Dictionary<string, IDrawable>();
        }

        public void AddDrawable(string key, IDrawable drawable)
        {
            _drawables.Add(key, drawable);
        }

        public void RemoveDrawable(string key)
        {
            _drawables.Remove(key);
        }

        public void Draw(Vector3 center, Vector3 min, Vector3 max)
        {
            foreach (var d in _drawables)
            {
                if (MenuManager.GetMenu(_menuName).GetEnabled(d.Key))
                    d.Value.Draw(center, min, max);
            }
        }
    }
}
