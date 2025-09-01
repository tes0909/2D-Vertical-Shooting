using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthenticationManager : SingletonDontDestroy<AuthenticationManager>, IBaseManager
{
    public bool IsInitialized { get; private set; }
    public void Init()
    {
        IsInitialized = true;
        Initialize();
    }

    private async void Initialize()
    {
        await InitializeUnityServices();
    }

    private async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized");
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async void Login(TMP_InputField idField, TMP_InputField passwordField)
    {
        string id = idField.text.Trim();
        string password = passwordField.text.Trim();

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            Debug.Log("아이디와 비밀번호를 입력하세요");
            return;
        }

        try
        {
            await InitializeUnityServices();
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(id, password);
            SceneManager.LoadScene("MainScene");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    public async void SignUp(TMP_InputField idField, TMP_InputField passwordField)
    {
        string id = idField.text.Trim();
        string password = passwordField.text.Trim();

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            Debug.Log("아이디와 비밀번호를 입력하세요");
            return;
        }
        
        try
        {
            await InitializeUnityServices();
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(id, password);
            SceneManager.LoadScene("MainScene");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
}
