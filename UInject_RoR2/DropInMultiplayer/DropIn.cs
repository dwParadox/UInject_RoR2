using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UInject;
using UInject.ULog;
using UnityEngine;
using UnityEngine.Networking;

namespace UInject_RoR2.DropInMultiplayer
{
    public class DropIn
    {
        public static void SpawnAs(string bodyName, string userName)
        {
            if (string.IsNullOrWhiteSpace(bodyName))
                throw new ArgumentNullException("bodyName");

            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            var user = NetworkUser.readOnlyInstancesList.Where(u => u.userName.Equals(userName)).FirstOrDefault();

            foreach (var u in NetworkUser.readOnlyInstancesList)
                UDebug.Log(LogMessageType.INFO, $"User: {u.userName}");

            UDebug.Log(LogMessageType.INFO, $"NetworkUser Found: {user.userName}");

            if (user == null)
                throw new InvalidOperationException($"Could not find user: {userName}");

            var bodyPrefab = BodyCatalog.FindBodyPrefab(bodyName);

            if (bodyPrefab == null)
                throw new InvalidOperationException($"Could not find body: {bodyName}");

            user.CallCmdSetBodyPreference(BodyCatalog.FindBodyIndex(bodyName));

            if (NetworkServer.active)
            {
                user.master.bodyPrefab = bodyPrefab;
                Vector3 spawnLoc = user.master.GetBody().transform.position;
                Quaternion spawnRot = user.master.GetBody().transform.rotation;

                user.master.DestroyBody();
                user.master.SpawnBody(bodyPrefab, spawnLoc, spawnRot);
            }
            else
            {
                if (user.master != null)
                    user.master.CallCmdRespawn(bodyName);
            }
        }
    }
}
