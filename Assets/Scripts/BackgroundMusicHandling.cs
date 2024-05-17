using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicHandling : MonoBehaviour
{
    private AudioSource[] _audioSources;
    private int _currentAudioSourceIndex = 0;

    private void Start()
    {
        _audioSources = GetComponents<AudioSource>();
        StartCoroutine(PlayAudioSources());
    }

    
    
    IEnumerator PlayAudioSources()
    {
        while (true)
        {
            if (_audioSources[_currentAudioSourceIndex].isPlaying == false)
            {
                _audioSources[_currentAudioSourceIndex].Play();
                yield return new WaitForSeconds(_audioSources[_currentAudioSourceIndex].clip.length);
                _currentAudioSourceIndex = (_currentAudioSourceIndex + 1) % _audioSources.Length;
            }
            else
            {
                yield return null;
            }
        }
        
    }
}
