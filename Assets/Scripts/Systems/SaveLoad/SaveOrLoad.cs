using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveOrLoad
{
    //This saves the game data to a file after being serialized.
    public static void SaveGame(GameManager gameMan)
    {
        GameData.SaveData data = new GameData.SaveData()
        {
            levelUpPoints = gameMan.upgradePoints,
            highestLevelCompleted = gameMan.highestLevelCompleted,
            guardLevel = gameMan.guardLevel,
            puncherLevel = gameMan.puncherLevel,
            archerLevel = gameMan.archerLevel
        };

        string path = Application.persistentDataPath + "/gameData.xml";

        XmlSerializer serializer = new XmlSerializer(typeof(GameData.SaveData));
        StreamWriter writer = new StreamWriter(path);
        serializer.Serialize(writer.BaseStream, data);
        writer.Close();

        Debug.Log("Game saved to " + path);
    }

    public static void LoadGame()
    {
        //Get the save data path from the same place as where we saved it. Persistent data path creates a path and makes sure the data path does not change.
        string path = Application.persistentDataPath + "/gameData.xml";

        //Check if the file exists at the place we tried to load from...
        if(File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData.SaveData));
            StreamReader reader = new StreamReader(path);
            GameData.SaveData deserialized = (GameData.SaveData)serializer.Deserialize(reader.BaseStream);
            reader.Close();

            GameManager.gameManInstance.upgradePoints = deserialized.levelUpPoints;
            GameManager.gameManInstance.highestLevelCompleted = deserialized.highestLevelCompleted;
            GameManager.gameManInstance.guardLevel = deserialized.guardLevel;
            GameManager.gameManInstance.puncherLevel = deserialized.puncherLevel;
            GameManager.gameManInstance.archerLevel = deserialized.archerLevel;

            //SceneManager.LoadScene("Level" + (GameManager.gameManInstance.highestLevelCompleted + 1));
        }
        else
        {
            
        }
    }
}
