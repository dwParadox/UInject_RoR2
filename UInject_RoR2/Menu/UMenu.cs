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

        private static void DumpObjects()
        {
            Debug.SceneDump dump = new Debug.SceneDump();
            dump.Dump(@"C:\Users\Public\Documents\BigRoRDump.txt");
        }

        private static void SpawnAs(object bodyType)
        {
            DropInMultiplayer.DropIn.SpawnAs((string)bodyType, MenuManager.GetMenu("Main").GetInput("Player to Set"));
        }

        public static List<MenuItem> MainMenu()
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(new MenuStartScroll());

            items.Add(new MenuToggle("No Cooldowns"));
            items.Add(new MenuSlider("Damage Multiplier", 12, 1000, 12));
            items.Add(new MenuSlider("Attack Speed", 1, 1000, 1));
            items.Add(new MenuSlider("Run Speed", 1, 100, 1));
            items.Add(new MenuSlider("Jump Height", 1, 100, 1));
            items.Add(new MenuAction("Give Money", SetMoney));
            items.Add(new MenuInput("Player to Set"));

            foreach (var b in BodyCatalog.allBodyPrefabs)
                items.Add(new MenuFunc("SpawnAs: " + b.name, SpawnAs, b.name));

            items.Add(new MenuEndScroll());

            return items;
        }

        public static List<MenuItem> AimbotMenu()
        {
            List<MenuItem> items = new List<MenuItem>();
            items.Add(new MenuStartScroll());
            items.Add(new MenuToggle("Aimbot"));
            items.Add(new MenuToggle("Silent Aim"));
            items.Add(new MenuToggle("Auto-Shoot"));
            items.Add(new MenuToggle("No Spread"));
            items.Add(new MenuEndScroll());
            return items;
        }

        public static List<MenuItem> ESPMenu()
        {
            List<MenuItem> items = new List<MenuItem>();
            items.Add(new MenuStartScroll());
            items.Add(new MenuToggle("Draw Monsters"));
            items.Add(new MenuToggle("Draw Allies"));
            items.Add(new MenuToggle("Draw Neutrals"));
            items.Add(new MenuToggle("Draw Teleporter"));
            items.Add(new MenuToggle("Draw Loot"));
            items.Add(new MenuLabel(""));
            items.Add(new MenuToggle("2D Boxes"));
            items.Add(new MenuToggle("3D Boxes"));
            items.Add(new MenuToggle("Snaplines"));
            items.Add(new MenuToggle("Display Names"));
            items.Add(new MenuEndScroll());
            return items;
        }

        public static List<MenuItem> DebugMenu()
        {
            List<MenuItem> items = new List<MenuItem>();
            items.Add(new MenuStartScroll());
            items.Add(new MenuAction("DumpObjects", DumpObjects));
            items.Add(new MenuToggle("isModded"));
            items.Add(new MenuEndScroll());
            return items;
        }
    }
}
