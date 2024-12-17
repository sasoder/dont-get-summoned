using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioClip bgMusic;  // Ensure this is assigned in the inspector.
    private List<AudioSource> audioSources = new List<AudioSource>();
    private AudioSource bgMusicSource;  // Dedicated AudioSource for background music.
    public AudioClip tickingSound;  // Ensure this is assigned in the inspector.
    public AudioClip closeSound;  // Ensure this is assigned in the inspector.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            bgMusicSource = gameObject.AddComponent<AudioSource>();
            bgMusicSource.loop = true;  // Set the background music to loop.
            bgMusicSource.clip = bgMusic;  // Assign the clip.
            bgMusicSource.spatialBlend = 0;  // Make it a global sound.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
        Instance = null;
    }

    private void Start()
    {
        bgMusicSource.Play();  // Play the background music.
    }

    public void PlayTickingSound()
    {
        PlaySound(tickingSound);
    }

    public void PlayCloseSound()
    {
        PlaySound(closeSound);
    }

    public void StopBackgroundMusic()
    {
        if (bgMusicSource.isPlaying)
        {
            bgMusicSource.Stop();
        }
    }


    public void PlaySound(AudioClip clip)
    {
        AudioSource availableSource = audioSources.Find(source => !source.isPlaying);

        if (availableSource != null)
        {
            availableSource.PlayOneShot(clip);
        }
        else
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.spatialBlend = 0;  // Make it a global sound.
            newSource.PlayOneShot(clip);
            audioSources.Add(newSource);
        }
    }
}
