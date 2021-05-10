using UnityEngine;

[CreateAssetMenu(fileName = "LevelBounds", menuName = "Bounds")]
public class LevelBounds : ScriptableObject
{
    public float minX, minY, maxX, maxY;
}
