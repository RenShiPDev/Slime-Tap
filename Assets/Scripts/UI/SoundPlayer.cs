using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundPlayer : MonoBehaviour
{
    private List<AudioClip> _buttonsSounds = new List<AudioClip>();
    private List<AudioClip> _playerSounds = new List<AudioClip>();
    private List<AudioClip> _starSounds = new List<AudioClip>();

    private List<AudioSource> _audioSources = new List<AudioSource>();

    private int _currentAudioSource;

    private void Start()
    {
        InitialiseSounds("ButtonsSounds", ref _buttonsSounds);
        InitialiseSounds("PlayerSounds", ref _playerSounds);
        InitialiseSounds("StarSounds", ref _starSounds);

        for (int i = 0; i < 6; i++)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(audioSource);
        }
    }

    private void InitialiseSounds(string folder, ref List<AudioClip> audioClips)
    {
        var resSounds = Resources.LoadAll(folder, typeof(AudioClip));
        foreach (var sound in resSounds)
        {
            audioClips.Add((AudioClip)sound);
        }
    }

    private void PlaySound(List<AudioClip> audioClips, float volume = 1)
    {
        _audioSources[_currentAudioSource].PlayOneShot(audioClips[Random.Range(0, audioClips.Count - 1)], volume);
        _currentAudioSource++;
        if (_currentAudioSource >= _audioSources.Count)
        {
            _currentAudioSource = 0;
        }
    }

    public void PlayButtonSound()
    {
        PlaySound(_buttonsSounds);
    }
    public void PlayPlayerSound()
    {
        PlaySound(_playerSounds);
    }
    public void PlayStarSound()
    {
        PlaySound(_starSounds, 0.03f);
    }

    public void SetButtonHandler(ref UnityEvent soundEvent){
        soundEvent.AddListener(PlayButtonSound);
    }
    public void SetPlayerHandler(ref UnityEvent soundEvent)
    {
        soundEvent.AddListener(PlayPlayerSound);
    }
    public void SetStarHandler(ref UnityEvent soundEvent)
    {
        soundEvent.AddListener(PlayStarSound);
    }
}
