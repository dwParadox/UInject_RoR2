using UnityEngine;
using System;

using UInject;
using UInject.ULog;

using UInject.UMenu;
using System.Collections.Generic;

using RoR2;

using UInject_RoR2.Drawing;
using UInject_RoR2.UPlayer;
using RoR2.UI.MainMenu;
using RoR2.UI;

namespace UInject_RoR2
{
    public class InjectedObject : UMain
    {
        protected override void Start()
        {
            // Menu Setups
            new MenuManager(Menu.UMenu.MainMenu, "Main");
            new MenuManager(Menu.UMenu.AimbotMenu, "Aimbot");
            new MenuManager(Menu.UMenu.ESPMenu, "ESP");
            new MenuManager(Menu.UMenu.GameMenu, "Game");
            new MenuManager(Menu.UMenu.SpawnAsMenu, "SpawnAs");
            new MenuManager(Menu.UMenu.DebugMenu, "Debug");

            UDebug.Log(LogMessageType.INFO, "UInject_RoR Loaded!");

            // Custom Components
            CustomComponent<CharacterMaster, UCharacterMaster>.Register();
            CustomComponent<PlayerCharacterMasterController, UPlayerCharacterMaster>.Register();
            CustomComponent<TeleporterInteraction, UTeleporterInteraction>.Register();
            CustomComponent<PurchaseInteraction, UPurchaseInteraction>.Register();
            CustomComponent<BarrelInteraction, UBarrelInteraction>.Register();

            // ESP Font
            DrawingUtils.DefaultFont = Font.CreateDynamicFontFromOSFont("Calibri", 10);

            // Spawn as different survivors
            CustomSurvivor.Init();
            CustomSurvivor.RegisterSurvivor("HANDBody");
            CustomSurvivor.RegisterSurvivor("ShopkeeperBody");
            CustomSurvivor.RegisterSurvivor("BanditBody");
            CustomSurvivor.RegisterSurvivor("GolemBodyInvincible");
            CustomSurvivor.RegisterSurvivor("Drone2Body");
            CustomSurvivor.RegisterSurvivor("AssassinBody");
            CustomSurvivor.RegisterSurvivor("MegaDroneBody");
            CustomSurvivor.AddRegisteredSurvivors();
        }

        private bool _lastModdedFlag = false, _lastPlayerCountFlag = false;
        protected override void Update()
        {
            DrawingUtils.mainCam = Camera.main;

            // Enables joining modded clients
            bool isModded = MenuManager.GetMenu("Game").GetEnabled("isModded");
            if (_lastModdedFlag != isModded)
            {
                RoR2Application.isModded = isModded;
                _lastModdedFlag = isModded;
            }

            // 16 Player Games
            bool is16Player = MenuManager.GetMenu("Game").GetEnabled("16 Player Lobby");
            if (_lastPlayerCountFlag != is16Player)
            {
                if (is16Player)
                {
                    Reflection.SetFieldValue<RoR2Application>("maxPlayers", 16);
                    Reflection.SetFieldValue<RoR2Application>("hardMaxPlayers", 16);
                    Reflection.SetFieldValue<RoR2Application>("maxLocalPlayers", 16);
                }
                else
                {
                    Reflection.SetFieldValue<RoR2Application>("maxPlayers", 4);
                    Reflection.SetFieldValue<RoR2Application>("hardMaxPlayers", 4);
                    Reflection.SetFieldValue<RoR2Application>("maxLocalPlayers", 4);
                }

                _lastPlayerCountFlag = is16Player;
            }
        }

        protected override void OnGUI()
        {

        }
    }
}
