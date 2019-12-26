using UnityEngine;
using UInject_RoR2;
using UInject.ULoad;
using System.Reflection;
using System;

namespace RoRCheat
{
    public class UInjectLoader
    {
        public static Loader<InjectedObject> uLoader = new Loader<InjectedObject>();

        static void Load()
        {
            uLoader.LoadObject();
        }

        static void Unload()
        {
            uLoader.UnloadObject();
        }
    }
}