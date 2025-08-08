using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPopupSetting : UIPopup
{
    [SerializeField] private Button exitButton;
    
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    
    [SerializeField] private TMP_InputField masterInputField;
    [SerializeField] private TMP_InputField bgmInputField;
    [SerializeField] private TMP_InputField sfxInputField;
    
    void Start()
    {
        exitButton.onClick.AddListener(() => UIManager.Instance.ClosePopup<UIPopupSetting>());
        
        // 초기 값
        masterSlider.value = SoundManager.Instance.GetMasterVolume() * 0.3f;
        bgmSlider.value = SoundManager.Instance.GetBGMVolume() * 0.3f;
        sfxSlider.value = SoundManager.Instance.GetSFXVolume() * 0.3f;

        masterInputField.text = Mathf.RoundToInt(masterSlider.value * 100).ToString();
        bgmInputField.text = Mathf.RoundToInt(bgmSlider.value * 100).ToString();
        sfxInputField.text = Mathf.RoundToInt(sfxSlider.value * 100).ToString();
        
        // 슬라이더 변경
        masterSlider.onValueChanged.AddListener(OnMasterValueChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXValueChanged);
        
        // 인풋 필드 값 변경
        masterInputField.onValueChanged.AddListener(OnMasterInputFieldChanged);
        bgmInputField.onValueChanged.AddListener(OnBGMInputFieldChanged);
        sfxInputField.onValueChanged.AddListener(OnSFXInputFieldChanged);
    }

    void OnMasterValueChanged(float value)
    {
        SoundManager.Instance.SetMasterVolume(value);
        masterInputField.text = Mathf.RoundToInt(value * 100).ToString(); 
    }

    void OnBGMValueChanged(float value) 
    {
        SoundManager.Instance.SetBGMVolume(value);
        bgmInputField.text = Mathf.RoundToInt(value * 100).ToString(); 
    }
    
    void OnSFXValueChanged(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
        sfxInputField.text = Mathf.RoundToInt(value * 100).ToString();
    }
    
    void OnMasterInputFieldChanged(string input)
    {
        // inputField → slider // string → float
        if (float.TryParse(input, out float result))
        {
            result = Mathf.Clamp(result, 0f, 100f); // 0~100
            
            SoundManager.Instance.SetMasterVolume(result / 100f); // 인풋필드 0~100, 슬라이더 0~1이므로 100 나눔
            masterSlider.value = result / 100f;
            masterInputField.text = Mathf.RoundToInt(result).ToString();
        }
    }
    
    void OnBGMInputFieldChanged(string input)
    {
        if (float.TryParse(input, out float result))
        {
            result = Mathf.Clamp(result, 0f, 100f); // 0~100
            
            SoundManager.Instance.SetBGMVolume(result / 100f);
            bgmSlider.value = result / 100f;
            bgmInputField.text = Mathf.RoundToInt(result).ToString();
        }
    }
    
    void OnSFXInputFieldChanged(string input)
    {
        if (float.TryParse(input, out float result))
        {
            result = Mathf.Clamp(result, 0f, 100f); // 0~100
            
            SoundManager.Instance.SetSFXVolume(result / 100f);
            sfxSlider.value = result / 100f;
            sfxInputField.text = Mathf.RoundToInt(result).ToString();
        }
    }
}
