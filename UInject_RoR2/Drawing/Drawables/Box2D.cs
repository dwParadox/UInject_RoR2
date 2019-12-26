using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UInject_RoR2.Drawing.Drawables
{
    class Box2D : IDrawable
    {
        private Vector3 _center, _min, _max;

        public Box2D()
        {

        }

        public void Setup(Vector3 center, Vector3 min, Vector3 max)
        {
            this._center = center;
            this._min = min;
            this._max = max;
        }

        public void Draw()
        {
            Vector3 scrCenter = DrawingUtils.WorldToScreen(_center);

            Vector3 objMin = _center;
            Vector3 objMax = _center;

            objMin.y -= (_max.y - _min.y) * 0.5f;
            objMax.y += (_max.y - _min.y) * 0.5f;

            Vector3 scrMin = DrawingUtils.WorldToScreen(objMin);
            Vector3 scrMax = DrawingUtils.WorldToScreen(objMax);

            if (scrCenter == Vector3.zero || scrMin == Vector3.zero || scrMax == Vector3.zero)
                return;

            float boxHeight = (scrMax.y - scrMin.y) / 2f;
            float boxWidth = boxHeight / 2f;

            GUIHelper.DrawLine(new Vector3(scrCenter.x - boxWidth, scrCenter.y - boxHeight), new Vector3(scrCenter.x + boxWidth, scrCenter.y - boxHeight), 1); // TOP
            GUIHelper.DrawLine(new Vector3(scrCenter.x - boxWidth, scrCenter.y + boxHeight), new Vector3(scrCenter.x + boxWidth, scrCenter.y + boxHeight), 1); // BOTTOM
            GUIHelper.DrawLine(new Vector3(scrCenter.x - boxWidth, scrCenter.y - boxHeight), new Vector3(scrCenter.x - boxWidth, scrCenter.y + boxHeight), 1); // LEFT
            GUIHelper.DrawLine(new Vector3(scrCenter.x + boxWidth, scrCenter.y - boxHeight), new Vector3(scrCenter.x + boxWidth, scrCenter.y + boxHeight), 1); // RIGHT
        }
    }
}
