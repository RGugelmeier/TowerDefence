using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public string name;

    public bool loop;

    [Range(0.0f, 1.0f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}
