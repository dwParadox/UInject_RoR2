using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UInject;
using UInject.ULog;
using UInject.UMenu;
using UInject_RoR2.UPlayer;
using UnityEngine;

namespace UInject_RoR2.Menu
{
    class UMenu : MonoBehaviour
    {
        private static void SetMoney()
        {
            foreach (var p in FindObjectsOfType<PlayerCharacterMasterController>())
                p.master?.GiveMoney(10000);
        }

        private static void SetCoins()
        {
            foreach (var p in FindObjectsOfType<PlayerCharacterMasterController>())
                p.networkUser?.AwardLunarCoins(10);
        }

        private static void DumpObjects()
        {
            Debug.SceneDump dump = new Debug.SceneDump();
            dump.Dump(@"C:\Users\Public\Documents\BigRoRDump.txt");
        }

        private static void GiveItems()
        {
            UPlayerCharacterMaster.instance.FillInventory();
        }

        private static void SpawnAs(object bodyType)
        {
            DropInMultiplayer.DropIn.SpawnAs((string)bodyType, MenuManager.GetMenu("SpawnAs").GetInput("Player to Set"));
        }

        private static void FuckEar()
        {
            DropInMultiplayer.DropIn.SpawnAs("ShopkeeperBody", UPlayerCharacterMaster.instance.GetName());
            UPlayerCharacterMaster.instance.FuckEar();
        }

        private static void CheckSpawnVars()
        {
            string userName = MenuManager.GetMenu("SpawnAs").GetInput("Player to Set");
            var user = NetworkUser.readOnlyInstancesList.Where(u => u.userName.Equals(userName)).FirstOrDefault();

            foreach (var u in NetworkUser.readOnlyInstancesList)
                UDebug.Log(LogMessageType.INFO, $"User: {u.userName}");

            UDebug.Log(LogMessageType.INFO, $"masterObject: {user.masterObject != null}");
            UDebug.Log(LogMessageType.INFO, $"masterController: {user.masterController != null}");
            UDebug.Log(LogMessageType.INFO, $"master: {user.master != null}");
            UDebug.Log(LogMessageType.INFO, $"gameObject: {user.gameObject != null}");
            UDebug.Log(LogMessageType.INFO, $"networkLoadout: {user.networkLoadout != null}");
            UDebug.Log(LogMessageType.INFO, $"authed: {user.authed}");
        }

        public static List<MenuItem> MainMenu()
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(new MenuStartScroll());

            items.Add(new MenuLabel("Player Stats:"));
            items.Add(new MenuSlider("Damage Multiplier", 10, 500, 12));
            items.Add(new MenuSlider("Health Multiplier", 10, 500, 12));
            items.Add(new MenuSlider("Attack Speed", 1, 100, 1));
            items.Add(new MenuSlider("Run Speed", 1, 100, 1));
            items.Add(new MenuSlider("Jump Height", 1, 100, 1));
            items.Add(new MenuSlider("Jump Count", 1, 20, 1));

            items.Add(new MenuLabel(""));
            items.Add(new MenuLabel("Player Cheats"));
            items.Add(new MenuToggle("God Mode"));
            items.Add(new MenuToggle("One Shot"));
            items.Add(new MenuToggle("No Cooldowns"));
            items.Add(new MenuAction("Fill Inventory", GiveItems));

            items.Add(new MenuLabel(""));
            items.Add(new MenuLabel("All Clients (Host Only):"));
            items.Add(new MenuAction("Give Money", SetMoney));
            items.Add(new MenuAction("Give Lunar Coins", SetCoins));
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
            items.Add(new MenuToggle("No Recoil/Spread"));
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

        public static List<MenuItem> GameMenu()
        {
            List<MenuItem> items = new List<MenuItem>(); 

            items.Add(new MenuStartScroll());
            items.Add(new MenuToggle("isModded"));
            items.Add(new MenuToggle("16 Player Lobby"));
            items.Add(new MenuEndScroll());

            return items;
        }

        public static List<MenuItem> SpawnAsMenu()
        {
            List<MenuItem> items = new List<MenuItem>();
            items.Add(new MenuStartScroll());
            items.Add(new MenuInput("Player to Set"));
            foreach (var b in BodyCatalog.allBodyPrefabs)
                items.Add(new MenuFunc("SpawnAs: " + b.name, SpawnAs, b.name));

            items.Add(new MenuEndScroll());
            return items;
        }

        public static List<MenuItem> DebugMenu()
        {
            List<MenuItem> items = new List<MenuItem>();
            items.Add(new MenuStartScroll());
            items.Add(new MenuAction("DumpObjects", DumpObjects));
            items.Add(new MenuAction("Fuck Ear", FuckEar));
            items.Add(new MenuAction("Dump NetworkUser", CheckSpawnVars));
            items.Add(new MenuEndScroll());
            return items;
        }
    }
}
