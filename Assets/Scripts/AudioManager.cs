using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] _sounds;
    void Awake()
    {
        foreach (Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }

    public void Play(string name)
    {
        foreach(Sound sound in _sounds)
        {
            if (sound.name == null)
            {
                return;
            }

            if (sound.name == name)
            {
                sound.source.Play();
            }
        }
    }

    public void StopPlaying(string name)
    {
        foreach (Sound sound in _sounds)
        {
            if (sound.name == null)
            {
                return;
            }

            if (sound.name == name)
            {
                sound.source.Stop();
            }
        }
    }
}
