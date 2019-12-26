using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UInject_RoR2.Drawing.Drawables
{
    class Snapline : IDrawable
    {
        private Vector3 _center, _min, _max;

        public Snapline()
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

            GUIHelper.DrawLine(new Vector3(Screen.width / 2f, Screen.height), new Vector3(scrCenter.x, scrCenter.y - boxHeight, 0), 1);
        }
    }
}
