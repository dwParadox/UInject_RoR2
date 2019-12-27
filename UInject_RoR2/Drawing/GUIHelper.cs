using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UInject_RoR2.Drawing
{
    public class GUIHelper
    {
        public static void DrawLine(Vector3 start, Vector3 end, int width)
        {
            Vector2 d = end - start;
            float a = Mathf.Rad2Deg * Mathf.Atan(d.y / d.x);
            if (d.x < 0)
                a += 180;

            int width2 = (int)Mathf.Ceil(width / 2);

            GUIUtility.RotateAroundPivot(a, start);
            GUI.DrawTexture(new Rect(start.x, start.y - width2, d.magnitude, width), Texture2D.whiteTexture);
            GUIUtility.RotateAroundPivot(-a, start);
        }

        public static void DrawText(string text, Rect position)
        {
            RectOffset rectOffset = new RectOffset(0, 0, 0, 0);

            GUIStyle uiStyle = new GUIStyle();
            uiStyle.clipping = TextClipping.Overflow;
            uiStyle.wordWrap = false;
            uiStyle.padding = rectOffset;
            uiStyle.margin = rectOffset;
            uiStyle.contentOffset = Vector2.zero;
            uiStyle.font = DrawingUtils.DefaultFont;
            uiStyle.normal.textColor = GUI.color;
            uiStyle.alignment = TextAnchor.UpperLeft;

            GUIContent uiContent = new GUIContent(text);

            Vector2 lblSize = uiStyle.CalcSize(uiContent);

            GUI.Label(new Rect(position.x - lblSize.x / 2f, position.y - lblSize.y / 2f, lblSize.x, lblSize.y), uiContent, uiStyle);
        }
    }
}
