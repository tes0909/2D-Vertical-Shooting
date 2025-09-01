using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingletonDontDestroy<SoundManager>, IBaseManager
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    
    private Dictionary<string, AudioClip> bgmDict = new();
    private Dictionary<string, AudioClip> sfxDict = new();

    public bool IsInitialized { get; private set; }
    public void Init()
    {
        IsInitialized = true;
        
        if (audioMixer == null)
        {
            audioMixer = Resources.Load<AudioMixer>("Audio/MainMixer");
        }

        var bgmGroup = audioMixer.FindMatchingGroups("BGM")[0];
        var sfxGroup = audioMixer.FindMatchingGroups("SFX")[0];

        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.outputAudioMixerGroup = bgmGroup;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = sfxGroup;
        }
        
        LoadAllClips("Audio/BGM", bgmDict);
        LoadAllClips("Audio/SFX", sfxDict);
        
        PlayBGM("BackgroundMusic");
    }

    private void LoadAllClips(string path, Dictionary<string, AudioClip> dict)
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(path); // 경로의 모든 AudioClip 로드
        foreach (var clip in clips)
        {
            if (!dict.ContainsKey(clip.name))
            {
                dict.Add(clip.name, clip);
            }
        }
    }

    // 배경음악 재생
    public void PlayBGM(string bgmName)
    {
        if (bgmDict.TryGetValue(bgmName, out AudioClip clip))
        {
            if (bgmSource.clip == clip && bgmSource.isPlaying) return; // 이미 같은 곡이면 무시

            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    // 효과음 재생
    public void PlaySFX(string sfxName)
    {
        if (sfxDict.TryGetValue(sfxName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }

    // 볼륨 값(0~1)을 dB로 변환
    private float LinearToDecibel(float volume) => Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
    
    public void SetMasterVolume(float volume) => audioMixer.SetFloat("MasterVolume", LinearToDecibel(volume));

    public void SetBGMVolume(float volume) => audioMixer.SetFloat("BGMVolume", LinearToDecibel(volume));

    public void SetSFXVolume(float volume) => audioMixer.SetFloat("SFXVolume", LinearToDecibel(volume));

    public float GetMasterVolume()
    {
        audioMixer.GetFloat("MasterVolume", out float db);
        return Mathf.Pow(10f, db / 20f);
    }

    public float GetBGMVolume()
    {
        audioMixer.GetFloat("BGMVolume", out float db);
        return Mathf.Pow(10f, db / 20f);
    }

    public float GetSFXVolume()
    {
        audioMixer.GetFloat("SFXVolume", out float db);
        return Mathf.Pow(10f, db / 20f);
    }
}
