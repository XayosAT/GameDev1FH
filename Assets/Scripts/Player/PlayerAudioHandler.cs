using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    public List<AudioSource> audioSources;
    private Dictionary<string, AudioSource> _audioSources;
    // Start is called before the first frame update
    void Start()
    {
        _audioSources = new Dictionary<string, AudioSource>();
        
        foreach (var audioSource in audioSources)
        {
            _audioSources.Add(audioSource.clip.name, audioSource);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string soundName)
    {
        if (_audioSources.ContainsKey(soundName))
        {
            _audioSources[soundName].Play();
        }
    }

}
