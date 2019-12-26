using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UInject_RoR2.Drawing.Drawables
{
    class Box3D : IDrawable
    {
        private List<Vector3> points;
        private Vector3 delta;

        public Box3D()
        {
            points = new List<Vector3>();
        }

        public void Setup(Vector3 center, Vector3 min, Vector3 max)
        {
            delta = new Vector3(max.x - min.x, max.y - min.y, max.z - min.z);
    
            points.Clear();
            points.Add(new Vector3(min.x, min.y, min.z));
            points.Add(new Vector3(min.x + delta.x, min.y, min.z));
            points.Add(new Vector3(min.x, min.y + delta.y, min.z));
            points.Add(new Vector3(min.x, min.y, min.z + delta.z));
            points.Add(new Vector3(max.x, max.y, max.z));
            points.Add(new Vector3(max.x - delta.x, max.y, max.z));
            points.Add(new Vector3(max.x, max.y - delta.y, max.z));
            points.Add(new Vector3(max.x, max.y, max.z - delta.z));
        }

        private void ConnectPoints(Vector3 start, Vector3 end)
        {
            Vector3 scrStart = DrawingUtils.WorldToScreen(start);
            Vector3 scrEnd = DrawingUtils.WorldToScreen(end);

            if (scrStart == Vector3.zero || scrEnd == Vector3.zero)
                return;

            GUIHelper.DrawLine(scrStart, scrEnd, 1);
        }

        public void Draw()
        {
            ConnectPoints(points[0], points[1]);
            ConnectPoints(points[1], points[6]);
            ConnectPoints(points[6], points[3]);
            ConnectPoints(points[3], points[0]);
            ConnectPoints(points[0], points[2]);
            ConnectPoints(points[1], points[7]);
            ConnectPoints(points[6], points[4]);
            ConnectPoints(points[3], points[5]);
            ConnectPoints(points[5], points[4]);
            ConnectPoints(points[5], points[2]);
            ConnectPoints(points[2], points[7]);
            ConnectPoints(points[7], points[4]);
        }

    }
}
