using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingletonDestroy<SoundManager>
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] sfxClips;

    private void Start()
    {
        PlayBGM(0);
    }

    // 배경음악 재생
    public void PlayBGM(int index)
    {
        if (index >= 0 && index < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[index];
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    // 효과음 재생
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }

    // 볼륨 값(0~1)을 dB로 변환
    private float LinearToDecibel(float volume)
    {
        return Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
    }
    
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", LinearToDecibel(volume));
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", LinearToDecibel(volume));
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", LinearToDecibel(volume));
    }

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
