using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{

    public Vector2 Position;
    public SaveData(Vector2 position)
    {
        Position = position;
    }
}

public static class SaveSystem
{



    private static List<string> pathList = new List<string>();
    private static int fileIndex = 0;
    private static string PATH = Application.persistentDataPath;

   
    public static void CheckFiles()
    {
        for (int i = 1; i<=3; i++)
        {
            if (!File.Exists(PATH+"/save0" + i+".json"))
            {
                File.WriteAllText(PATH + "/save0" + i + ".json", "");
            }
            pathList.Add(PATH + "/save0" + i + ".json");
        }
    }
    public static string GetPath()
    {
        return pathList[fileIndex];
    }
    public static void save(SaveData dataToSave)
    {
        
        string convertedData = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(pathList[fileIndex], convertedData);
    }

    public static SaveData load()
    {
        if (File.Exists(pathList[fileIndex]))
        {
            
            string fileContent = File.ReadAllText(pathList[fileIndex]);
            SaveData convertedData = JsonUtility.FromJson<SaveData>(fileContent);
            return convertedData;
        }
        return null;
    }


    public static void SetFileIndex(int _fileIndex)
    {
        fileIndex = _fileIndex;
        Debug.Log(fileIndex);
        Debug.Log(pathList[fileIndex]);
    }
}
