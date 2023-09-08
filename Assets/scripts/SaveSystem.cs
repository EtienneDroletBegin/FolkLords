using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{

    public int currentLevel;
    public SaveData(int level)
    {
        currentLevel = level;
    }
}

public class SaveSystem
{



    private static string PATH = Application.persistentDataPath;


    public static string getPath() { 
        return PATH;
    }

    public static void CheckFiles()
    {
        for (int i = 1; i<=3; i++)
        {
            if (!File.Exists(PATH+"/save0" + i+".json"))
            {
                File.WriteAllText(PATH + "/save0" + i + ".json", "");
            }
        }
    }

    public static void save()
    {
        SaveData dataTosave = new SaveData(2);

        string convertedData = JsonUtility.ToJson(dataTosave);
        File.WriteAllText(PATH, convertedData);
    }

    public static SaveData load()
    {
        if (File.Exists(PATH))
        {
            string fileContent = File.ReadAllText(PATH);
            SaveData convertedData = JsonUtility.FromJson<SaveData>(fileContent);
            return convertedData;
        }
        return null;
    }
}
