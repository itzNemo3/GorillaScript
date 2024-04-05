using Photon.Pun;
using System;
using System.IO;
using UnityExplorer.CSConsole;
using UnityExplorer;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GorillaScript.Main
{
    public class MainThing : MonoBehaviourPunCallbacks
    {
        public static void Awake()
        {
            string filePath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\BepInEx\\code.txt";
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                ConsoleController.Evaluate(fileContent);
            }
        }
    }
}