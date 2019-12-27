using UnityEngine;
using System;

using UInject;
using UInject.ULog;

using UInject.UMenu;
using System.Collections.Generic;

using RoR2;

using UInject_RoR2.Drawing;
using UInject_RoR2.UPlayer;

namespace UInject_RoR2
{
    public class InjectedObject : UMain
    {
        protected override void Start()
        {
            new MenuManager(Menu.UMenu.MainMenu, "Main");
            new MenuManager(Menu.UMenu.AimbotMenu, "Aimbot");
            new MenuManager(Menu.UMenu.ESPMenu, "ESP");

            UDebug.Log(LogMessageType.INFO, "UInject_RoR Loaded!");
            CustomComponent<CharacterMaster, UCharacterMaster>.Register();
            CustomComponent<PlayerCharacterMasterController, UPlayerCharacterMaster>.Register();
            CustomComponent<TeleporterInteraction, UTeleporterInteraction>.Register();
            CustomComponent<PurchaseInteraction, UPurchaseInteraction>.Register();

            DrawingUtils.DefaultFont = Font.CreateDynamicFontFromOSFont("Calibri", 10);
        }

        protected override void Update()
        {
            DrawingUtils.mainCam = Camera.main;
        }

        protected override void OnGUI()
        {

        }
    }
}
