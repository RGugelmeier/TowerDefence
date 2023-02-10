using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveOrLoad
{
    //This saves the game data to a file after being serialized.
    public static void SaveGame(GameManager gameMan, UnitManager unitMan)
    {
        //Create a new binary formatter to serialize the save game data.
        BinaryFormatter formatter = new BinaryFormatter();

        //Set the save data path to someone where unity chooses. Persistent data path creates a path and makes sure the data path does not change.
        string path = Application.persistentDataPath + "/gameData.data";
        //Creates a file at the previously created path.
        FileStream fileStream = new FileStream(path, FileMode.Create);

        //Create a new object of GameData type to get the data to be saved.
        GameData gameData = new GameData(gameMan, unitMan);

        //Serialize the data from the fileStream.
        formatter.Serialize(fileStream, gameData);

        //Close the file stream.
        fileStream.Close();
    }

    public static GameData LoadGame()
    {
        //Get the save data path from the same place as where we saved it. Persistent data path creates a path and makes sure the data path does not change.
        string path = Application.persistentDataPath + "/gameData.data";

        //Check if the file exists at the p[lace we tried to load from...
        if(File.Exists(path))
        {
            //Then create a new formatter and file stream...
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            //then create a new GameData variable and store the deserialized data from the file to it.
            GameData data = formatter.Deserialize(fileStream) as GameData;

            //Then close the file...
            fileStream.Close();

            //and return the data.
            return data;
        }
        else
        {
            //Print a debug message and break the game.
            Debug.LogError("Save file not found in: " + path);
            return null;
        }
    }
}
