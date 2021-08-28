using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    public static string SAVE_DIRECTORY = Application.persistentDataPath + "/saves";
    public static string SAVE_FILE = SAVE_DIRECTORY + "/Save.save";


    public static bool Save(object saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = SAVE_DIRECTORY;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string filePath = SAVE_FILE;

        FileStream file = File.Create(filePath);

        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }

    public static object Load()
    {
        if (!File.Exists(SAVE_FILE))
        {
#if UNITY_EDITOR
            Debug.LogWarning("Not exists file: " + SAVE_FILE);
#endif
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(SAVE_FILE, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogError("Failed to load file: " + SAVE_FILE);
            file.Close();
            return null;
        }
    }
}
