using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UInject.UMenu;

namespace UInject_RoR2.Menu
{
    class UMenu
    {
        public static List<MenuItem> MainMenu()
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(new MenuToggle("No Spread"));
            items.Add(new MenuToggle("No Cooldowns"));
            items.Add(new MenuSlider("Damage Multiplier", 12, 1000, 12));

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
            items.Add(new MenuLabel(""));
            items.Add(new MenuToggle("2D Boxes"));
            items.Add(new MenuToggle("3D Boxes"));
            items.Add(new MenuToggle("Snaplines"));

            return items;
        }
    }
}
