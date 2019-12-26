using UnityEngine;

namespace UInject_RoR2.Drawing
{
    class DrawingUtils : MonoBehaviour
    {
        public static Camera mainCam;
        public static Vector3 WorldToScreen(Vector3 wp)
        {
            Matrix4x4 mat = mainCam.projectionMatrix * mainCam.worldToCameraMatrix;

            Vector4 temp = mat * new Vector4(wp.x, wp.y, wp.z, 1f);

            if (temp.w < 0.1f)
                return Vector3.zero;

            float invw = 1.0f / temp.w;

            temp.x *= invw;
            temp.y *= invw;

            Vector2 Center = new Vector2((.5f * mainCam.pixelWidth), (.5f * mainCam.pixelHeight));

            Center.x += 0.5f * temp.x * mainCam.pixelWidth + 0.5f;
            Center.y -= 0.5f * temp.y * mainCam.pixelHeight + 0.5f;

            return new Vector3(Center.x, Center.y, wp.z);
        }

        private static void MakeLines(Vector3 Origin, float X1, float Y1, float Z1, float X2, float Y2, float Z2, int Size = 1)
        {
            Vector3 Origin1 = new Vector3(Origin.x + X1, Origin.y + Y1, Origin.z + Z1);
            Vector3 Origin2 = new Vector3(Origin.x + X2, Origin.y + Y2, Origin.z + Z2);

            Vector3 Screen1 = WorldToScreen(Origin1);
            Vector3 Screen2 = WorldToScreen(Origin2);

            if (Screen1 == Vector3.zero || Screen2 == Vector3.zero)
                return;

            GUIHelper.DrawLine(Screen1, Screen2, 1);
        }

        public static void Draw3DBox(Vector3 objPos, float W, float H, Color gCol)
        {
            GUI.color = gCol;
            MakeLines(objPos, -W, 0, W, W, 0, W);
            MakeLines(objPos, -W, 0, W, -W, H, W);
            MakeLines(objPos, W, 0, W, W, H, W);
            MakeLines(objPos, -W, H, W, W, H, W);

            MakeLines(objPos, -W, 0, W, -W, 0, -W);
            MakeLines(objPos, W, 0, -W, W, 0, W);
            MakeLines(objPos, W, 0, -W, -W, 0, -W);
            MakeLines(objPos, -W, 0, -W, -W, H, -W);

            MakeLines(objPos, W, 0, -W, W, H, -W);
            MakeLines(objPos, -W, H, W, -W, H, -W);
            MakeLines(objPos, W, H, -W, W, H, W);
            MakeLines(objPos, W, H, -W, -W, H, -W);
        }

    }
}