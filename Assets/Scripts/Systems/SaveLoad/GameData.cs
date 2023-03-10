using UnityEngine;
using System.Xml.Serialization;

public class GameData : MonoBehaviour
{
    //public int highestLevelCompleted;
    //Each unit's level. If the player does not have the unit unlocked, their level is 0.
    public int guardLevel, monkLevel, archerLevel;

    [XmlRoot("SaveData")]
    public struct SaveData
    {
        [XmlElement("levelUpPoints")]
        public int levelUpPoints;

        [XmlElement("highestLevelCompleted")]
        public int highestLevelCompleted;

        [XmlElement("guardLevel")]
        public int guardLevel;

        [XmlElement("puncherLevel")]
        public int puncherLevel;

        [XmlElement("archerLevel")]
        public int archerLevel;
    }
}
