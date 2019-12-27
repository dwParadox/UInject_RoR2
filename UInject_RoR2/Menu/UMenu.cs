using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UInject.UMenu;
using UnityEngine;

namespace UInject_RoR2.Menu
{
    class UMenu : MonoBehaviour
    {
        private static void SetMoney()
        {
            foreach (var p in FindObjectsOfType<PlayerCharacterMasterController>())
                p.master.GiveMoney(10000);
        }

        public static List<MenuItem> MainMenu()
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(new MenuToggle("No Spread"));
            items.Add(new MenuToggle("No Cooldowns"));
            items.Add(new MenuSlider("Damage Multiplier", 12, 1000, 12));
            items.Add(new MenuAction("Give Money", SetMoney));

            return items;
        }

        public static List<MenuItem> AimbotMenu()
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(new MenuToggle("Aimbot"));
            items.Add(new MenuToggle("Auto-Shoot"));

            return items;
        }

        public static List<MenuItem> ESPMenu()
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(new MenuToggle("Draw Monsters"));
            items.Add(new MenuToggle("Draw Allies"));
            items.Add(new MenuToggle("Draw Teleporter"));
            items.Add(new MenuToggle("Draw Loot"));
            items.Add(new MenuLabel(""));
            items.Add(new MenuToggle("2D Boxes"));
            items.Add(new MenuToggle("3D Boxes"));
            items.Add(new MenuToggle("Snaplines"));
            items.Add(new MenuToggle("Display Names"));

            return items;
        }
    }
}
