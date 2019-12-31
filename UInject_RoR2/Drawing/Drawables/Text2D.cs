using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UInject_RoR2.Drawing.Drawables
{
    public enum TextPosition
    {
        Top,
        Bottom
    }

    class Text2D : IDrawable
    {
        private TextPosition _position;

        private Func<string> _getText;
        private Vector3 _center, _min, _max;

        public Text2D(Func<string> getTextFunc, TextPosition position)
        {
            this._getText = getTextFunc;
            this._position = position;
        }

        private void Setup(Vector3 center, Vector3 min, Vector3 max)
        {
            this._center = center;
            this._min = min;
            this._max = max;
        }

        public void Draw(Vector3 center, Vector3 min, Vector3 max, bool simulateWidth = true)
        {
            Setup(center, min, max);

            if (_getText == null)
                return;

            string stringToDraw = _getText();

            Vector3 scrCenter = Camera.main.WorldToScreenPoint(_center);

            Vector3 objMin = _center;
            Vector3 objMax = _center;
             
            objMin.y -= (_max.y - _min.y) * 0.5f;
            objMax.y += (_max.y - _min.y) * 0.5f;

            Vector3 scrMin = Camera.main.WorldToScreenPoint(objMin);
            Vector3 scrMax = Camera.main.WorldToScreenPoint(objMax);

            if (scrCenter.z < 0.0f || scrMin.z < 0.0f || scrMax.z < 0.0f)
                return;

            float boxHeight = (scrMax.y - scrMin.y) / 2f;
            float boxWidth = boxHeight / 2f;

            if (_position == TextPosition.Top)
                GUIHelper.DrawText(stringToDraw, new Rect(scrCenter.x, Screen.height - scrCenter.y - boxHeight - 7f, 0, 0));
            else
                GUIHelper.DrawText(stringToDraw, new Rect(scrCenter.x, Screen.height - scrCenter.y + boxHeight + 5f, 0, 0));
        }
    }
}
