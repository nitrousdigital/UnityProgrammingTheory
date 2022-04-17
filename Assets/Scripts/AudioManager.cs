using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  A singleton AudioManager to play audio.
///  Makes it easy to play audio for components that are being Destroyed.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource audioPlayer;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();    
    }

    /// <summary>
    ///  Play the specified AudioClip at the specified volume
    /// </summary>
    public void Play(AudioClip audio, float volume)
    {
        audioPlayer.PlayOneShot(audio, volume);
    }
}
