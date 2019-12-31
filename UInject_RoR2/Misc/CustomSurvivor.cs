using RoR2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UInject_RoR2
{
    // Credit: R2API(Tristan McPherson)

    public class CustomSurvivor
    {
        private static bool survivorsAlreadyAdded = false;
        public static ObservableCollection<SurvivorDef> SurvivorDefinitions = new ObservableCollection<SurvivorDef>();

        public static void Init()
        {
            SurvivorCatalog.getAdditionalSurvivorDefs += AddSurvivorAction;
        }

        public static bool RegisterSurvivor(string bodyName)
        {
            var body = BodyCatalog.FindBodyPrefab(bodyName);
            SurvivorDef item = new SurvivorDef
            {
                bodyPrefab = body,
                descriptionToken = bodyName,
                displayPrefab = body.GetComponent<ModelLocator>().modelTransform.gameObject,
                primaryColor = new Color(0.87890625f, 0.662745098f, 0.3725490196f),
                unlockableName = "",
                survivorIndex = SurvivorIndex.Count
            };

            return RegisterSurvivor(item);
        }

        public static bool RegisterSurvivor(SurvivorDef survivor)
        {
            if (survivorsAlreadyAdded)
                return false;

            if (!survivor.bodyPrefab)
                return false;

            SurvivorDefinitions.Add(survivor);

            return true;
        }

        public static void AddRegisteredSurvivors()
        {
            typeof(SurvivorCatalog).GetMethod("Init", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, null);
        }

        private static void AddSurvivorAction(List<SurvivorDef> survivorDefinitions)
        {
            survivorsAlreadyAdded = true;

            var newSurvivorCount = SurvivorDefinitions.Count;
            var exisitingSurvivorCount = SurvivorCatalog.idealSurvivorOrder.Length;

            Array.Resize(ref SurvivorCatalog.idealSurvivorOrder, exisitingSurvivorCount + newSurvivorCount);

            SurvivorCatalog.survivorMaxCount += newSurvivorCount;


            foreach (var survivor in SurvivorDefinitions)
            {
                if (BodyCatalog.FindBodyIndex(survivor.bodyPrefab) == -1 || BodyCatalog.GetBodyPrefab(BodyCatalog.FindBodyIndex(survivor.bodyPrefab)) != survivor.bodyPrefab)
                {

                }

                survivorDefinitions.Add(survivor);

                SurvivorCatalog.idealSurvivorOrder[exisitingSurvivorCount++] = (SurvivorIndex)exisitingSurvivorCount;
            }
        }
    }
}
