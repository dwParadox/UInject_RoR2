using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UInject_RoR2.Drawing.Drawables
{
    public interface IDrawable
    {
        void Draw(Vector3 center, Vector3 min, Vector3 max);
    }
}
