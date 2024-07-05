using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Adventurer;

public class DataParser
{
    public static void Save(string fileName, SavesData.Data gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);
        bf.Serialize(file, gameData);
        file.Close();

        Debug.Log($"Saved: {Application.persistentDataPath}{fileName}");
    }

    public static SavesData.Data Load(string fileName)
    {
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            Debug.Log(Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            var savedGame = (SavesData.Data)bf.Deserialize(file);
            file.Close();

            return savedGame;
        }

        return new SavesData.Data();
    }
}