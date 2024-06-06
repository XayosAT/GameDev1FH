using System;
using System.Collections;
using UnityEngine;

public class BackgroundMusicHandling : MonoBehaviour
{
    private AudioSource[] _audioSources;
    private int _currentAudioSourceIndex = 0;
    private bool _playMusic = true;

    private void Start()
    {
        _audioSources = GetComponents<AudioSource>();
        StartCoroutine(PlayAudioSources());
    }

    IEnumerator PlayAudioSources()
    {
        AudioSource previousAudioSource = null;
        while (_playMusic)
        {
            if (!previousAudioSource || !previousAudioSource.isPlaying)
            {
                _audioSources[_currentAudioSourceIndex].Play();
                previousAudioSource = _audioSources[_currentAudioSourceIndex];
                _currentAudioSourceIndex = (_currentAudioSourceIndex + 1) % _audioSources.Length;
            }
            yield return null;
        }
    }

    public void StopMusic()
    {
        _playMusic = false;
        foreach (var audioSource in _audioSources)
        {
            audioSource.Stop();
        }
    }
}
