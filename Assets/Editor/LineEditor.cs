using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LineEditor : EditorWindow
{
    private string[] fileName;
    [MenuItem("Window/Dialogue/LineConverter")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LineEditor));
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Generate File"))
        {
            GeneratePreset();
        }
    }

    private void GeneratePreset()
    {
        fileName = Directory.GetFiles(Application.dataPath + "/Resources", "*.json", SearchOption.AllDirectories).Select(x => Path.GetFileName(x)).ToArray();

        foreach (string file in fileName)
        {
            List<DialogueChoices> lines = new List<DialogueChoices>();

            using (StreamReader sr = new StreamReader(Application.dataPath + "/Resources/" + file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lineDiv= line.Split("|");

                    DialogueChoices choice = new DialogueChoices();
                    choice.dictAdd = new List<int> { 0 };
                    choice.line = lineDiv[0];

                    if (lineDiv[1] != string.Empty)
                    {
                        
                        int numChoices = int.Parse(lineDiv[1]);

                        List<int> arrayAddress = new List<int>();

                        string[] strAddr = lineDiv[2].Split(",");
                        //Debug.Log(strAddr[0]);
                        for (int i = 0; i < numChoices; i++)
                        {
                            arrayAddress.Add(int.Parse(strAddr[i]) - 1);
                        }
                        //Debug.Log(arrayAddress[0]);
                        choice.dictAdd = arrayAddress;
                        //Debug.Log(choice.dictAdd[0]);
                        
                    }
                    else
                    {
                        choice.abyssLvl = int.Parse(lineDiv[2]);
                        //Debug.Log(lineDiv[2]);
                    }
                    lines.Add(choice);
                }
            }

            DialogueChoice fileToCreate = CreateInstance<DialogueChoice>();

            fileToCreate.dialogueChoices = lines;

            AssetDatabase.CreateAsset(fileToCreate, "Assets/Preset/" + file.TrimEnd("json") + "asset");
            AssetDatabase.SaveAssets();
        }

    }
}
