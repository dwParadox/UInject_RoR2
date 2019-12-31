using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UInject_RoR2.Debug
{
    public class SceneDump : MonoBehaviour
    {
        private readonly List<GameObject> gameObjects;
        private readonly List<GameObject> rootObjects;

        public SceneDump()
        {
            gameObjects = new List<GameObject>();
            rootObjects = new List<GameObject>();
        }

        public void Reset()
        {
            gameObjects.Clear();
            rootObjects.Clear();
        }

        private void GetObjects() =>
            gameObjects.AddRange(FindObjectsOfType<GameObject>());
        
        private void GetRootObjects()
        {
            if (gameObjects.Count <= 0)
                GetObjects();

            rootObjects.AddRange(gameObjects.Where(g => g.transform.parent == null));
        }

        private void WriteObject(StreamWriter stream, GameObject obj)
        {
            stream.Write($"{obj.name} : (");

            Component[] allComponents = obj.GetComponents<Component>();
            int componentId = 0;
            foreach (var component in allComponents)
            {
                componentId++;
                stream.Write($"{component.GetType().Name}");
                stream.Write((componentId < allComponents.Length) ? ", " : "");
            }

            stream.Write(");" + Environment.NewLine);
        }

        private void WriteChildren(StreamWriter stream, GameObject obj)
        {
            WriteObject(stream, obj);

            foreach (Transform child in obj.transform)
            {
                stream.Write("\t"); 
                WriteChildren(stream, child.gameObject);
            }
        }

        public void Dump(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException("filePath", "filePath must be specified.");

            GetRootObjects();

            using (StreamWriter stream = new StreamWriter(filePath, false))
            {
                foreach (GameObject root in rootObjects)
                    WriteChildren(stream, root);
            }
        }
    }
}
