using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Stores each sound that can be used.
    public Sound[] sounds;

    //Used to make the audio manager a singleton.
    public static AudioManager audioManInstance;

    // Start is called before the first frame update
    void Awake()
    {
        //Make this a singleton. This will not allow more than one audio manager to exist at once.
        if(audioManInstance == null)
        {
            audioManInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        //Create an audio source in the audio manager obj for each sounds in Sounds[]. Copy all stats from the sound to the audio source.
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        //Play the menu music when the game starts.
        Play("MenuMusic");
    }

    //Play a sound. When this is called, a string is passed in for the sound's name. Search all sounds for one that matches the pased in string and play it.
    public void Play (string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if(sound.name == soundName)
            {
                sound.source.Play();
                return;
            }
        }

        Debug.Log("Sound " + soundName + " not found! Look for typos in the AudioManager or Play() function call.");
    }

    //Play a sound after a timed delay. When this is called, a string is passed in for the sound's name. Search all sounds for one that matches the pased in string and play it after delayTime is up.
    public void PlayDelayed(string soundName, float delayTime)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == soundName)
            {
                sound.source.PlayDelayed(delayTime);
                return;
            }
        }

        Debug.Log("Sound " + soundName + " not found! Look for typos in the AudioManager or PlayDelayed() function call.");
    }

    //Stop a sound. When this is called, a string is passed in for the sound's name. Search all sounds for one that matches the pased in string and stop it.
    public void Stop(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == soundName)
            {
                sound.source.Stop();
                return;
            }
        }

        Debug.Log("Sound " + soundName + " not found! Look for typos in the AudioManager or Stop() function call.");
    }

    //Stop all audio.
    public void Stop()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.Stop();
        }
    }

    //Get a sound that is in the manager.
    public Sound GetSound(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == soundName)
            {
                return sound;
            }
        }

        Debug.Log("Sound " + soundName + " not found in GetAudioSoure call.");
        return null;
    }
}
